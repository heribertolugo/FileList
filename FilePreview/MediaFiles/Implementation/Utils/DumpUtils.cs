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

using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace Implementation.Utils
{
    [SuppressUnmanagedCodeSecurity]
    class DumpUtils
    {
        private static readonly int MiniDumpWithFullMemory = 2;

        internal static void WriteDump(string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            NativeMethods.MINIDUMP_EXCEPTION_INFORMATION info = new NativeMethods.MINIDUMP_EXCEPTION_INFORMATION();
            info.ClientPointers = 1;
            info.ExceptionPointers = Marshal.GetExceptionPointers();
            info.ThreadId = NativeMethods.GetCurrentThreadId();

            NativeMethods.MiniDumpWriteDump(NativeMethods.GetCurrentProcess(), NativeMethods.GetCurrentProcessId(), 
                file.SafeFileHandle, MiniDumpWithFullMemory, ref info, IntPtr.Zero, IntPtr.Zero);
            file.Close();
        }

        internal static bool CreateDumpFile(out string dumpFullpath)
        {
            DumpFiles sec = (DumpFiles)ConfigurationManager.GetSection("DumpFiles");
            if (sec == null || !sec.GenerateDumpFileOnCrash)
            {
                dumpFullpath = string.Empty;
                return false;
            }

            string directory = sec.Directory;
            if (directory.StartsWith("\\"))
            {
                string asm = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                directory = asm + directory;
            }

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            string fullName = Path.Combine(directory, string.Format("nVLC_{0}.dmp", DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")));
            WriteDump(fullName);

            if (sec.MaxDumps > 0)
                DeleteOldDumps(sec.MaxDumps, directory);

            dumpFullpath = fullName;
            return true;
        }

        internal static void DeleteOldDumps(int maxDumps, string directory)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(directory);
            var oldDumps = dirInfo.GetFiles("*.dmp").OrderByDescending(file => file.CreationTime).Skip(maxDumps).ToList();
            oldDumps.ForEach(f => File.Delete(f.FullName));
        }
    }
}
