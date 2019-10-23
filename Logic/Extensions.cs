
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace FileList
{
    public static class Extensions
    {
        #region Multi Threading
        public static void InvokeIfRequired(this ISynchronizeInvoke obj, MethodInvoker action)
        {
            if (obj.InvokeRequired)
            {
                object[] args = new object[0];
                obj.Invoke((Delegate)action, args);
            }
            else
                action();
        }

        public static void InvokeIfRequired<T>(
          this T obj,
          Extensions.InvokeIfRequiredDelegate<T> action)
          where T : ISynchronizeInvoke
        {
            if (obj.InvokeRequired)
                obj.Invoke((Delegate)action, new object[1]
                {
          (object) obj
                });
            else
                action(obj);
        }

        public delegate void InvokeIfRequiredDelegate<T>(T obj) where T : ISynchronizeInvoke;
        #endregion

        #region Drawing
        public static Bitmap ToBitmap(this BitmapSource imgsrc)
        {
            int pixelWidth = imgsrc.PixelWidth;
            int pixelHeight = imgsrc.PixelHeight;
            int stride = pixelWidth * ((imgsrc.Format.BitsPerPixel + 7) / 8);
            IntPtr num = Marshal.AllocHGlobal(pixelHeight * stride);
            imgsrc.CopyPixels(new Int32Rect(0, 0, pixelWidth, pixelHeight), num, pixelHeight * stride, stride);
            return new Bitmap(pixelWidth, pixelHeight, stride, PixelFormat.Format32bppArgb, num);
        }
        #endregion

        #region I.O
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
                if (Extensions.IsSystemObjectAccessable(directory))
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
                if (Extensions.IsSystemObjectAccessable(file))
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
                Console.WriteLine(format, values);
        }
        #endregion

        #region IntPtr
        public static IntPtr ToIntPtr(this object target)
        {
            return (IntPtr)GCHandle.Alloc(target);
        }

        public static GCHandle ToGcHandle(this object target)
        {
            return GCHandle.Alloc(target);
        }

        //public static IntPtr ToIntPtr(this GCHandle target)
        //{
        //    return GCHandle.ToIntPtr(target);
        //}

        public static T ToObject<T>(IntPtr ptr)
        {
            try
            {
                GCHandle gch = GCHandle.FromIntPtr(ptr);
                T t = (T)(gch.Target);
                gch.Free();
                return t;
            }
            catch (Exception ex)
            {

            }
            return default(T);
        }
        #endregion

        #region TreeView
        public static bool IsInVisibleScope(this System.Windows.Forms.TreeNode node)
        {
            if (node == null)
                return false;
            return node.TreeView.ClientRectangle.Contains(new Rectangle(node.Bounds.Location, new System.Drawing.Size(1, 1)));
        }

        public static bool IsFullyVisible(this System.Windows.Forms.TreeNode node)
        {
            if (node == null)
                return false;
            return node.TreeView.ClientRectangle.Contains(node.Bounds);
        }

        public static IEnumerable<TreeNode> SortChildNodes(this TreeNode parent, IComparer<TreeNode> comparer)
        {
            if (parent is null || parent.Nodes.Count < 2)
                return parent == null ? Enumerable.Empty<TreeNode>() : parent.Nodes.Cast<TreeNode>();

            TreeNode[] nodes = parent.Nodes.Cast<TreeNode>().OrderBy(n => comparer).ToArray();
            parent.Nodes.Clear();
            parent.Nodes.AddRange(nodes);

            return nodes;
        }

        public static int GetNodeCount(this TreeView treeView)
        {
            if (treeView.Nodes.Count < 1)
                return 0;

            int count = 0;
            TreeNode node = treeView.Nodes[0];
            
            while (node != null)
            {
                count++;

                if (node.IsExpanded)
                {
                    node = node.Nodes[0];
                    continue;
                }

                TreeNode tempNode = node;
                node = node.NextNode;

                if (node == null && tempNode.Parent != null)
                {
                    node = tempNode.Parent.NextNode;
                }
            }

            return count;
        }

        #endregion

        #region Collections
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> collection, int count)
        {
            return collection.Reverse().Take(count).Reverse();
        }

        public static int IndexOf<T>(this SortedSet<T> set, T item)
        {
            try
            {
                // which has better performance??
                //return set.Select((o, i) => new { o, i }).First(o => o.o.Equals(item)).i;
                return set.TakeWhile((o, i) => !o.Equals(item)).Count()-1;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }
        #endregion
    }
}