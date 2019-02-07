using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiserObjectify
{
    internal static class StandardTypes
    {

        public static Dictionary<Type, TypeFormer> STypes = new Dictionary<Type, TypeFormer>();
        internal static void InitDict()
        {
            if (STypes.Count > 0)
                return;

            STypes.Add(typeof(byte[]), new TypeFormer { FGet = "decoder.GetByteArray()", DefaultValue="null" });
            STypes.Add(typeof(int), new TypeFormer { FGet = "decoder.GetInt()", DefaultValue = "0" });
            STypes.Add(typeof(int?), new TypeFormer { FGet = "decoder.GetInt_NULL()", DefaultValue = "null" });
            STypes.Add(typeof(uint), new TypeFormer { FGet = "decoder.GetUInt()", DefaultValue = "0" });
            STypes.Add(typeof(uint?), new TypeFormer { FGet = "decoder.GetUInt_NULL()", DefaultValue = "null" });
            STypes.Add(typeof(long), new TypeFormer { FGet= "decoder.GetLong()", DefaultValue = "0" });
            STypes.Add(typeof(long?), new TypeFormer { FGet = "decoder.GetLong_NULL()", DefaultValue = "null" });
            STypes.Add(typeof(ulong), new TypeFormer { FGet = "decoder.GetULong()", DefaultValue = "0" });
            STypes.Add(typeof(ulong?), new TypeFormer { FGet = "decoder.GetULong_NULL()", DefaultValue = "null" });
            STypes.Add(typeof(short), new TypeFormer { FGet = "decoder.GetShort()", DefaultValue = "0" });
            STypes.Add(typeof(short?), new TypeFormer { FGet = "decoder.GetShort_NULL()", DefaultValue = "null" });
            STypes.Add(typeof(ushort), new TypeFormer { FGet = "decoder.GetUShort()", DefaultValue = "0" });
            STypes.Add(typeof(ushort?), new TypeFormer { FGet = "decoder.GetUShort_NULL()", DefaultValue = "null" });
            STypes.Add(typeof(byte), new TypeFormer { FGet = "decoder.GetByte()", DefaultValue = "0" });
            STypes.Add(typeof(byte?), new TypeFormer { FGet = "decoder.GetByte_NULL()", DefaultValue = "null" });
            STypes.Add(typeof(sbyte), new TypeFormer { FGet = "decoder.GetSByte()", DefaultValue = "0" });
            STypes.Add(typeof(sbyte?), new TypeFormer { FGet = "decoder.GetSByte_NULL()", DefaultValue = "null" });
            STypes.Add(typeof(DateTime), new TypeFormer { FGet = "decoder.GetDateTime()", DefaultValue = "default(DateTime)" });
            STypes.Add(typeof(DateTime?), new TypeFormer { FGet = "decoder.GetDateTime_NULL()", DefaultValue = "null" });
            STypes.Add(typeof(double), new TypeFormer { FGet = "decoder.GetDouble()", DefaultValue = "0" });
            STypes.Add(typeof(double?), new TypeFormer { FGet = "decoder.GetDouble_NULL()", DefaultValue = "null" });
            STypes.Add(typeof(float), new TypeFormer { FGet = "decoder.GetFloat()", DefaultValue = "0" });
            STypes.Add(typeof(float?), new TypeFormer { FGet = "decoder.GetFloat_NULL()", DefaultValue = "null" });
            STypes.Add(typeof(decimal), new TypeFormer { FGet = "decoder.GetDecimal()", DefaultValue = "0" });
            STypes.Add(typeof(decimal?), new TypeFormer { FGet = "decoder.GetDecimal_NULL()", DefaultValue = "null" });
            STypes.Add(typeof(string), new TypeFormer { FGet = "decoder.GetString()", DefaultValue = "null" });
            STypes.Add(typeof(bool), new TypeFormer { FGet = "decoder.GetBool()", DefaultValue = "false" });
            STypes.Add(typeof(bool?), new TypeFormer { FGet = "decoder.GetBool_NULL()", DefaultValue = "null" });
            //STypes.Add(typeof(object), new TypeFormer { FGet = "decoder.()" }); //????
            STypes.Add(typeof(char), new TypeFormer { FGet = "decoder.GetChar()", DefaultValue = "'\0'" });
            STypes.Add(typeof(char?), new TypeFormer { FGet = "decoder.GetChar_NULL()", DefaultValue = "null" });
            STypes.Add(typeof(Guid), new TypeFormer { FGet = "decoder.GetGuid()", DefaultValue = "default(Guid)" });
            STypes.Add(typeof(Guid?), new TypeFormer { FGet = "decoder.GetGuid_NULL()", DefaultValue = "null" });

        }

        public class TypeFormer
        {
            public string FGet { get; set; } = "";
            public string DefaultValue { get; set; } = "";

        }

        public static string GetDefaultValue(Type type)
        {
            if (STypes.TryGetValue(type, out var tf))
                return tf.DefaultValue;
            if (type.GetInterface("ICollection`1") != null)
                return "null";
            return null;
        }

        //public static string GetCSharpArrayName(Type type)
        //{
        //    int io = type.FullName.IndexOf("[");
        //    if(io>0)
        //        return type.FullName.Substring(0, io);
        //    return type.FullName;
        //}

        //public static string GetFriendlyName(Type type)
        //{
        //    return GetFriendlyName(type);

        //    int idx = Int32.MaxValue;
        //    int io = type.FullName.IndexOf("`");
        //    if (io > 0 && idx > io)
        //        idx = io;
        //    io = type.FullName.IndexOf("[");
        //    if (io > 0 && idx > io)
        //        idx = io;
        //    //var rio = type.FullName.IndexOf("?"); //-1

        //    if (type.IsArray)
        //    {
        //        //return type.FullName.Substring(0, idx);
        //        return type.FullName;
        //    }
        //    else if (idx == Int32.MaxValue)
        //        return type.FullName;
        //    else
        //        return type.FullName.Substring(0, idx);
        //}


        //    private static readonly Dictionary<Type, string> _typeToFriendlyName = new Dictionary<Type, string>
        //{
        //    { typeof(string), "string" },
        //    { typeof(object), "object" },
        //    { typeof(bool), "bool" },
        //    { typeof(byte), "byte" },
        //    { typeof(char), "char" },
        //    { typeof(decimal), "decimal" },
        //    { typeof(double), "double" },
        //    { typeof(short), "short" },
        //    { typeof(int), "int" },
        //    { typeof(long), "long" },
        //    { typeof(sbyte), "sbyte" },
        //    { typeof(float), "float" },
        //    { typeof(ushort), "ushort" },
        //    { typeof(uint), "uint" },
        //    { typeof(ulong), "ulong" },
        //    { typeof(void), "void" }
        //};

        //public static string GetFriendlyName(Type type)
        public static string GetFriendlyName(Type type)
        {            
            string friendlyName;
            //if (_typeToFriendlyName.TryGetValue(type, out friendlyName))
            //{
            //    return friendlyName;
            //}

            //friendlyName = type.Name;
            friendlyName = type.FullName;
            if (type.IsGenericType)
            {
                int backtick = friendlyName.IndexOf('`');
                if (backtick > 0)
                {
                    friendlyName = friendlyName.Remove(backtick);
                }
                friendlyName += "<";
                Type[] typeParameters = type.GetGenericArguments();
                for (int i = 0; i < typeParameters.Length; i++)
                {
                    string typeParamName = GetFriendlyName(typeParameters[i]);
                    if(typeParameters[i].IsArray)
                    {
                        //Reversing array
                        typeParamName = ReverseArrayDefinition(typeParamName);
                    }
                    friendlyName += (i == 0 ? typeParamName : ", " + typeParamName);
                }
                friendlyName += ">";
            }

            if (type.IsArray)
            {
                int iof = 0;
                StringBuilder revArr = new StringBuilder();
                StringBuilder revCut = new StringBuilder();
                for (int j = type.Name.Length - 1; j >= 0; j--)
                {
                    var l = type.Name[j];
                    if (l == '[' || l == ']' || l == ',')
                    {
                        iof++;
                        if (l == '[')
                            l = ']';
                        else if (l == ']')
                            l = '[';
                        revArr.Append(l);
                    }
                    else
                        break;
                }
             
                foreach (var el in revArr.ToString())
                {
                    revCut.Append(el);
                    if (el == ']')
                        break;
                }

                return GetFriendlyName(type.GetElementType()) + revCut.ToString();               
            }

            return friendlyName;
        }

        static string ReverseArrayDefinition(string typeName)
        {
            int iof = 0;
            StringBuilder revArr = new StringBuilder();
            for (int j = typeName.Length - 1; j >= 0; j--)
            {
                var l = typeName[j];
                if (l == '[' || l == ']' || l == ',')
                {
                    iof++;
                    if (l == '[')
                        l = ']';
                    else if (l == ']')
                        l = '[';
                    revArr.Append(l);
                }
                else
                    break;
            }
            return typeName.Substring(0, typeName.Length - iof) + revArr.ToString();
        }

    }
}
