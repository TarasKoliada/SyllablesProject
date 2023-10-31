using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public class RegexHelper
    {
        public static string RemoveAllLatinExcept(List<char> charsToSkip)
        {
            var regex = "[a-zA-Z]|[0-9]";          

    /*       if (charsToSkip.Any())
            {
                var exceptChars = $"(?![{String.Join("", charsToSkip)}])";
                regex = $"{exceptChars}{regex}";
            }  */

            return regex;
        }
    }
}
