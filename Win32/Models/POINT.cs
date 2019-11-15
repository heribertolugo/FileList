using System.Drawing;
using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;

        public static implicit operator Point(POINT point)
        {
            return new Point(point.x, point.y);
        }
    }
}
