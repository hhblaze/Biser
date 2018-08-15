using System;
using System.Collections.Generic;
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
            if(lastchar != '{' && lastchar != '[')
                sb.Append(",");           
            sb.Append("\"");
            sb.Append(str);
            sb.Append("\":");

            lastchar = ',';
        }

        void AddStr(string str)
        {
            sb.Append("\"");
            sb.Append(str);
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
            return finished;
                
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







        Type TypeString = typeof(string);
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
                    sb.Append(",");                
                AddProp((string)Convert.ChangeType(item.Key, TypeString));
                //AddProp((string)(object)item.Key);
                f(item.Value);

                lastchar = '}'; //to put commas after standard values
            }
            sb.Append("}");
            lastchar = '}';
            return this;
        }

        public JsonEncoder Add<K,V>(IDictionary<K,V> val, Action<V> f)
        {
            return Add(null, val, f);
        }
        
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

        public JsonEncoder Add<T>(IEnumerable<T> items, Action<T> f)
        {
            return Add(null, items, f);
        }

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


            //if (items == null)
            //    return this;
            //if(!String.IsNullOrEmpty(propertyName))
            //    AddProp(propertyName);

            //if (items.Count() == 0)
            //{
            //    sb.Append("[]");
            //    lastchar = ']';
            //    return this;
            //}
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

//            ic = false;

            foreach (var item in items)
            {
                if (lastchar == '}' || lastchar == ']')
                    sb.Append(",");

                //if (ic)
                //{
                //    sb.Append("{");
                //    lastchar = '{';
                //}

                f(item);

                //if (ic)
                //    sb.Append("}");

                lastchar = '}'; //to put commas after standard values
            }
            sb.Append("]");
            lastchar = ']';            
            return this;
        }

    }
}
