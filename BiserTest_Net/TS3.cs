/* 
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
    public partial class TS3
    {
        public string P1 { get; set; }
        public int? P2 { get; set; }
        public DateTime P3 { get; set; } = DateTime.UtcNow;
    }
}
