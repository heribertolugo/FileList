using System.Runtime.InteropServices;

namespace Win32.Models
{
    [StructLayout(LayoutKind.Explicit)]
    public struct DWORD
    {
        [FieldOffset(0)]
        private uint _value;
        [FieldOffset(0)]
        private ushort _low;
        [FieldOffset(2)]
        private ushort _high;

        public DWORD(uint value)
        {
            this._low = 0;
            this._high = 0;
            this._value = value;
        }

        public uint Value { get { return this._value; } private set { } }

        public ushort Low { get { return this._low; } private set { } }

        public ushort High { get { return this._high; } private set { } }

        // setter methods for low and high, maybe which return new dword ??
    }
}
