using System;
using System.Runtime.InteropServices;

namespace Common.Extensions
{
    public static class IntPtrExtensions
    {
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
    }
}
