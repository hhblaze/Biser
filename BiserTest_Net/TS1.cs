﻿/* 
  Copyright (C) 2012 dbreeze.tiesky.com / Alex Solovyov
  It's a free software for those, who think that it should be free.
*/

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
        public TS2 P7 { get; set; }
        public List<Tuple<string,byte[],TS3>> P8 { get; set; }
        public Tuple<float, TS2, TS3, decimal?> P9 { get; set; }
        //--- extra for JSON testings
        public string P10;
        public Dictionary<int, int> P11 { get; set; }
        public int P12 { get; set; }
        public List<TS3> P13 { get; set; }
        public Dictionary<int, int> P14 { get; set; }
        public List<List<TS3>> P15 { get; set; }
        public Dictionary<long,List<TS3>> P16 { get; set; }
        public DateTime P17 { get; set; }
        public List<int> P18 { get; set; }
        public Tuple<int,TS3> P19 { get; set; }
    }
}
