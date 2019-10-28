//    nVLC
//    
//    Author:  Roman Ginzburg
//
//    nVLC is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    nVLC is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.
//     
// ========================================================================

using Declarations.Attributes;
using LibVlcWrapper;
using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Implementation.Utils
{
    internal class MiscUtils
    {
        public static string DwordToFourCC(uint fourCC)
        {
            char[] chars = new char[4];
            chars[0] = (char)(fourCC & 0xFF);
            chars[1] = (char)((fourCC >> 8) & 0xFF);
            chars[2] = (char)((fourCC >> 16) & 0xFF);
            chars[3] = (char)((fourCC >> 24) & 0xFF);
            return new string(chars);
        }

        public static string GetMinimalSupportedVersion(EntryPointNotFoundException ex)
        {
            MinimalLibVlcVersion minVersion = (MinimalLibVlcVersion)Attribute.GetCustomAttribute(ex.TargetSite, typeof(MinimalLibVlcVersion));
            if (minVersion != null)
            {
                return minVersion.MinimalVersion;
            }

            return string.Empty;
        }

        public static T FindNestedException<T>(Exception source) where T : Exception
        {
            if (source == null)
                return default(T);

            if (source.GetType() == typeof(T))
            {
                return (T)source;
            }

            while (source.InnerException != null)
            {
                return FindNestedException<T>(source.InnerException);
            }

            return default(T);
        }

        public static string LogNestedException(Exception ex)
        {
            if (ex == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            Exception error = ex;
            do
            {
                if(error is AggregateException)
                {
                    AggregateException aggregateExc = ((AggregateException)error).Flatten();
                    foreach (var item in aggregateExc.InnerExceptions)
                    {
                        sb.AppendLine(item.ToString());
                    }
                }

                sb.AppendLine(error.ToString());
                error = error.InnerException;
            }
            while (error != null);
            return sb.ToString();
        }

        public static Version ConvertToVersion(string version)
        {
            const string pattern = @"\d+(\.\d+)+"; // numbers separated by dots
            Match versionMatch = Regex.Match(version, pattern);
            Version libVlcVersion = new Version(versionMatch.Value);
            return libVlcVersion;
        }
    }

    public static class Utility
    {
        public static long ElapsedNanoSeconds(this Stopwatch watch)
        {
            return watch.ElapsedTicks * 1000000000 / Stopwatch.Frequency;
        }
        public static long ElapsedMicroSeconds(this Stopwatch watch)
        {
            return watch.ElapsedTicks * 1000000 / Stopwatch.Frequency;
        }
    }
}
