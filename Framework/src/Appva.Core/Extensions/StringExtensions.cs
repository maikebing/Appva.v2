// <copyright file="StringExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Extensions
{
    #region Imports.

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Utilities;

    #endregion

    /// <summary>
    /// <see cref="String"/> extension helper methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks whether or not the string is null or empty.
        /// </summary>
        /// <param name="str">The string to be checked</param>
        /// <returns>True if the string is null or empty</returns>
        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Checks whether or not the string is not null or empty.
        /// </summary>
        /// <param name="str">The string to be checked</param>
        /// <returns>True if the string is not null or empty</returns>
        public static bool IsNotEmpty(this string str)
        {
            return ! str.IsEmpty();
        }

        /// <summary>
        /// Replaces the format item in a specified string with the string representation
        /// of a corresponding object in a specified array.
        /// </summary>
        /// <param name="str">
        /// A composite format string
        /// </param>
        /// <param name="args">
        /// An object array that contains zero or more objects to format
        /// </param>
        /// <returns>
        /// A copy of format in which the format items have been replaced by the string
        /// representation of the corresponding objects in args
        /// </returns>
        public static string FormatWith(this string str, params object[] args)
        {
            return (str.IsEmpty()) ? str : string.Format(str, args);
        }

        /// <summary>
        /// Replaces the format item in a specified string with the string representation
        /// of a corresponding object in a specified array.
        /// </summary>
        /// <param name="str">
        /// A composite format string
        /// </param>
        /// <param name="provider">
        /// An object that supplies culture-specific formatting information
        /// </param>
        /// <param name="args">
        /// An object array that contains zero or more objects to format
        /// </param>
        /// <returns>
        /// A copy of format in which the format items have been replaced by the string
        /// representation of the corresponding objects in args
        /// </returns>
        public static string FormatWith(this string str, IFormatProvider provider, params object[] args)
        {
            return (str.IsEmpty()) ? str : string.Format(provider, str, args);
        }

        /// <summary>
        /// Removes the last occuring character in a string.
        /// </summary>
        /// <param name="str">The string to be processed</param>
        /// <param name="character">The character to be removed</param>
        /// <returns>A new string without the last occuring specified character</returns>
        public static string StripLast(this string str, char character)
        {
            if (str.IsEmpty())
            {
                return str;
            }
            var index = str.LastIndexOf(character);
            return (index > -1) ? str.Remove(index) : str;
        }

        /// <summary>
        /// Converts the first character to an upper case letter.
        /// </summary>
        /// <param name="str">The string to be processed</param>
        /// <returns>A new string with the first letter upper case</returns>
        public static string FirstToUpper(this string str)
        {
            if (str.IsEmpty())
            {
                return str;
            }
            if (str.Length > 1)
            {
                return char.ToUpper(str[0]) + str.Substring(1);
            }
            return str.ToUpper();
        }
        
        /// <summary>
        /// Converts a <see cref="String"/> to a <see cref="Guid"/>.
        /// </summary>
        /// <param name="str">The string to be converted</param>
        /// <returns>An empty <see cref="Guid"/> if the string is empty, otherwise a <see cref="Guid"/></returns>
        /// <exception cref="FormatException">The format of the string is invalid</exception>
        /// <exception cref="OverflowException">The format of the string is invalid</exception>
        public static Guid ToGuid(this string str)
        {
            return str.IsEmpty() ? Guid.Empty : new Guid(str);
        }

        /// <summary>
        /// Converts a string to UTF-8 bytes.
        /// </summary>
        /// <param name="value">The string to bytify</param>
        /// <returns>An UTF-8 byte array</returns>
        public static byte[] ToUtf8Bytes(this string value)
        {
            return value.IsEmpty() ? null : Encoding.UTF8.GetBytes(value);
        }

        /// <summary>
        /// Returns a string without invalid file name characters
        /// </summary>
        /// <param name="str">The string to be checked</param>
        /// <returns>A string without invalid file name characters</returns>
        public static string StripInvalidFileNameCharacters(this string str)
        {
            if (str.IsEmpty())
            {
                return str;
            }
            var illegalCharacters = Path.GetInvalidFileNameChars().ToList();
            foreach (var illegal in illegalCharacters.Where(str.Contains))
            {
                str = str.Replace(illegal, '_');
            }
            return str;
        }

        /// <summary>
        /// Converts a string to hex format.
        /// </summary>
        /// <param name="str">The string to be converted</param>
        /// <returns>Hex string</returns>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1407:ArithmeticExpressionsMustDeclarePrecedence", Justification = "Reviewed.")]
        public static string ToHex(this string str)
        {
            return str.IsEmpty() ? str : str.ToUtf8Bytes().ToHex();
        }

        /// <summary>
        /// Converts a string from hex to non hex.
        /// </summary>
        /// <param name="str">The hex string to be converted</param>
        /// <returns>A non hex formatted string</returns>
        public static string FromHex(this string str)
        {
            if (str.IsEmpty())
            {
                return str;
            }
            var totalChars = str.Length / 2;
            var bytes = new byte[totalChars];
            using (var sr = new StringReader(str))
            {
                for (var i = 0; i < totalChars; i++)
                {
                    bytes[i] = Convert.ToByte(new string(new[] { (char) sr.Read(), (char) sr.Read() }), 16);
                }
            }
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Converts a string to be URL friendly.
        /// </summary>
        /// <param name="str">The string to be processed</param>
        /// <returns>A URL friendly string</returns>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1303:ConstFieldNamesMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        public static string ToUrlFriendly(this string str)
        {
            if (str.IsEmpty())
            {
                return str;
            }
            const int maxLength = 80;
            var previousCharIsDash = false;
            var builder = new StringBuilder(str.Length);
            for (var i = 0; i < str.Length; i++)
            {
                var c = str[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    builder.Append(c);
                    previousCharIsDash = false;
                }
                if (c >= 'A' && c <= 'Z')
                {
                    builder.Append((char)(c | 32));
                    previousCharIsDash = false;
                }
                if (c.Equals(' ') || c.Equals(',') || c.Equals('.') || c.Equals('/') ||
                    c.Equals('\\') || c.Equals('-') || c.Equals('_') || c.Equals('='))
                {
                    if (! previousCharIsDash && builder.Length > 0)
                    {
                        builder.Append('-');
                        previousCharIsDash = true;
                    }
                }
                if (c >= 128)
                {
                    var prevlen = builder.Length;
                    builder.Append(StringUtilities.RemapInternationalCharToAscii(c));
                    if (prevlen != builder.Length)
                    {
                        previousCharIsDash = false;
                    }
                }
                if (i.Equals(maxLength))
                {
                    break;
                }
            }
            return previousCharIsDash ? builder.ToString().Substring(0, builder.Length - 1) : builder.ToString();
        }

        /// <summary>
        /// Converts a string to lowercase underscore. E.g. My Little Pony will convert to 
        /// my_little_pony.
        /// </summary>
        /// <param name="str">The string to be converted</param>
        /// <returns>A new lowercased underscored string</returns>
        public static string ToLowerCaseUnderScore(this string str)
        {
            if (str.IsEmpty())
            {
                return str;
            }
            var builder = new StringBuilder(str.Length * 2);
            builder.Append(str[0]);
            for (var i = 1; i < str.Length; i++)
            {
                if (char.IsUpper(str[i]))
                {
                    if ((str[i - 1] != '_' && (! char.IsUpper(str[i - 1]) && char.IsLetter(str[i - 1]))) ||
                        (char.IsUpper(str[i - 1]) &&
                         i < str.Length - 1 && (! char.IsUpper(str[i + 1]) && char.IsLetter(str[i + 1]))))
                    {
                        builder.Append('_');
                    }
                }
                builder.Append(str[i]);
            }
            return builder.ToString().ToLower();
        }

        /// <summary>
        /// Converts a string to base64.
        /// </summary>
        /// <param name="str">The string to be converted</param>
        /// <returns>A base64 string</returns>
        public static string ToBase64(this string str)
        {
            return str.IsEmpty() ? str : Convert.ToBase64String(str.ToUtf8Bytes());
        }

        /// <summary>
        /// Converts a base64 string to bytes.
        /// </summary>
        /// <param name="str">The string to be converted</param>
        /// <returns>A byte array</returns>
        public static byte[] FromBase64(this string str)
        {
            return str.IsEmpty() ? null : Convert.FromBase64String(str);
        }

        /// <summary>
        /// Returns a copy of this string converted to lowercase.
        /// </summary>
        /// <param name="str">The string to be converted</param>
        /// <returns>A string in lowercase</returns>
        public static string ToNullSafeLower(this string str)
        {
            return str.IsEmpty() ? str : str.ToLower();
        }

        ////////////////////// FROM UTILS
        public static string First(this string str, int number)
        {
            return str.IsNotEmpty() && str.Length >= number ? str.Substring(0, number) : string.Empty;
        }

        public static bool Is(this string str, Func<Char, bool> predicate)
        {
            return str.IsNotEmpty() && str.All(predicate);
        }

        public static string Strip(this string str, string replacement)
        {
            return str.IsEmpty() ? str : str.Replace(replacement, string.Empty);
        }
    }
}
