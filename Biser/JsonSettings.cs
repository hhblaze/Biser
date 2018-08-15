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
            EpochTime,
            /// <summary>
            /// Each local time must be converted into UTC and then represented as ISO
            /// </summary>
            Javascript
        }

        public enum JsonStringStyle
        {  
            Default,
            Prettify
        }

        public JsonSettings()
        {

        }

        public DateTimeStyle DateFormat { get; set; } = DateTimeStyle.Default;
        public JsonStringStyle JsonStringFormat { get; set; } = JsonStringStyle.Default;

    }
}
