using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Win32.Constants
{
    public struct WindowStylesEx
    {
        ///// <summary>
        ///// Represents no known value.
        ///// </summary>
        // public static WindowStylesEx None = new WindowStylesEx(0x00000000);
        /// <summary>Specifies a window that accepts drag-drop files.</summary>
        public static WindowStylesEx WS_EX_ACCEPTFILES = new WindowStylesEx(0x00000010);

        /// <summary>Forces a top-level window onto the taskbar when the window is visible.</summary>
        public static WindowStylesEx WS_EX_APPWINDOW = new WindowStylesEx(0x00040000);

        /// <summary>Specifies a window that has a border with a sunken edge.</summary>
        public static WindowStylesEx WS_EX_CLIENTEDGE = new WindowStylesEx(0x00000200);

        /// <summary>
        /// Specifies a window that paints all descendants in bottom-to-top painting order using double-buffering.
        /// This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC. This style is not supported in Windows 2000.
        /// </summary>
        /// <remarks>
        /// With WS_EX_COMPOSITED set, all descendants of a window get bottom-to-top painting order using double-buffering.
        /// Bottom-to-top painting order allows a descendent window to have translucency (alpha) and transparency (color-key) effects,
        /// but only if the descendent window also has the WS_EX_TRANSPARENT bit set.
        /// Double-buffering allows the window and its descendents to be painted without flicker.
        /// </remarks>
        public static WindowStylesEx WS_EX_COMPOSITED = new WindowStylesEx(0x02000000);

        /// <summary>
        /// Specifies a window that includes a question mark in the title bar. When the user clicks the question mark,
        /// the cursor changes to a question mark with a pointer. If the user then clicks a child window, the child receives a WM_HELP message.
        /// The child window should pass the message to the parent window procedure, which should call the WinHelp function using the HELP_WM_HELP command.
        /// The Help application displays a pop-up window that typically contains help for the child window.
        /// WS_EX_CONTEXTHELP cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.
        /// </summary>
        public static WindowStylesEx WS_EX_CONTEXTHELP = new WindowStylesEx(0x00000400);

        /// <summary>
        /// Specifies a window which contains child windows that should take part in dialog box navigation.
        /// If this style is specified, the dialog manager recurses into children of this window when performing navigation operations
        /// such as handling the TAB key, an arrow key, or a keyboard mnemonic.
        /// </summary>
        public static WindowStylesEx WS_EX_CONTROLPARENT = new WindowStylesEx(0x00010000);

        /// <summary>Specifies a window that has a double border.</summary>
        public static WindowStylesEx WS_EX_DLGMODALFRAME = new WindowStylesEx(0x00000001);

        /// <summary>
        /// Specifies a window that is a layered window.
        /// This cannot be used for child windows or if the window has a class style of either CS_OWNDC or CS_CLASSDC.
        /// </summary>
        public static WindowStylesEx WS_EX_LAYERED = new WindowStylesEx(0x00080000);

        /// <summary>
        /// Specifies a window with the horizontal origin on the right edge. Increasing horizontal values advance to the left.
        /// The shell language must support reading-order alignment for this to take effect.
        /// </summary>
        public static WindowStylesEx WS_EX_LAYOUTRTL = new WindowStylesEx(0x00400000);

        /// <summary>Specifies a window that has generic left-aligned properties. This is the default.</summary>
        public static WindowStylesEx WS_EX_LEFT = new WindowStylesEx(0x00000000);

        /// <summary>
        /// Specifies a window with the vertical scroll bar (if present) to the left of the client area.
        /// The shell language must support reading-order alignment for this to take effect.
        /// </summary>
        public static WindowStylesEx WS_EX_LEFTSCROLLBAR = new WindowStylesEx(0x00004000);

        /// <summary>
        /// Specifies a window that displays text using left-to-right reading-order properties. This is the default.
        /// </summary>
        public static WindowStylesEx WS_EX_LTRREADING = new WindowStylesEx(0x00000000);

        /// <summary>
        /// Specifies a multiple-document interface (MDI) child window.
        /// </summary>
        public static WindowStylesEx WS_EX_MDICHILD = new WindowStylesEx(0x00000040);

        /// <summary>
        /// Specifies a top-level window created with this style does not become the foreground window when the user clicks it.
        /// The system does not bring this window to the foreground when the user minimizes or closes the foreground window.
        /// The window does not appear on the taskbar by default. To force the window to appear on the taskbar, use the WS_EX_APPWINDOW style.
        /// To activate the window, use the SetActiveWindow or SetForegroundWindow function.
        /// </summary>
        public static WindowStylesEx WS_EX_NOACTIVATE = new WindowStylesEx(0x08000000);

        /// <summary>
        /// Specifies a window which does not pass its window layout to its child windows.
        /// </summary>
        public static WindowStylesEx WS_EX_NOINHERITLAYOUT = new WindowStylesEx(0x00100000);

        /// <summary>
        /// Specifies that a child window created with this style does not send the WM_PARENTNOTIFY message to its parent window when it is created or destroyed.
        /// </summary>
        public static WindowStylesEx WS_EX_NOPARENTNOTIFY = new WindowStylesEx(0x00000004);

        /// <summary>
        /// The window does not render to a redirection surface.
        /// This is for windows that do not have visible content or that use mechanisms other than surfaces to provide their visual.
        /// </summary>
        public static WindowStylesEx WS_EX_NOREDIRECTIONBITMAP = new WindowStylesEx(0x00200000);

        /// <summary>Specifies an overlapped window.</summary>
        public static WindowStylesEx WS_EX_OVERLAPPEDWINDOW = new WindowStylesEx(0x00000100 | 0x00000200);

        /// <summary>Specifies a palette window, which is a modeless dialog box that presents an array of commands.</summary>
        public static WindowStylesEx WS_EX_PALETTEWINDOW = new WindowStylesEx(0x00000100 | 0x00000080 | 0x00000008);

        /// <summary>
        /// Specifies a window that has generic "right-aligned" properties. This depends on the window class.
        /// The shell language must support reading-order alignment for this to take effect.
        /// Using the WS_EX_RIGHT style has the same effect as using the SS_RIGHT (static), ES_RIGHT (edit), and BS_RIGHT/BS_RIGHTBUTTON (button) control styles.
        /// </summary>
        public static WindowStylesEx WS_EX_RIGHT = new WindowStylesEx(0x00001000);

        /// <summary>Specifies a window with the vertical scroll bar (if present) to the right of the client area. This is the default.</summary>
        public static WindowStylesEx WS_EX_RIGHTSCROLLBAR = new WindowStylesEx(0x00000000);

        /// <summary>
        /// Specifies a window that displays text using right-to-left reading-order properties.
        /// The shell language must support reading-order alignment for this to take effect.
        /// </summary>
        public static WindowStylesEx WS_EX_RTLREADING = new WindowStylesEx(0x00002000);

        /// <summary>Specifies a window with a three-dimensional border style intended to be used for items that do not accept user input.</summary>
        public static WindowStylesEx WS_EX_STATICEDGE = new WindowStylesEx(0x00020000);

        /// <summary>
        /// Specifies a window that is intended to be used as a floating toolbar.
        /// A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font.
        /// A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB.
        /// If a tool window has a system menu, its icon is not displayed on the title bar.
        /// However, you can display the system menu by right-clicking or by typing ALT+SPACE.
        /// </summary>
        public static WindowStylesEx WS_EX_TOOLWINDOW = new WindowStylesEx(0x00000080);

        /// <summary>
        /// Specifies a window that should be placed above all non-topmost windows and should stay above them, even when the window is deactivated.
        /// To add or remove this style, use the SetWindowPos function.
        /// </summary>
        public static WindowStylesEx WS_EX_TOPMOST = new WindowStylesEx(0x00000008);

        /// <summary>
        /// Specifies a window that should not be painted until siblings beneath the window (that were created by the same thread) have been painted.
        /// The window appears transparent because the bits of underlying sibling windows have already been painted.
        /// To achieve transparency without these restrictions, use the SetWindowRgn function.
        /// </summary>
        public static WindowStylesEx WS_EX_TRANSPARENT = new WindowStylesEx(0x00000020);

        /// <summary>Specifies a window that has a border with a raised edge.</summary>
        public static WindowStylesEx WS_EX_WINDOWEDGE = new WindowStylesEx(0x00000100);

        private static Dictionary<uint, WindowStylesEx> _values;

        private WindowStylesEx(uint value)
        {
            this.Value = value;

            if (WindowStylesEx._values == null)
                WindowStylesEx._values = new Dictionary<uint, WindowStylesEx>();

            if (!WindowStylesEx._values.ContainsKey(value))
                WindowStylesEx._values.Add(value, this);
        }

        public uint Value { get; private set; }
        public WindowStylesEx[] Values { get { return WindowStylesEx._values.Values.ToArray(); } private set { } }

        public static implicit operator uint(WindowStylesEx mCode)
        {
            return mCode.Value;
        }

        public static implicit operator WindowStylesEx(uint mCode)
        {
            if (WindowStylesEx._values.ContainsKey(mCode))
                return WindowStylesEx._values[mCode];
            return WS_EX_LEFT;
        }

        public static bool operator ==(WindowStylesEx mc1, WindowStylesEx mc2)
        {
            return mc1.Value == mc2.Value;
        }

        public static bool operator !=(WindowStylesEx mc1, WindowStylesEx mc2)
        {
            return mc1.Value != mc2.Value;
        }

        public static bool operator ==(WindowStylesEx mc1, uint mc2)
        {
            return mc1.Value == mc2;
        }

        public static bool operator !=(WindowStylesEx mc1, uint mc2)
        {
            return mc1.Value != mc2;
        }

        public static bool operator ==(uint mc1, WindowStylesEx mc2)
        {
            return mc1 == mc2.Value;
        }

        public static bool operator !=(uint mc1, WindowStylesEx mc2)
        {
            return mc1 != mc2.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is WindowStylesEx || obj is uint)
            {
                WindowStylesEx message = (WindowStylesEx)obj;

                return this.Value == message.Value;
            }

            try
            {
                uint i = Convert.ToUInt32(obj);

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
