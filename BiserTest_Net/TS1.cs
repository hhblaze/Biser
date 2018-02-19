using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiserTest_Net
{
    public partial class TS1
    {
        public int P1 { get; set; }
        public int P2 { get; set; }

        public decimal P3 { get; set; }

        public List<TS2> P4 { get; set; }

        public Dictionary<long,TS3> P5 { get; set; }

        public Dictionary<uint, List<TS3>> P6 { get; set; }
    }
}
