using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;

namespace OEC.Model
{
    public class Tourney
    {
        public List<Mappool> pools;
        public List<string> matchIDs;
        public string name;
        public int ID;
    }



}
