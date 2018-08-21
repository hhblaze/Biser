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
    public partial class TS2 : Biser.IEncoder, Biser.IJsonEncoder
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
                if (decoder.CheckNull())
                    return null;
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

        public void BiserJsonEncode(Biser.JsonEncoder encoder)
        {
            encoder.Add("P1", this.P1);
            encoder.Add("P2", this.P2);
            encoder.Add("P3", this.P3,(r)=> { encoder.Add(r); });
            encoder.Add("P4", this.P4);
            encoder.Add("P5", this.P5);


        }

        public static TS2 BiserJsonDecode(string enc = null, Biser.JsonDecoder extDecoder = null, Biser.JsonSettings settings = null) //!!!!!!!!!!!!!! change return type
        {
            Biser.JsonDecoder decoder = null;

            if (extDecoder == null)
            {
                if (enc == null || String.IsNullOrEmpty(enc))
                    return null;
                decoder = new Biser.JsonDecoder(enc, settings);
                if (decoder.CheckNull())
                    return null;
            }
            else
            {
                //JSONSettings of the existing decoder will be used
                decoder = extDecoder;
            }

            TS2 m = new TS2();  //!!!!!!!!!!!!!! change return type
            foreach (var props in decoder.GetMap<string>())
            {
                switch (props)
                {
                    case "P1":
                        m.P1 = decoder.GetLong();
                        break;
                    case "P2":
                        m.P2 = decoder.GetDouble();
                        break;
                    case "P3":
                        m.P3 = decoder.CheckNull() ? null : new List<TS3>();
                        if (m.P3 != null)
                        {
                            foreach (var el in decoder.GetArray())
                                m.P3.Add(TS3.BiserJsonDecode(null, decoder));
                        }
                            //decoder.GetCollection(
                            //           () => { return TS3.BiserJsonDecode(null, decoder); }, m.P3, true);
                        break;
                    case "P4":
                        m.P4 = TS3.BiserJsonDecode(null, decoder);
                        break;
                    case "P5":
                        m.P5 = decoder.GetUInt_NULL();
                        break;
                    default:
                        decoder.SkipValue();
                        break;
                }
            }
            return m;

            //while (true)
            //{
            //    switch (decoder.GetProperty())
            //    {
            //        case "P1":
            //            m.P1 = decoder.GetLong();
            //            break;
            //        case "P2":
            //            m.P2 = decoder.GetDouble();
            //            break;
            //        case "P3":
            //            m.P3 = decoder.CheckNull() ? null : new List<TS3>();
            //            if (m.P3 != null)
            //                decoder.GetCollection(
            //                           () => { return TS3.BiserJsonDecode(null, decoder); }, m.P3, true);                       
            //            break;
            //        case "P4":
            //            m.P4 = TS3.BiserJsonDecode(null, decoder);
            //            break;
            //        case "P5":
            //            m.P5 = decoder.GetUInt_NULL();
            //            break;
            //        default:
            //            return m;
            //    }

            //}
        }//eof

        //public T BiserJsonDecoder<T>(Biser.JsonDecoder decoder)
        //{
        //    return (T)(object)TS2.BiserJsonDecode(null, decoder, null);
        //}
    }
}
