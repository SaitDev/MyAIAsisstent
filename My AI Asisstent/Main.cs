using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MaterialSkin.Controls;
using MaterialSkin;
using System.Configuration;
using Opulos.Core.UI;
using System.Runtime.InteropServices;
using MetroFramework;
using MyAIAsisstent.Properties;

namespace MyAIAsisstent
{
    public partial class Main : MaterialForm
    {
        public MaterialSkinManager materialSkinManager;
        private Login _login;
        private MaterialFlatButton lastActive;
        private DateTime remindAtTime;
        private bool remindCreating = false, messageInputed;

        #region Form Managament

        public Main()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.LightBlue800, Primary.BlueGrey400,
                                                               Accent.LightBlue400, TextShade.WHITE);
            if (Settings.Default.LightTheme)
            {
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            }
            else
            {
                materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            }
            materialSkinManager.AddFormToManage(this);
            _login = new Login(this);
            Show();

         //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
         //MessageBox.Show(config.FilePath);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                this.Show();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (Settings.Default.RemindMessage != null)
            {
                for (int i = 0; i < Settings.Default.RemindMessage.Count; i++)
                {
                    if (Settings.Default.RemindCompleted[i]) continue;
                    System.Timers.Timer t = new System.Timers.Timer();
                    t.AutoReset = false;
                    DateTime temp = DateTime.Now;
                    if (Settings.Default.RemindAt[i] <= temp)
                        t.Interval = 10;
                    else t.Interval = (Settings.Default.RemindAt[i] - temp).TotalMilliseconds;
                    string message = Settings.Default.RemindMessage[i];
                    int remindIndex = i;
                    t.Elapsed += delegate (object o, System.Timers.ElapsedEventArgs evnt)
                    {
                        Reminder rmd = new Reminder(remindIndex);
                        MessageBox.Show(message);
                        rmd.FinishTime = DateTime.Now;
                        rmd.Completed = true;
                        Settings.Default.Save();

                        ((System.Timers.Timer)o).Stop();
                        ((System.Timers.Timer)o).Enabled = false;
                        ((System.Timers.Timer)o).Dispose();
                    };
                    t.Start();
                }
            }
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            lastActive = materialFlatButton3; 
            materialLabel1.Font = SkinManager.ROBOTO_MEDIUM_12;
            timePickerPanel1.timePicker.ClockMenu.SnapWindow(this.Handle, this.Handle, new SnapPoint
            {
                OffsetConstantX = this.Location.X + 180,
                OffsetConstantY = materialListView1.Location.Y + 104
            });
            timePickerPanel1.timePicker.ValueChanged += TimePicker_ValueChanged;
            timePickerPanel1.timePicker.ClockMenu.Closed += delegate (object o, ToolStripDropDownClosedEventArgs evnt)
            {
                materialSingleLineTextField1.Enabled = true;
            };
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void Main_BackColorChanged(object sender, EventArgs e)
        {
            UpdateMenuButton();
            materialContextMenuStrip1.BackColor = this.BackColor;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }

