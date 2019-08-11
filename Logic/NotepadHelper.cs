using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FileList.Logic
{
    public static class NotepadHelper
    {
        [DllImport("user32.dll")]
        private static extern int SetWindowText(IntPtr hWnd, string text);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowEx(
          IntPtr hwndParent,
          IntPtr hwndChildAfter,
          string lpszClass,
          string lpszWindow);

        [DllImport("User32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        public static void ShowMessage(string message = null, string title = null)
        {
            Process process = Process.Start(new ProcessStartInfo("notepad.exe"));
            if (process == null)
                return;
            process.WaitForInputIdle();
            if (!string.IsNullOrEmpty(title))
                NotepadHelper.SetWindowText(process.MainWindowHandle, title);
            if (!string.IsNullOrEmpty(message))
                NotepadHelper.SendMessage(NotepadHelper.FindWindowEx(process.MainWindowHandle, new IntPtr(0), "Edit", (string)null), 12, 0, message);
        }
    }
}
