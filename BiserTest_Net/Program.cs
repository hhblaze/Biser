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
    class Program
    {
        static void Main(string[] args)
        {
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
            //TestT5();
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

        }



        static void TestT5()
        {
            //Testing extensions with IDecoder interface

            TS5 voc = new TS5()
            {
                TermId = 12,
                VoteType = TS5.eVoteType.VoteReject
            };
           
            var lst = new List<TS5> { voc, voc, voc };
            var bt1= Biser.Biser.SerializeBiserList(lst);

            var lst1 = new List<TS5>();
            Biser.Biser.DeserializeBiserList(bt1, lst1);
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
