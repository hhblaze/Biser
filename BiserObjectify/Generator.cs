using BiserObjectify.Properties;
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
            BinaryGenerator bg = new BinaryGenerator();

            string tmplIfcJson = "";
            string tmplIfcBinary = "";

            string contentJson = "";
            string contentBinary = "";

            if (forBiserJson)
            {
                
                tmplIfcJson = "Biser.IJsonEncoder";
                contentJson = jg.Run(incomingType);
            }

            if (forBiserBinary)
            {
                contentBinary = bg.Run(incomingType);
                tmplIfcBinary = " Biser.IEncoder";
            }

            string tmplIfcComma1 = (forBiserJson && forBiserBinary) ? "," : "";


            var nsLen = incomingType.FullName.Length - incomingType.Name.Length - 1;

            var ret = Resource1.tmplBiserContainer.ReplaceMultiple(
                new Dictionary<string, string> {
                    { "{@NamespaceName}", incomingType.FullName.Substring(0, nsLen) },
                    { "{@ObjName}", incomingType.Name},
                    { "{@IfcJson}", tmplIfcJson},
                    { "{@IfcBinary}", tmplIfcBinary},
                    { "{@IfcComma1}", tmplIfcComma1 },
                    { "{@ContentJson}", contentJson },
                    { "{@ContentBinary}", contentBinary}
                });               

            System.IO.File.WriteAllText(@"D:\Temp\1\TS6_Biser.cs", ret);

        }
    }
}
