using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Helpers
{
    public static class StringExtensions
    {
        public static string FirstLetterToUpper(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            return text.First().ToString().ToUpper() + text.Substring(1).ToLower();
        }
    }
}
