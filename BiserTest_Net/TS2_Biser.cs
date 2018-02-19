/* 
  Copyright (C) 2012 tiesky.com / Alex Solovyov
  It's a free software for those, who think that it should be free.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiserTest_Net
{
    public partial class TS2 : Biser.IEncoder
    {
        public Biser.Encoder BiserEncoder(Biser.Encoder existingEncoder = null)
        {
            Biser.Encoder enc = new Biser.Encoder(existingEncoder);

            enc
            .Add(P1)
            .Add(P2)
            .Add(P3, (r) => { enc.Add(r); })
            .Add(P4)
            .Add(P5)
            ;
            return enc;
        }

        public static TS2 BiserDecode(byte[] enc = null, Biser.Decoder extDecoder = null) //!!! change return type
        {
            Biser.Decoder decoder = null;
            if (extDecoder == null)
            {
                if (enc == null || enc.Length == 0)
                    return null;
                decoder = new Biser.Decoder(enc);
            }
            else
            {
                decoder = new Biser.Decoder(extDecoder);
                if (decoder.IsNull)
                    return null;
            }

            TS2 m = new TS2();      //!!!!!!!!!!!!!! change return type

            m.P1 = decoder.GetLong();
            m.P2 = decoder.GetDouble();

            m.P3 = decoder.CheckNull() ? null : new List<TS3>();
            if (m.P3 != null)
                decoder.GetCollection(() => { return TS3.BiserDecode(null, decoder); }, m.P3, true);

            m.P4 = TS3.BiserDecode(null, decoder);

            m.P5 = decoder.GetUInt_NULL();

            return m;
        }
    }
}
