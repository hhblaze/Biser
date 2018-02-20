using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiserTest_Net
{
    public class TS4 : Biser.IEncoder
    {
        public enum eVoteType
        {
            VoteFor,
            VoteReject
        }
        public TS4()
        {
            TermId = 0;
            VoteType = eVoteType.VoteFor;
        }

        //public ulong TermId { get; set; }
        public uint TermId { get; set; }

        public eVoteType VoteType { get; set; }


        public Biser.Encoder BiserEncoder(Biser.Encoder existingEncoder = null)
        {
            Biser.Encoder enc = new Biser.Encoder(existingEncoder);

            enc
            .Add(TermId)
            .Add((int)VoteType)
            ;
            return enc;
        }

        public static TS4 BiserDecode(byte[] enc = null, Biser.Decoder extDecoder = null) //!!!!!!!!!!!!!! change return type
        {
            Biser.Decoder decoder = null;
            if (extDecoder == null)
            {
                if (enc == null || enc.Length == 0)
                    return null;
                decoder = new Biser.Decoder(enc);
                if (decoder.CheckNull())
                    return null;
            }
            else
            {
                decoder = new Biser.Decoder(extDecoder);
                if (decoder.IsNull)
                    return null;
            }

            TS4 m = new TS4();  //!!!!!!!!!!!!!! change return type

            m.TermId = decoder.GetUInt();
            m.VoteType = (eVoteType)decoder.GetInt();

            return m;
        }
    }
}
