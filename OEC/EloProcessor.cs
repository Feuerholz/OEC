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

namespace OEC
{
    public class EloProcessor
    {
        public List<string> MatchIDs = new List<string>();
        public Mappool mappool = new Mappool();
        public List<Match> matches = new List<Match>();

        public void ProcessMatches(string matchlinks, string maplinks, string apiKey)
        {
            PopulateMatchIDs(matchlinks);
            CreatePool(maplinks);
            APIAccessor.ApiKey = apiKey;
            
            foreach(string matchID in MatchIDs)
            {
                Match match = new Match(matchID);
                matches.Add(match);
                JArray matchJSON = APIAccessor.RetrieveMatchDataAsync(matchID).Result;
                match.FillMaps(matchJSON);
            }

            foreach(Match match in matches)
            {
                foreach (MatchMap map in match.Maps)
                {
                    if (mappool.MapIDs.Contains(map.mapID))
                    {
                        CalculateAllPlayerRatings(map);
                    }
                }
            }
        }


        //take the match links from a supplied, newline seperated string and add the contained Match IDs to the list
        void PopulateMatchIDs(string matchlinks)
        {
            string[] seperatedLinks = matchlinks.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (string str in seperatedLinks)
            {
                if (!String.IsNullOrWhiteSpace(str))
                {
                    string[] splitLink = str.Split('/');                    
                    if (!String.IsNullOrWhiteSpace(splitLink[splitLink.Length]))
                    {
                        MatchIDs.Add(splitLink[splitLink.Length]);
                    }
                    else
                    {
                        MatchIDs.Add(splitLink[splitLink.Length - 1]);
                    }
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
                    string[] splitLink = str.Split('/');

                    if (!String.IsNullOrWhiteSpace(splitLink[splitLink.Length]))
                    {
                                               
                        mappool.AddMap(splitLink[splitLink.Length].Split('&')[0]);              //if link is from new site, the original split is always enough. If it's from old site, it needs to be split at '&' in case of mode specification
                    }
                    else
                    {
                        mappool.AddMap(splitLink[splitLink.Length - 1].Split('&')[0]);          //if link is from new site, the original split is always enough. If it's from old site, it needs to be split at '&' in case of mode specification
                    }
                }
            }
        }
        
        void CalculateAllPlayerRatings(MatchMap map)
        {
            List<Tuple<PlayerScore, PlayerScore>> scorepairs = new List<Tuple<PlayerScore, PlayerScore>>();
            for (int i = 0; i < map.scores.Count - 1; i++)
            {
                for (int j = i + 1; j < map.scores.Count; j++)
                {
                    scorepairs.Add(new Tuple<PlayerScore,PlayerScore>(map.scores[i], map.scores[j]));
                }
            }
            foreach (Tuple<PlayerScore,PlayerScore> scorepair in scorepairs)
            {

            }
        }
    }
}
