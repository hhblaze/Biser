using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biser
{
    public interface IJsonEncoder
    {
        void BiserJsonEncode(JsonEncoder encoder);
        //T BiserJsonDecoder<T>(JsonDecoder decoder);
    }
}
