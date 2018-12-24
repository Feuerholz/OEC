using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMP.Model;

namespace OEC.Model
{
    public class Scorepair
    {
        public PlayerScore Score1;
        public PlayerScore Score2;

        public Scorepair(PlayerScore score1, PlayerScore score2)
        {
            this.Score1 = score1;
            this.Score2 = score2;
        }
    }
}
