/* 
  Copyright (C) 2012 tiesky.com / Alex Solovyov
  It's a free software for those, who think that it should be free.
*/

using System;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Biser
{
    /// <summary>
    /// Binary serializer "biser", that can be translated as "beads" from Russian
    /// </summary>
    public static class Biser
    {
        public static long EncodeZigZag(long value, int bitLength)
        {
            return (value << 1) ^ (value >> (bitLength - 1));
        }

        public static long DecodeZigZag(ulong value)
        {
            if ((value & 0x1) == 0x1)
                return (-1 * ((long)(value >> 1) + 1));

            return (long)(value >> 1);
        }

        public static string UTF8_GetString(this byte[] btText)
        {
            return btText == null ? null : System.Text.Encoding.UTF8.GetString(btText, 0, btText.Length);
        }

        public static byte[] To_UTF8Bytes(this string text)
        {
            return System.Text.Encoding.UTF8.GetBytes(text);
        }

        public static byte[] GetVarintBytes(ulong value)
        {
            var buffer = new byte[10];
            var pos = 0;
            byte byteVal;
            do
            {
                byteVal = (byte)(value & 0x7f);
                value >>= 7;

                if (value != 0)
                {
                    byteVal |= 0x80;
                }

                buffer[pos++] = byteVal;

            } while (value != 0);

            var result = new byte[pos];
            Buffer.BlockCopy(buffer, 0, result, 0, pos);

            return result;
        }



        #region "testing extensions"
        public static byte[] SerializeBiser(this IEncoder obj)
        {
            return new Encoder().Add(obj).Encode();
        }

        public static byte[] SerializeBiserList(this IEnumerable<IEncoder> objs)
        {
            //Check DeserializeBiserList

            var en = new Encoder();
            en.Add(objs, r => { en.Add(r); });
            return en.Encode();
        }
        
        public static void DeserializeBiserList<T>(byte[] enc, IList<T> lst)
        {
            //Emulates extension for fast encoding/decoding list
            //Of course, not so efficeint, because of GetInstanceCreator and a serie of casts

            /*
              TS5 voc = new TS5()
            {
                TermId = 12,
                VoteType = TS5.eVoteType.VoteReject
            };
           
            var lst = new List<TS5> { voc, voc, voc };
            var bt1= Biser.Biser.SerializeBiserList(lst);

            var lst1 = new List<TS5>();
            Biser.Biser.DeserializeBiserList(bt1, lst1);             
             */

            var decoder = new Decoder(enc);
            var o = GetInstanceCreator(typeof(T))();
            decoder.GetCollection(() => { return (T)(((IDecoder)o).BiserDecoderV1(decoder)); }, lst, false);
        }

        /// <summary>
        /// Creates an instance type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Func<object> GetInstanceCreator(Type type)
        {
            var constructorCallExpression = Expression.New(type);
            var constructorCallingLambda = Expression
                .Lambda<Func<object>>(constructorCallExpression)
                .Compile();
            return constructorCallingLambda;
        }
        #endregion
    }
}
