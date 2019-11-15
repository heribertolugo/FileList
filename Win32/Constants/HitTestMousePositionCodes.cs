using System;
using System.Collections.Generic;
using System.Linq;

namespace Win32.Constants
{
    /// <summary>
    /// WM_NCHITTEST and MOUSEHOOKSTRUCT Mouse Position Codes
    /// </summary>
    public struct HitTestMousePositionCodes
    {
        public static HitTestMousePositionCodes HTERROR = new HitTestMousePositionCodes(-2);
        public static HitTestMousePositionCodes HTTRANSPARENT = new HitTestMousePositionCodes(-1);
        public static HitTestMousePositionCodes HTNOWHERE = new HitTestMousePositionCodes(0);
        public static HitTestMousePositionCodes HTCLIENT = new HitTestMousePositionCodes(1);
        public static HitTestMousePositionCodes HTCAPTION = new HitTestMousePositionCodes(2);
        public static HitTestMousePositionCodes HTSYSMENU = new HitTestMousePositionCodes(3);
        public static HitTestMousePositionCodes HTGROWBOX = new HitTestMousePositionCodes(4);
        public static HitTestMousePositionCodes HTSIZE = new HitTestMousePositionCodes(HTGROWBOX);
        public static HitTestMousePositionCodes HTMENU = new HitTestMousePositionCodes(5);
        public static HitTestMousePositionCodes HTHSCROLL = new HitTestMousePositionCodes(6);
        public static HitTestMousePositionCodes HTVSCROLL = new HitTestMousePositionCodes(7);
        public static HitTestMousePositionCodes HTMINBUTTON = new HitTestMousePositionCodes(8);
        public static HitTestMousePositionCodes HTMAXBUTTON = new HitTestMousePositionCodes(9);
        public static HitTestMousePositionCodes HTLEFT = new HitTestMousePositionCodes(10);
        public static HitTestMousePositionCodes HTRIGHT = new HitTestMousePositionCodes(11);
        public static HitTestMousePositionCodes HTTOP = new HitTestMousePositionCodes(12);
        public static HitTestMousePositionCodes HTTOPLEFT = new HitTestMousePositionCodes(13);
        public static HitTestMousePositionCodes HTTOPRIGHT = new HitTestMousePositionCodes(14);
        public static HitTestMousePositionCodes HTBOTTOM = new HitTestMousePositionCodes(15);
        public static HitTestMousePositionCodes HTBOTTOMLEFT = new HitTestMousePositionCodes(16);
        public static HitTestMousePositionCodes HTBOTTOMRIGHT = new HitTestMousePositionCodes(17);
        public static HitTestMousePositionCodes HTBORDER = new HitTestMousePositionCodes(18);
        public static HitTestMousePositionCodes HTREDUCE = new HitTestMousePositionCodes(HTMINBUTTON);
        public static HitTestMousePositionCodes HTZOOM = new HitTestMousePositionCodes(HTMAXBUTTON);
        public static HitTestMousePositionCodes HTSIZEFIRST = new HitTestMousePositionCodes(HTLEFT);
        public static HitTestMousePositionCodes HTSIZELAST = new HitTestMousePositionCodes(HTBOTTOMRIGHT);
        public static HitTestMousePositionCodes HTOBJECT = new HitTestMousePositionCodes(19);
        public static HitTestMousePositionCodes HTCLOSE = new HitTestMousePositionCodes(20);
        public static HitTestMousePositionCodes HTHELP = new HitTestMousePositionCodes(21);


        private static Dictionary<int, HitTestMousePositionCodes> _values;
        private HitTestMousePositionCodes(int value)
        {
            this.Value = value;

            if (HitTestMousePositionCodes._values == null)
                HitTestMousePositionCodes._values = new Dictionary<int, HitTestMousePositionCodes>();

            if (!HitTestMousePositionCodes._values.ContainsKey(value))
                HitTestMousePositionCodes._values.Add(value, this);
        }

        public int Value { get; private set; }
        public HitTestMousePositionCodes[] Values { get { return HitTestMousePositionCodes._values.Values.ToArray(); } private set { } }

        public static implicit operator int(HitTestMousePositionCodes mCode)
        {
            return mCode.Value;
        }

        public static implicit operator HitTestMousePositionCodes(int mCode)
        {
            if (HitTestMousePositionCodes._values.ContainsKey(mCode))
                return HitTestMousePositionCodes._values[mCode];
            return HitTestMousePositionCodes.HTNOWHERE;
        }

        public static bool operator ==(HitTestMousePositionCodes mc1, HitTestMousePositionCodes mc2)
        {
            return mc1.Value == mc2.Value;
        }

        public static bool operator !=(HitTestMousePositionCodes mc1, HitTestMousePositionCodes mc2)
        {
            return mc1.Value != mc2.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is HitTestMousePositionCodes || obj is int)
            {
                HitTestMousePositionCodes message = (HitTestMousePositionCodes)obj;

                return this.Value == message.Value;
            }
            else
            {
                try
                {
                    int i = Convert.ToInt32(obj);

                    return this.Value == i;
                }
                catch (Exception ex) { }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
}
