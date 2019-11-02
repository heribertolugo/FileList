using System;
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
           Win32.Libraries.kernal32.AttachConsole(Win32.Constants.System.ATTACH_PARENT_PROCESS);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Views.MainForm());
        }
    }
}
