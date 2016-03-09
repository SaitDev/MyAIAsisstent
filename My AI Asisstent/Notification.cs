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

        private MaterialSkinManager mSkin = MaterialSkinManager.Instance;
        private Mode NotifiMode;
        private Rectangle screenSize;
        private double speed = 25;

        public Notification(Mode mode = Mode.Reminder)
        {
            InitializeComponent();
            this.TextChanged += new System.EventHandler(this.Notification_TextChanged);
            //label1.Text = Environment.OSVersion.VersionString;
            if (mode == Mode.Message)
            {
                materialFlatButton1.Location = new Point(25, 75);
                materialFlatButton1.Text = "OK, Thanks.";
                materialRaisedButton1.Location = new Point(140, 75);
                materialRaisedButton1.Size = new Size(150, 30);
                materialRaisedButton1.Text = "Hi. Open AI Asisstent";
                metroComboBox1.Hide();
            }
            NotifiMode = mode;
        }

        private void Notification_Load(object sender, EventArgs e)
        {
            //Point temp = new Point(SystemInformation.VirtualScreen.Width - Size.Width,
            //                       SystemInformation.VirtualScreen.Height - Size.Height - 10);
            screenSize = SystemInformation.VirtualScreen;
            base.Location = new Point(screenSize.Width + 100, screenSize.Height - Size.Height - 30);
            //base.BackColor = Program._main.BackColor Program._main.materialSkinManager.Theme
            base.BackColor = mSkin.GetApplicationBackgroundColor();
            MessageLabel.ForeColor = mSkin.GetPrimaryTextColor();
            if (mSkin.Theme == MaterialSkinManager.Themes.DARK)
            {
                metroComboBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
                metroComboBox1.BackColor = Color.FromArgb(51, 51, 51);
                metroComboBox1.UseCustomBackColor = true;
            }
            else metroComboBox1.Theme = MetroFramework.MetroThemeStyle.Light;
            metroComboBox1.SelectedIndex = 0;
            this.TextChanged += new System.EventHandler(this.Notification_TextChanged);
        }

        private void Notification_Shown(object sender, EventArgs e)
        {
            Program._main.BackColorChanged += Main_BackColorChanged;
            timer1.Start();
        }

        private void Notification_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program._main.BackColorChanged -= Main_BackColorChanged;
            if (Location.X < screenSize.Width)
            {
                speed = 5;
                timer2.Start();
                e.Cancel = true;
            }
        }

        private void Main_BackColorChanged(object sender, EventArgs e)
        {
            base.BackColor = Program._main.BackColor;
            materialFlatButton1.Invalidate();
            metroComboBox1.BackColor = base.BackColor;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            //DelayHide(300);
            try
            {
                if (Done != null)
                    Done(this);
            }
            catch (Exception exc)
            { MessageBox.Show(exc.Message, "Error"); }
            //Close();
            DelayClose(200);
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            //DelayHide(300);
            try
            {
                if (OnRemindNotify != null)
                    Remind(new NotificationEventArgs((TimeRemind)metroComboBox1.SelectedIndex));
            }
            catch (Exception exc)
            { MessageBox.Show(exc.Message, "Error"); }
            //Close();
            DelayClose(200);
        }

        private async void DelayClose(int i)
        {
            await Task.Delay(i);
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
            MessageLabel.Text = Text;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Location.X > screenSize.Width - 220) speed -= 0.4;
            else if (Location.X > screenSize.Width - 300) speed -= 2.5;
            else if (Location.X > screenSize.Width - 340) speed -= 0.1;
            if (Location.X - speed <= screenSize.Width - 330)
            {
                timer1.Stop();
                Location = new Point(screenSize.Width - 330, Location.Y);
            }
            else Location = new Point((int)(Location.X - speed), Location.Y);
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (Location.X < screenSize.Width - 320) speed += 2;
            else if (Location.X < screenSize.Width - 70) speed += 8;
            else if (Location.X < screenSize.Width) speed -= 1;
            Location = new Point((int)(Location.X + speed), Location.Y);
            if (Location.X + speed >= screenSize.Width - 330)
            {
                timer2.Stop();
                Close();
            }
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

    public enum Mode
    {
        Message,
        Reminder
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

