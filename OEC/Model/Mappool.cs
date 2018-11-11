using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OEC.Model
{
    public class Mappool
    {
        public List<string> MapIDs;

        public Mappool()
        {
            this.MapIDs = new List<string>();
        }

        public void AddMap(string mapID)
        {
            this.MapIDs.Add(mapID);
        }
    }
}
