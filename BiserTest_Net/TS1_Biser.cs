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
    /// <summary>
    /// Biser Extension
    /// </summary>
    public partial class TS1 : Biser.IEncoder, Biser.IJsonEncoder
    {
        public Biser.Encoder BiserEncoder(Biser.Encoder existingEncoder = null)
        {
            Biser.Encoder enc = new Biser.Encoder(existingEncoder);

            enc
            .Add(P1)
            .Add(P2)
            .Add(P3)
            .Add(P4, (r) => { enc.Add(r); })
            .Add(P5, (r) => { enc.Add(r.Key); enc.Add(r.Value); })
            .Add(P6, (r) => { enc.Add(r.Key); enc.Add(r.Value, (r1) => { enc.Add(r1); }); })
            .Add(P7)
            .Add(P8, (r) => { enc.Add(r.Item1); enc.Add(r.Item2); enc.Add(r.Item3); })
            .Add(P9.Item1).Add(P9.Item2).Add(P9.Item3).Add(P9.Item4)
            ;
            return enc;
        }

        public void BiserJsonEncode(Biser.JsonEncoder encoder) 
        {
            encoder.Add("P20", this.P20);

            encoder.Add("P1", this.P1);
            encoder.Add("P2", this.P2);
            encoder.Add("P17", this.P17);
            
            ////encoder.Add("P13", this.P13,(r)=> { r.BiserJsonEncode(encoder); });
           encoder.Add("P13", this.P13, (r) => { encoder.Add(r); });
            encoder.Add("P18", this.P18, (r) => { encoder.Add(r); });

            encoder.Add("P16", this.P16, (r) => { encoder.Add(r, (r1) => { encoder.Add(r1); }); });
            //encoder.Add("P16", this.P16);

            ////encoder.Add("P15", this.P15, (r) => { encoder.Add(r,(r1)=> { r1.BiserJsonEncode(encoder); }); });
            encoder.Add("P15", this.P15, (r) => { encoder.Add(r, (r1) => { encoder.Add(r1); }); });

            //As object
            if(this.P19 !=null)
                encoder.Add("P19", new Dictionary<string, Action>() {
                    { "Item1", ()=>encoder.Add(this.P19.Item1)},
                    { "Item2", ()=>encoder.Add(this.P19.Item2)},
                });

            ////As Array
            //encoder.Add("P19", new List<Action>() {
            //    ()=>encoder.Add(this.P19.Item1),
            //    ()=>encoder.Add(this.P19.Item2)
            //});
        }

        public static TS1 BiserJsonDecode(string enc = null,Biser.JsonDecoder extDecoder = null, Biser.JsonSettings settings = null) //!!!!!!!!!!!!!! change return type
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

            TS1 m = new TS1();  //!!!!!!!!!!!!!! change return type
            while (true)
            {
                switch (decoder.GetProperty())
                {
                    case "P20":
                        m.P20 = decoder.GetTimeSpan();
                        break;
                    case "P1":
                        m.P1 = decoder.GetInt();
                        break;
                    case "P2":
                        m.P2 = decoder.GetInt();
                        break;
                    case "P5":
                        m.P5 = decoder.CheckNull() ? null : new Dictionary<long, TS3>();
                        if (m.P5 != null)
                            decoder.GetCollection(() => { return decoder.GetLong(); },
                                    () => { return TS3.BiserJsonDecode(null, decoder); }, m.P5, true);
                        break;
                    case "P11":
                        m.P11 = decoder.CheckNull() ? null : new Dictionary<int, int>();
                        if (m.P11 != null)
                            decoder.GetCollection(() => { return decoder.GetInt(); },
                                    () => { return decoder.GetInt(); }, m.P11, true);
                        break;
                    case "P12":
                        m.P12 = decoder.GetInt();
                        break;
                    case "P13":

                        m.P13 = decoder.CheckNull() ? null : new List<TS3>();
                        if (m.P13 != null)
                            decoder.GetCollection(
                                       () => { return TS3.BiserJsonDecode(null, decoder); }, m.P13, true);
                        break;
                    case "P14":
                        m.P14 = decoder.CheckNull() ? null : new Dictionary<int, int>();
                        if (m.P14 != null)
                            decoder.GetCollection(() => { return decoder.GetInt(); },
                                    () => { return decoder.GetInt(); }, m.P14, true);
                        break;
                    case "P15":
                        m.P15 = decoder.CheckNull() ? null : new List<List<TS3>>();
                        if (m.P15 != null)
                            decoder.GetCollection(
                                       () =>
                                       {
                                           var il = decoder.CheckNull() ? null : new List<TS3>();
                                           if(il != null)
                                               decoder.GetCollection(
                                                        () => { return TS3.BiserJsonDecode(null, decoder); }, il, true);
                                           return il;
                                       },m.P15,true);
                        break;
                    case "P16":
                        m.P16 = decoder.CheckNull() ? null : new Dictionary<long, List<TS3>>();
                        if (m.P16 != null)
                            decoder.GetCollection(() => { return decoder.GetLong(); },
                                       () =>
                                       {
                                           var il = decoder.CheckNull() ? null : new List<TS3>();
                                           if(il != null)
                                               decoder.GetCollection(
                                                        () => { return TS3.BiserJsonDecode(null, decoder); }, il, true);
                                           return il;
                                       }, m.P16, true);
                        break;
                    case "P17":
                        m.P17 = decoder.GetDateTime();
                        break;
                    case "P18":

                        m.P18 = decoder.CheckNull() ? null : new List<int>();
                        if (m.P18 != null)
                            decoder.GetCollection(
                                       () => { return decoder.GetInt(); }, m.P18, true);
                        break;
                    case "P19":
                        if (decoder.CheckNull())
                        {
                            m.P19 = null;
                        }
                        else
                        {
                            //for Dictionary
                            m.P19 = new Tuple<int, TS3>(decoder.SkipProperty().GetInt(), TS3.BiserJsonDecode(null, decoder.SkipProperty()));
                            //for List
                            //m.P19 = new Tuple<int, TS3>(decoder.SkipProperty(true).GetInt(), TS3.BiserJsonDecode(null, decoder.SkipProperty(true)));
                        }

                        break;
                    default:
                        return m;
                }

            }
        }

        public static TS1 BiserDecode(byte[] enc = null, Biser.Decoder extDecoder = null) //!!!!!!!!!!!!!! change return type
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

            TS1 m = new TS1();  //!!!!!!!!!!!!!! change return type
            
            m.P1 = decoder.GetInt();
            m.P2 = decoder.GetInt();
            m.P3 = decoder.GetDecimal();

            m.P4 = decoder.CheckNull() ? null : new List<TS2>();
            if(m.P4 != null)
                decoder.GetCollection(() => { return TS2.BiserDecode(null, decoder); }, m.P4, true);

            m.P5 = decoder.CheckNull() ? null : new Dictionary<long, TS3>();
            if (m.P5 != null)
                decoder.GetCollection(() => {
                    return decoder.GetLong(); }, 
                    () => {  return TS3.BiserDecode(null, decoder); }, m.P5, true);

            m.P6 = decoder.CheckNull() ? null : new Dictionary<uint, List<TS3>>();
            if (m.P6 != null)
                decoder.GetCollection(
                    () => { return decoder.GetUInt(); },
                    () =>
                    {
                        var iList = decoder.CheckNull() ? null : new List<TS3>();
                        if (iList != null)
                        {
                            decoder.GetCollection(() => { return TS3.BiserDecode(extDecoder: decoder); }, iList, true);
                        }
                        return iList;
                    },
                    m.P6, true);

            m.P7 = TS2.BiserDecode(extDecoder: decoder);

            m.P8 = decoder.CheckNull() ? null : new List<Tuple<string, byte[], TS3>>();
            if (m.P8 != null)
                decoder.GetCollection
                    (() => { return new Tuple<string, byte[], TS3>
                        (decoder.GetString(), 
                        decoder.GetByteArray(), 
                        TS3.BiserDecode(null, decoder));
                    }, m.P8, true);

            m.P9 = new Tuple<float, TS2, TS3, decimal?>
                (decoder.GetFloat(), TS2.BiserDecode(null, decoder), TS3.BiserDecode(null, decoder), decoder.GetDecimal_NULL());

            return m;
        }
    }
}
