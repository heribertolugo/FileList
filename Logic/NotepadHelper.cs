using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using FileList.Models;

namespace FileList.Logic
{
    public static class Notepad
    {

        public static void ShowMessage(string message = null, string title = null)
        {
            Process process = Process.Start(new ProcessStartInfo("notepad.exe"));
            if (process == null)
                return;
            process.WaitForInputIdle();
            if (!string.IsNullOrEmpty(title))
                Models.Win32.Win32Methods.SetWindowText(process.MainWindowHandle, title);
            if (!string.IsNullOrEmpty(message))
                 Models.Win32.Win32Methods.SendMessage(Models.Win32.Win32Methods.FindWindowEx(process.MainWindowHandle, new IntPtr(0), "Edit", null), 12, 0, message);
        }
    }
}
