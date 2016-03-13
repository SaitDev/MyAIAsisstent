using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MaterialSkin;
using MaterialSkin.Controls;
using MyAIAsisstent.Properties;

namespace MyAIAsisstent.Controls
{
    public partial class ReminderControl : UserControl
    {
        [Category("Appearance")]
        public Image Icon { get; set; }

        [Category("Appearance")]
        public string Message
        {
            get { return MessageLabel.Text; }
            set { MessageLabel.Text = value; }
        }

        private DateTime remindTime;
        [Browsable(false)]
        [ReadOnly(true)]
        public DateTime RemindTime
        {
            get { return remindTime; }
            set
            {
                remindTime = value;
                RemindWait.Enabled = false;
                if (value == new DateTime()) return;
                DateTime temp = DateTime.Now;
                if (remindTime.Date == temp.Date)
                {
                    day = "Today  ";
                }
                else if (remindTime.AddDays(1).Date == temp.Date)
                    day = "Tomorrow  ";
                else day = remindTime.DayOfWeek.ToString() + "  ";
                //day = DateTime.Now.ToString("ddddddd") + "  ";
                TimeLabel.Text = day + remindTime.ToShortDateString() + "  At  " + remindTime.ToShortTimeString();
                if (remindFinish || DesignMode) return;
                if (remindTime > temp)
                    RemindWait.Interval = (int)(remindTime - temp).TotalMilliseconds;
                if (!remindFinish) RemindWait.Enabled = true;
            }
        }

        private bool remindFinish = true;
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool RemindFinish
        {
            get
            {
                return remindFinish;
            }
            set
            {
                remindFinish = value;
                CheckFinish();
            }
        }

        [Category("Behavior")]
        //[ReadOnly(true)]
        //[Browsable(false)]
        public new MaterialForm ParentForm
        {
            get; set;
        }

        private ReminderMouseState lastState = ReminderMouseState.Default;
        [ReadOnly(true)]
        [Browsable(false)]
        public ReminderMouseState LastState
        {
            get
            {
                return lastState;
            }
            set
            {
                if (value == ReminderMouseState.LostFocus)
                {
                    this.lastState = ReminderMouseState.Hover;
                    this.OnMouseLeave(new EventArgs());
                    lastReminderClick = null;
                    return;
                }
                if (value == lastState || lastState == ReminderMouseState.Clicked) return;
                switch (value)
                {
                    case ReminderMouseState.Default:
                        {
                            lastState = ReminderMouseState.Hover;
                            this.OnMouseLeave(new EventArgs());
                            break;
                        }
                    case ReminderMouseState.Hover:
                        {
                            lastState = ReminderMouseState.Default;
                            this.OnMouseEnter(new EventArgs());
                            break;
                        }

                }
            }
        }

        private string day;
        public string Day
        {
            get { return day; }
        }

