using System;

namespace Win32
{
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public static implicit operator System.Drawing.Rectangle(RECT rect)
        {
            try
            {
                return new System.Drawing.Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
            }
            catch (Exception ex) { }

            return default(System.Drawing.Rectangle);
        }

        public static implicit operator RECT(System.Drawing.Rectangle rect)
        {
            try
            {
                return new RECT() { left = rect.Left, top = rect.Top, right = rect.Right, bottom = rect.Bottom };
            }
            catch (Exception ex) { }

            return default(RECT);
        }
    }
}
