using System;
using System.Diagnostics;
using Win32.Libraries;

namespace FileList.Logic
{
    public static class Notepad
    {
        private const int WM_SETTEXT = 0x000c;
        public static void ShowMessage(string message = null, string title = null)
        {
            Process process = Process.Start(new ProcessStartInfo("notepad.exe"));
            if (process == null)
                return;
            process.WaitForInputIdle();
            if (!string.IsNullOrEmpty(title))
                user32.SetWindowText(process.MainWindowHandle, title);
            if (!string.IsNullOrEmpty(message))
                user32.SendMessage(user32.FindWindowEx(process.MainWindowHandle, new IntPtr(0), "Edit", null), WM_SETTEXT, 0, message);
        }
    }
}
