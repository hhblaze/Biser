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
                if (extDecoder.CheckNull())
                    return null;
                else
                    decoder = extDecoder;

                //decoder = new Biser.Decoder(extDecoder);
                //if (decoder.IsNull)
                //    return null;
            }

            TS1 m = new TS1();  //!!!!!!!!!!!!!! change return type

            m.P1 = decoder.GetInt();
            m.P2 = decoder.GetInt();
            m.P3 = decoder.GetDecimal();

            m.P4 = decoder.CheckNull() ? null : new List<TS2>();
            if (m.P4 != null)
                decoder.GetCollection(() => { return TS2.BiserDecode(null, decoder); }, m.P4, true);

            m.P5 = decoder.CheckNull() ? null : new Dictionary<long, TS3>();
            if (m.P5 != null)
                decoder.GetCollection(() => {
                    return decoder.GetLong();
                },
                    () => { return TS3.BiserDecode(null, decoder); }, m.P5, true);

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
                    (() => {
                        return new Tuple<string, byte[], TS3>
                   (decoder.GetString(),
                   decoder.GetByteArray(),
                   TS3.BiserDecode(null, decoder));
                    }, m.P8, true);

            m.P9 = new Tuple<float, TS2, TS3, decimal?>
                (decoder.GetFloat(), TS2.BiserDecode(null, decoder), TS3.BiserDecode(null, decoder), decoder.GetDecimal_NULL());

            return m;
        }






        public void BiserJsonEncode(Biser.JsonEncoder encoder) 
        {
          
            encoder.Add("P1", this.P1);
            encoder.Add("P2", this.P2);
            encoder.Add("P3", this.P3);
            encoder.Add("P4", this.P4, (r) => { encoder.Add(r); });
            encoder.Add("P5", this.P5, (r) => { encoder.Add(r); });
            encoder.Add("P6", this.P6, (r) => { encoder.Add(r, (r1) => { encoder.Add(r1); }); });
            encoder.Add("P7", this.P7);

            ////Storing as List
            //if (this.P8 != null)
            //    encoder.Add("P8", this.P8, (r) =>
            //    {
            //        encoder.Add(new List<Action>() {
            //        { ()=>encoder.Add(r.Item1)},
            //        { ()=>encoder.Add(r.Item2)},
            //        { ()=>encoder.Add(r.Item3)},
            //    });
            //    });
            ////Storing as Dictionary
            /// if (this.P8 != null)
            encoder.Add("P8", this.P8, (r) =>
            {
                encoder.Add(new Dictionary<string, Action>() {
                    { "Item1",()=>encoder.Add(r.Item1)},
                    { "Item2",()=>encoder.Add(r.Item2)},
                    { "Item3",()=>encoder.Add(r.Item3)},
                });
            });

            ////Storing as List
            //if (this.P9 != null)
            //    encoder.Add("P9", new List<Action>() { //Array of heterogenous types
            //        { ()=>encoder.Add(this.P9.Item1)},
            //        { ()=>encoder.Add(this.P9.Item2)},
            //        { ()=>encoder.Add(this.P9.Item3)},
            //        { ()=>encoder.Add(this.P9.Item4)},
            //    });
            ////Storing as Dictionary
            if (this.P9 != null)
                encoder.Add("P9", new Dictionary<string,Action>() { //Array of heterogenous types
                    { "Item1",()=>encoder.Add(this.P9.Item1)},
                    { "Item2",()=>encoder.Add(this.P9.Item2)},
                    { "Item3",()=>encoder.Add(this.P9.Item3)},
                    { "Item4",()=>encoder.Add(this.P9.Item4)},
                });

            if (this.P11 != null)
                encoder.Add("P11", P11, (r) => { encoder.Add(r); });

            encoder.Add("P12", this.P12);

            encoder.Add("P13", this.P13, (r) => { encoder.Add(r); });

            encoder.Add("P15", this.P15, (r) => { encoder.Add(r, (r1) => { encoder.Add(r1); }); });

            encoder.Add("P16", this.P16, (r) => { encoder.Add(r, (r1) => { encoder.Add(r1); }); });
         
            encoder.Add("P17", this.P17);

            encoder.Add("P18", this.P18, (r) => { encoder.Add(r); });
            

            ////Storing as List
            //if (this.P19 != null)
            //    encoder.Add("P19", new List<Action>() { //Array of heterogenous types
            //        { ()=>encoder.Add(this.P19.Item1)},
            //        { ()=>encoder.Add(this.P19.Item2)},
            //    });

            ////Storing as Dictionary with Item1, item2 etc
            if (this.P19 != null)
                encoder.Add("P19", new Dictionary<string, Action>() { //Array of heterogenous types
                    { "Item1", ()=>encoder.Add(this.P19.Item1)},
                    { "Item2", ()=>encoder.Add(this.P19.Item2)},
                });

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

            foreach (var props in decoder.GetDictionary<string>())
            {
                switch(props)
                {
                    case "P1":
                        m.P1 = decoder.GetInt();
                       
                        break;
                    case "P2":
                        m.P2 = decoder.GetInt();
                      
                        break;
                    case "P3":
                        m.P3 = decoder.GetDecimal();
                        break;
                    case "P4":

                        m.P4 = decoder.CheckNull() ? null : new List<TS2>();
                        if (m.P4 != null)
                        {
                            foreach (var el in decoder.GetList())
                                m.P4.Add(TS2.BiserJsonDecode(null, decoder));
                        }

                        break;
                    case "P5":
                        m.P5 = decoder.CheckNull() ? null : new Dictionary<long, TS3>();
                        if (m.P5 != null)
                        {
                            foreach (var el in decoder.GetDictionary<long>())
                                m.P5.Add(el, TS3.BiserJsonDecode(null, decoder));
                        }

                        break;
                    case "P6":
                        m.P6 = decoder.CheckNull() ? null : new Dictionary<uint, List<TS3>>();
                        if (m.P6 != null)
                        {
                            foreach (var el in decoder.GetDictionary<uint>())
                            {
                                var lst = decoder.CheckNull() ? null : new List<TS3>();
                                if (lst != null)
                                {
                                    foreach (var el1 in decoder.GetList())
                                    {
                                        lst.Add(TS3.BiserJsonDecode(null, decoder));
                                    }
                                }

                                m.P6.Add(el, lst);

                            }
                        }

                        break;
                    case "P7":
                        m.P7 = TS2.BiserJsonDecode(null, decoder);
                        break;
                    case "P8":
                        m.P8 = decoder.CheckNull() ? null : new List<Tuple<string, byte[], TS3>>();
                        if (m.P8 != null)
                        {
                            Tuple<string, byte[], TS3> tpl = null;
                            foreach (var el in decoder.GetList())
                            {
                                //foreach (var el1 in decoder.GetList()) //Tuple was also represented as an array
                                //{
                                //    tpl = new Tuple<string, byte[], TS3>(
                                //        decoder.GetString(),
                                //        decoder.GetByteArray(),
                                //        TS3.BiserJsonDecode(null, decoder));
                                //}//must come to the end, no returns in the middle of iteration

                                //Or Dictionary that is represented with Item1, item2, Item3 for compatibility with other systems
                                string i1 = "";
                                byte[] i2 = null;
                                TS3 i3 = null;
                                foreach (var tupleProps in decoder.GetDictionary<string>())
                                {
                                    switch(tupleProps)
                                    {
                                        case "Item1":
                                            i1 = decoder.GetString();
                                            break;
                                        case "Item2":
                                            i2 = decoder.GetByteArray();
                                            break;
                                        case "Item3":
                                            i3 = TS3.BiserJsonDecode(null, decoder);
                                            break;
                                        default:
                                            decoder.SkipValue();
                                            break;
                                    }
                                }

                                tpl = new Tuple<string, byte[], TS3>(i1, i2, i3);

                                m.P8.Add(tpl);
                            }
                        }

                        break;
                    case "P9":
                        if (decoder.CheckNull())
                        {
                            m.P9 = null;
                        }
                        else
                        {
                            //foreach (var el in decoder.GetList()) //heterogenous array
                            //{
                            //    m.P9 = new Tuple<float, TS2, TS3, decimal?>(decoder.GetFloat(), 
                            //        TS2.BiserJsonDecode(null, decoder), TS3.BiserJsonDecode(null, decoder), decoder.GetDecimal_NULL());
                            //}

                            //Or Dictionary that is represented with Item1, item2, Item3, Item4 for compatibility with other systems
                            float i1 = 0;
                            TS2 i2 = null;
                            TS3 i3 = null;
                            decimal? i4 = null;
                            foreach (var tupleProps in decoder.GetDictionary<string>())
                            {
                                switch (tupleProps)
                                {
                                    case "Item1":
                                        i1 = decoder.GetFloat();
                                        break;
                                    case "Item2":
                                        i2 = TS2.BiserJsonDecode(null, decoder);
                                        break;
                                    case "Item3":
                                        i3 = TS3.BiserJsonDecode(null, decoder);
                                        break;
                                    case "Item4":
                                        i4 = decoder.GetDecimal_NULL();
                                        break;
                                    default:
                                        decoder.SkipValue(); //must be here
                                        break;
                                }
                            }

                            m.P9 = new Tuple<float, TS2, TS3, decimal?>(i1, i2, i3, i4);

                        }

                        break;
                    case "P11":
                        m.P11 = decoder.CheckNull() ? null : new Dictionary<int, int>();
                        if (m.P11 != null)
                        {
                            foreach (var el in decoder.GetDictionary<int>())
                                m.P11.Add(el, decoder.GetInt());
                        }
                          
                        break;
                    case "P12":
                        m.P12 = decoder.GetInt();
                        break;
                    case "P13":

                        m.P13 = decoder.CheckNull() ? null : new List<TS3>();
                        if (m.P13 != null)
                        {
                            foreach (var el in decoder.GetList())
                                m.P13.Add(TS3.BiserJsonDecode(null, decoder));
                        }

                        break;
                    case "P14":
                        m.P14 = decoder.CheckNull() ? null : new Dictionary<int, int>();
                        if (m.P14 != null)
                        {
                            foreach (var el in decoder.GetDictionary<int>())
                                m.P14.Add(el, decoder.GetInt());
                        }
                          
                        break;
                    case "P15":
                        m.P15 = decoder.CheckNull() ? null : new List<List<TS3>>();
                        if (m.P15 != null)
                        {
                            foreach (var el in decoder.GetList())
                            {
                                var lst = decoder.CheckNull() ? null : new List<TS3>();
                                if (lst != null)
                                {
                                    foreach (var el1 in decoder.GetList())
                                        lst.Add(TS3.BiserJsonDecode(null, decoder));
                                }
                                m.P15.Add(lst);
                            }
                        }
                          
                        break;
                    case "P16":
                        m.P16 = decoder.CheckNull() ? null : new Dictionary<long, List<TS3>>();
                        if (m.P16 != null)
                        {
                            foreach (var el in decoder.GetDictionary<long>())
                            {
                                var lst = decoder.CheckNull() ? null : new List<TS3>();
                                if (lst != null)
                                {
                                    foreach (var el1 in decoder.GetList())
                                        lst.Add(TS3.BiserJsonDecode(null, decoder));
                                }
                                m.P16.Add(el, lst);
                            }
                        }
                           
                        break;
                    case "P17":
                        m.P17 = decoder.GetDateTime();
                        break;
                    case "P18":

                        m.P18 = decoder.CheckNull() ? null : new List<int>();
                        if (m.P18 != null)
                        {
                            foreach (var el in decoder.GetList())
                                m.P18.Add(decoder.GetInt());
                        }
                           
                        break;
                    case "P19":
                        if (decoder.CheckNull())
                        {
                            m.P19 = null;
                        }
                        else
                        {
                            //When stored as List
                            //foreach (var el in decoder.GetList()) //heterogenous array
                            //{
                            //    m.P19 = new Tuple<int, TS3>(decoder.GetInt(), TS3.BiserJsonDecode(null, decoder));
                            //}

                            //Or Dictionary that is represented with Item1, item2, Item3 for compatibility with other systems
                            int i1 = 0;                            
                            TS3 i2 = null;
                            foreach (var tupleProps in decoder.GetDictionary<string>())
                            {
                                switch (tupleProps)
                                {
                                    case "Item1":
                                        i1 = decoder.GetInt();
                                        break;
                                    case "Item2":
                                        i2 = TS3.BiserJsonDecode(null, decoder);
                                        break;                                   
                                    default:
                                        decoder.SkipValue();
                                        break;
                                }
                            }

                            m.P19 = new Tuple<int, TS3>(i1, i2);

                        }

                        break;
                    default:
                        decoder.SkipValue(); //must be here
                        break;
                }
            }

            return m;

        }

        

       

    }
}
