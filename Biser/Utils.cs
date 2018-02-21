/* 
  Copyright (C) 2012 tiesky.com / Alex Solovyov
  It's a free software for those, who think that it should be free.
*/

using System;
using System.Collections.Generic;


namespace Biser
{
    /// <summary>
    /// Binary serializer "biser", that can be translated as "beads" from Russian
    /// </summary>
    public static class Biser
    {
        public static long EncodeZigZag(long value, int bitLength)
        {
            return (value << 1) ^ (value >> (bitLength - 1));
        }

        public static long DecodeZigZag(ulong value)
        {
            if ((value & 0x1) == 0x1)
                return (-1 * ((long)(value >> 1) + 1));

            return (long)(value >> 1);
        }

        public static string UTF8_GetString(this byte[] btText)
        {
            return btText == null ? null : System.Text.Encoding.UTF8.GetString(btText, 0, btText.Length);
        }

        public static byte[] To_UTF8Bytes(this string text)
        {
            return System.Text.Encoding.UTF8.GetBytes(text);
        }

        public static byte[] GetVarintBytes(ulong value)
        {
            var buffer = new byte[10];
            var pos = 0;
            byte byteVal;
            do
            {
                byteVal = (byte)(value & 0x7f);
                value >>= 7;

                if (value != 0)
                {
                    byteVal |= 0x80;
                }

                buffer[pos++] = byteVal;

            } while (value != 0);

            var result = new byte[pos];
            Buffer.BlockCopy(buffer, 0, result, 0, pos);

            return result;
        }


    }
}
