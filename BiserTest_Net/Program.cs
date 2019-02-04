/* 
  Copyright (C) 2012 dbreeze.tiesky.com / Alex Solovyov
  It's a free software for those, who think that it should be free.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Biser;

namespace BiserTest_Net
{
    class Program
    {
        static void Main(string[] args)
        {
            var resbof = BiserObjectify.Generator.Run(typeof(TS6), true, @"D:\Temp\1\", true, false);
            return;

            TS6 t6 = new TS6()
            {
                //P1 = "dsfs",
                //P2 = 456,
                //P3 = DateTime.UtcNow,
                //P4 = new List<Dictionary<DateTime, Tuple<int, string>>>
                //    {
                //        new Dictionary<DateTime, Tuple<int, string>>{
                //            { DateTime.UtcNow.AddMinutes(-1), new Tuple<int, string>(12,"pipec") },
                //            { DateTime.UtcNow.AddMinutes(-2), new Tuple<int, string>(125,"pipec123") }
                //        },
                //        new Dictionary<DateTime, Tuple<int, string>>{
                //            { DateTime.UtcNow.AddMinutes(-3), new Tuple<int, string>(17,"pihfpec") },
                //            { DateTime.UtcNow.AddMinutes(-4), new Tuple<int, string>(15625,"pipfghec123") }
                //        }
                //    },
                //P5 = new Dictionary<int, Tuple<int, string>> {
                //     { 12, new Tuple<int, string>(478,"dsffdf") },
                //     { 178, new Tuple<int, string>(5687,"sdfsd") }
                // },
                //P6 = new Tuple<int, string, Tuple<List<string>, DateTime>>(445, "dsfdfgfgfg", new Tuple<List<string>, DateTime>(new List<string> { "a1", "a2" }, DateTime.Now.AddDays(58))),
                //P7 = new List<string> { "fgdfgrdfg", "dfgfdgdfg" },
                //P8 = new Dictionary<int, List<string>> {
                //        { 34,new List<string> { "drtttz","ghhtht"} },
                //        { 4534,new List<string> { "dfgfghfgz","6546ghhtht"} }
                //    },


                //P13 = new List<List<int>> { new List<int> { 12, 43, 54 }, new List<int> { 12, 43, 54 } },

                //P16 = new List<Dictionary<int, Tuple<int, string>>>
                //    {
                //        new Dictionary<int, Tuple<int, string>>{
                //            { 1, new Tuple<int, string>(12,"pipec") },
                //            { 2, new Tuple<int, string>(125,"pipec123") }
                //        },
                //        new Dictionary<int, Tuple<int, string>>{
                //            { 3, new Tuple<int, string>(17,"pihfpec") },
                //            { 4, new Tuple<int, string>(15625,"pipfghec123") }
                //        }
                //    },

                //P17 = new int[3] { 12,3545,7987}
            };


            //t6.P11 = new int[2][];
            //t6.P11[0] = new int[3];
            //t6.P11[1] = new int[3];
            //t6.P11[0][0] = 12;
            //t6.P11[0][1] = 14;
            //t6.P11[1][0] = 125;
            //t6.P11[1][2] = 19;

            //t6.P12 = new int[2, 3, 4];
            //t6.P12[0, 0, 0] = 12;
            //t6.P12[0, 0, 1] = 13;
            //t6.P12[0, 1, 0] = 14;
            //t6.P12[0, 1, 1] = 15;
            //t6.P12[1, 0, 0] = 16;
            //t6.P12[1, 0, 1] = 17;


            //Json test


            //var njSer = NetJSON.NetJSON.Serialize(t6, new NetJSON.NetJSONSettings { DateFormat = NetJSON.NetJSONDateFormat.ISO });

            //var jsonSet = new Biser.JsonSettings { DateFormat = Biser.JsonSettings.DateTimeStyle.ISO };
            //Biser.JsonEncoder enc = new Biser.JsonEncoder(t6, jsonSet);
            //string es = enc.GetJSON(Biser.JsonSettings.JsonStringStyle.Prettify);
            //var ot2 = TS6.BiserJsonDecode(es, settings: jsonSet);


            //Binary test



            //var encb = t6.BiserEncoder().Encode();
            //var t6n = TS6.BiserDecode(encb);

            return;
            TestDecodeV1();
            return;
            //var jsres = NetJSON.NetJSON.Serialize((int)12); //12
            //var ojsres = NetJSON.NetJSON.Deserialize<int>(jsres);

            //var jsres2 = NetJSON.NetJSON.Serialize((double)12.45687); //12.45687
            //var ojsres2 = NetJSON.NetJSON.Deserialize<double>(jsres2);

            //Dictionary<string, byte[]> dic1d = new Dictionary<string, byte[]>();
            //dic1d.Add("str1", new byte[] { 1, 2, 3 });
            //dic1d.Add("str2", new byte[] { 1, 2 });
            //dic1d.Add("str3", null);

            //var jsres1 = NetJSON.NetJSON.Serialize(dic1d); //{"str1":"AQID","str2":"AQI=","str3":null}
            //var ojsres1 = NetJSON.NetJSON.Deserialize<Dictionary<string, byte[]>>(jsres1);

            //List<Dictionary<string, byte[]>> ldic1d = new List<Dictionary<string, byte[]>>();
            //ldic1d.Add(dic1d);
            //ldic1d.Add(dic1d);
            //var jsres3 = NetJSON.NetJSON.Serialize(ldic1d); //[{"str1":"AQID","str2":"AQI=","str3":null},{"str1":"AQID","str2":"AQI=","str3":null}]
            //var ojsres3 = NetJSON.NetJSON.Deserialize<List<Dictionary<string, byte[]>>>(jsres3);

            //var jsres4 = NetJSON.NetJSON.Serialize((string)"ds\"fs{d}f"); //"ds\"fs{d}f"
            //// var jsres4 = NetJSON.NetJSON.Serialize("dsf\"sdfdsf{fdgdfgdf{dsfdsf[sdf\"\"dfdsf}"); //"dsf\"sdfdsf{fdgdfgdf{dsfdsf[sdf\"\"dfdsf}"
            //var ojsres4 = NetJSON.NetJSON.Deserialize<string>(jsres4); //"ds"fsdf"

            TS2 jts2 = new TS2()
            {
                P1 = long.MinValue,
                P2 = 4587.4564,
                P3 = new List<TS3> {
                     new TS3 { P3 = DateTime.UtcNow.AddDays(-1) },
                     null,
                     //new TS3 { P3 = DateTime.UtcNow.AddDays(-2) },
                     new TS3 { P3 = DateTime.UtcNow.AddDays(-3) }
                },
                P4 = new TS3 { P1 = "hi" },
                P5 = 111
            };

            //TS1 jts1 = new TS1()
            //{
            //    P1 = 12,
            //    P2 = 15,
            //    P3 = 478.5879m,
            //    P4 = new List<TS2> { jts2, jts2 },
            //    P5 = new Dictionary<long, TS3> {
            //        { 1, new TS3{ P1 = "t1" } },
            //        { 2, new TS3{ P1 = "t2" } },
            //        { 3, new TS3{ P1 = "t3" } }
            //    },
            //    P6 = new Dictionary<uint, List<TS3>> {
            //        { 1, new List<TS3>{ new TS3 { P1 = "h1" }, new TS3 { P1 = "h2" }, new TS3 { P1 = "h3" } } },
            //        { 2, new List<TS3>{ new TS3 { P1 = "h2" }, new TS3 { P1 = "h2" }, new TS3 { P1 = "h4" } } },
            //        { 3, new List<TS3>{ new TS3 { P1 = "h3" }, new TS3 { P1 = "h2" }, new TS3 { P1 = "h5" } } },
            //        { 4, new List<TS3>{ new TS3 { P1 = "h4" }, new TS3 { P1 = "h2" }, new TS3 { P1 = "h6" } } }
            //    },
            //    P7 = new TS2 { P1 = -789 },
            //    P8 = new List<Tuple<string, byte[], TS3>> {
            //        new Tuple<string, byte[], TS3>("tt1",new byte[] { 1,2,3},new TS3 { P1 = "z1" }),
            //        new Tuple<string, byte[], TS3>("tt2",new byte[] { 3,2,3},new TS3 { P1 = "z2" }),
            //        new Tuple<string, byte[], TS3>("tt3",new byte[] { 4,2,3},new TS3 { P1 = "z3" }),
            //    },
            //    P9 = new Tuple<float, TS2, TS3, decimal?>(-.8f, new TS2 { P2 = 45 }, new TS3 { P2 = 12 }, -58.8m),
            //    P10 = "dsf\"sdfdsf{fdgdfgdf{dsfdsf[sdf\"\"dfdsf}"
            //};



            //var jsres5 = NetJSON.NetJSON.Serialize(jts1, new NetJSON.NetJSONSettings { DateFormat = NetJSON.NetJSONDateFormat.ISO, Format = NetJSON.NetJSONFormat.Prettify }); 
            ///*{"P1":12,"P2":15,"P3":478.5879,"P4":[{"P1":-9223372036854775808,"P2":4587.4564,"P3":[{"P3":"\/Date(15340651396274201)\/"},null,{"P3":"\/Date(15338923396274201)\/"}],"P4":{"P1":"hi"},"P5":111},{"P1":-9223372036854775808,"P2":4587.4564,"P3":[{"P3":"\/Date(15340651396274201)\/"},null,{"P3":"\/Date(15338923396274201)\/"}],"P4":{"P1":"hi"},"P5":111}],"P5":{"1":{"P1":"t1"},"2":{"P1":"t2"},"3":{"P1":"t3"}},"P6":{"1":[{"P1":"h1"},{"P1":"h2"},{"P1":"h3"}],"2":[{"P1":"h2"},{"P1":"h2"},{"P1":"h4"}],"3":[{"P1":"h3"},{"P1":"h2"},{"P1":"h5"}],"4":[{"P1":"h4"},{"P1":"h2"},{"P1":"h6"}]},"P7":{"P1":-789},"P8":[{"Item1":"tt1","Item2":"AQID","Item3":{"P1":"z1"}},{"Item1":"tt2","Item2":"AwID","Item3":{"P1":"z2"}},{"Item1":"tt3","Item2":"BAID","Item3":{"P1":"z3"}}],"P9":{"Item1":12.8,"Item2":{"P2":45},"Item3":{"P2":12},"Item4":-58.8}}*/
            //var ojsres5 = NetJSON.NetJSON.Deserialize<TS1>(jsres5); //"ds"fsdf"


            //Dictionary<int, byte[]> dic1d2 = new Dictionary<int, byte[]>(); //key will be transformed toString, so key can't be byte[]
            //dic1d2.Add(12, new byte[] { 1, 2, 3 });
            //dic1d2.Add(17, new byte[] { 1, 2 });


            //var jsres6 = NetJSON.NetJSON.Serialize(dic1d2); //{"12":"AQID","17":"AQI="}
            //var ojsres6 = NetJSON.NetJSON.Deserialize<Dictionary<int, byte[]>>(jsres6);


            //Dictionary<int, string> dic1d3 = new Dictionary<int, string>(); //key will be transformed toString, so key can't be byte[]
            //dic1d3.Add(12, "dsf\"sdfdsf{fdgdfgdf{dsfdsf[sdf\"\"dfdsf}");
            //dic1d3.Add(17, "dsf\"sdfdsf{fdgddddf{dsfdsf[sdf\"\"dfdsf}");


            //var jsres7 = NetJSON.NetJSON.Serialize(dic1d3, new NetJSON.NetJSONSettings { DateFormat = NetJSON.NetJSONDateFormat.ISO, Format = NetJSON.NetJSONFormat.Prettify }); //{"12":"AQID","17":"AQI="}
            //var ojsres7 = NetJSON.NetJSON.Deserialize<Dictionary<int, string>>(jsres7);




            ////var jsres8 = NetJSON.NetJSON.Serialize((int?)null); //{"12":"AQID","17":"AQI="}
            ////var ojsres8 = NetJSON.NetJSON.Deserialize<int?>(jsres8);

            ////var jsres8 = NetJSON.NetJSON.Serialize("dsf\"sdfdsf{fdgdfgdf{dsfdsf[sdf\"\"dfdsf}"); //{"12":"AQID","17":"AQI="}
            ////var ojsres8 = NetJSON.NetJSON.Deserialize<string>(jsres8);

            ////Dictionary<int, int> dic1d4 = new Dictionary<int, int>(); //key will be transformed toString, so key can't be byte[]
            ////dic1d4.Add(12, 15);
            ////dic1d4.Add(17, 57);
            ////var jsres8 = NetJSON.NetJSON.Serialize(dic1d4); //{"12":"AQID","17":"AQI="}
            ////var ojsres8 = NetJSON.NetJSON.Deserialize<Dictionary<int, int>>(jsres8);

            //List<int> dic1d4 = new List<int>();
            //dic1d4.Add(324);
            //dic1d4.Add(33);
            //var jsres8 = NetJSON.NetJSON.Serialize(dic1d4); //{"12":"AQID","17":"AQI="}
            ////var ojsres8 = NetJSON.NetJSON.Deserialize<List<int, int>>(jsres8);



            //JsonDecoder jsdec = new JsonDecoder(jsres8); //{"12":15,"17":57}
            ////var iuz = jsdec.GetInt_NULL();
            ////var iuz = jsdec.GetString();
            ////Dictionary<int,int> iuzd = jsdec.CheckNull() ? null : new Dictionary<int, int>();
            //List<int> iuzd = jsdec.CheckNull() ? null : new List<int>();

            //if (iuzd != null)
            //{
            //    //jsdec.GetCollection(() => { return jsdec.GetInt(); },
            //    //    () => { return jsdec.GetInt(); }, iuzd, true);
            //    //foreach (var item in iuzd)
            //    //    Debug.WriteLine(item.Key);
            //    jsdec.GetCollection(() => { return jsdec.GetInt(); }, iuzd, true);
            //    foreach (var item in iuzd)
            //        Debug.WriteLine(item);
            //}

            TS1 jsts1 = new TS1()
            {
                P1 = 12,
                P2 = 17,
                P3 = 478.5879m,
                P4 = new List<TS2> { jts2, jts2 },
                //P5 = new Dictionary<long, TS3> {
                //        { 1, new TS3{ P1 = "t1" } },
                //        { 2, new TS3{ P1 = "t2" } },
                //        { 3, new TS3{ P1 = "t3" } }
                //    },
                P6 = new Dictionary<uint, List<TS3>> {
                        { 1, new List<TS3>{ new TS3 { P1 = "h1" }, new TS3 { P1 = "h2" }, new TS3 { P1 = "h3" } } },
                        { 2, new List<TS3>{ new TS3 { P1 = "h2" }, new TS3 { P1 = "h2" }, new TS3 { P1 = "h4" } } },
                        { 3, new List<TS3>{ new TS3 { P1 = "h3" }, new TS3 { P1 = "h2" }, new TS3 { P1 = "h5" } } },
                        { 4, new List<TS3>{ new TS3 { P1 = "h4" }, new TS3 { P1 = "h2" }, new TS3 { P1 = "h6" } } }
                    },
                P7 = new TS2 { P1 = -789 },
                P8 = new List<Tuple<string, byte[], TS3>> {
                        new Tuple<string, byte[], TS3>("tt1",new byte[] { 1,2,3},new TS3 { P1 = "z1" }),
                        new Tuple<string, byte[], TS3>("tt2",new byte[] { 3,2,3},new TS3 { P1 = "z2" }),
                        new Tuple<string, byte[], TS3>("tt3",new byte[] { 4,2,3},new TS3 { P1 = "z3" }),
                    },
                P9 = new Tuple<float, TS2, TS3, decimal?>(-.8f, new TS2 { P2 = 45 }, new TS3 { P2 = 12 }, -58.8m),
            };

            jsts1.P11 = new Dictionary<int, int>();
            jsts1.P11.Add(12, 14);
            jsts1.P11.Add(17, 89);

            //jsts1.P14 = new Dictionary<int, int>();
            //jsts1.P14.Add(17, 14);
            //jsts1.P14.Add(19, 89);


            jsts1.P5 = new Dictionary<long, TS3>();
            jsts1.P5.Add(189, new TS3 { P1 = "dsf", P2 = 45, P3 = DateTime.UtcNow });
            jsts1.P5.Add(178, new TS3 { P1 = "sdfsdfsdfs", P2 = null, P3 = DateTime.UtcNow });
            jsts1.P5.Add(148, new TS3 { P1 = "dfdff", P2 = null, P3 = DateTime.UtcNow });

            jsts1.P12 = 789;

            jsts1.P13 = new List<TS3>();
            jsts1.P13.Add(new TS3 { P1 = "dsf", P2 = 45, P3 = DateTime.UtcNow });
            jsts1.P13.Add(new TS3 { P1 = "sdfsdfsdfs", P2 = null, P3 = DateTime.UtcNow });

            jsts1.P15 = new List<List<TS3>>();
            jsts1.P15.Add(jsts1.P13);
            jsts1.P15.Add(jsts1.P13);

            jsts1.P16 = new Dictionary<long, List<TS3>>();
            jsts1.P16.Add(12, jsts1.P13);
            jsts1.P16.Add(14, jsts1.P13);
            jsts1.P16.Add(28, jsts1.P13);


            jsts1.P18 = new List<int>();
            jsts1.P18.Add(178);
            jsts1.P18.Add(912);

            jsts1.P19 = new Tuple<int, TS3>(12, new TS3 { P1 = "dsf", P2 = 45, P3 = DateTime.UtcNow });



           

            jsts1.P17 = new DateTime(2018, 6, 5, 17,44,15,443, DateTimeKind.Utc);

            //var jsres9 = NetJSON.NetJSON.Serialize(jsts1, new NetJSON.NetJSONSettings() { Format = NetJSON.NetJSONFormat.Prettify });
            var jsres9 = NetJSON.NetJSON.Serialize(jsts1, new NetJSON.NetJSONSettings() {
                Format = NetJSON.NetJSONFormat.Prettify,
                DateFormat = NetJSON.NetJSONDateFormat.Default
            });
            var njdv1 = NetJSON.NetJSON.Deserialize<TS1>(jsres9, new NetJSON.NetJSONSettings()
            { //Format = NetJSON.NetJSONFormat.Prettify,
                DateFormat = NetJSON.NetJSONDateFormat.Default
            });
            //var jsres9 = NetJSON.NetJSON.Serialize(jsts1);
            TS1 jsts1d = null;
            
            //jsts1d = TS1.BiserJsonDecode(jsres9, null, new JsonSettings { DateFormat = JsonSettings.DateTimeStyle.ISO });
               
            //-----------------
            //var jsres91 = NetJSON.NetJSON.Serialize(jsts1.P13, new NetJSON.NetJSONSettings()
            //{ //Format = NetJSON.NetJSONFormat.Prettify,
            //    DateFormat = NetJSON.NetJSONDateFormat.ISO
            //}); 

            JsonEncoder jenc = new JsonEncoder(null, new JsonSettings { DateFormat = JsonSettings.DateTimeStyle.Default,
                JsonStringFormat = JsonSettings.JsonStringStyle.Prettify });
           // jsts1.BiserJsonEncode(jenc);
            
            
            string wow1 = jenc.GetJSON();
            
            var jsts1d1 = TS1.BiserJsonDecode(wow1, null, new JsonSettings { DateFormat = JsonSettings.DateTimeStyle.Default });

            //TestJSONv1();            
            //StreamReader sr=new StreamReader("",Encoding.UTF8)
            //StreamWriter sw=new StreamWriter()
            Console.WriteLine("Press to start test");
            Console.ReadLine();

             
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for (int i = 0; i < 10000; i++)
            {
                jenc = new JsonEncoder(new JsonSettings
                {
                    DateFormat = JsonSettings.DateTimeStyle.Default,
                    JsonStringFormat = JsonSettings.JsonStringStyle.Default
                });
                jsts1.BiserJsonEncode(jenc);
                wow1 = jenc.GetJSON();
            }
            sw.Stop();
            Console.WriteLine($"Biser encode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 10000; i++)
            {
                jsres9 = NetJSON.NetJSON.Serialize(jsts1, new NetJSON.NetJSONSettings()
                {
                    Format = NetJSON.NetJSONFormat.Default,
                    DateFormat = NetJSON.NetJSONDateFormat.Default
                });
            }
            sw.Stop();
            Console.WriteLine($"NetJSON encode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();



            sw.Start();
            for (int i = 0; i < 10000; i++)
            {
                jsts1d = TS1.BiserJsonDecode(wow1, null, new JsonSettings { DateFormat = JsonSettings.DateTimeStyle.Default });
            }
            sw.Stop();
            Console.WriteLine($"Biser decode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 10000; i++)
            {
                jsts1d = NetJSON.NetJSON.Deserialize<TS1>(jsres9);
            }
            sw.Stop();
            Console.WriteLine($"NetJSON decode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();



           // jsts1d = TS1.BiserJsonDecode(jsres9);
            Console.ReadLine();
            return;
            Biser.Encoder en2 = new Biser.Encoder();
            en2.Add((int)12);
            Dictionary<string, byte[]> dic1 = new Dictionary<string, byte[]>();
            dic1.Add("str1", new byte[] { 1, 2, 3 });
            dic1.Add("str2", new byte[] { 1, 2 });
            dic1.Add("str3", null);
            dic1.Add("str4", new byte[0]);
            dic1.Add("str5", new byte[] { 1, 2,3,4,5 });
            en2.Add(dic1, r => { en2.Add(r.Key); en2.Add(r.Value); });
            //List<int> lst1 = new List<int>();
            //lst1.Add(1);
            //lst1.Add(2);
            //lst1.Add(3);
            //en2.Add(lst1, r => { en2.Add(r); });
            en2.Add((int)14);
            Biser.Decoder de2 = new Biser.Decoder(en2.Encode());
            Debug.WriteLine(de2.GetInt());
            //List<int> lst = de2.CheckNull() ? null : new List<int>();
            //if (lst != null)
            //{
            //    de2.GetCollection(() => { return de2.GetInt(); }, lst, true);
            //    foreach (var item in lst)
            //        Debug.WriteLine(item);
            //}
            Dictionary<string, byte[]> dic = de2.CheckNull() ? null : new Dictionary<string, byte[]>();
            if (dic != null)
            {
                de2.GetCollection(() => { return de2.GetString(); },
                    () => { return de2.GetByteArray(); }, dic, true);
                foreach (var item in dic)
                    Debug.WriteLine(item.Key);
            }
            Debug.WriteLine(de2.GetInt());
            return;

            //var le = BitConverter.IsLittleEndian;
            Biser.Encoder enn = new Biser.Encoder();
            byte[] btEnn = null;

            double flv = -17.32;

            enn.Add(flv);
            //enn.Add((long)1);
            btEnn = enn.Encode();
            var res = true ^ false;
            //BitConverter.ToDouble()
            var fltBts = BitConverter.GetBytes(flv);
            return;

            
            //enn.Add((long)-123879);            
            enn.Add((ulong)1521797378957);
            btEnn=enn.Encode();

            long tr = 1521797378957;

            do
            {
                tr = tr >> 7;
                Console.WriteLine("М " + tr);
            } while (tr != 0);

            

            Biser.Decoder denn = new Biser.Decoder(btEnn);
            //var hzj = denn.GetLong();
            var hzj = denn.GetULong();

            var tt = Biser.Biser.EncodeZigZag((long)-123879, 64);
            var value = -123879;
            
            var tt1 = (value << 1) ^ (value >> 63);
           var tt2 =  Biser.Biser.DecodeZigZag((ulong)tt1);
            Console.WriteLine("L=" + (value << 1));
            Console.WriteLine("R=" + (value >> 63));
            Console.WriteLine("T=" + tt1 + "; dec=" + tt2);

            Console.WriteLine("tmp=" + (100000000000 & 0x7fffffff));
            Console.WriteLine("tmp=" + ((-100000000000) & 0x7fffffff));

            /*
            var v1 = 123879;
            var hi = 0x80000000;
            var low = 0x7fffffff;
            var hi1 = ~~(v1 / hi);
            var low1 = v1 & low;
            var v1b =  hi1 * hi + low1;
            */

            //var tm = new DateTime(1521797378957*10000, DateTimeKind.Utc);
            var tm = new DateTime(1970,1,1,0,0,0,0,DateTimeKind.Utc)
                .AddMilliseconds(1521797378957); //
            
            for(int i =0;i<256;i++)
            {
                //Console.WriteLine((sbyte)(i) + " _ " + (i & 0x80)+ " _ " + i + " _ " + (i - (i&0x80)));
                sbyte p = (sbyte)(i);
                sbyte s = (sbyte)(i);
                byte b = (byte)((p + 128) + (1 - 2 * (((p + 128) & 0x80) >> 7)) * 128);                
                byte b1 = (byte)(s + (256 & ((s & 0x80) << 1)) );
                Console.WriteLine(i + " _ " + (sbyte)(i) + " _ " + (i - ((i & 128) << 1)) 
                    + " _ "  + b + " _> " + b1
                    ); //128 0x80 byte to sbyte converter
            }
            //double flv = 12.56;
            //double flv = 124.56;
            
            var uBts = BitConverter.ToUInt64(fltBts, 0);

            return;

            //float flv = 12.56f;
            //flv = 0;
            //flv = float.MinValue;
            //flv = float.MaxValue;
            //var fltBts = BitConverter.GetBytes(flv);
            //var uBts = BitConverter.ToUInt32(fltBts, 0);

            return;
            TS3 ts3 = new TS3()
            {
                P1 = "welldone",
                P2 = null,
                P3 = DateTime.UtcNow
            };

            //var bt3 = ts3.BiserEncoder().Encode();
            //TS3 ts2D = TS3.BiserDecode(bt3);

            TS2 ts2 = new TS2()
            {
                P1 = long.MinValue,
                P2 = 4587.4564,
                P3 = new List<TS3> {
                     new TS3 { P3 = DateTime.UtcNow.AddDays(-1) },
                     null,
                     //new TS3 { P3 = DateTime.UtcNow.AddDays(-2) },
                     new TS3 { P3 = DateTime.UtcNow.AddDays(-3) }
                },
                P4 = new TS3 { P1 = "hi" },
                P5 = 111
            };

            //var bt2 = ts2.BiserEncoder().Encode();
            //TS2 ts2D = TS2.BiserDecode(bt2);

            TS1 ts1 = new TS1()
            {
                P1 = 12,
                P2 = 15,
                P3 = 478.5879m,
                P4 = new List<TS2> { ts2, ts2 },
                P5 =new Dictionary<long, TS3> {
                    { 1, new TS3{ P1 = "t1" } },
                    { 2, new TS3{ P1 = "t2" } },
                    { 3, new TS3{ P1 = "t3" } }
                },
                P6=new Dictionary<uint, List<TS3>> {
                    { 1, new List<TS3>{ new TS3 { P1 = "h1" }, new TS3 { P1 = "h2" }, new TS3 { P1 = "h3" } } },
                    { 2, new List<TS3>{ new TS3 { P1 = "h2" }, new TS3 { P1 = "h2" }, new TS3 { P1 = "h4" } } },
                    { 3, new List<TS3>{ new TS3 { P1 = "h3" }, new TS3 { P1 = "h2" }, new TS3 { P1 = "h5" } } },
                    { 4, new List<TS3>{ new TS3 { P1 = "h4" }, new TS3 { P1 = "h2" }, new TS3 { P1 = "h6" } } }
                },
                P7 = new TS2 { P1 = -789 },
                P8=new List<Tuple<string, byte[], TS3>> {
                    new Tuple<string, byte[], TS3>("tt1",new byte[] { 1,2,3},new TS3 { P1 = "z1" }),
                    new Tuple<string, byte[], TS3>("tt2",new byte[] { 3,2,3},new TS3 { P1 = "z2" }),
                    new Tuple<string, byte[], TS3>("tt3",new byte[] { 4,2,3},new TS3 { P1 = "z3" }),
                },
                P9=new Tuple<float, TS2, TS3, decimal?>(12.8f,new TS2 { P2 = 45 }, new TS3 { P2 = 12 }, -58.8m)
            };


            var bt1 = ts1.BiserEncoder().Encode();
            TS1 ts1D = TS1.BiserDecode(bt1);

            //TestMultiDimensionArray
            //TestCustom();
            //TestPrimitives();
            //TestBE1();
            TestT5();
            //TestListDictionary();

            Console.ReadLine();

            //var enc = new Biser.Encoder();
            //enc.Add(double.MinValue);
            //enc.Add(double.MaxValue);
            //enc.Add((double)-455.45);
            //enc.Add((double)465.45);

            //var decoder = new Biser.Decoder(enc.Encode());
            //var d1 = decoder.GetDouble();
            //var d2 = decoder.GetDouble();
            //var d3 = decoder.GetDouble();
            //var d4 = decoder.GetDouble();


            //var enc = new Biser.Encoder();
            //enc.Add(float.MinValue);
            //enc.Add(float.MaxValue);
            //enc.Add((float)-455.45);
            //enc.Add((float)465.45);

            //var decoder = new Biser.Decoder(enc.Encode());
            //var d1 = decoder.GetFloat();
            //var d2 = decoder.GetFloat();
            //var d3 = decoder.GetFloat();
            //var d4 = decoder.GetFloat();
        }

        #region "test JSONv1"
        public partial class t1 : Biser.IJsonEncoder
        {
            public int p1 { get; set; }
            public string p2 { get; set; }
            public t2 p3 { get; set; }
            public List<t2> p4 { get; set; }

            public void BiserJsonEncode(Biser.JsonEncoder encoder)
            {
                encoder.Add("p1", this.p1);
                encoder.Add("p2", this.p2);
                encoder.Add("p3", this.p3);
                encoder.Add("p4", this.p4, (r)=> { encoder.Add(r); });
            }

            public static t1 BiserJsonDecode(string enc = null, Biser.JsonDecoder extDecoder = null, Biser.JsonSettings settings = null) //!!!!!!!!!!!!!! change return type
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

                t1 m = new t1();  //!!!!!!!!!!!!!! change return type
                foreach (var props in decoder.GetDictionary<string>())
                {
                    switch (props)
                    {
                        case "p1":
                            m.p1 = decoder.GetInt();
                            break;
                        case "p2":
                            m.p2 = decoder.GetString();
                            break;
                        case "p3":
                            m.p3 = t2.BiserJsonDecode(null, decoder);
                            break;
                        case "p4":
                            m.p4 = decoder.CheckNull() ? null : new List<t2>();
                            if (m.p4 != null)
                            {
                                foreach (var el in decoder.GetList())
                                    m.p4.Add(t2.BiserJsonDecode(null, decoder));
                            }
                            break;
                        default:
                            decoder.SkipValue();//MUST BE HERE
                            break;
                    }
                }
                return m;
            }
        }

        public partial class t2 : Biser.IJsonEncoder
        {
            public DateTime p1 { get; set; }
            public string p2 { get; set; }

            public void BiserJsonEncode(Biser.JsonEncoder encoder)
            {
                encoder.Add("p1", this.p1);
                encoder.Add("p2", this.p2);
            }

            public static t2 BiserJsonDecode(string enc = null, Biser.JsonDecoder extDecoder = null, Biser.JsonSettings settings = null) //!!!!!!!!!!!!!! change return type
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

                t2 m = new t2();  //!!!!!!!!!!!!!! change return type
                foreach (var props in decoder.GetDictionary<string>())
                {
                    switch (props)
                    {
                        case "p1":
                            m.p1 = decoder.GetDateTime();
                            break;
                        case "p2":
                            m.p2 = decoder.GetString();
                            break;
                        default:
                            decoder.SkipValue();//MUST BE HERE
                            break;
                    }
                }
                return m;
            }


        }

        static void TestDecodeV1()
        {
            TS3 ts3 = new TS3()
            {
                P1 = "dsf",
                P2 = 45,
                P3 = DateTime.UtcNow
            };

            var xl=new List<TS3>();
            for (int i = 0; i < 10; i++)
                xl.Add(ts3);

            TS2 ts2 = new TS2()
            {
                P1 = 17,
                P2 = 45.7,
                //P3 = new List<TS3> { ts3, ts3 },
                P3 = xl,
                P4 = ts3,
                P5 = 47
            };

            TS1 ts1 = new TS1()
            {
                P1 = 12,
                P2 = 17,
                P3 = 478.5879m,
                P4 = new List<TS2> { ts2, ts2 },
                P5 = new Dictionary<long, TS3> {
                        { 1, new TS3{ P1 = "t1" } },
                        { 2, new TS3{ P1 = "t2" } },
                        { 3, new TS3{ P1 = "t3" } }
                    },
                P6 = new Dictionary<uint, List<TS3>> {
                        { 1, new List<TS3>{ new TS3 { P1 = "h1" }, new TS3 { P1 = "h2" }, new TS3 { P1 = "h3", P3 = DateTime.UtcNow } } },
                        { 2, new List<TS3>{ new TS3 { P1 = "h2" }, new TS3 { P1 = "h2" }, new TS3 { P1 = "h4" } } },
                        { 3, new List<TS3>{ new TS3 { P1 = "h3" }, new TS3 { P1 = "h2" }, new TS3 { P1 = "h5" } } },
                        { 4, new List<TS3>{ new TS3 { P1 = "h4" }, new TS3 { P1 = "h2" }, new TS3 { P1 = "h6" } } }
                    },
                P7 = new TS2 { P1 = -789 },
                P8 = new List<Tuple<string, byte[], TS3>> {
                        new Tuple<string, byte[], TS3>("tt1",new byte[] { 1,2,3},new TS3 { P1 = "z1" }),
                        new Tuple<string, byte[], TS3>("tt2",new byte[] { 3,2,3},new TS3 { P1 = "z2" }),
                        new Tuple<string, byte[], TS3>("tt3",new byte[] { 4,2,3},new TS3 { P1 = "z3" }),
                    },
                P9 = new Tuple<float, TS2, TS3, decimal?>(-.8f, new TS2 { P2 = 45 }, new TS3 { P2 = 12 }, -58.8m)
            };
            
            var bt1 = new Biser.Encoder(ts1).Encode();
            
            var ts1a = TS1.BiserDecode(bt1);

            var jsonSet = new JsonSettings() { DateFormat = JsonSettings.DateTimeStyle.ISO };
            var str = new JsonEncoder(ts1, jsonSet).GetJSON();
            var ts1b = TS1.BiserJsonDecode(str, settings: jsonSet);
        }

        static void TestJSONv1()
        {
            t1 ot1 = new t1()
            {
                p1 = 12,
                p2 = "dsfg",
                p3 = new t2 { p1 = DateTime.UtcNow, p2 = "uioziuz" },
                p4=new List<t2> {
                    new t2 { p1 = DateTime.UtcNow.AddDays(12), p2 ="k1" },
                    new t2 { p1 = DateTime.UtcNow.AddDays(7), p2 ="k2" }
                }
            };

            var jsonSet = new Biser.JsonSettings { DateFormat = Biser.JsonSettings.DateTimeStyle.ISO };
            Biser.JsonEncoder enc = new Biser.JsonEncoder(ot1, jsonSet);
            string es = enc.GetJSON(Biser.JsonSettings.JsonStringStyle.Prettify);
            var ot2 = t1.BiserJsonDecode(es, settings: jsonSet);
        }
        #endregion

        static void TestBE1()
        {            
            //Testing slower biser extensions
            var ttz= ((int)15).BiserEncode();
            var btx = (new HashSet<TS5> { new TS5 { TermId = 15 }, new TS5 { TermId = 16 }, new TS5 { TermId = 17 } }).BiserEncodeList();
            var ttzD = ttz.BiserDecode<int>();
            var btxD = btx.BiserDecodeHashSet<TS5>();
            
        }


        static void TestListDictionary()
        {
            //Encoding
            Biser.Encoder enc = new Biser.Encoder();
            enc.Add((int)123);            
            enc.Add(new List<string> { "Hi", "there" }, r => { enc.Add(r); });
            enc.Add((float)-458.4f);

            enc.Add(new Dictionary<uint, string> { { 1, "Well" }, { 2, "done" } }
           , r => { enc.Add(r.Key); enc.Add(r.Value); });

            enc.Add((decimal)-587.7m);

            //TS4 implements IEncoder
            enc.Add(new Dictionary<uint, TS4> { { 1, new TS4 { TermId = 1 } }, { 2, new TS4 { TermId = 5 } } }
            , r => { enc.Add(r.Key); enc.Add(r.Value); });

            enc.Add(new TS4 { TermId = 188 });

            //Decoding

            var decoder = new Biser.Decoder(enc.Encode());

            Console.WriteLine(decoder.GetInt());


            //////Alternative to the following instruction. Slower than supplying List directly
            ////foreach (var item in decoder.GetCollection().Select(r => r.GetString()))
            ////{
            ////    Console.WriteLine(item);
            ////}

            List<string> lst = decoder.CheckNull() ? null : new List<string>();      
            if (lst != null)
            {
                decoder.GetCollection(() => { return decoder.GetString(); }, lst, true);
                foreach (var item in lst)
                    Console.WriteLine(item);
            }

            Console.WriteLine(decoder.GetFloat());


            ////////Alternative to the following instruction. Slower than supplying Dictionary directly
            //////foreach (var item in decoder.GetCollection())
            //////{
            //////    Console.WriteLine($"K: {item.GetUInt()}; V: {item.GetString()}");
            //////}

            Dictionary<uint, string> d1 = decoder.CheckNull() ? null : new Dictionary<uint, string>();
            if (d1 != null)
            {
                decoder.GetCollection(
                    () => { return decoder.GetUInt(); },
                    () => { return decoder.GetString(); },
                    d1, true);
                foreach (var item in d1)
                    Console.WriteLine(item.Key + "; " + item.Value);
            }

            Console.WriteLine(decoder.GetDecimal());

            Dictionary<uint, TS4> d2 = decoder.CheckNull() ? null : new Dictionary<uint, TS4>();
            if (d2 != null)
            {
                decoder.GetCollection(
                    () => { return decoder.GetUInt(); },
                    () => { return TS4.BiserDecode(extDecoder: decoder); },
                    d2, true);
                foreach (var item in d2)
                    Console.WriteLine(item.Key + "; " + item.Value.TermId);
            }

            Console.WriteLine(TS4.BiserDecode(extDecoder: decoder).TermId);
        }
        

        static void TestT5()
        {
            //Testing extensions with IDecoder and Biser.Extension interface

            TS5 voc = new TS5()
            {
                TermId = 12,
                VoteType = TS5.eVoteType.VoteReject
            };
           
            var lst = new List<TS5> { voc, voc, voc };
            var btEn = lst.BiserEncodeList();           
            var lst1 = btEn.BiserDecodeList<TS5>();
            
        }

        static void TestT4()
        {
            TS4 voc = new TS4()
            {
                TermId = 12,
                VoteType = TS4.eVoteType.VoteReject
            };

            Biser.Encoder enc = new Biser.Encoder()
                .Add(voc);

            var voc1 = TS4.BiserDecode(enc.Encode());
        }


        static void TestPrimitives()
        {
            Biser.Encoder en = new Biser.Encoder();
            DateTime dt = DateTime.UtcNow;
            decimal d1 = 548.45m;
            string s1 = "well done";
            bool? b1 = null;
            double o1 = 45.7887;

            //Adding one by one to encoder
            en.Add(dt);
            en.Add(d1);
            en.Add(s1);
            en.Add(b1);
            en.Add(o1);

            byte[] btEnc = en.Encode(); //Getting serialized value

            //Decoding
            Biser.Decoder dec = new Biser.Decoder(btEnc); //decoder is based on encoded byte[]

            //Decoding one by one in the same sequence
            dt = dec.GetDateTime();
            d1 = dec.GetDecimal();
            s1 = dec.GetString();
            b1 = dec.GetBool_NULL();
            o1 = dec.GetDouble();
            
        }

        static void TestCustom()
        {
            Biser.Encoder en = new Biser.Encoder();
            List<int> l1 = new List<int> { 1, 2, 3 };
            List<TS2> l2 = new List<TS2> { new TS2 { P1 = 15 } , new TS2 { P1 = 16 }, new TS2 { P1 = 17 } };
            DateTime dt = DateTime.UtcNow;
            decimal d1 = 548.45m;
            string s1 = "well done";
            bool? b1 = null;
            double o1 = 45.7887;

            //It is possible to add IList directly, but we test custom serialization
            
            //Skipping saving NULLS
            //Addin length of the list l1
            en.Add(l1.Count);
            //Adding items one by one
            foreach (var item in l1)
                en.Add(item);
            //Addin length of the list l2
            en.Add(l2.Count);
            //Adding items one by one
            foreach (var item in l2)
                en.Add(item); //item is IEncoder so can be easily added 
            //adding other elements
            en.Add(dt);
            en.Add(d1);
            en.Add(s1);
            en.Add(b1);
            en.Add(o1);

            byte[] btEnc = en.Encode(); //Getting serialized value
            
            //Decoding
            Biser.Decoder dec = new Biser.Decoder(btEnc);

            //No null checks for lists (we didn't save them)
            //Getting list l1
            l1 = new List<int>();

            var cnt = dec.GetInt(); //getting count of items - !!!Note!!! don'T put it into for-loop

            for (int i = 0; i < cnt; i++) //reading count of elements
                l1.Add(dec.GetInt()); //getting items one by one
            //Getting list l2
            l2 = new List<TS2>();
            cnt = dec.GetInt(); //getting count of items
            for (int i = 0; i < cnt; i++) 
                //getting items one by one, supplying existing decoder 
                //to unroll TS2 element
                l2.Add(TS2.BiserDecode(extDecoder: dec));

            //Getting other elements
            dt = dec.GetDateTime();
            d1 = dec.GetDecimal();
            s1 = dec.GetString();
            b1 = dec.GetBool_NULL();
            o1 = dec.GetDouble();

            //done
        }


        static void TestMultiDimensionArray()
        {
          

            Biser.Encoder en = new Biser.Encoder();
            int[,,] ar3 = new int[,,]
            {
                {
                    { 1, 2, 3 },
                    { 4, 5, 6 }
                },
                {
                    { 7, 8, 9 },
                    { 10, 11, 12 }
                }
            };
            //ar3 = null; //can be null
            //ar3[0,1,2] = 6
            //ar3[1,1,1] = 11
            //ar3.Rank = 3
            //ar3.Length = 12
            //ar3.GetLength(0) = 2
            //ar3.GetLength(1) = 2
            //ar3.GetLength(3) = 3
            //var x = ar3.Length;

            
            if (ar3 == null)
                en.Add((byte)1); //Saving isNULL
            else
            {
                en.Add((byte)0); //not null
                ////Saving array rank (not necessary when we know it)
                //en.Add(ar3.Rank);
                //Saving array dimension length  (not necessary when we know it)
                for (int i = 0; i < ar3.Rank; i++)
                    en.Add(ar3.GetLength(i));
                //Saving array values
                foreach (var el in ar3)
                    en.Add(el);
            }

            byte[] btEnc = en.Encode(); //Getting serialized value

            //Restoring values
            int[,,] ar3clone = null;

            Biser.Decoder dec = new Biser.Decoder(btEnc);
            //Checking on null
            if (!dec.CheckNull())
            {

                ar3clone = new int[dec.GetInt(), dec.GetInt(), dec.GetInt()];

                for (int x = 0; x < ar3clone.GetLength(0); x++)
                    for (int y = 0; y < ar3clone.GetLength(1); y++)
                        for (int z = 0; z < ar3clone.GetLength(2); z++)
                            ar3clone[x, y, z] = dec.GetInt();

            }
        }

        
    }
}
