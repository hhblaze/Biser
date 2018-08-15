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

        public JsonEncoder(JsonSettings settings=null)
        {
            jsonSettings = (settings == null) ? new JsonSettings() : settings;

            sb = new StringBuilder();
            sb.Append("{"); //Always start as an object
        }

        /// <summary>
        /// Adds propertyName
        /// </summary>
        /// <param name="pn"></param>
        void AddPN(string pn)
        {
            sb.Append("\"");
            sb.Append(pn);
            sb.Append("\":");
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

        public JsonEncoder Add(string propertyName, DateTime dt)
        {
            AddPN(propertyName);
            AppendDateTime(dt);
            return this;
        }

        public JsonEncoder Add(string propertyName, DateTime? dt)
        {
            if (dt == null)
                return this;
            AddPN(propertyName);
            AppendDateTime((DateTime)dt);
            return this;
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

    }
}
