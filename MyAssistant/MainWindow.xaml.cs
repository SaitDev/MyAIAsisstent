using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;
using SimpleWifi;

using MyAssistant.Properties;
using MyAssistant.WinAPI;
using MyAssistant.Database.Hotspot;
using MyAssistant.Network;
using MyAssistant.Utils;

namespace MyAssistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Wifi wifi;
        Hotspot hotspot;
        RegistryKey regKey;

        public MainWindow()
        {
            InitializeComponent();
        }

        void Load(object sender, RoutedEventArgs e)
        {
            wifi = new Wifi();
            hotspot = new Hotspot(this);
            regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (Settings.Default.Hotspot != null)
            {
                Task.Delay(1000).ContinueWith(_ =>
                {
                    if (!NetworkUtil.IsConnectedToInternet) Connect();
                });
            }

            Task.Delay(5000).ContinueWith(_ =>
            {
                new Updater().Update();
            });
        }

        void AutoHotspot(object sender, RoutedEventArgs e)
        {
            wifiList.SelectedIndex = -1;
            foreach (var ap in wifi.GetAccessPoints())
            {
                wifiList.Items.Add(ap.Name);
            }

            if (Settings.Default.Hotspot != null)
            {
                Task.Delay(500).ContinueWith(_ =>
                {
                    Dispatcher.Invoke(() => { wifiList.Text = Settings.Default.Hotspot.Device; });
                });
                username.Text = Settings.Default.Hotspot.Username;
                password.Password = "";
            }

            settingPanel.Visibility = Visibility.Visible;
        }

        void RefreshWifi(object sender, RoutedEventArgs e)
        {
            wifiList.SelectedIndex = -1;
            foreach (var ap in wifi.GetAccessPoints())
            {
                wifiList.Items.Add(ap.Name);
            }
            Task.Delay(500).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() => 
                {
                    if (Settings.Default.Hotspot.Device != null &&
                        wifiList.Items.Contains(Settings.Default.Hotspot.Device))
                    {
                        wifiList.Text = Settings.Default.Hotspot.Device;
                    }
                });
            });
        }

        void SaveSetting(object sender, RoutedEventArgs e)
        {
            if (wifiList.SelectedIndex < 0 || username.Text == "" 
                || (password.Password == "" && Settings.Default.Hotspot == null))
            {
                MessageBox.Show("Dont forget your hotspot info");
                return;
            }

            if (Settings.Default.Hotspot == null)
            {
                Settings.Default.Hotspot = new Credential()
                {
                    Device = wifiList.Text,
                    Username = username.Text,
                    Password = password.Password
                };
            }
            else
            {
                Settings.Default.Hotspot.Device = wifiList.Text;
                Settings.Default.Hotspot.Username = username.Text;
                if (password.Password != "") Settings.Default.Hotspot.Password = password.Password;
            }
            Settings.Default.Save();

            if (Settings.Default.AutoStart)
            {
                regKey.SetValue("MyAssistant", string.Format("\"{0}\" --autostart", System.Reflection.Assembly.GetExecutingAssembly().Location));
            }
            else
            {
                regKey.DeleteValue("MyAssistant", false);
            }

            Task.Delay(300).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() =>
                {
                    settingPanel.Visibility = Visibility.Collapsed;
                    wifiList.Items.Clear();
                    password.Clear();
                });
            });

            Connect();
        }

        async void Connect()
        {
            if (IsVisible) Dispatcher.Invoke(() =>
            { 
                    snackbarMessage.MessageQueue.Enqueue("Connecting you to the world");
            });
            var connect = await hotspot.Connect(Settings.Default.Hotspot);
            Dispatcher.Invoke(() =>
            {
                switch (connect)
                {
                    case Hotspot.Status.Connected:
                        snackbarMessage.MessageQueue.Enqueue($"You are now connected with Alien ( ͡° ͜ʖ ͡°)");
                        break;
                    case Hotspot.Status.NoWifi:
                        Error($"Cannot find wifi {Settings.Default.Hotspot.Device}");
                        break;
                    case Hotspot.Status.FailWifi:
                        Error($"Unable to connect to wifi {Settings.Default.Hotspot.Device}");
                        break;
                    case Hotspot.Status.FailHotspot:
                        Error("Unable to login to hotspot");
                        break;
                }
            });
        }

        void Unimplemented(object sender, RoutedEventArgs e)
        {
            settingPanel.Visibility = Visibility.Collapsed;
            MessageBox.Show(@"Làm gì có chức năng nào khác (╯°□°）╯︵ ┻━┻" + "\n" + @"Người đâu cả tin vl  \ (•◡•) /");
        }

        public void Message(string message)
        {
            snackbarMessage.MessageQueue.Enqueue(message);
        }

        void Error(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
