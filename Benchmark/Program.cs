using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Benchmark.Objects;

namespace Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            t1();
            /*
           Start
                  Protobuf obj length: 22
                  Biser obj length: 17
                  Protobuf encode: 891 ms
                  Protobuf decode: 1242 ms
                  Biser encode: 523 ms
                  Biser decode: 410 ms
           Press any key
           */


            t2();
            /*
           Start
               Protobuf obj length: 28
               Biser obj length: 20
               Protobuf encode: 1377 ms
               Protobuf decode: 1613 ms
               Biser encode: 588 ms
               Biser decode: 577 ms
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
                    Biser obj length: 17
                    Protobuf encode: 891 ms
                    Protobuf decode: 1242 ms
                    Biser encode: 523 ms
                    Biser decode: 410 ms
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

            //Protobuf. Warming up, getting length
            var pBt = obj.SerializeProtobuf();
            Console.WriteLine($"Protobuf obj length: {pBt.Length}");
            var pObj = pBt.DeserializeProtobuf<StateLogEntry>();

            //Biser. Getting length
            var bBt = new Biser.Encoder().Add(obj).Encode();
            Console.WriteLine($"Biser obj length: {bBt.Length}");            
            var bObj = StateLogEntry.BiserDecode(bBt);

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
            Console.WriteLine($"Biser encode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                tobj = StateLogEntry.BiserDecode(bBt);
            }
            sw.Stop();
            Console.WriteLine($"Biser decode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

        }

        static void t2()
        {
            /*
            Start
                Protobuf obj length: 28
                Biser obj length: 20
                Protobuf encode: 1377 ms
                Protobuf decode: 1613 ms
                Biser encode: 588 ms
                Biser decode: 577 ms
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

            //Protobuf. Warming up, getting length
            var pBt = obj.SerializeProtobuf();
            Console.WriteLine($"Protobuf obj length: {pBt.Length}");
            var pObj = pBt.DeserializeProtobuf<StateLogEntrySuggestion>();

            //Biser. Getting length
            var bBt = new Biser.Encoder().Add(obj).Encode();
            Console.WriteLine($"Biser obj length: {bBt.Length}");
            var bObj = StateLogEntry.BiserDecode(bBt);

            
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
            Console.WriteLine($"Biser encode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                tobj = StateLogEntrySuggestion.BiserDecode(bBt);
            }
            sw.Stop();
            Console.WriteLine($"Biser decode: {sw.ElapsedMilliseconds} ms");
            sw.Reset();

        }
    }
}
