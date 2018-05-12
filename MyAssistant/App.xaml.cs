using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using MyAssistant.Properties;
using MyAssistant.WinAPI;
using MyAssistant.Network;
using MyAssistant.Utils;

namespace MyAssistant
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Hotspot hotspot;
        bool lazyStart = false;

        void Starting(object sender, StartupEventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                switch (args[1])
                {
                        break;
                    case "--autostart":
                        lazyStart = true;
                        break;
                }
            }

            if (lazyStart)
            {
                if (Settings.Default.Hotspot == null) return;
                hotspot = new Hotspot(null);
                Task.Delay(5000).ContinueWith(async (dontCare) =>
                {
                    var connect = await hotspot.Connect(Settings.Default.Hotspot);
                    if (connect != Hotspot.Status.Connected) DebugHelper.File("Fail to auto hotspot");
                    Environment.Exit(0);
                });
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                MainWindow = mainWindow;
                mainWindow.Show();
            }
        }
    }
}
