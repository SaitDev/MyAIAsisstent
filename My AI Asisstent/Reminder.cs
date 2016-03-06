using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.ComponentModel;
using MyAIAsisstent.Properties;

namespace MyAIAsisstent
{
    //[TypeConverter(typeof(ReminderSettingConverter))]
    //[SettingsSerializeAs(SettingsSerializeAs.String)]
    //[SettingsSerializeAs(SettingsSerializeAs.Xml)]
    //[DefaultSettingValue("")]
    //[Serializable]
    public class Reminder : IDisposable
    {
        private int index;

        public Reminder(int i)
        {
            index = i;
            int ReminderCount;
            if (Settings.Default.RemindMessage == null)
            {
                ReminderCount = 0;
                Settings.Default.RemindMessage = new System.Collections.Specialized.StringCollection();
            }
            else ReminderCount = Settings.Default.RemindMessage.Count;
            if (i == ReminderCount)
            {
                int size = i + 1;
                Settings.Default.RemindMessage.Add("");
                DateTime[] temp1 = Settings.Default.RemindAt;
                Array.Resize<DateTime>(ref temp1, size);
                Settings.Default.RemindAt = temp1;
                bool[] temp2 = Settings.Default.RemindCompleted;
                Array.Resize<Boolean>(ref temp2, size);
                Settings.Default.RemindCompleted = temp2;
                Settings.Default.RemindCompleted[i] = false;
                DateTime[] temp3 = Settings.Default.RemindFinishTime;
                Array.Resize<DateTime>(ref temp3, size);
                Settings.Default.RemindFinishTime = temp3;
                TimeSpan[] temp4 = Settings.Default.RemindAfter;
                Array.Resize<TimeSpan>(ref temp4, size);
                Settings.Default.RemindAfter = temp4;
                Settings.Default.RemindAfter[i] = new TimeSpan();
                bool[] temp5 = Settings.Default.RemindDismiss;
                Array.Resize<Boolean>(ref temp5, size);
                Settings.Default.RemindDismiss = temp5;
                Settings.Default.RemindDismiss[i] = false;
                Settings.Default.Save();
            }
        }
        
        public string Message
        {
            get { return Settings.Default.RemindMessage[index]; }
            set { Settings.Default.RemindMessage[index] = value; }
        }

        public DateTime Time
        {
            get { return Settings.Default.RemindAt[index]; }
            set { Settings.Default.RemindAt[index] = value; }
        }

        public bool Completed
        {
            get { return Settings.Default.RemindCompleted[index]; }
            set { Settings.Default.RemindCompleted[index] = value; }
        }

        public DateTime FinishTime
        {
            get { return Settings.Default.RemindFinishTime[index]; }
            set { Settings.Default.RemindFinishTime[index] = value; }
        }

        public TimeSpan RemindAfter
        {
            get { return Settings.Default.RemindAfter[index]; }
            set { Settings.Default.RemindAfter[index] = value; }
        }

        public bool Dismiss
        {
            get { return Settings.Default.RemindDismiss[index]; }
            set { Settings.Default.RemindDismiss[index] = value; }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Reminder() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }

    public class ReminderSettingConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

    }
}
