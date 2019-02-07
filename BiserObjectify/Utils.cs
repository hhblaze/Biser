using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiserObjectify
{
    public static class Utils
    {
        public static string ReplaceMultiple(this string input, Dictionary<string, string> replaceWith)
        {
            if (input == null || replaceWith == null || replaceWith.Count < 1)
                return input;

            replaceWith = replaceWith.OrderByDescending(r => r.Key.Length).ToDictionary(r => r.Key, r => r.Value);

            System.Text.RegularExpressions.Regex regex = null;
            regex = new System.Text.RegularExpressions.Regex(String.Join("|", replaceWith.Keys.Select(k => System.Text.RegularExpressions.Regex.Escape(k))));

            return regex.Replace(input, m => replaceWith[m.Value]);

        }

        public static string Reverse(this string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
