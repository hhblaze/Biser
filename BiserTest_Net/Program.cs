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
            TS2 ts2 = new TS2()
            {
                P1 = long.MinValue,
                P2 = 4587.4564
            };
            //var bt2 = ts2.BiserEncoder().Encode();
            //TS2 ts2D = TS2.BiserDecode(bt2);

            TS3 ts3 = new TS3()
            {
                P1 = "welldone",
                P2 = null,
                P3 = DateTime.UtcNow
            };

            //var bt3 = ts3.BiserEncoder().Encode();
            //TS3 ts2D = TS3.BiserDecode(bt3);



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
                }
            };
                       


            var bt1 = ts1.BiserEncoder().Encode();
            TS1 ts1D = TS1.BiserDecode(bt1);
            

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
    }
}