        private int index;
        [Browsable(false)]
        [ReadOnly(true)]
        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
                if (value >= 0)
                    reminderSettings = new Reminder(value);
                else reminderSettings = new Reminder(int.Parse(Name.Remove(0, 15)));
            }
        }

        public static ReminderControl lastReminderClick;
        private Reminder reminderSettings;
        private bool finishLoad = false;

        public ReminderControl()
        {
            InitializeComponent();
        }

        private void ReminderControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                base.BackColor = ((Main)ParentForm).NoteLabelColor(0);
                UpdateSkin(((Main)ParentForm), new EventArgs());
                ParentForm.BackColorChanged += UpdateSkin;
                finishLoad = true;
            }
        }

        private void UpdateSkin(object sender, EventArgs e)
        {
            MessageLabel.ForeColor = ParentForm.SkinManager.GetPrimaryTextColor();
            TimeLabel.ForeColor = ParentForm.SkinManager.GetPrimaryTextColor();
            if (lastState == ReminderMouseState.Default)
            {
                base.BackColor = ((Main)ParentForm).NoteLabelColor(0);
            }
            else if (lastState == ReminderMouseState.Clicked)
            {
                base.BackColor = ((Main)ParentForm).NoteLabelColor(Main.NoteLabelStatus.Clicked);
            }
        }

        private void CheckFinish()
        {
            if (remindFinish)
            {
                if (!DesignMode) RemindWait.Enabled = false;
                MessageLabel.MaximumSize = new Size(this.Size.Width - 30 - 22, 25);
                MessageLabel.Location = new Point(30, 0);
                TimeLabel.Location = new Point(30, MessageLabel.Location.Y + MessageLabel.Size.Height + 5);
                pictureBox1.Show();
            }
            else
            {
                if (!DesignMode) RemindWait.Enabled = true;
                MessageLabel.MaximumSize = new Size(this.Size.Width - 5 - 22, 25);
                MessageLabel.Location = new Point(3, 0);
                TimeLabel.Location = new Point(3, MessageLabel.Location.Y + MessageLabel.Size.Height + 5);
                pictureBox1.Hide();
            }
            if (!DesignMode && finishLoad) reminderSettings.Completed = remindFinish;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (lastReminderClick != this )
            {
                if (lastReminderClick != null) lastReminderClick.LastState = ReminderMouseState.LostFocus;
                if (lastState != ReminderMouseState.Clicked)
                {
                    if (ParentForm.SkinManager.Theme == MaterialSkinManager.Themes.DARK)
                    {
                        base.BackColor = Color.FromArgb(110, 110, 110);
                    }
                    else base.BackColor = Color.FromArgb(170, 170, 170);
                    lastState = ReminderMouseState.Clicked;
                }
                lastReminderClick = this;
            }
        }

        private void MessageLabel_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void TimeLabel_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
            ((Main)ParentForm).editRemind(int.Parse(this.Name.Remove(0, 15)));
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (lastState == ReminderMouseState.Default)
            {
                if (ParentForm.SkinManager.Theme == MaterialSkinManager.Themes.DARK)
                {
                    base.BackColor = ParentForm.SkinManager.GetFlatButtonHoverBackgroundColor();
                }
                else base.BackColor = Color.FromArgb(205, 205, 205);
                this.Cursor = Main.HandCursor;
                lastState = ReminderMouseState.Hover;
            }
        }

        private void MessageLabel_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
        }

        private void TimeLabel_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
            pictureBox2.Image = Resources.right_blue;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (lastState == ReminderMouseState.Hover)
            {
                if (ParentForm.SkinManager.Theme == MaterialSkinManager.Themes.DARK)
                {
                    base.BackColor = Color.FromArgb(60, 60, 60);
                }
                else base.BackColor = (ParentForm).SkinManager.GetFlatButtonHoverBackgroundColor();
                this.Cursor = Cursors.Default;
                lastState = ReminderMouseState.Default;
            }
        }

        private void MessageLabel_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }

        private void TimeLabel_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
            pictureBox2.Image = Resources.right_dark;
        }

        private void RemindWait_Tick(object sender, EventArgs e)
        {
            RemindWait.Stop();
            RemindWait.Enabled = false;
            Notification noti = new Notification();
            noti.Text = this.Message;
            noti.Dismiss += delegate (Notification nt)
            {
                this.reminderSettings.Dismiss = true;
                Settings.Default.Save();
            };
            noti.OnRemindNotify += delegate (object obj, NotificationEventArgs evnt)
            {
                RemindWait.Interval = (int)evnt.RemindAfter.TotalMilliseconds;
                RemindWait.Start();
                MessageBox.Show("shit");
                MessageBox.Show(this.reminderSettings.Message);
                reminderSettings.RemindAfter = evnt.RemindAfter;
                Settings.Default.Save();
                MessageBox.Show(remindTime.ToString());
                MessageBox.Show(remindTime.Add(evnt.RemindAfter).ToString());
                RemindTime = remindTime.Add(evnt.RemindAfter);
            };
            noti.Done += delegate (Notification nt)
            {
                RemindWait.Dispose();
                reminderSettings.FinishTime = DateTime.Now;
                reminderSettings.Completed = true;
                Settings.Default.Save();
                this.RemindFinish = true;
            };
            noti.Show();
        }
    }

    public enum ReminderMouseState
    {
        Hover,
        Default,
        Clicked,
        LostFocus
    }
}
