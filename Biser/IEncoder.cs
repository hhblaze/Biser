﻿/* 
  Copyright (C) 2012 tiesky.com / Alex Solovyov
  It's a free software for those, who think that it should be free.
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Biser
{
    public interface IEncoder
    {
        Encoder BiserEncoder(Encoder existingEncoder = null);        
    }

    public interface IDecoder
    {
        object BiserDecodeToObject(Decoder extDecoder);
        object BiserDecodeToObject(byte[] encoded);
    }

    /// <summary>
    /// Starting from JSAdd
    /// </summary>
    public interface IJSEncoder
    {
        Encoder BiserJSEncoder(Encoder existingEncoder = null);
    }

    /// <summary>
    /// Starting from JSGet
    /// </summary>
    public interface IJSDecoder
    {
        object BiserJSDecodeToObject(Decoder extDecoder);
        object BiserJSDecodeToObject(byte[] encoded);
    }
}
