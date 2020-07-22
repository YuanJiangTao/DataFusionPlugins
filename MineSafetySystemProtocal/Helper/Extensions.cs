using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSafetySystemProtocal.Helper
{
    public static class Extensions
    {
        public static int ToInt(this string content)
        {
            return int.TryParse(content, out var value) ? value : 0;
        }
        public static float ToFloat(this string content)
        {
            return float.TryParse(content, out var value) ? value : 0;
        }
        public static DateTime ToDateTime(this string content)
        {
            return DateTime.TryParse(content, out var value) ? value : DateTime.Now;
        }
    }
}
