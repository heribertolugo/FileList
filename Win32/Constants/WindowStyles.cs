using System;
using System.Collections.Generic;
using System.Linq;

namespace Win32.Constants
{
    /// <summary>
    /// The following are the window styles. After the window has been created, these styles cannot be modified, except as noted.
    /// </summary>
    public struct WindowStyles
    {
        /// <summary>
        /// Represents no known value.
        /// </summary>
        public static WindowStyles None = new WindowStyles(-1L);
        /// <summary>
        /// The window has a thin-line border.
        /// </summary>
        public static WindowStyles WS_BORDER = new  WindowStyles(0x00800000L);
        /// <summary>
        /// The window has a title bar (includes the WS_BORDER style).
        /// </summary>
        public static WindowStyles WS_CAPTION = new  WindowStyles(0x00C00000L);
        /// <summary>
        /// The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style.
        /// </summary>
        public static WindowStyles WS_CHILD = new  WindowStyles(0x40000000L);
        /// <summary>
        /// Same as the WS_CHILD style.
        /// </summary>
        public static WindowStyles WS_CHILDWINDOW = new  WindowStyles(0x40000000L);
        /// <summary>
        /// Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when creating the parent window.
        /// </summary>
        public static WindowStyles WS_CLIPCHILDREN = new  WindowStyles(0x02000000L);
        /// <summary>
        /// Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be updated. If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the client area of a child window, to draw within the client area of a neighboring child window.
        /// </summary>
        public static WindowStyles WS_CLIPSIBLINGS = new  WindowStyles(0x04000000L);
        /// <summary>
        /// The window is initially disabled. A disabled window cannot receive input from the user. To change this after a window has been created, use the EnableWindow function.
        /// </summary>
        public static WindowStyles WS_DISABLED = new  WindowStyles(0x08000000L);
        /// <summary>
        /// The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title bar.
        /// </summary>
        public static WindowStyles WS_DLGFRAME = new  WindowStyles(0x00400000L);
        /// <summary>
        /// The window is the first control of a group of controls. The group consists of this first control and all controls defined after it, up to the next control with the WS_GROUP style. The first control in each group usually has the WS_TABSTOP style so that the user can move from group to group. The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys.
        /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
        /// </summary>
        public static WindowStyles WS_GROUP = new  WindowStyles(0x00020000L);
        /// <summary>
        /// The window has a horizontal scroll bar.
        /// </summary>
        public static WindowStyles WS_HSCROLL = new  WindowStyles(0x00100000L);
        /// <summary>
        /// The window is initially minimized. Same as the WS_MINIMIZE style.
        /// </summary>
        public static WindowStyles WS_ICONIC = new  WindowStyles(0x20000000L);
        /// <summary>
        /// The window is initially maximized.
        /// </summary>
        public static WindowStyles WS_MAXIMIZE = new  WindowStyles(0x01000000L);
        /// <summary>
        /// The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.
        /// </summary>
        public static WindowStyles WS_MAXIMIZEBOX = new  WindowStyles(0x00010000L);
        /// <summary>
        /// The window is initially minimized. Same as the WS_ICONIC style.
        /// </summary>
        public static WindowStyles WS_MINIMIZE = new  WindowStyles(0x20000000L);
        /// <summary>
        /// The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.
        /// </summary>
        public static WindowStyles WS_MINIMIZEBOX = new  WindowStyles(0x00020000L);
        /// <summary>
        /// The window is an overlapped window. An overlapped window has a title bar and a border. Same as the WS_TILED style.
        /// </summary>
        public static WindowStyles WS_OVERLAPPED = new  WindowStyles(0x00000000L);
        /// <summary>
        /// The window is an overlapped window. Same as the WS_TILEDWINDOW style.
        /// WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX
        /// </summary>
        public static WindowStyles WS_OVERLAPPEDWINDOW = new  WindowStyles(0x00000000L | 0x00C00000L | 0x00080000L | 0x00040000L | 0x00020000L | 0x00010000L);
        /// <summary>
        /// The windows is a pop-up window. This style cannot be used with the WS_CHILD style.
        /// </summary>
        public static WindowStyles WS_POPUP = new  WindowStyles(0x80000000L);
        /// <summary>
        /// The window is a pop-up window. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible.
        /// </summary>
        public static WindowStyles WS_POPUPWINDOW = new  WindowStyles(0x80000000L | 0x00800000L | 0x00080000L);
        /// <summary>
        /// The window has a sizing border. Same as the WS_THICKFRAME style.
        /// </summary>
        public static WindowStyles WS_SIZEBOX = new  WindowStyles(0x00040000L);
        /// <summary>
        /// The window has a window menu on its title bar. The WS_CAPTION style must also be specified.
        /// </summary>
        public static WindowStyles WS_SYSMENU = new  WindowStyles(0x00080000L);
        /// <summary>
        /// The window is a control that can receive the keyboard focus when the user presses the TAB key. Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style.
        /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function. For user-created windows and modeless dialogs to work with tab stops, alter the message loop to call the IsDialogMessage function.
        /// </summary>
        public static WindowStyles WS_TABSTOP = new  WindowStyles(0x00010000L);
        /// <summary>
        /// The window has a sizing border. Same as the WS_SIZEBOX style.
        /// </summary>
        public static WindowStyles WS_THICKFRAME = new  WindowStyles(0x00040000L);
        /// <summary>
        /// The window is an overlapped window. An overlapped window has a title bar and a border. Same as the WS_OVERLAPPED style.
        /// </summary>
        public static WindowStyles WS_TILED = new  WindowStyles(0x00000000L);
        /// <summary>
        /// The window is an overlapped window. Same as the WS_OVERLAPPEDWINDOW style.
        /// </summary>
        public static WindowStyles WS_TILEDWINDOW = new  WindowStyles(0x00000000L | 0x00C00000L | 0x00080000L | 0x00040000L | 0x00020000L | 0x00010000L);
        /// <summary>
        /// The window is initially visible.
        /// This style can be turned on and off by using the ShowWindow or SetWindowPos function.
        /// </summary>
        public static WindowStyles WS_VISIBLE = new  WindowStyles(0x10000000L);
        /// <summary>
        /// The window has a vertical scroll bar.
        /// </summary>
        public static WindowStyles WS_VSCROLL = new  WindowStyles(0x00200000L);

