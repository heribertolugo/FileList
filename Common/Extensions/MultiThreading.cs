using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Common.Extensions
{
    public static class MultiThreadingExtensions
    {

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
          InvokeIfRequiredDelegate<T> action)
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
    }
}
