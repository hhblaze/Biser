using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Benchmark.Objects;
using MessagePack;

namespace Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
             
            t1();

            /*
             Protobuf v.2.3.17
             NetJSON v.1.2.5
             Biser v.1.7
             MessagePack 1.7.3.4
             */

            /*
           Start
               Protobuf obj length: 22
                Biser Binary obj length: 17
                NetJson obj length: 129
                Biser Json obj length: 129
                Message Pack obj length: 20

                Protobuf encode: 1237 ms
                Protobuf decode: 1525 ms
                Biser Binary encode: 402 ms
                Biser Binary decode: 207 ms
                NetJson encode: 1314 ms
                NetJson decode: 1839 ms
                Biser Json encode: 2265 ms
                Biser Json decode: 3552 ms
                MessagePack encode: 223 ms
                MessagePack decode: 182 ms
           Press any key
           */


            t2();
            /*
           Start
                Protobuf obj length: 28
                Biser Binary obj length: 20
                NetJson obj length: 182
                Biser Json obj length: 182
                Message Pack obj length: 23

                Protobuf encode: 1418 ms
                Protobuf decode: 1810 ms
                Biser Binary encode: 473 ms
                Biser Binary decode: 269 ms
                NetJson encode: 1634 ms
                NetJson decode: 2353 ms
                Biser Json encode: 2821 ms
                Biser Json decode: 4717 ms
                MessagePack encode: 279 ms
                MessagePack decode: 241 ms
           Press any key
            */


            Console.WriteLine("Press any key");
            Console.ReadLine();
        }


        static void t1()
        {
            /*
           Start
                Protobuf obj length: 22
                Biser Binary obj length: 17
                NetJson obj length: 129
                Biser Json obj length: 129

                Protobuf encode: 1184 ms
                Protobuf decode: 1569 ms
                Biser Binary encode: 396 ms
                Biser Binary decode: 209 ms
                NetJson encode: 1350 ms
                NetJson decode: 1902 ms
                Biser Json encode: 2266 ms
                Biser Json decode: 3659 ms
           Press any key
           */

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            // It's an operational class from https://github.com/hhblaze/Raft.Net/blob/master/Raft/StateMachine/StateLogEntry.cs
            StateLogEntry obj = new StateLogEntry()
            {
                Data = new byte[] { 1, 2, 3, 4, 5 },
                Index = 458,
                IsCommitted = true,
                PreviousStateLogId = 4789,
                PreviousStateLogTerm = 447,
                RedirectId = 12,
                Term = 99
            };

            Console.WriteLine("t1----------------------------------");

            //Protobuf. Warming up, getting length
            var pBt = obj.SerializeProtobuf();
            Console.WriteLine($"Protobuf obj length: {pBt.Length}");
            var pObj = pBt.DeserializeProtobuf<StateLogEntry>();

            //Biser. Getting length
            var bBt = new Biser.Encoder().Add(obj).Encode();
            Console.WriteLine($"Biser Binary obj length: {bBt.Length}");            
            var bObj = StateLogEntry.BiserDecode(bBt);

            //NetJson. Getting length
            var njss = NetJSON.NetJSON.Serialize(obj);
            Console.WriteLine($"NetJson obj length: {System.Text.Encoding.UTF8.GetBytes(njss).Length}");
            var bnjss = NetJSON.NetJSON.Deserialize<StateLogEntry>(njss);

            //Biser Json. Getting length
            var bjss = new Biser.JsonEncoder(obj).GetJSON();
            Console.WriteLine($"Biser Json obj length: {System.Text.Encoding.UTF8.GetBytes(bjss).Length}");
            var bbjss = StateLogEntry.BiserJsonDecode(bjss);

            //Message Pack
            var mBt = MessagePackSerializer.Serialize(obj);
            Console.WriteLine($"Message Pack obj length: {mBt.Length}");
            var mc2 = MessagePackSerializer.Deserialize<StateLogEntry>(mBt);

            Console.WriteLine("");

            byte[] tbt = null;
            StateLogEntry tobj = null;

            sw.Start();
            for (int i=0;i<1000000;i++)
            {
                tbt = obj.SerializeProtobuf();
            }
            sw.Stop();
            Console.WriteLine($"Protobuf encode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                tobj = pBt.DeserializeProtobuf<StateLogEntry>();
            }
            sw.Stop();
            Console.WriteLine($"Protobuf decode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                tbt = new Biser.Encoder().Add(obj).Encode();
            }
            sw.Stop();
            Console.WriteLine($"Biser Binary encode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                tobj = StateLogEntry.BiserDecode(bBt);
            }
            sw.Stop();
            Console.WriteLine($"Biser Binary decode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                njss = NetJSON.NetJSON.Serialize(obj);
            }
            sw.Stop();
            Console.WriteLine($"NetJson encode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                bnjss = NetJSON.NetJSON.Deserialize<StateLogEntry>(njss);
            }
            sw.Stop();
            Console.WriteLine($"NetJson decode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();


            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                bjss = new Biser.JsonEncoder(obj).GetJSON();
            }
            sw.Stop();
            Console.WriteLine($"Biser Json encode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                bbjss = StateLogEntry.BiserJsonDecode(bjss);
            }
            sw.Stop();
            Console.WriteLine($"Biser Json decode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();


            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                mBt = MessagePackSerializer.Serialize(obj);
            }
            sw.Stop();
            Console.WriteLine($"MessagePack encode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                mc2 = MessagePackSerializer.Deserialize<StateLogEntry>(mBt);
            }
            sw.Stop();
            Console.WriteLine($"MessagePack decode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

        }

        static void t2()
        {
            /*
            Start
                Protobuf obj length: 28
                Biser Binary obj length: 20
                NetJson obj length: 182
                Biser Json obj length: 182

                Protobuf encode: 1367 ms
                Protobuf decode: 1909 ms
                Biser Binary encode: 464 ms
                Biser Binary decode: 271 ms
                NetJson encode: 1687 ms
                NetJson decode: 2383 ms
                Biser Json encode: 2871 ms
                Biser Json decode: 4748 ms
            Press any key
             */

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            // It's an operational class from https://github.com/hhblaze/Raft.Net/blob/master/Raft/StateMachine/StateLogEntry.cs
            StateLogEntry sle = new StateLogEntry()
            {
                Data = new byte[] { 1, 2, 3, 4, 5 },
                Index = 458,
                IsCommitted = true,
                PreviousStateLogId = 4789,
                PreviousStateLogTerm = 447,
                RedirectId = 12,
                Term = 99
            };

            // It's an operational class from https://github.com/hhblaze/Raft.Net/blob/master/Raft/Objects/StateLogEntrySuggestion.cs
            StateLogEntrySuggestion obj = new StateLogEntrySuggestion()
            {
                IsCommitted = true,
                LeaderTerm = 77,
                StateLogEntry = sle
            };

            Console.WriteLine("t2----------------------------------");

            //Protobuf. Warming up, getting length
            var pBt = obj.SerializeProtobuf();
            Console.WriteLine($"Protobuf obj length: {pBt.Length}");
            var pObj = pBt.DeserializeProtobuf<StateLogEntrySuggestion>();

            //Biser. Getting length
            var bBt = new Biser.Encoder().Add(obj).Encode();
            Console.WriteLine($"Biser Binary obj length: {bBt.Length}");
            var bObj = StateLogEntry.BiserDecode(bBt);

            //NetJson. Getting length
            var njss = NetJSON.NetJSON.Serialize(obj);
            Console.WriteLine($"NetJson obj length: {System.Text.Encoding.UTF8.GetBytes(njss).Length}");
            var bnjss = NetJSON.NetJSON.Deserialize<StateLogEntrySuggestion>(njss);

            //Biser Json. Getting length
            var bjss = new Biser.JsonEncoder(obj).GetJSON();
            Console.WriteLine($"Biser Json obj length: {System.Text.Encoding.UTF8.GetBytes(bjss).Length}");
            var bbjss = StateLogEntrySuggestion.BiserJsonDecode(bjss);

            //Message Pack
            var mBt = MessagePackSerializer.Serialize(obj);
            Console.WriteLine($"Message Pack obj length: {mBt.Length}");
            var mc2 = MessagePackSerializer.Deserialize<StateLogEntrySuggestion>(mBt);

            Console.WriteLine("");

            byte[] tbt = null;
            StateLogEntrySuggestion tobj = null;

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                tbt = obj.SerializeProtobuf();
            }
            sw.Stop();
            Console.WriteLine($"Protobuf encode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                tobj = pBt.DeserializeProtobuf<StateLogEntrySuggestion>();
            }
            sw.Stop();
            Console.WriteLine($"Protobuf decode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                tbt = new Biser.Encoder().Add(obj).Encode();
            }
            sw.Stop();
            Console.WriteLine($"Biser Binary encode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                tobj = StateLogEntrySuggestion.BiserDecode(bBt);
            }
            sw.Stop();
            Console.WriteLine($"Biser Binary decode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();


            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                njss = NetJSON.NetJSON.Serialize(obj);
            }
            sw.Stop();
            Console.WriteLine($"NetJson encode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                bnjss = NetJSON.NetJSON.Deserialize<StateLogEntrySuggestion>(njss);
            }
            sw.Stop();
            Console.WriteLine($"NetJson decode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();


            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                bjss = new Biser.JsonEncoder(obj).GetJSON();
            }
            sw.Stop();
            Console.WriteLine($"Biser Json encode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                bbjss = StateLogEntrySuggestion.BiserJsonDecode(bjss);
            }
            sw.Stop();
            Console.WriteLine($"Biser Json decode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                mBt = MessagePackSerializer.Serialize(obj);
            }
            sw.Stop();
            Console.WriteLine($"MessagePack encode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                mc2 = MessagePackSerializer.Deserialize<StateLogEntrySuggestion>(mBt);
            }
            sw.Stop();
            Console.WriteLine($"MessagePack decode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

        }
    }
}
