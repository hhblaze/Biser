/* 
  Copyright (C) 2012 tiesky.com / Alex Solovyov
  It's a free software for those, who think that it should be free.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Biser
{

    public static class BiserExtensions
    {
       

        public static byte[] BiserEncodeList<T>(this IEnumerable<T> objs)
        {               
            Tuple<Action<Encoder, object>, Func<Decoder, object>> f = GetTypeOfCollection<T>();         
            var enc = new Encoder();
            enc.Add(objs, r => { f.Item1(enc, r); });
            return enc.Encode();            
        }


        public static byte[] BiserEncode<T>(this T obj)
        {
            Tuple<Action<Encoder, object>, Func<Decoder, object>> f = GetTypeOfCollection<T>();
            var enc = new Encoder();
            f.Item1(enc, obj);
            return enc.Encode();
        }


        /// <summary>
        /// Creates new decoder. 
        /// <para>Decoding type must be either .NET primitive or to implement IDecoder</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static T BiserDecode<T>(this byte[] enc)
        {
            var decoder = new Decoder(enc);
            return decoder.BiserDecode<T>();            
        }

        /// <summary>
        /// Re-uses existing decoder.
        /// <para>Decoding type must be either .NET primitive or to implement IDecoder</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="decoder"></param>
        /// <returns></returns>
        public static T BiserDecode<T>(this Decoder decoder)
        {
            Tuple<Action<Encoder, object>, Func<Decoder, object>> f = GetTypeOfCollection<T>();
            return (T)f.Item2(decoder);            
        }

        public static List<T> BiserDecodeList<T>(this byte[] enc)
        { 
            if (enc == null)
                return null;
            
            var t1 = (List<T>)GetInstanceCreator(typeof(List<T>))();
            var decoder = new Decoder(enc);
            Tuple<Action<Encoder, object>, Func<Decoder, object>> f = GetTypeOfCollection<T>();
            decoder.GetCollection(() => { return (T)f.Item2(decoder); }, t1, false);
            return t1;
        }

        public static HashSet<T> BiserDecodeHashSet<T>(this byte[] enc)
        {
            if (enc == null)
                return null;

            var t1 = (HashSet<T>)GetInstanceCreator(typeof(HashSet<T>))();
            var decoder = new Decoder(enc);
            Tuple<Action<Encoder, object>, Func<Decoder, object>> f = GetTypeOfCollection<T>();
            decoder.GetCollection(() => { return (T)f.Item2(decoder); }, t1, false);
            return t1;            
        }


        #region "Technical block"
        

        static Dictionary<Type, Tuple<Action<Encoder, object>, Func<Decoder, object>>> dCoders = new Dictionary<Type, Tuple<Action<Encoder, object>, Func<Decoder, object>>>();
        static object lock_dCoders = new object();
        static Type TIDecoder = typeof(IDecoder);
        static Type TIEncoder = typeof(IEncoder);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        static Tuple<Action<Encoder, object>, Func<Decoder, object>> GetTypeOfCollection<T>()
        {
            Type t = typeof(T);
            Tuple<Action<Encoder, object>, Func<Decoder, object>> f = null;

            if (dCoders.TryGetValue(t, out f))
                return f;

            if (TIEncoder.IsAssignableFrom(t))
            {
                f = new Tuple<Action<Encoder, object>, Func<Decoder, object>>(
                    (e, o) =>
                    {
                        e.Add((IEncoder)o);
                    },
                    (d) => {
                        if (TIDecoder.IsAssignableFrom(t))
                            return (T)(((IDecoder)GetInstanceCreator(typeof(T))()).BiserDecodeToObject(d));
                        else
                            throw new Exception($"Biser: type {t.ToString()} doesn't implement IDecoder");
                    });

                lock (lock_dCoders)
                    dCoders[t] = f;
                return f;
            }

            FillDecoder();

            if (dCoders.TryGetValue(t, out f))
                return f;

            throw new Exception($"Biser: type {t.ToString()} doesn't implement IEncoder");


        }


        static void FillDecoder()
        {           
            lock (lock_dCoders)
            {
                if (dCoders.ContainsKey(typeof(long)))
                    return;
                
                dCoders[typeof(long)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((long)o); }, (d) => { return d.GetLong(); });
                dCoders[typeof(long?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((long?)o); }, (d) => { return d.GetLong_NULL(); });
                dCoders[typeof(int)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((int)o); }, (d) => { return d.GetInt(); });
                dCoders[typeof(int?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((int?)o); }, (d) => { return d.GetInt_NULL(); });
                dCoders[typeof(ulong)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((ulong)o); }, (d) => { return d.GetULong(); });
                dCoders[typeof(ulong?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((ulong?)o); }, (d) => { return d.GetULong_NULL(); });
                dCoders[typeof(uint)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((uint)o); }, (d) => { return d.GetUInt(); });
                dCoders[typeof(uint?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((uint?)o); }, (d) => { return d.GetUInt_NULL(); });
                dCoders[typeof(short)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((short)o); }, (d) => { return d.GetShort(); });
                dCoders[typeof(short?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((short?)o); }, (d) => { return d.GetShort_NULL(); });
                dCoders[typeof(ushort)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((ushort)o); }, (d) => { return d.GetUShort(); });
                dCoders[typeof(ushort?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((ushort?)o); }, (d) => { return d.GetUShort_NULL(); });
                dCoders[typeof(byte)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((byte)o); }, (d) => { return d.GetByte(); });
                dCoders[typeof(byte?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((byte?)o); }, (d) => { return d.GetByte_NULL(); });
                dCoders[typeof(float)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((float)o); }, (d) => { return d.GetFloat(); });
                dCoders[typeof(float?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((float?)o); }, (d) => { return d.GetFloat_NULL(); });
                dCoders[typeof(double)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((double)o); }, (d) => { return d.GetDouble(); });
                dCoders[typeof(double?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((double?)o); }, (d) => { return d.GetDouble_NULL(); });
                dCoders[typeof(decimal)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((decimal)o); }, (d) => { return d.GetDecimal(); });
                dCoders[typeof(decimal?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((decimal?)o); }, (d) => { return d.GetDecimal_NULL(); });
                dCoders[typeof(DateTime)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((DateTime)o); }, (d) => { return d.GetDateTime(); });
                dCoders[typeof(DateTime?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((DateTime?)o); }, (d) => { return d.GetDateTime_NULL(); });
                dCoders[typeof(sbyte)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((sbyte)o); }, (d) => { return d.GetSByte(); });
                dCoders[typeof(sbyte?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((sbyte?)o); }, (d) => { return d.GetSByte_NULL(); });
                dCoders[typeof(bool)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((bool)o); }, (d) => { return d.GetBool(); });
                dCoders[typeof(bool?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((bool?)o); }, (d) => { return d.GetBool_NULL(); });
                dCoders[typeof(string)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((string)o); }, (d) => { return d.GetString(); });
                dCoders[typeof(byte[])] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((byte[])o); }, (d) => { return d.GetByteArray(); });
                dCoders[typeof(char)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((char)o); }, (d) => { return d.GetChar(); });
                dCoders[typeof(char?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((char?)o); }, (d) => { return d.GetChar_NULL(); });
                dCoders[typeof(Guid)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((Guid)o); }, (d) => { return d.GetGuid(); });
                dCoders[typeof(Guid?)] = new Tuple<Action<Encoder, object>, Func<Decoder, object>>((e, o) => { e.Add((Guid?)o); }, (d) => { return d.GetGuid_NULL(); });
            }
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

        #endregion
    }
}
