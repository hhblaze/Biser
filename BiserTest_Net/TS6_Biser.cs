using System;
using System.Collections.Generic;

namespace BiserTest_Net
{

    public partial class TS6 : Biser.IEncoder
    {


        public Biser.Encoder BiserEncoder(Biser.Encoder existingEncoder = null)
        {
            Biser.Encoder encoder = new Biser.Encoder(existingEncoder);


            if (P18 == null)
                encoder.Add((byte)1);
            else
            {
                for (int it1 = 0; it1 < P18.Rank; it1++)
                    encoder.Add(P18.GetLength(it1));
                foreach (var el2 in P18)
                    encoder.Add(el2, (r3) => {
                        encoder.Add(r3);
                    });
            }

            return encoder;
        }


        public static TS6 BiserDecode(byte[] enc = null, Biser.Decoder extDecoder = null)
        {
            Biser.Decoder decoder = null;
            if (extDecoder == null)
            {
                if (enc == null || enc.Length == 0)
                    return null;
                decoder = new Biser.Decoder(enc);
                /*
                if (decoder.CheckNull())
                    return null;
				*/
            }
            else
            {
                if (extDecoder.CheckNull())
                    return null;
                else
                    decoder = extDecoder;
            }

            TS6 m = new TS6();



            m.P18 = null;
            if (!decoder.CheckNull())
            {
                m.P18 = new System.Collections.Generic.List<System.Int32>[decoder.GetInt(), decoder.GetInt(), decoder.GetInt()];

                for (int ard0 = 0; ard0 < m.P18.GetLength(0); ard0++)
                    for (int ard1 = 0; ard1 < m.P18.GetLength(1); ard1++)
                        for (int ard2 = 0; ard2 < m.P18.GetLength(2); ard2++)
                        {
                            m.P18[ard0, ard1, ard2] = decoder.CheckNull() ? null : new System.Collections.Generic.List<System.Int32>();
                            if (m.P18[ard0, ard1, ard2] != null)
                            {
                                decoder.GetCollection(() => {
                                    var pvar2 = decoder.GetInt();
                                    return pvar2;
                                }, m.P18[ard0, ard1, ard2], true);
                            }
                        }
            }


            return m;
        }


    }
}