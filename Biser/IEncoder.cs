using System;
using System.Collections.Generic;
using System.Text;

namespace Biser
{
    public interface IEncoder
    {
        Encoder BiserEncoder(Encoder existingEncoder = null);        
    }
}
