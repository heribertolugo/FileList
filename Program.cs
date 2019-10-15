using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FileList
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Models.Win32.Win32Methods.AttachConsole(Models.Win32.MiscConstants.ATTACH_PARENT_PROCESS);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Views.MainForm());
        }
    }
}
