using System.Runtime.InteropServices;

namespace Win32.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public class SystemTime
    {
        public ushort year;
        public ushort month;
        public ushort weekday;
        public ushort day;
        public ushort hour;
        public ushort minute;
        public ushort second;
        public ushort millisecond;
    }
}
