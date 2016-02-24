using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MyAIAsisstent
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        public static Main _main;
        static Mutex mutex = new Mutex(true, "MyAIAsisstent");
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (Properties.Settings.Default.UpdateSettingsRequired)
                {
                    Properties.Settings.Default.Upgrade();
                    Properties.Settings.Default.UpdateSettingsRequired = false;
                    Properties.Settings.Default.Save();
                }
                _main = new Main();
                Application.Run();
                mutex.ReleaseMutex();
            }
            else
            {
                NativeMethods.PostMessage((IntPtr)NativeMethods.HWND_BROADCAST, NativeMethods.WM_SHOWME,
                    IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}