        private void materialToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void materialToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception exc)
            {
                string mess = string.Format("Exception error: {0}", exc.Message);
                MessageBox.Show(mess);
            }
        }

        #endregion

        #region Main Buttons

        private void UpdateMenuButton()
        {
            if (SkinManager.Theme == MaterialSkinManager.Themes.DARK)
            {
                materialFlatButton1.BackColor = Color.FromArgb(75, 75, 75);
                materialFlatButton2.BackColor = Color.FromArgb(75, 75, 75);
                materialFlatButton3.BackColor = Color.FromArgb(75, 75, 75);
            }
            else
            {
                materialFlatButton1.BackColor = Color.FromArgb(224, 224, 224);
                materialFlatButton2.BackColor = Color.FromArgb(224, 224, 224);
                materialFlatButton3.BackColor = Color.FromArgb(224, 224, 224);
            }
            materialFlatButton1.UseCustomBackColor = true;
            materialFlatButton2.UseCustomBackColor = true;
            materialFlatButton3.UseCustomBackColor = true;
            if (lastActive != null) lastActive.UseCustomBackColor = false;
            else materialFlatButton3.UseCustomBackColor = false;
        }

        private void materialFlatButton_Click(object sender, EventArgs e)
        {
            lastActive.UseCustomBackColor = true;
            lastActive.Refresh();
            ((MaterialFlatButton)sender).UseCustomBackColor = false;
            ((MaterialFlatButton)sender).Refresh();
            panel1.Location = ((MaterialFlatButton)sender).Location;
            if ((MaterialFlatButton)sender != materialFlatButton1) lastActive = (MaterialFlatButton)sender;
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            materialFlatButton_Click(sender, e);
            if (_login._setting.Visible == true)
                _login._setting.Hide();
            _login._setting.materialCheckBox3.Enabled = false;
            _login._setting.ShowDialog();
            _login._setting.materialCheckBox3.Enabled = true;
            materialFlatButton1.UseCustomBackColor = true;
            materialFlatButton1.Refresh();
            lastActive.UseCustomBackColor = false;
            lastActive.Refresh();
            panel1.Location = lastActive.Location;
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            materialFlatButton_Click(sender, e);
            materialTabSelector1.Show();
            materialTabControl1.Show();
            materialFlatButton4.Show();
        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            materialFlatButton_Click(sender, e);
            materialTabSelector1.Hide();
            materialTabControl1.Hide();
            materialFlatButton4.Hide();
        }

        #endregion


        private void materialTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (materialTabControl1.SelectedIndex == 1)
            {
                if (remindCreating)
                {
                    DialogResult answer = MetroMessageBox.Show(this, "You have not saved reminder. Are you sure to discard?",
                                                                "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 150);
                    if (answer == DialogResult.Yes)
                    {
                        materialLabel1.Hide();
                        materialListView1.Hide();
                        materialListView2.Hide();
                        materialListView3.Hide();
                        monthCalendar1.Hide();
                        if (timePickerPanel1.timePicker.ClockMenu.Visible)
                            timePickerPanel1.timePicker.ClockMenu.ClockButtonCancel.PerformClick();
                        materialSingleLineTextField1.TextChanged -= materialSingleLineTextField1_TextChanged;
                        materialSingleLineTextField1.Clear();
                        materialListView1.Items[0].Text = "Message to remind";
                        materialListView2.Items[0].Text = "Day";
                        materialListView3.Items[0].Text = "Time";
                        remindCreating = false;
                        messageInputed = false;
                        remindAtTime = new DateTime();
                        materialFlatButton4.Icon = Properties.Resources.alarm_blue;
                        materialFlatButton5.Hide();
                    }
                    else
                    {
                        materialTabControl1.SelectedIndex = 0;
                        tabPage1.Show();
                    }
                }
            }
        }


        #region Reminder Module

        private void TimePicker_ValueChanged(object sender, ValueChangedEventArgs<DateTime> e)
        {
            DateTime temp = DateTime.Now;
            if (remindAtTime.Date < temp.Date)
            {
                //if (TimeSpan.Compare(timePickerPanel1.timePicker.Value.TimeOfDay, temp.TimeOfDay) > -1)
                if (timePickerPanel1.timePicker.Value.TimeOfDay >= temp.TimeOfDay)
                {
                    materialListView2.Items[0].Text = "Today";
                    remindAtTime = temp.Date + timePickerPanel1.timePicker.Value.TimeOfDay;
                }
                else
                {
                    materialListView2.Items[0].Text = "Tomorrow";
                    remindAtTime = temp.AddDays(1).Date + timePickerPanel1.timePicker.Value.TimeOfDay;
                }
            }
            else remindAtTime = remindAtTime.Date + timePickerPanel1.timePicker.Value.TimeOfDay;
            materialListView3.Items[0].Text = timePickerPanel1.timePicker.Value.ToShortTimeString();
            materialSingleLineTextField1.Enabled = true;
        }

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            if (!remindCreating)
            {
                remindCreating = true;
                materialLabel1.Text = "WHAT do you want me to remind?";
                materialLabel1.Show();
                materialLabel1.BackColor = SkinManager.GetFlatButtonHoverBackgroundColor();
                materialLabel1.ForeColor = SkinManager.ColorScheme.PrimaryColor;
                materialLabel1.Font = SkinManager.ROBOTO_MEDIUM_11;

                materialListView1.Visible = !materialListView1.Visible;
                materialListView1.Items[0].ForeColor = SkinManager.GetDividersColor();
                materialListView2.Show();
                materialListView3.Show();
                materialFlatButton4.Icon = Properties.Resources.done_blue;
                materialFlatButton5.Show();
                materialSingleLineTextField1.TextChanged += materialSingleLineTextField1_TextChanged;
                materialSingleLineTextField1.Focus();
            }
            else
            {
                if (messageInputed == false)
                {
                    MetroMessageBox.Show(this, "You didn't enter the reminder.", "",
                                          MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 120);
                    materialSingleLineTextField1.Focus();
                    return;
                }
                else if (materialListView2.Items[0].Text == "Day")
                {
                    MetroMessageBox.Show(this, "You didn't enter the Day to remind.", "",
                                          MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 120);
                    materialListView2_Click(materialListView2, new EventArgs());
                    return;
                }
                else if (materialListView3.Items[0].Text == "Time")
                {
                    MetroMessageBox.Show(this, "You didn't enter the Time to remind.", "",
                                          MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 120);
                    materialListView3_Click(materialListView3, new EventArgs());
                    return;
                }
                int ReminderCount;
                if (Settings.Default.RemindMessage == null)
                    ReminderCount = 0;
                else ReminderCount = Settings.Default.RemindMessage.Count;
                System.Timers.Timer t = new System.Timers.Timer();
                t.AutoReset = false;
                if (remindAtTime <= DateTime.Now)
                    t.Interval = 10;
                else t.Interval = (remindAtTime - DateTime.Now).TotalMilliseconds; 
                string s = materialListView1.Items[0].Text; 
                t.Elapsed += delegate (object o, System.Timers.ElapsedEventArgs evnt)
                   {
                       Reminder rmd = new Reminder(ReminderCount);
                       MessageBox.Show(s);
                       /*       
                       Notification noti = new Notification();
                       noti.Dismiss += DismissNotification;
                       noti.Remind += RemindNotification;
                       noti.Done += DoneNotification;
                       noti.Show(); */
                       rmd.FinishTime = DateTime.Now;
                       rmd.Completed = true;
                       Settings.Default.Save();

                       ((System.Timers.Timer)o).Stop();
                       ((System.Timers.Timer)o).Enabled = false;
                       ((System.Timers.Timer)o).Dispose();
                   };
                t.Start();
                //MessageBox.Show(remindAtTime.ToString());
                //MessageBox.Show(t.Interval.ToString());
                Reminder reminder = new Reminder(ReminderCount);
                reminder.Message = materialListView1.Items[0].Text;
                reminder.Time = remindAtTime;
                Settings.Default.Save();
                //rmd.Dispose();
                materialLabel1.Hide();
                materialListView1.Hide();
                materialListView2.Hide();
                materialListView3.Hide();
                materialListView1.Items[0].Text = "Message to remind";
                materialListView2.Items[0].Text = "Day";
                materialListView3.Items[0].Text = "Time";
                remindCreating = false;
                messageInputed = false;
                remindAtTime = new DateTime();
                materialSingleLineTextField1.TextChanged -= materialSingleLineTextField1_TextChanged;
                materialSingleLineTextField1.Clear();
                materialFlatButton4.Icon = Properties.Resources.alarm_blue;
                materialFlatButton5.Hide();
            }
        }

        private void DismissNotification(Notification sender)
        {
            //MessageBox.Show("User canceled");
        }

        private void DoneNotification(Notification sender)
        {
            //MessageBox.Show("Done");
        }

        private void RemindNotification(object sender, NotificationEventArgs e)
        {
            //MessageBox.Show(e.Time.ToString());
        }

        private void materialFlatButton5_Click(object sender, EventArgs e)
        {
            DialogResult answer = MetroMessageBox.Show(this, "Are you sure to cancel this reminder?", "",
                                                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 150);
            if (answer == DialogResult.No) return;
            materialLabel1.Hide();
            materialListView1.Hide();
            materialListView2.Hide();
            materialListView3.Hide();
            monthCalendar1.Hide();
            if (timePickerPanel1.timePicker.ClockMenu.Visible)
                timePickerPanel1.timePicker.ClockMenu.ClockButtonCancel.PerformClick();
            materialSingleLineTextField1.TextChanged -= materialSingleLineTextField1_TextChanged;
            materialSingleLineTextField1.Clear();
            materialListView1.Items[0].Text = "Message to remind";
            materialListView2.Items[0].Text = "Day";
            materialListView3.Items[0].Text = "Time";
            remindCreating = false;
            messageInputed = false;
            remindAtTime = new DateTime();
            materialFlatButton4.Icon = Properties.Resources.alarm_blue;
            materialFlatButton5.Hide();
        }

        private void materialListView1_Click(object sender, EventArgs e)
        {
            if (monthCalendar1.Visible) monthCalendar1.Hide();
            else if (timePickerPanel1.timePicker.ClockMenu.Visible)
                timePickerPanel1.timePicker.ClockMenu.ClockButtonCancel.PerformClick();
            materialLabel1.Text = "WHAT do you want me to remind?";
            if (messageInputed) materialSingleLineTextField1.Text = materialListView1.Items[0].Text;
            materialSingleLineTextField1.Focus();
        }

        private void materialListView2_Click(object sender, EventArgs e)
        {
            if (timePickerPanel1.timePicker.ClockMenu.Visible)
                timePickerPanel1.timePicker.ClockMenu.ClockButtonCancel.PerformClick();
            materialLabel1.Text = "WHEN do you want me to remind?";
            monthCalendar1.MinDate = DateTime.Now;
            monthCalendar1.Show();
            materialSingleLineTextField1.Enabled = false;
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime temp = DateTime.Now;
            remindAtTime = monthCalendar1.SelectionRange.Start.Date + remindAtTime.TimeOfDay;
            if ((remindAtTime.Day - temp.Day) == 0)
                materialListView2.Items[0].Text = "Today";
            else if ((remindAtTime.Day - temp.Day) == 1)
                materialListView2.Items[0].Text = "Tomorrow";
            else materialListView2.Items[0].Text = monthCalendar1.SelectionRange.Start.ToShortDateString();
            monthCalendar1.Hide();
            materialSingleLineTextField1.Enabled = true;
        }

        private void materialListView3_Click(object sender, EventArgs e)
        {
            if (monthCalendar1.Visible) monthCalendar1.Hide();
            materialLabel1.Text = "WHEN do you want me to remind?";
            DateTime temp1, temp2;
            temp1 = DateTime.Now;
            temp2 = temp1.Date + new TimeSpan(temp1.Hour, temp1.Minute, 0);
            //timePickerPanel1.timePicker.attacher.ShowMenu();
            timePickerPanel1.timePicker.ClockMenu.Show(this, 260, materialListView1.Location.Y + 104);
            timePickerPanel1.timePicker.ClockMenu.Value = temp2;
            SetWindowPos(timePickerPanel1.timePicker.ClockMenu.Handle, (IntPtr)(-1), 0, 0, 0, 0,
                                                       SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE);
            materialSingleLineTextField1.Enabled = false;
        }

        private void materialSingleLineTextField1_TextChanged(object sender, EventArgs e)
        {
            if (remindCreating) messageInputed = true;
            materialListView1.Items[0].Text = materialSingleLineTextField1.Text;
        }

        #endregion

        private void materialFlatButton6_Click(object sender, EventArgs e)
        {
            _login.newNote(_login.noteCount);
        }

        private void materialSingleLineTextField1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (remindCreating)
                {
                    materialListView1.Items[0].Text = materialSingleLineTextField1.Text;
                    materialLabel1.Focus();
                }
            }
        }


        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOACTIVATE = 0x0010;
        private const int SWP_NOMOVE = 0x0002;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, 
                                                 int X, int Y, int cx, int cy, uint uFlags);
    }
}
