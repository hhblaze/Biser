using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Biser
{
    public static class BiserExtensions
    {
        public static byte[] BiserEncode(this IEncoder obj)
        {
            return new Encoder().Add(obj).Encode();
        }

        public static byte[] BiserEncode(this IEnumerable<IEncoder> objs)
        {
            //Check BiserDecodeList

            var en = new Encoder();
            en.Add(objs, r => { en.Add(r); });
            return en.Encode();
        }

        public static List<T> BiserDecodeList<T>(this byte[] enc)
        {
            //Emulates extension for fast encoding/decoding list
            //Of course, NOT SO EFFICIENT as an explicit instance creation because of GetInstanceCreator and a serie of casts

            /*
             TS5 voc = new TS5()
            {
                TermId = 12,
                VoteType = TS5.eVoteType.VoteReject
            };
           
            var lst = new List<TS5> { voc, voc, voc };            
            var btEn = lst.BiserEncode();            
            var lst1 = btEn.BiserDecodeList<TS5>();  
            
             */

            if (enc == null)
                return null;

            var t1 = (List<T>)GetInstanceCreator(typeof(List<T>))();

            var decoder = new Decoder(enc);
            var o = GetInstanceCreator(typeof(T))();
            decoder.GetCollection(() => { return (T)(((IDecoder)o).BiserDecodeToObject(decoder)); }, t1, false);
            return t1;
            
        }
        

        /// <summary>
        /// Holder of compiled instance creators
        /// </summary>
        static Dictionary<Type, Func<object>> dInstanceCreator = new Dictionary<Type, Func<object>>();
        /// <summary>
        /// Returns an instance creator for the given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Func<object> GetInstanceCreator(Type type)
        {
            Func<object> f = null;
            if (dInstanceCreator.TryGetValue(type, out f))
                return f;
            var constructorCallExpression = Expression.New(type);
            var constructorCallingLambda = Expression
                .Lambda<Func<object>>(constructorCallExpression)
                .Compile();
            dInstanceCreator[type] = constructorCallingLambda;
            return constructorCallingLambda;
        }
    }
}
