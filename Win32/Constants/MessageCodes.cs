using System;
using System.Collections.Generic;
using System.Linq;

namespace Win32.Constants
{
    public struct MessageCodes
    {
        public static MessageCodes None = new MessageCodes(0);
        public static MessageCodes WM_USER = new MessageCodes(0x0400);
        public static MessageCodes WM_NOTIFY = new MessageCodes(0x004E);
        public static MessageCodes WM_INPUTLANGCHANGEREQUEST = new MessageCodes(0x0050);
        public static MessageCodes WM_INPUTLANGCHANGE = new MessageCodes(0x0051);
        public static MessageCodes WM_TCARD = new MessageCodes(0x0052);
        public static MessageCodes WM_HELP = new MessageCodes(0x0053);
        public static MessageCodes WM_USERCHANGED = new MessageCodes(0x0054);
        public static MessageCodes WM_NOTIFYFORMAT = new MessageCodes(0x0055);
        public static MessageCodes WM_CONTEXTMENU = new MessageCodes(0x007B);
        public static MessageCodes WM_STYLECHANGING = new MessageCodes(0x007C);
        public static MessageCodes WM_STYLECHANGED = new MessageCodes(0x007D);
        public static MessageCodes WM_DISPLAYCHANGE = new MessageCodes(0x007E);
        public static MessageCodes WM_GETICON = new MessageCodes(0x007F);
        public static MessageCodes WM_SETICON = new MessageCodes(0x0080);
        public static MessageCodes WM_NCCREATE = new MessageCodes(0x0081);
        public static MessageCodes WM_NCDESTROY = new MessageCodes(0x0082);
        public static MessageCodes WM_NCCALCSIZE = new MessageCodes(0x0083);
        public static MessageCodes WM_NCHITTEST = new MessageCodes(0x0084);
        public static MessageCodes WM_NCPAINT = new MessageCodes(0x0085);
        public static MessageCodes WM_NCACTIVATE = new MessageCodes(0x0086);
        public static MessageCodes WM_GETDLGCODE = new MessageCodes(0x0087);
        public static MessageCodes WM_NCMOUSEMOVE = new MessageCodes(0x00A0);
        public static MessageCodes WM_NCLBUTTONDOWN = new MessageCodes(0x00A1);
        public static MessageCodes WM_NCLBUTTONUP = new MessageCodes(0x00A2);
        public static MessageCodes WM_NCLBUTTONDBLCLK = new MessageCodes(0x00A3);
        public static MessageCodes WM_NCRBUTTONDOWN = new MessageCodes(0x00A4);
        public static MessageCodes WM_NCRBUTTONUP = new MessageCodes(0x00A5);
        public static MessageCodes WM_NCRBUTTONDBLCLK = new MessageCodes(0x00A6);
        public static MessageCodes WM_NCMBUTTONDOWN = new MessageCodes(0x00A7);
        public static MessageCodes WM_NCMBUTTONUP = new MessageCodes(0x00A8);
        public static MessageCodes WM_NCMBUTTONDBLCLK = new MessageCodes(0x00A9);
        public static MessageCodes WM_NCXBUTTONDOWN = new MessageCodes(0x00AB);
        public static MessageCodes WM_NCXBUTTONUP = new MessageCodes(0x00AC);
        public static MessageCodes WM_NCXBUTTONDBLCLK = new MessageCodes(0x00AD);
        public static MessageCodes WM_INPUT_DEVICE_CHANGE = new MessageCodes(0x00FE);
        public static MessageCodes WM_INPUT = new MessageCodes(0x00FF);
        public static MessageCodes WM_KEYFIRST = new MessageCodes(0x0100);
        public static MessageCodes WM_KEYDOWN = new MessageCodes(0x0100);
        public static MessageCodes WM_KEYUP = new MessageCodes(0x0101);
        public static MessageCodes WM_CHAR = new MessageCodes(0x0102);
        public static MessageCodes WM_DEADCHAR = new MessageCodes(0x0103);
        public static MessageCodes WM_SYSKEYDOWN = new MessageCodes(0x0104);
        public static MessageCodes WM_SYSKEYUP = new MessageCodes(0x0105);
        public static MessageCodes WM_SYSCHAR = new MessageCodes(0x0106);
        public static MessageCodes WM_SYSDEADCHAR = new MessageCodes(0x0107);
        public static MessageCodes WM_UNICHAR = new MessageCodes(0x0109);
        public static MessageCodes WM_KEYLAST = new MessageCodes(0x0109);
        public static MessageCodes UNICODE_NOCHAR = new MessageCodes(0xFFFF);
        public static MessageCodes WM_IME_STARTCOMPOSITION = new MessageCodes(0x010D);
        public static MessageCodes WM_IME_ENDCOMPOSITION = new MessageCodes(0x010E);
        public static MessageCodes WM_IME_COMPOSITION = new MessageCodes(0x010F);
        public static MessageCodes WM_IME_KEYLAST = new MessageCodes(0x010F);
        public static MessageCodes WM_INITDIALOG = new MessageCodes(0x0110);
        public static MessageCodes WM_COMMAND = new MessageCodes(0x0111);
        public static MessageCodes WM_SYSCOMMAND = new MessageCodes(0x0112);
        public static MessageCodes WM_TIMER = new MessageCodes(0x0113);
        public static MessageCodes WM_HSCROLL = new MessageCodes(0x0114);
        public static MessageCodes WM_VSCROLL = new MessageCodes(0x0115);
        public static MessageCodes WM_INITMENU = new MessageCodes(0x0116);
        public static MessageCodes WM_INITMENUPOPUP = new MessageCodes(0x0117);
        public static MessageCodes WM_MENUSELECT = new MessageCodes(0x011F);
        public static MessageCodes WM_MENUCHAR = new MessageCodes(0x0120);
        public static MessageCodes WM_ENTERIDLE = new MessageCodes(0x0121);
        public static MessageCodes WM_CHANGEUISTATE = new MessageCodes(0x0127);
        public static MessageCodes WM_UPDATEUISTATE = new MessageCodes(0x0128);
        public static MessageCodes WM_QUERYUISTATE = new MessageCodes(0x0129);
        public static MessageCodes WM_CTLCOLORMSGBOX = new MessageCodes(0x0132);
        public static MessageCodes WM_CTLCOLOREDIT = new MessageCodes(0x0133);
        public static MessageCodes WM_CTLCOLORLISTBOX = new MessageCodes(0x0134);
        public static MessageCodes WM_CTLCOLORBTN = new MessageCodes(0x0135);
        public static MessageCodes WM_CTLCOLORDLG = new MessageCodes(0x0136);
        public static MessageCodes WM_CTLCOLORSCROLLBAR = new MessageCodes(0x0137);
        public static MessageCodes WM_CTLCOLORSTATIC = new MessageCodes(0x0138);
        public static MessageCodes MN_GETHMENU = new MessageCodes(0x01E1);
        public static MessageCodes WM_MOUSEFIRST = new MessageCodes(0x0200);
        public static MessageCodes WM_MOUSEMOVE = new MessageCodes(0x0200);
        public static MessageCodes WM_LBUTTONDOWN = new MessageCodes(0x0201);
        public static MessageCodes WM_LBUTTONUP = new MessageCodes(0x0202);
        public static MessageCodes WM_LBUTTONDBLCLK = new MessageCodes(0x0203);
        public static MessageCodes WM_RBUTTONDOWN = new MessageCodes(0x0204);
        public static MessageCodes WM_RBUTTONUP = new MessageCodes(0x0205);
        public static MessageCodes WM_RBUTTONDBLCLK = new MessageCodes(0x0206);
        public static MessageCodes WM_MBUTTONDOWN = new MessageCodes(0x0207);
        public static MessageCodes WM_MBUTTONUP = new MessageCodes(0x0208);
        public static MessageCodes WM_MBUTTONDBLCLK = new MessageCodes(0x0209);
        public static MessageCodes WM_MOUSEWHEEL = new MessageCodes(0x020A);
        public static MessageCodes WM_XBUTTONDOWN = new MessageCodes(0x020B);
        public static MessageCodes WM_XBUTTONUP = new MessageCodes(0x020C);
        public static MessageCodes WM_XBUTTONDBLCLK = new MessageCodes(0x020D);
        public static MessageCodes WM_MOUSEHWHEEL = new MessageCodes(0x020E);
        public static MessageCodes WM_MOUSELAST = new MessageCodes(0x020E);
        public static MessageCodes WM_PARENTNOTIFY = new MessageCodes(0x0210);
        public static MessageCodes WM_ENTERMENULOOP = new MessageCodes(0x0211);
        public static MessageCodes WM_EXITMENULOOP = new MessageCodes(0x0212);
        public static MessageCodes WM_NEXTMENU = new MessageCodes(0x0213);
        public static MessageCodes WM_SIZING = new MessageCodes(0x0214);
        public static MessageCodes WM_CAPTURECHANGED = new MessageCodes(0x0215);
        public static MessageCodes WM_MOVING = new MessageCodes(0x0216);
        public static MessageCodes WM_MOUSEHOVER = new MessageCodes(0x02A1);
        public static MessageCodes WM_MOUSELEAVE = new MessageCodes(0x02A3);
        public static MessageCodes WM_DEVMODECHANGE = new MessageCodes(0x001B);
        public static MessageCodes WM_ACTIVATEAPP = new MessageCodes(0x001C);
        public static MessageCodes WM_FONTCHANGE = new MessageCodes(0x001D);
        public static MessageCodes WM_TIMECHANGE = new MessageCodes(0x001E);
        public static MessageCodes WM_CANCELMODE = new MessageCodes(0x001F);
        public static MessageCodes WM_SETCURSOR = new MessageCodes(0x0020);
        public static MessageCodes WM_MOUSEACTIVATE = new MessageCodes(0x0021);
        public static MessageCodes WM_CHILDACTIVATE = new MessageCodes(0x0022);
        public static MessageCodes WM_QUEUESYNC = new MessageCodes(0x0023);
        public static MessageCodes WM_GETMINMAXINFO = new MessageCodes(0x0024);

