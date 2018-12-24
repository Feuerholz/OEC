using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OEC.Model
{
    public class MatchPlayer : Player
    {

        public double ExpectedWins;
        public double ActualWins;
        public double KFactor;

        public MatchPlayer(string playerID, string playerName, double elo, double mapsPlayed, double kfactor)
        {
            this.PlayerID = playerID;
            this.PlayerName = playerName;
            this.Elo = elo;
            this.KFactor = kfactor;
            this.ExpectedWins = 0;
            this.ActualWins = 0;
            this.MapsPlayed = mapsPlayed;
        }

        public void CalculateNewElo()
        {
            this.Elo = Elo + KFactor * (ActualWins - ExpectedWins);
        }
    }
}
