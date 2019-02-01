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

        public static string GetCSharpTypeName(Type type)
        {
            int idx = Int32.MaxValue;
            int io = type.FullName.IndexOf("`");
            if (io > 0 && idx > io)
                idx = io;
            io = type.FullName.IndexOf("[");
            if (io > 0 && idx > io)
                idx = io;
            //var rio = type.FullName.IndexOf("?"); //-1

            if (type.IsArray)
                return type.FullName;
            else if (idx == Int32.MaxValue)
                return type.FullName;
            else
                return type.FullName.Substring(0, idx);
        }
    }
}
