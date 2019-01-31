using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiserTest_Net
{
    public partial class TS6
    {
        public string P1 { get; set; }
        public int P2 { get; set; }
        public DateTime P3 { get; set; }

        public List<Dictionary<DateTime,Tuple<int,string>>> P4 { get; set; }

        public Dictionary<int, Tuple<int, string>> P5 { get; set; }

        public Tuple<int, string, Tuple<List<string>, DateTime>> P6 { get; set; }

        public List<string> P7 { get; set; }

        public Dictionary<int,List<string>> P8 { get; set; }
    }

}


