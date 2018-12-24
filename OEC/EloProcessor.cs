using OEC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMP;
using OMP.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Raven.Client.Documents.Session;
using System.Text.RegularExpressions;
using Raven.Client.Documents.Linq;

namespace OEC
{
    public class EloProcessor
    {
        public List<string> MatchIDs = new List<string>();
        public Mappool mappool = new Mappool();
        public List<OMP.Model.Match> matches = new List<OMP.Model.Match>();
        public List<MatchPlayer> players = new List<MatchPlayer>();

        public void ProcessMatches(string matchlinks, string maplinks, string apiKey)
        {
            PopulateMatchIDs(matchlinks);
            CreatePool(maplinks);
            APIAccessor.ApiKey = apiKey;
            
            foreach(string matchID in MatchIDs)
            {
                OMP.Model.Match match = new OMP.Model.Match(matchID);
                matches.Add(match);
                JArray matchJSON = APIAccessor.RetrieveMatchDataAsync(matchID).Result;
                match.FillMaps(matchJSON);
            }

            foreach(OMP.Model.Match match in matches)
            {
                foreach (MatchMap map in match.Maps)
                {
                    if (mappool.MapIDs.Contains(map.mapID))
                    {
                        CalculateAllPlayerRatings(map);
                    }
                }
            }

            foreach(MatchPlayer player in players)
            {
                player.CalculateNewElo();
            }

            UpdateElo(players);
        }


        //take the match links from a supplied, newline seperated string and add the contained Match IDs to the list
        void PopulateMatchIDs(string matchlinks)
        {
            string[] seperatedLinks = matchlinks.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (string str in seperatedLinks)
            {
                if (!String.IsNullOrWhiteSpace(str))
                {
                    Regex rx = new Regex("[0-9]+");
                    System.Text.RegularExpressions.Match m = rx.Match(str);
                    MatchIDs.Add(m.Value);
                }

            }
        }

        void CreatePool(string maplinks)
        {
            string[] seperatedLinks = maplinks.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (string str in seperatedLinks)
            {
                if (!String.IsNullOrWhiteSpace(str))
                {
                    //Get the map ID from the link, for both old and new site.
                    //there has to be a better way of doing this than chaining 2 regexes together?
                    Regex rx = new Regex("(#.*/[0-9]+)|(/b/[0-9]+)");
                    System.Text.RegularExpressions.Match m = rx.Match(str);
                    Regex secondrx = new Regex("[0-9]+");
                    System.Text.RegularExpressions.Match m2 = secondrx.Match(m.Value);

                    mappool.AddMap(m2.Value);


                }
            }
        }
        
        void CalculateAllPlayerRatings(MatchMap map)
        {
            /* Any match regardless of team size is getting treated as a 1v1v1v1v1...
             * In order to calculate the elo changes for a match, each resulting pair of scores for the imaginary 1v1s is created
             */

            double kfactor = 35/map.scores.Count;               //scale kfactor with team size to avoid matches with more players overly affecting rating compared to actual 1v1s - this is currently set on each players' first map.
            List<Scorepair> scorepairs = new List<Scorepair>();
            for (int i = 0; i < map.scores.Count - 1; i++)
            {
                for (int j = i + 1; j < map.scores.Count; j++)
                {
                    scorepairs.Add(new Scorepair(map.scores[i], map.scores[j]));
                }
            }

            //The projected wins (as per Elo-Rating algorithm) have to be calculated for each imaginary 1v1 and added up for each player in the match along with the actual wins
            foreach (Scorepair pair in scorepairs)
            {
                //If one of the player hasn't been initialized yet, do that
                if (!players.Any(p => p.PlayerID == pair.Score1.PlayerID))
                {
                    players.Add(FetchPlayer(pair.Score1.PlayerID, kfactor));
                }

                if (!players.Any(p => p.PlayerID == pair.Score2.PlayerID))
                {
                    players.Add(FetchPlayer(pair.Score2.PlayerID, kfactor));
                }

                //get the players of the scorepair from the list of players
                MatchPlayer p1 = players.Where(p => p.PlayerID == pair.Score1.PlayerID).ToList().First();
                MatchPlayer p2 = players.Where(p => p.PlayerID == pair.Score2.PlayerID).ToList().First();

                //add expected wins, maps played, and actual wins
                p1.ExpectedWins += (1 / (1 + Math.Pow(10, (p2.Elo - p1.Elo) / 400)));
                p1.MapsPlayed += 1/((double)map.scores.Count-1);
                p2.ExpectedWins += 1 - (1 / (1 + Math.Pow(10, (p2.Elo - p1.Elo) / 400)));
                p2.MapsPlayed += 1/((double)map.scores.Count-1);

                if (pair.Score1.Score > pair.Score2.Score)
                {
                    p1.ActualWins += 1;
                }
                else if (pair.Score2.Score > pair.Score1.Score)
                {
                    p2.ActualWins += 1;
                }
                else
                {
                    p1.ActualWins += 0.5;
                    p2.ActualWins += 0.5;
                }
            }


        }


        //gets player from the database and returns a matching MatchPlayer object
        public MatchPlayer FetchPlayer(string playerID, double kfactor)
        {
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                Player p = session.Load<Player>(playerID);
                if (p == null)
                {
                    return (GetNewPlayer(playerID, kfactor));
                }
                return (new MatchPlayer(playerID, p.PlayerName, p.Elo, p.MapsPlayed, kfactor));
            }
        }

        //gets data (rank and name) of a user from the API and seeds them based on pp rank
        public MatchPlayer GetNewPlayer(string playerID, double kfactor)
        {
            JArray playerJSON = APIAccessor.RetrievePlayerDataAsync(playerID).Result;
            string name = playerJSON[0]["username"].Value<string>();
            int rank = playerJSON[0]["pp_rank"].Value<int>();
            double elo = SeedPlayer(rank);
            return new MatchPlayer(playerID, name, elo, 0, kfactor);
        }

        //use rank for initial seeding because as flawed as it is it's better than giving cookiezi the same initial elo as some random 5 digit
        public double SeedPlayer(int rank)
        {
            //TODO: Implement
            return 1200;
        }

        //Updates the elo of all players in the supplied list of MatchPlayer objects
        public static void UpdateElo(List<MatchPlayer> matchPlayers)
        {

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                foreach (MatchPlayer matchPlayer in matchPlayers)
                {
                    Player player = new Player
                    {
                        PlayerID = matchPlayer.PlayerID,
                        PlayerName = matchPlayer.PlayerName,
                        Elo = matchPlayer.Elo,
                        MapsPlayed = matchPlayer.MapsPlayed
                    };
                    session.Store(player, player.PlayerID);
                }
                session.SaveChanges();
            }
        }

        public List<Player> getPlayerList()
        {
            List<Player> pList = new List<Player>();
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                IRavenQueryable<Player> query = from player in session.Query<Player>()
                                                orderby player.Elo descending
                                                select player;

                return query.ToList();
            }
        }

    }
}
