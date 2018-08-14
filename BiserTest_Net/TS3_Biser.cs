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
    public partial class TS3 : Biser.IEncoder
    {
        public Biser.Encoder BiserEncoder(Biser.Encoder existingEncoder = null)
        {
            Biser.Encoder enc = new Biser.Encoder(existingEncoder);

            enc
            .Add(P1)
            .Add(P2)
            .Add(P3)
            ;
            return enc;
        }

        public static TS3 BiserDecode(byte[] enc = null, Biser.Decoder extDecoder = null) //!!! change return type
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

            TS3 m = new TS3();      //!!!!!!!!!!!!!! change return type

            m.P1 = decoder.GetString();
            m.P2 = decoder.GetInt_NULL();
            m.P3 = decoder.GetDateTime();

            return m;
        }//eof


        public static TS3 BiserJsonDecode(string enc = null, Biser.JsonDecoder extDecoder = null) //!!!!!!!!!!!!!! change return type
        {
            Biser.JsonDecoder decoder = null;

            if (extDecoder == null)
            {
                if (enc == null || String.IsNullOrEmpty(enc))
                    return null;
                decoder = new Biser.JsonDecoder(enc);
                if (decoder.CheckNull())
                    return null;
            }
            else
            {
                //decoder = new Biser.JsonDecoder(extDecoder);
                decoder = extDecoder;
                //if (decoder.CheckNull())
                //    return null;                
            }

            TS3 m = new TS3();  //!!!!!!!!!!!!!! change return type
            while (true)
            {
                switch (decoder.GetProperty())
                {
                    case "P1":
                        m.P1 = decoder.GetString();
                        break;
                    case "P2":
                        m.P2 = decoder.GetInt_NULL();
                        break;
                    case "P3":
                        m.P3 = decoder.GetDateTime();
                        break;
                    default:
                        return m;
                }

            }
        }//eof



    }
}
