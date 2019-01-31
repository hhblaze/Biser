using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiserObjectify
{
    public static class Generator
    {
       

        public static void Run(Type incomingType, bool generateIncludedTypes, string destinationFolder, bool forBiserBinary=true, bool forBiserJson = true)
        {
            StandardTypes.InitDict();
            JsonGenerator jg = new JsonGenerator();

            if (forBiserJson)
            {
                jg.Run(incomingType);
            }

        }
    }
}
