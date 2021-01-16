using System;
using System.Collections.Generic;
using System.Text;

namespace MyCms.Core.Convertor
{
   public static class TextConvertor
    {
        public static string FixText(this string value)
        {
            return value.ToLower().Trim();
        }
    }
}
