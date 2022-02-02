using System;
using System.Collections.Generic;

namespace Common.Helpers
{
    public static class IoHelper
    {
        public static IEnumerable<string> AccessableDirectories(string path)
        {
            //List<string> accessable = new List<string>();
            string[] directories = new string[0];

            try
            {
                directories = System.IO.Directory.GetDirectories(path);
            }
            catch (Exception ex)
            {

            }

            foreach (string directory in directories)
            {
                if (IsSystemObjectAccessable(directory))
                    yield return directory;
                //accessable.Add(directory);
            }

            //return accessable;
        }
        public static IEnumerable<string> AccessableFiles(string path)
        {
            //List<string> accessable = new List<string>();
            string[] files = new string[0];

            try
            {
                files = System.IO.Directory.GetFiles(path);
            }
            catch (Exception ex)
            {

            }

            foreach (string file in files)
            {
                if (IsSystemObjectAccessable(file))
                    yield return file;
                //accessable.Add(file);
            }

            //return accessable;
        }

        public static bool IsSystemObjectAccessable(string path)
        {
            try
            {
                if (System.IO.Directory.Exists(path))
                {
                    //System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(path);
                    //System.Security.AccessControl.DirectorySecurity dirAC = dirInfo.GetAccessControl(System.Security.AccessControl.AccessControlSections.All);
                    System.IO.Directory.GetDirectories(path);
                    System.IO.Directory.GetFiles(path);
                }
                else if (System.IO.File.Exists(path))
                {
                    //System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
                    //System.Security.AccessControl.FileSecurity fileAC = fileInfo.GetAccessControl(System.Security.AccessControl.AccessControlSections.All);

                    System.IO.FileStream stream = System.IO.File.Open(path, System.IO.FileMode.Open,
                                                    System.IO.FileAccess.Read, System.IO.FileShare.None);
                    stream.Close();
                    //using (System.IO.FileStream reader = new System.IO.FileStream(path, System.IO.FileMode.Open))
                    //{
                    //    byte[] bytes = new byte[1];
                    //    reader.Read(bytes, 0, 1);
                    //}
                }
                else
                {
                    return false;
                    //throw new Exception();
                }
                return true;
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public static bool IsAdministrator()
        {
            return (new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()))
                      .IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        public static bool OutputIsRequested()
        {
            string[] args = Environment.GetCommandLineArgs();
            bool requested = false;

            if (args.Length < 2)
                return false;

            if (!bool.TryParse(args[1], out requested))
                return false;

            return requested;
        }

        public static void WriteToConsole()
        {
            if (OutputIsRequested())
                Console.WriteLine();
        }

        public static void WriteToConsole(object value)
        {
            if (OutputIsRequested())
                Console.WriteLine(value);
        }
        public static void WriteToConsole(string format, params object[] values)
        {
            if (OutputIsRequested())
            {
                if (values.Length < 1)
                    Console.WriteLine(format);
                else
                    Console.WriteLine(format, values);
            }
        }
    }
}