        private static Dictionary<long, WindowStyles> _values;
        private WindowStyles(long value)
        {
            this.Value = value;

            if (WindowStyles._values == null)
                WindowStyles._values = new Dictionary<long, WindowStyles>();

            if (!WindowStyles._values.ContainsKey(value))
                WindowStyles._values.Add(value, this);
        }

        public long Value { get; private set; }
        public WindowStyles[] Values { get { return WindowStyles._values.Values.ToArray(); } private set { } }

        public static implicit operator long(WindowStyles mCode)
        {
            return mCode.Value;
        }

        public static implicit operator WindowStyles(long mCode)
        {
            if (WindowStyles._values.ContainsKey(mCode))
                return WindowStyles._values[mCode];
            return None;
        }

        public static bool operator ==(WindowStyles mc1, WindowStyles mc2)
        {
            return mc1.Value == mc2.Value;
        }

        public static bool operator !=(WindowStyles mc1, WindowStyles mc2)
        {
            return mc1.Value != mc2.Value;
        }

        public static bool operator ==(WindowStyles mc1, long mc2)
        {
            return mc1.Value == mc2;
        }

        public static bool operator !=(WindowStyles mc1, long mc2)
        {
            return mc1.Value != mc2;
        }

        public static bool operator ==(long mc1, WindowStyles mc2)
        {
            return mc1 == mc2.Value;
        }

        public static bool operator !=(long mc1, WindowStyles mc2)
        {
            return mc1 != mc2.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is WindowStyles || obj is long)
            {
                WindowStyles message = (WindowStyles)obj;

                return this.Value == message.Value;
            }

            try
            {
                long i = Convert.ToInt64(obj);

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
