using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biser
{
    public class JsonEncoder
    {
        JsonSettings jsonSettings = null;

        StringBuilder sb = null;
        string finished = null;
        char lastchar = '{';

        public JsonEncoder(JsonSettings settings=null)
        {
            jsonSettings = (settings == null) ? new JsonSettings() : settings;

            sb = new StringBuilder();
            sb.Append("{"); //Always start as an object
        }


        void AddProp(string str)
        {
            if(lastchar != '{' && lastchar != '[' && lastchar != ',')
                sb.Append(",");

            AddStr(str);
            sb.Append(":");
            lastchar = ':';
        }

        void AddStr(string str)
        {
            sb.Append("\"");
            foreach (var ch in str)
            {
                if (ch == '\"')
                    sb.Append('\\');
                sb.Append(ch);
            }

            sb.Append("\"");
        }

        void AddNull()
        {            
            sb.Append("null");
        }

        public string GetJSON()
        {
            if (finished == null)
            {
                sb.Append("}");
                finished = sb.ToString();
            }

            if(this.jsonSettings.JsonStringFormat == JsonSettings.JsonStringStyle.Prettify)
            {
                return Prettify();
            }
            else
                return finished;
                
        }

        string prettified = null;
        string Prettify()
        {
            if (prettified != null)
                return prettified;

            StringBuilder sbp = new StringBuilder();
            int tabs = 0;           
            bool instr = false;
            char prevchar = ',';
            bool toDrawTabs = false;
            bool toDrawCL = false;
            foreach (var el in finished)
            {                
                if(!instr && el == '\"')
                    instr = true;
                else if (instr && el == '\"' && prevchar != '\\')
                    instr = false;

                if (!instr && (el == ' ' || el == '\t' || el == '\r' || el == '\n'))
                    continue;

               
                if (!instr && el == ',')
                {
                    sbp.Append(el);                    
                    toDrawCL = true;
                    toDrawTabs = true;                   
                }
                else if (!instr && (el == '[' || el == '{'))
                {
                    if (toDrawCL)
                    {
                        sbp.Append('\n');
                        toDrawCL = false;
                    }

                    if (toDrawTabs)
                    {
                        DrawTabs(tabs, sbp);
                        toDrawTabs = false;
                    }

                    if (prevchar != ',')
                    {
                        sbp.Append('\n');
                        DrawTabs(tabs, sbp);
                    }
                 
                    sbp.Append(el);
                    tabs++;
                    
                    toDrawCL = true;
                    toDrawTabs = true;
                }
                else if (!instr && (el == ']' || el == '}'))
                {
                    if (toDrawCL)
                    {
                        sbp.Append('\n');
                        toDrawCL = false;
                    }
                    if (toDrawTabs)
                    {
                        DrawTabs(tabs, sbp);
                        toDrawTabs = false;
                    }
                    if (prevchar != ',')                    
                        sbp.Append('\n');
                   
                    tabs--;
                    DrawTabs(tabs, sbp);
                    sbp.Append(el);
                    
                    toDrawCL = true;
                    toDrawTabs = true;
                    
                }
                else
                {
                    if (toDrawCL)
                    {
                        sbp.Append('\n');
                        toDrawCL = false;
                    }
                    if (toDrawTabs)
                    {
                        DrawTabs(tabs, sbp);
                        toDrawTabs = false;
                    }
                    sbp.Append(el);
                }

                prevchar = el;
            }
            prettified = sbp.ToString();
            return prettified;
        }

        void DrawTabs(int cnt, StringBuilder sbp)
        {
            if (cnt == 0)
                return;
            //sbp.Append('\n');
            for (int i = 0; i < cnt; i++)
                sbp.Append('\t');
        }

        public JsonEncoder Add(string propertyName, DateTime val)
        {
            if (!String.IsNullOrEmpty(propertyName))
                AddProp(propertyName);
            
            AppendDateTime(val);
            return this;
        }

        public JsonEncoder Add(string propertyName, DateTime? val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }

            AppendDateTime((DateTime)val);
            return this;
        }

        public JsonEncoder Add(DateTime val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(DateTime? val)
        {
            return Add(null, val);
        }

        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        void AppendDateTime(DateTime dt)
        {           
            switch (this.jsonSettings.DateFormat)
            {
                case JsonSettings.DateTimeStyle.Default:
                    sb.Append("\"");
                    sb.Append("\\/Date(");
                    if (dt.Kind == DateTimeKind.Utc)
                        sb.Append(((ulong)(dt.Subtract(epoch).TotalMilliseconds) * 10000).ToString());
                    else
                        sb.Append(((ulong)(dt.ToUniversalTime().Subtract(epoch).TotalMilliseconds) * 10000).ToString());
                    sb.Append(")\\/");
                    sb.Append("\"");
                    break;
                case JsonSettings.DateTimeStyle.EpochTime:
                    if (dt.Kind == DateTimeKind.Utc)
                        sb.Append(((ulong)(dt.Subtract(epoch).TotalMilliseconds) * 10000).ToString());
                    else
                        sb.Append(((ulong)(dt.ToUniversalTime().Subtract(epoch).TotalMilliseconds) * 10000).ToString());
                    break;
                case JsonSettings.DateTimeStyle.ISO:
                    sb.Append("\"");
                    sb.Append(dt.ToString("o"));
                    sb.Append("\"");
                    break;
                case JsonSettings.DateTimeStyle.Javascript:
                    sb.Append("\"");
                    if (dt.Kind == DateTimeKind.Utc)
                        sb.Append(dt.ToString("o"));
                    else
                        sb.Append(dt.ToUniversalTime().ToString("o"));
                    sb.Append("\"");
                    break;

            }
        }


        public JsonEncoder Add(string propertyName, int val)
        {
            if (!String.IsNullOrEmpty(propertyName))
                AddProp(propertyName);
            sb.Append(val);
            return this;
        }

        public JsonEncoder Add(string propertyName, int? val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }

            sb.Append(val);
            return this;
        }

        public JsonEncoder Add(int val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(int? val)
        {
            return Add(null, val);
        }


     
        public JsonEncoder Add(string propertyName, string val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }

            AddStr(val);            
            return this;
        }

        public JsonEncoder Add(string val)
        {
            return Add(null, val);
        }
        


        public JsonEncoder Add(string propertyName, long val)
        {
            if (!String.IsNullOrEmpty(propertyName))
                AddProp(propertyName);
            sb.Append(val);
            return this;
        }

        public JsonEncoder Add(string propertyName, long? val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }

            sb.Append(val);
            return this;
        }

        public JsonEncoder Add(long val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(long? val)
        {
            return Add(null, val);
        }


        public JsonEncoder Add(string propertyName, ulong val)
        {
            if (!String.IsNullOrEmpty(propertyName))
                AddProp(propertyName);
            sb.Append(val);
            return this;
        }

        public JsonEncoder Add(string propertyName, ulong? val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }

            sb.Append(val);
            return this;
        }

        public JsonEncoder Add(ulong val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(ulong? val)
        {
            return Add(null, val);
        }


        public JsonEncoder Add(string propertyName, uint val)
        {
            if (!String.IsNullOrEmpty(propertyName))
                AddProp(propertyName);
            sb.Append(val);
            return this;
        }

        public JsonEncoder Add(string propertyName, uint? val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }

            sb.Append(val);
            return this;
        }

        public JsonEncoder Add(uint val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(uint? val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(string propertyName, short val)
        {
            if (!String.IsNullOrEmpty(propertyName))
                AddProp(propertyName);
            sb.Append(val);
            return this;
        }

        public JsonEncoder Add(string propertyName, short? val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }

            sb.Append(val);
            return this;
        }

        public JsonEncoder Add(short val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(short? val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(string propertyName, ushort val)
        {
            if (!String.IsNullOrEmpty(propertyName))
                AddProp(propertyName);
            sb.Append(val);
            return this;
        }

        public JsonEncoder Add(string propertyName, ushort? val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }

            sb.Append(val);
            return this;
        }

        public JsonEncoder Add(ushort val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(ushort? val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(string propertyName, sbyte val)
        {
            if (!String.IsNullOrEmpty(propertyName))
                AddProp(propertyName);
            sb.Append(val);
            return this;
        }

        public JsonEncoder Add(string propertyName, sbyte? val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }

            sb.Append(val);
            return this;
        }

        public JsonEncoder Add(sbyte val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(sbyte? val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(string propertyName, byte val)
        {
            if (!String.IsNullOrEmpty(propertyName))
                AddProp(propertyName);
            sb.Append(val);
            return this;
        }

        public JsonEncoder Add(string propertyName, byte? val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }

            sb.Append(val);
            return this;
        }

        public JsonEncoder Add(byte val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(byte? val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(string propertyName, bool val)
        {
            if (!String.IsNullOrEmpty(propertyName))
                AddProp(propertyName);
            sb.Append(val.ToString().ToLower());
            return this;
        }

        public JsonEncoder Add(string propertyName, bool? val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }

            sb.Append(val.ToString().ToLower());
            return this;
        }

        public JsonEncoder Add(bool val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(bool? val)
        {
            return Add(null, val);
        }


        public JsonEncoder Add(string propertyName, char val)
        {
            if (!String.IsNullOrEmpty(propertyName))
                AddProp(propertyName);
            AddStr(val.ToString());
            return this;
        }

        public JsonEncoder Add(string propertyName, char? val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }

            AddStr(val.ToString());
            return this;
        }

        public JsonEncoder Add(char val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(char? val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(string propertyName, float val)
        {
            if (!String.IsNullOrEmpty(propertyName))
                AddProp(propertyName);
            sb.Append(val.ToString(CultureInfo.InvariantCulture));
            return this;
        }

        public JsonEncoder Add(string propertyName, float? val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }

            sb.Append(((float)val).ToString(CultureInfo.InvariantCulture));
            return this;
        }

        public JsonEncoder Add(float val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(float? val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(string propertyName, double val)
        {
            if (!String.IsNullOrEmpty(propertyName))
                AddProp(propertyName);
            sb.Append(val.ToString("r",CultureInfo.InvariantCulture));
            return this;
        }

        public JsonEncoder Add(string propertyName, double? val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }

            sb.Append(((double)val).ToString("r",CultureInfo.InvariantCulture));
            return this;
        }

        public JsonEncoder Add(double val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(double? val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(string propertyName, decimal val)
        {
            if (!String.IsNullOrEmpty(propertyName))
                AddProp(propertyName);
            sb.Append(val.ToString(CultureInfo.InvariantCulture));
            return this;
        }

        public JsonEncoder Add(string propertyName, decimal? val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }

            sb.Append(((decimal)val).ToString(CultureInfo.InvariantCulture));
            return this;
        }

        public JsonEncoder Add(decimal val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(decimal? val)
        {
            return Add(null, val);
        }


        public JsonEncoder Add(string propertyName, Guid val)
        {
            if (!String.IsNullOrEmpty(propertyName))
                AddProp(propertyName);
            AddStr(val.ToString());
            return this;
        }

        public JsonEncoder Add(string propertyName, Guid? val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }

            AddStr(((Guid)val).ToString());
            
            return this;
        }

        public JsonEncoder Add(Guid val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(Guid? val)
        {
            return Add(null, val);
        }


        public JsonEncoder Add(string propertyName, byte[] val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }

            AddStr(Convert.ToBase64String(val));
            return this;
        }


        public JsonEncoder Add(byte[] val)
        {
            return Add(null, val);
        }

        public JsonEncoder Add(string propertyName, TimeSpan val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }

            AddStr(val.ToString());
            return this;
        }


        public JsonEncoder Add(TimeSpan val)
        {
            return Add(null, val);
        }



        /// <summary>
        /// To supply heterogen values inside of Dictionary
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public JsonEncoder Add(string propertyName, Dictionary<string, Action> val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                {
                    AddProp(propertyName);
                    if (val.Count == 0)
                    {
                        sb.Append("{}");
                        lastchar = '}';
                        return this;
                    }
                }
            }
            else if (val == null)
            {
                AddNull();
                lastchar = '}';
                return this;
            }


            sb.Append("{");
            lastchar = '{';

            foreach (var item in val)
            {
                if (lastchar == '}' || lastchar == ']')
                {
                    sb.Append(",");
                    lastchar = ',';
                }
                AddProp(item.Key);
                item.Value();

                lastchar = '}'; //to put commas after standard values
            }
            sb.Append("}");
            lastchar = '}';
            return this;
        }

        public JsonEncoder Add(Dictionary<string, Action> val)
        {
            return Add(null, val);
        }

        /// <summary>
        /// Supplies heterogonenous array elements
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public JsonEncoder Add(string propertyName, List<Action> val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                {
                    AddProp(propertyName);
                    if (val.Count() == 0)
                    {
                        sb.Append("[]");
                        lastchar = ']';
                        return this;
                    }
                }
            }
            else if (val == null)
            {
                AddNull();
                lastchar = ']';
                return this;
            }

            sb.Append("[");
            lastchar = '[';

            foreach (var item in val)
            {
                if (lastchar == '}' || lastchar == ']')
                {
                    sb.Append(",");
                    lastchar = ',';
                }

                item();

                lastchar = '}'; //to put commas after standard values
            }
            sb.Append("]");
            lastchar = ']';
            return this;
        }

        public JsonEncoder Add(List<Action> val)
        {
            return Add(null, val);
        }


        Type TypeString = typeof(string);

        /// <summary>
        /// Adds Dictionary each Key will be transformed into String
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="val"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public JsonEncoder Add<K, V>(string propertyName, IDictionary<K, V> val, Action<V> f)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                {
                    AddProp(propertyName);
                    if (val.Count == 0)
                    {
                        sb.Append("{}");
                        lastchar = '}';
                        return this;
                    }
                }
            }
            else if (val == null)
            {
                AddNull();
                lastchar = '}';
                return this;
            }


            sb.Append("{");
            lastchar = '{';

            foreach (var item in val)
            {
                if (lastchar == '}' || lastchar == ']')
                {
                    sb.Append(",");
                    lastchar = ',';
                }
                AddProp((string)Convert.ChangeType(item.Key, TypeString));           
                f(item.Value);

                lastchar = '}'; //to put commas after standard values
            }
            sb.Append("}");
            lastchar = '}';
            return this;
        }

        /// <summary>
        ///  Adds Dictionary each Key will be transformed into String
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="val"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public JsonEncoder Add<K, V>(IDictionary<K,V> val, Action<V> f)
        {
            return Add(null, val, f);
        }

        /// <summary>
        /// Adds class implementing IJsonEncoder
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public JsonEncoder Add(string propertyName, IJsonEncoder val)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (val == null)
                    return this;
                else
                    AddProp(propertyName);
            }
            else if (val == null)
            {
                AddNull();
                return this;
            }


            sb.Append("{");
            lastchar = '{';
            val.BiserJsonEncode(this);
            sb.Append("}");
            lastchar = '}';

            return this;
        }

        public JsonEncoder Add(IJsonEncoder val)
        {
            return Add(null,val);
        }

        /// <summary>
        ///  Supply array and transformation function, one for each array element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public JsonEncoder Add<T>(IEnumerable<T> items, Action<T> f)
        {
            return Add(null, items, f);
        }

        /// <summary>
        /// Supply array and transformation function, one for each array element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="items"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public JsonEncoder Add<T>(string propertyName, IEnumerable<T> items, Action<T> f)
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                if (items == null)
                    return this;
                else
                {
                    AddProp(propertyName);
                    if (items.Count() == 0)
                    {
                        sb.Append("[]");
                        lastchar = ']';
                        return this;
                    }
                }
            }
            else if (items == null)
            {
                AddNull();
                lastchar = ']';
                return this;
            }

            sb.Append("[");
            lastchar = '[';

//#if NETSTANDARD
//            bool ic = typeof(IJsonEncoder).GetTypeInfo().IsAssignableFrom(typeof(T).Ge‌​tTypeInfo());
//            if(!ic)
//                 ic = typeof(System.Collections.IDictionary).GetTypeInfo().IsAssignableFrom(typeof(T).Ge‌​tTypeInfo());
//#else
//            bool ic = typeof(IJsonEncoder).IsAssignableFrom(typeof(T));
//            if(!ic)
//                ic = typeof(System.Collections.IDictionary).IsAssignableFrom(typeof(T));
//#endif          



            foreach (var item in items)
            {
                if (lastchar == '}' || lastchar == ']')
                {
                    sb.Append(",");
                    lastchar = ',';
                }

                f(item);               

                lastchar = '}'; //to put commas after standard values
            }
            sb.Append("]");
            lastchar = ']';            
            return this;
        }

    }
}
