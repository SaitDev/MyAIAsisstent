using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Media;

namespace MyAIAsisstent
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        public static Main _main;
        static bool NoMutexException = false;
        public static bool silentStart = false;
        static Mutex mutex = new Mutex(false, "MyAIAsisstent");
        static int timeRetry = 3;
        

        [STAThread]
        static void Main(string[] args)
        {
            do
            {
                Debug.WriteLine("  " + timeRetry.ToString());
                try
                {
                    if (mutex.WaitOne(TimeSpan.Zero, true))
                    {
                        NoMutexException = false;
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        if (Properties.Settings.Default.UpdateSettingsRequired)
                        {
                            Properties.Settings.Default.Upgrade();
                            Properties.Settings.Default.UpdateSettingsRequired = false;
                            Properties.Settings.Default.Save();
                        }
                        foreach (string arg in args)
                        {
                            if (arg.ToUpper() == "-SILENTSTART") silentStart = true;
                        }
                        _main = new Main();
                        Application.Run(_main);
                        mutex.ReleaseMutex();
                    }
                    else
                    {
                        NativeMethods.PostMessage((IntPtr)NativeMethods.HWND_BROADCAST, NativeMethods.WM_SHOWME,
                            IntPtr.Zero, IntPtr.Zero);
                    }
                }
                catch (AbandonedMutexException exc)
                {
                    NoMutexException = true;
                    Debug.WriteLine("\n*****   *****  ***** Found an AbandonedMutexException. Retry now... \n");
                    mutex.ReleaseMutex();
                    timeRetry--;
                }
            }
            while (NoMutexException && timeRetry > 0);
        }
    }
}
