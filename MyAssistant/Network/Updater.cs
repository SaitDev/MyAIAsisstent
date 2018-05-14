using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using Squirrel;

namespace MyAssistant.Network
{
    class Updater
    {
        Task<ReleaseEntry> updating;
        string updateHost = "";
        Version version = new Version("0.2.0");

        public async void Update()
        {
            try
            {
                if (!Debugger.IsAttached)
                {
                    using (var updater = new UpdateManager(updateHost))
                    {
                        updating = updater.UpdateApp();
                    }
                }
            }
            catch (Exception e) { }
        }

        public async Task<bool> IsUpdated()
        {
            if (updating == null || updating.IsFaulted) return false;
            var release = await updating;
            return release.Version.CompareTo(version) > 0;
        }
    }
}
