using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiserTest_Net
{
    public partial class TS7
    {
        public enum eVoteType:short
        {
            VoteFor,
            VoteReject
        }

        public eVoteType VoteType { get; set; }
        public int Barabaka { get; set; } = 12;

    }
}
