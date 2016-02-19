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

        public event EventHandler<NotificationEventArgs> Remind;
        protected virtual void OnRemindNotify(NotificationEventArgs e)
        {
            Remind(this, e);
        }

        public delegate void RemindNotificationHandler(Notification sender,
                                                       NotificationEventArgs e);
        public event RemindNotificationHandler ReminNotify;

        public Notification()
        {
            InitializeComponent();

            //MaterialSkinManager SkinMng = Program._login.materialSkinManager;
            //SkinMng.AddFormToManage(this as MaterialSkin.Controls.MaterialForm);
            this.BackColor = Program._main.BackColor;
            label1.Text = Environment.OSVersion.VersionString;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            Hide();
            try
            { Done(this); }
            catch (Exception exc)
            { MessageBox.Show(exc.Message, "Error"); }
            this.Close();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            Hide();
            try
            { OnRemindNotify(new NotificationEventArgs((TimeRemind)metroComboBox1.SelectedIndex)); }
            catch (Exception exc)
            { MessageBox.Show(exc.Message, "Error"); }
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Hide();
            try
            { Dismiss(this); }
            catch (Exception exc)
            { MessageBox.Show(exc.Message); }
            this.Close();
        }

        private void Notification_Load(object sender, EventArgs e)
        {
            Point temp = new Point(SystemInformation.VirtualScreen.Width - Size.Width,
                                   SystemInformation.VirtualScreen.Height - Size.Height - 10);
            this.Location = temp;
            materialFlatButton1.BackColor = Color.Black;
            if (Program._main.materialSkinManager.Theme == MaterialSkinManager.Themes.DARK)
            {
                metroComboBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
                metroComboBox1.BackColor = Color.FromArgb(255, 51, 51, 51);
                metroComboBox1.UseCustomBackColor = true;
            }
            else metroComboBox1.Theme = MetroFramework.MetroThemeStyle.Light;
            metroComboBox1.SelectedIndex = 0;
        }
    }

    public enum TimeRemind
    {
        min10,
        min30,
        hour1,
        hour2,
        hour3,
        hour6,
        hour12,
        day1
    }

    public class NotificationEventArgs : EventArgs
    {
        public TimeRemind Time { get; internal set; }

        public NotificationEventArgs(TimeRemind time)
        {
            Time = time;
        }
    }
}
