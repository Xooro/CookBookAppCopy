using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CookBookApp.Helpers
{
    public class TypeConverter
    {
        public static byte[] streamToByteArrayConverter(Stream stream)
        {
            byte[] result = new byte[stream.Length];
            stream.Read(result, 0, result.Length);
            return result;
        }
    }
}