        private static Dictionary<int, MessageCodes> _values;
        private MessageCodes(int value)
        {
            this.Value = value;

            if (MessageCodes._values == null)
                MessageCodes._values = new Dictionary<int, MessageCodes>();

            if (!MessageCodes._values.ContainsKey(value))
                MessageCodes._values.Add(value, this);
        }

        public int Value { get; private set; }

        public MessageCodes[] Values { get { return MessageCodes._values.Values.ToArray(); } private set { } }

        public static implicit operator int(MessageCodes mCode)
        {
            return mCode.Value;
        }

        public static implicit operator MessageCodes(int mCode)
        {
            if (MessageCodes._values.ContainsKey(mCode))
                return MessageCodes._values[mCode];
            return None;
        }

        public static bool operator ==(MessageCodes mc1, MessageCodes mc2)
        {
            return mc1.Value == mc2.Value;
        }

        public static bool operator !=(MessageCodes mc1, MessageCodes mc2)
        {
            return mc1.Value != mc2.Value;
        }

        public static bool operator ==(MessageCodes mc1, int mc2)
        {
            return mc1.Value == mc2;
        }

        public static bool operator !=(MessageCodes mc1, int mc2)
        {
            return mc1.Value != mc2;
        }

        public static bool operator ==(int mc1, MessageCodes mc2)
        {
            return mc1 == mc2.Value;
        }

        public static bool operator !=(int mc1, MessageCodes mc2)
        {
            return mc1 != mc2.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is MessageCodes || obj is int)
            {
                MessageCodes message = (MessageCodes)obj;

                return this.Value == message.Value;
            }

            try
            {
                int i = Convert.ToInt32(obj);

                return this.Value == i;
            }
            catch (Exception ex) { }

            return false;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
}
