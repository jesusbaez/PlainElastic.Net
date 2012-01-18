﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace PlainElastic.Net
{
    public static class StringExtensions
    {
        private static readonly Regex splitBySpaceAndCommaRegex = new Regex("(?:^|,|\\s)(\"(?:[^\"]+|\"\")*\"|[^,\\s]*)", RegexOptions.Compiled);

        
        public static string F(this string source, params object[] args)
        {
            return String.Format(source, args);
        }

        public static bool IsNullOrEmpty(this string source)
        {
            return String.IsNullOrEmpty(source);
        }

        public static string ToCamelCase(this string value)
        {
            if (!Char.IsUpper(value, 0))
                return value;
            return Char.ToLower(value[0]) + value.Substring(1);
        }

        public static string JoinWithSeparator(this IEnumerable<string> list, string separator)
        {
            return list == null ? "" : String.Join(separator, list);
        }

        public static string JoinWithComma(this IEnumerable<string> list)
        {
            return list == null ? "" : String.Join(",", list);
        }

        public static string Quotate(this string value)
        {
            return "\"" + value + "\"";
        }

        public static string LowerAndQuotate(this string value)
        {
            return value.ToLower().Quotate();
        }

        public static IEnumerable<string> LowerAndQuotate(this IEnumerable<string> values)
        {
            return values.Select(v => v.LowerAndQuotate());
        }

        public static string[] SplitByCommaAndSpaces(this string text)
        {
            // Split text by commas and spaces unless it quoted.
            var textsToSearch = from Match m in splitBySpaceAndCommaRegex.Matches(text)
                                where !(m.Value.Trim(' ', ',', '"').IsNullOrEmpty()) // skip empty matches..
                                select m.Value.Trim(' ', ',', '"');

            return textsToSearch.ToArray();
        }


        public static string AsString(this bool value)
        {
            return value.ToString(CultureInfo.InvariantCulture).ToLower();
        }
    }
}