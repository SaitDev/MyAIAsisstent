using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MaterialSkin;

namespace MyAIAsisstent
{
    public partial class Notification : Form
    {
        public event Action<Notification> Dismiss;

        public event Action<Notification> Done;

        public event EventHandler<NotificationEventArgs> OnRemindNotify;
        protected virtual void Remind(NotificationEventArgs e)
        {
            if (OnRemindNotify != null)
            OnRemindNotify(this, e);
        }

        public delegate void RemindNotificationHandler(Notification sender,
                                                       NotificationEventArgs e);
        //public event RemindNotificationHandler ReminNotify;

        public Notification()
        {
            InitializeComponent();
            //MaterialSkinManager SkinMng = Program._login.materialSkinManager;
            //SkinMng.AddFormToManage(this as MaterialSkin.Controls.MaterialForm);
            this.BackColor = Program._main.BackColor;
            Program._main.BackColorChanged += Main_BackColorChanged;
            label1.Text = Environment.OSVersion.VersionString;
        }

        private void Main_BackColorChanged(object sender, EventArgs e)
        {
            base.BackColor = Program._main.BackColor;
            materialFlatButton1.Invalidate();
            metroComboBox1.BackColor = base.BackColor;
        }

        private void Notification_Load(object sender, EventArgs e)
        {
            Point temp = new Point(SystemInformation.VirtualScreen.Width - Size.Width,
                                   SystemInformation.VirtualScreen.Height - Size.Height - 10);
            base.Location = temp;
            
            if (Program._main.materialSkinManager.Theme == MaterialSkinManager.Themes.DARK)
            {
                metroComboBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
                metroComboBox1.BackColor = Color.FromArgb(51, 51, 51);
                metroComboBox1.UseCustomBackColor = true;
            }
            else metroComboBox1.Theme = MetroFramework.MetroThemeStyle.Light;
            metroComboBox1.SelectedIndex = 0;
        }

        private void Notification_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program._main.BackColorChanged -= Main_BackColorChanged;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            Hide();
            try
            {
                if (Done != null)
                Done(this);
            }
            catch (Exception exc)
            { MessageBox.Show(exc.Message, "Error"); }
            Close();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            Hide();
            try
            {
                if (OnRemindNotify != null)
                Remind(new NotificationEventArgs((TimeRemind)metroComboBox1.SelectedIndex));
            }
            catch (Exception exc)
            { MessageBox.Show(exc.Message, "Error"); }
            Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Hide();
            try
            {
                if (Dismiss != null)
                Dismiss(this);
            }
            catch (Exception exc)
            { MessageBox.Show(exc.Message); }
            Close();
        }

        private void Notification_TextChanged(object sender, EventArgs e)
        {
            label1.Text = Text;
        }
    }

    public enum TimeRemind
    {
        min5,
        min15,
        min30,
        hour1,
        hour2,
        hour6,
        hour12,
        day1
    }

    public class NotificationEventArgs : EventArgs
    {
        public TimeRemind Time { get; internal set; }

        public TimeSpan RemindAfter
        {
            get
            {
                switch (Time)
                {
                    case TimeRemind.min5: return TimeSpan.FromMinutes(5);
                    case TimeRemind.min15: return TimeSpan.FromMinutes(15);
                    case TimeRemind.min30: return TimeSpan.FromMinutes(30);
                    case TimeRemind.hour1: return TimeSpan.FromHours(1);
                    case TimeRemind.hour2: return TimeSpan.FromHours(2);
                    case TimeRemind.hour6: return TimeSpan.FromHours(6);
                    case TimeRemind.hour12: return TimeSpan.FromHours(12);
                    case TimeRemind.day1: return TimeSpan.FromDays(1);
                    default: return TimeSpan.Zero;
                }
            }
        }

        public NotificationEventArgs(TimeRemind time)
        {
            Time = time;
        }
    }
}
