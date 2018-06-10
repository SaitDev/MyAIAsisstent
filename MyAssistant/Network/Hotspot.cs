using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SimpleWifi;
using System.Net.Http;
using System.Net.NetworkInformation;

using MyAssistant.Database.Hotspot;
using MyAssistant.WinAPI;
using MyAssistant.Utils;

namespace MyAssistant.Network
{
    class Hotspot
    {
        MainWindow main;
        Wifi wifi;
        HttpClient client;
        string loginHost = @"http://10.10.0.1/wifi/login";
        Dictionary<string, string> content;
        string user = "username", pass = "password";

        public Hotspot(MainWindow m)
        {
            main = m;
            wifi = new Wifi();
            client = new HttpClient()
            {
                BaseAddress = new Uri(loginHost)
            };
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.139 Safari/537.36");

            content = new Dictionary<string, string>
            {
                { user, "" },
                { pass, "" }
            };
        }

        public async Task<Status> Connect(Credential credential)
        {
            var wifi = await ConnectWifi(credential, true);
            if (wifi == Status.Connected)
            {
                return await LoginHotspot(credential, true);
            }
            return wifi;
        }

        async Task<Status> ConnectWifi(Credential credential, bool retry = false)
        {
            Status status = Status.NoWifi;
            if (!NetworkInterface.GetIsNetworkAvailable() || wifi.GetAccessPoints().Count < 1)
            {
                if (retry) status = await RetryWifi(credential, 20000);
            }
            else
            {
                bool found = false;
                foreach (var ap in wifi.GetAccessPoints())
                {
                    bool done = false;
                    if (ap.Name == credential.Device && !ap.IsSecure)
                    {
                        found = true;
                        if (ap.IsConnected)
                        {
                            status = Status.Connected;
                        }
                        else await Task.Run(async () =>
                        {
                            if (ap.Connect(new AuthRequest(ap)))
                            {
                                status = Status.Connected;
                                done = true;
                            }
                            else if (retry) status = await RetryWifi(credential);
                            else status = Status.FailWifi;
                        });
                    }

                    if (done) break;
                }
                if (!found && retry)
                {
                    status = await RetryWifi(credential);
                }
            }

            return status;
        }

        async Task<Status> RetryWifi(Credential credential, int delay = 10000)
        {
            await Task.Delay(delay);
            Message($"Retry connect to wifi {credential.Device}");
            var wifi = await ConnectWifi(credential);
            return wifi;
        }

        async Task<Status> LoginHotspot(Credential credential, bool retry = false)
        {
            Status status = Status.FailHotspot;
            try
            {
                await client.PostAsync("", Parse(credential));
                Ping ping = new Ping();
                if (ping.Send("google.com").Status == IPStatus.Success)
                {
                    status = Status.Connected;
                }
                else if (retry) status = await RetryHotspot(credential);
            }
            catch (Exception e)
            {
                DebugHelper.Log(e.Message);
                DebugHelper.Log(e.StackTrace);
                if (retry) status = await RetryHotspot(credential);
            }

            return status;
        }

        async Task<Status> RetryHotspot(Credential credential, int delay = 5000)
        {
            await Task.Delay(delay);
            Message($"Retry login to your hotspot...");
            var login = await LoginHotspot(credential);
            return login;
        }

        void Message(string message)
        {
            if (main != null)
            {
                main.Dispatcher.Invoke(() =>
                {
                    main.Message(message);
                });
            }
        }

        HttpContent Parse(Credential credential)
        {
            content[user] = credential.Username;
            content[pass] = credential.Password;
            return new FormUrlEncodedContent(content);
        }

        public enum Status
        {
            Connected,
            FailWifi,
            FailHotspot,
            NoWifi
        }
    }
}
