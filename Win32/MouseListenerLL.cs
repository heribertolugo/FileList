using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Win32
{

    /// <summary>
    /// Low Level Mouse Listener
    /// read https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644986(v=vs.85)
    /// bottom for possible issues
    /// </summary>
    public sealed class MouseListenerLL
    {
        private static Win32.Libraries.user32.LowLevelMouseProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        private const int XBUTTON1 = 0x0001;
        private const int XBUTTON2 = 0x0002;

        #region Events
        public static EventHandler<MouseListenerEventArgs> MouseAction;
        public static EventHandler<MouseListenerEventArgs> MouseMove;
        //public static EventHandler<MouseListenerEventArgs> LeftClick;
        //public static EventHandler<MouseListenerEventArgs> RightClick;
        public static EventHandler<MouseListenerEventArgs> WheelClick;
        public static EventHandler<MouseListenerEventArgs> LeftDoubleClick;
        public static EventHandler<MouseListenerEventArgs> RightDoubleClick;
        public static EventHandler<MouseListenerEventArgs> RightDown;
        public static EventHandler<MouseListenerEventArgs> LeftDown;
        public static EventHandler<MouseListenerEventArgs> RightUp;
        public static EventHandler<MouseListenerEventArgs> LeftUp;
        public static EventHandler<MouseListenerEventArgs> WheelScroll;
        #endregion

        public static void Start()
        {
            MouseListenerLL._hookID = SetHook(_proc);
        }

        public static void Stop()
        {
            if (MouseListenerLL._hookID != IntPtr.Zero)
                Win32.Libraries.user32.UnhookWindowsHookEx(MouseListenerLL._hookID);
            MouseListenerLL._hookID = IntPtr.Zero;
        }

        public static bool IsListening { get { return MouseListenerLL._hookID != IntPtr.Zero; } }

        #region Event Raisers
        private static void OnMouseAction(MouseListenerEventArgs args)
        {
            EventHandler<MouseListenerEventArgs> handler = MouseAction;

            if (handler != null)
                handler(null, args);
        }
        private static void OnMouseMove(MouseListenerEventArgs args)
        {
            EventHandler<MouseListenerEventArgs> handler = MouseMove;

            if (handler != null)
                handler(null, args);
        }
        //private static void OnLeftClick(Win32.Models.MSLLHOOKSTRUCT mSLLHOOKSTRUCT)
        //{
        //    EventHandler<MouseListenerEventArgs> handler = LeftClick;

        //    if (handler != null)
        //        handler(null, new MouseListenerEventArgs(mSLLHOOKSTRUCT));
        //}
        //private static void OnRightClick(Win32.Models.MSLLHOOKSTRUCT mSLLHOOKSTRUCT)
        //{
        //    EventHandler<MouseListenerEventArgs> handler = RightClick;

        //    if (handler != null)
        //        handler(null, new MouseListenerEventArgs(mSLLHOOKSTRUCT));
        //}
        private static void OnWheelClick(MouseListenerEventArgs args)
        {
            EventHandler<MouseListenerEventArgs> handler = WheelClick;

            if (handler != null)
                handler(null, args);
        }
        private static void OnLeftDoubleClick(MouseListenerEventArgs args)
        {
            EventHandler<MouseListenerEventArgs> handler = LeftDoubleClick;

            if (handler != null)
                handler(null, args);
        }
        private static void OnRightDoubleClick(MouseListenerEventArgs args)
        {
            EventHandler<MouseListenerEventArgs> handler = RightDoubleClick;

            if (handler != null)
                handler(null, args);
        }
        private static void OnRightDown(MouseListenerEventArgs args)
        {
            EventHandler<MouseListenerEventArgs> handler = RightDown;

            if (handler != null)
                handler(null, args);
        }
        private static void OnLeftDown(MouseListenerEventArgs args)
        {
            EventHandler<MouseListenerEventArgs> handler = LeftDown;

            if (handler != null)
                handler(null, args);
        }
        private static void OnRightUp(MouseListenerEventArgs args)
        {
            EventHandler<MouseListenerEventArgs> handler = RightUp;

            if (handler != null)
                handler(null, args);
        }
        private static void OnLeftUp(MouseListenerEventArgs args)
        {
            EventHandler<MouseListenerEventArgs> handler = LeftUp;

            if (handler != null)
                handler(null, args);
        }
        private static void OnWheelScroll(MouseListenerEventArgs args)
        {
            EventHandler<MouseListenerEventArgs> handler = WheelScroll;

            if (handler != null)
                handler(null, args);
        }
        #endregion

        #region Helpers
        private static IntPtr SetHook(Win32.Libraries.user32.LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return Win32.Libraries.user32.SetWindowsHookEx(Win32.Constants.WindowHooks.WH_MOUSE_LL, proc,
                        Win32.Libraries.kernal32.GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }

        // do we need monitor awareness??
        // https://docs.microsoft.com/en-us/windows/win32/api/shellscalingapi/ne-shellscalingapi-process_dpi_awareness
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                MouseListenerEventArgs args = null;
                Win32.Models.MSLLHOOKSTRUCT hookStruct = (Win32.Models.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Win32.Models.MSLLHOOKSTRUCT));
                Win32.Models.DWORD mouseData = new Win32.Models.DWORD(hookStruct.mouseData);
                Win32.Constants.MessageCodes messageCode = (int)wParam;
                Win32.Constants.HitTestMousePositionCodes hitTest = mouseData.Low;

                //Console.WriteLine("{0:X}", messageCode.Value);

                if (messageCode == Win32.Constants.MessageCodes.WM_MOUSEWHEEL)
                {
                    Direction scroll = mouseData.High == 120 ? Direction.Up : (mouseData.High == 0 ? Direction.None : Direction.Down);
                    Win32.MouseAction mouseAction = scroll == Direction.Up ? Win32.MouseAction.WheelScrollUp : (scroll == Direction.Down ? Win32.MouseAction.WheelScrollDown : Win32.MouseAction.None);
                    args = new MouseListenerEventArgs(hookStruct.pt, scroll, Direction.None, false, MouseButtonClick.Wheel, mouseAction, hitTest);
                    OnMouseAction(args);
                    OnWheelScroll(args);
                }
                else if (messageCode == Win32.Constants.MessageCodes.WM_XBUTTONDBLCLK || messageCode == Win32.Constants.MessageCodes.WM_LBUTTONDBLCLK || messageCode == Win32.Constants.MessageCodes.WM_RBUTTONDBLCLK)
                {
                    if (mouseData.High == XBUTTON1 || messageCode == Win32.Constants.MessageCodes.WM_LBUTTONDBLCLK)
                    {
                        args = new MouseListenerEventArgs(hookStruct.pt, Direction.None, Direction.Up, true, MouseButtonClick.Left, Win32.MouseAction.LeftDoubleClick, hitTest);
                        OnMouseAction(args);
                        OnLeftDoubleClick(args);
                    }
                    else if (mouseData.High == XBUTTON2 || messageCode == Win32.Constants.MessageCodes.WM_RBUTTONDBLCLK)
                    {
                        args = new MouseListenerEventArgs(hookStruct.pt, Direction.None, Direction.Up, true, MouseButtonClick.Right, Win32.MouseAction.RightDoubleClick, hitTest);
                        OnMouseAction(args);
                        OnRightDoubleClick(args);
                    }
                    else
                    {
                        // ?? wth ??
                    }
                }
                else if (messageCode == Win32.Constants.MessageCodes.WM_XBUTTONDOWN || messageCode == Win32.Constants.MessageCodes.WM_LBUTTONDOWN || messageCode == Win32.Constants.MessageCodes.WM_RBUTTONDOWN)
                {
                    if (mouseData.High == XBUTTON1 || messageCode == Win32.Constants.MessageCodes.WM_LBUTTONDOWN)
                    {
                        args = new MouseListenerEventArgs(hookStruct.pt, Direction.None, Direction.Down, false, MouseButtonClick.Left, Win32.MouseAction.LeftDown, hitTest);
                        OnMouseAction(args);
                        OnLeftDown(args);
                    }
                    else if (mouseData.High == XBUTTON2 || messageCode == Win32.Constants.MessageCodes.WM_RBUTTONDOWN)
                    {
                        args = new MouseListenerEventArgs(hookStruct.pt, Direction.None, Direction.Down, false, MouseButtonClick.Right, Win32.MouseAction.RightDown, hitTest);
                        OnMouseAction(args);
                        OnRightDown(args);
                    }
                    else
                    {
                        // ?? wth ?? wheel click????????
                    }
                }
                else if (messageCode == Win32.Constants.MessageCodes.WM_XBUTTONUP || messageCode == Win32.Constants.MessageCodes.WM_LBUTTONUP || messageCode == Win32.Constants.MessageCodes.WM_RBUTTONUP)
                {
                    if (mouseData.High == XBUTTON1 || messageCode == Win32.Constants.MessageCodes.WM_LBUTTONUP)
                    {
                        args = new MouseListenerEventArgs(hookStruct.pt, Direction.None, Direction.Up, false, MouseButtonClick.Left, Win32.MouseAction.LeftUp, hitTest);
                        OnMouseAction(args);
                        OnLeftUp(args);
                    }
                    else if (mouseData.High == XBUTTON2 || messageCode == Win32.Constants.MessageCodes.WM_RBUTTONUP)
                    {
                        args = new MouseListenerEventArgs(hookStruct.pt, Direction.None, Direction.Up, false, MouseButtonClick.Right, Win32.MouseAction.RightUp, hitTest);
                        OnMouseAction(args);
                        OnRightUp(args);
                    }
                    else
                    {
                        // ?? wth ?? wheel click????????
                    }
                }
                else if (messageCode == Win32.Constants.MessageCodes.WM_NCXBUTTONDOWN)
                {
                    if (mouseData.High == XBUTTON1)
                    {
                        args = new MouseListenerEventArgs(hookStruct.pt, Direction.None, Direction.Down, false, MouseButtonClick.Left, Win32.MouseAction.LeftDown, hitTest);
                        OnMouseAction(args);
                        OnLeftDown(args);
                    }
                    else if (mouseData.High == XBUTTON2)
                    {
                        args = new MouseListenerEventArgs(hookStruct.pt, Direction.None, Direction.Down, false, MouseButtonClick.Right, Win32.MouseAction.RightDown, hitTest);
                        OnMouseAction(args);
                        OnRightDown(args);
                    }
                    else
                    {
                        // ?? wth ?? wheel click????????
                    }
                }
                else if (messageCode == Win32.Constants.MessageCodes.WM_NCXBUTTONUP)
                {
                    if (mouseData.High == XBUTTON1)
                    {
                        args = new MouseListenerEventArgs(hookStruct.pt, Direction.None, Direction.Up, false, MouseButtonClick.Left, Win32.MouseAction.LeftUp, hitTest);
                        OnMouseAction(args);
                        OnLeftUp(args);
                    }
                    else if (mouseData.High == XBUTTON2)
                    {
                        args = new MouseListenerEventArgs(hookStruct.pt, Direction.None, Direction.Up, false, MouseButtonClick.Right, Win32.MouseAction.RightUp, hitTest);
                        OnMouseAction(args);
                        OnRightUp(args);
                    }
                    else
                    {
                        // ?? wth ??
                    }
                }
                else if (messageCode == Win32.Constants.MessageCodes.WM_NCXBUTTONDBLCLK)
                {
                    if (mouseData.High == XBUTTON1)
                    {
                        args = new MouseListenerEventArgs(hookStruct.pt, Direction.None, Direction.Up, true, MouseButtonClick.Left, Win32.MouseAction.LeftDoubleClick, hitTest);
                        OnMouseAction(args);
                        OnLeftDoubleClick(args);
                    }
                    else if (mouseData.High == XBUTTON2)
                    {
                        args = new MouseListenerEventArgs(hookStruct.pt, Direction.None, Direction.Up, true, MouseButtonClick.Right, Win32.MouseAction.RightDoubleClick, hitTest);
                        OnMouseAction(args);
                        OnRightDoubleClick(args);
                    }
                    else
                    {
                        // ?? wth ??
                    }
                }
                else if (messageCode == Win32.Constants.MessageCodes.WM_MOUSEMOVE || messageCode == Win32.Constants.MessageCodes.WM_NCMOUSEMOVE || messageCode == Win32.Constants.MessageCodes.WM_MOVING)
                {
                    args = new MouseListenerEventArgs(hookStruct.pt, Direction.None, Direction.None, false, MouseButtonClick.None, Win32.MouseAction.MouseMove, hitTest);
                    OnMouseAction(args);
                    OnMouseMove(args);
                }
            }

            return Win32.Libraries.user32.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
        #endregion
    }

    public class MouseListenerEventArgs : EventArgs
    {
        public MouseListenerEventArgs(System.Drawing.Point location, Direction scrollDirection, Direction clickStatus, bool isDoubleClick, MouseButtonClick buttonClick, MouseAction action, Win32.Constants.HitTestMousePositionCodes hitTest)
        {
            this.Location = location;
            this.ScrollDirection = scrollDirection;
            this.ClickStatus = clickStatus;
            this.IsDoubleClick = isDoubleClick;
            this.ButtonClick = buttonClick;
            this.Action = action;
            this.HitTest = hitTest;
        }

        public System.Drawing.Point Location { get; private set; }
        public Direction ScrollDirection { get; private set; }
        public Direction ClickStatus { get; private set; }
        public bool IsDoubleClick { get; private set; }
        public MouseButtonClick ButtonClick { get; private set; }
        public MouseAction Action { get; private set; }
        public Win32.Constants.HitTestMousePositionCodes HitTest { get; private set; }
    }

    public enum Direction
    {
        None,
        Up,
        Down
    }

    public enum MouseButtonClick
    {
        None,
        Left,
        Right,
        Wheel
    }

    public enum MouseAction
    {
        None,
        LeftDown,
        LeftUp,
        LeftClick,
        LeftDoubleClick,
        RightDown,
        RightUp,
        RightClick,
        RightDoubleClick,
        WheelDown,
        WheelUp,
        WheelClick,
        WheelDoubleClick,
        WheelScrollUp,
        WheelScrollDown,
        MouseMove
    }
}
