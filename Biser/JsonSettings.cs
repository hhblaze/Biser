using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biser
{
    public class JsonSettings
    {
        public enum DateTimeStyle
        {

            /// <summary>
            /// Default /Date(...)/
            /// </summary>
            Default,
            /// <summary>
            /// ISO Format
            /// </summary>
            ISO,
            /// <summary>
            /// Unix Epoch Milliseconds
            /// </summary>
            EpochTime
            ////
            //// Summary:
            ////     JSON.NET Format for backward compatibility
            //JsonNetISO = 6,
            ////
            //// Summary:
            ////     .NET System.Web.Script.Serialization.JavaScriptSerializer backward compatibility
            //JavascriptSerializer = 8
        }

        public JsonSettings()
        {

        }

        public DateTimeStyle DateFormat { get; set; } = DateTimeStyle.Default;

    }
}
