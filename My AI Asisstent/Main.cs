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
using MyAIAsisstent.Controls;
using System.IO;
using System.Media;

namespace MyAIAsisstent
{
    public partial class Main : MaterialForm
    {
        public MaterialSkinManager materialSkinManager;
        public static bool Stop_AI_Asisstent = false;
        private Login _login;
        private MaterialFlatButton lastActive;
        private DateTime remindAtTime;
        private bool remindCreating = false, remindMessageInputed, noteEditing = false, finishLoad = false;
        private int speedButton, speedPanel, ReminderIndex;
        private DialogResult answer;
        public static Cursor HandCursor;
        private MaterialLabel lastNoteLabelClick;

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
            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            //MessageBox.Show(config.FilePath);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                this.Show();
                this.Activate();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (Settings.Default.RemindMessage != null)
            {
                for (int i = 0; i < Settings.Default.RemindMessage.Count; i++)
                {
                    if (Settings.Default.RemindCompleted[i] || Settings.Default.RemindDismiss[i]) continue;
                    Timer t = new Timer();
                    //t.AutoReset = false;
                    DateTime temp = DateTime.Now;
                    if (Settings.Default.RemindAt[i] <= temp)
                        t.Interval = 10;
                    else t.Interval = (int)((Settings.Default.RemindAt[i] - temp).TotalMilliseconds
                                            + Settings.Default.RemindAfter[i].TotalMilliseconds);
                    string message = Settings.Default.RemindMessage[i];
                    int remindIndex = i;
                    t.Tick += delegate (object o, EventArgs evnt)
                    {
                        if (!finishLoad) return;
                        ((Timer)o).Stop();                       
                        Reminder rmd = new Reminder(remindIndex);
                        //MessageBox.Show(message);
                        Notification noti = new Notification();
                        noti.Text = message;
                        noti.Dismiss += delegate (Notification nt)
                        {
                            rmd.Dismiss = true;
                            Settings.Default.Save();
                        };
                        noti.OnRemindNotify += delegate (object obj, NotificationEventArgs ev)
                        {
                            ((Timer)o).Interval = (int)ev.RemindAfter.TotalMilliseconds;
                            ((Timer)o).Start();
                            rmd.RemindAfter = ev.RemindAfter;
                            Settings.Default.Save();
                        };
                        noti.Done += delegate (Notification nt)
                        {
                            //((System.Timers.Timer)o).Stop();
                            //((System.Timers.Timer)o).Enabled = false;
                            ((Timer)o).Dispose();
                            rmd.FinishTime = DateTime.Now;
                            rmd.Completed = true;
                            Settings.Default.Save();
                        };
                        noti.Show();
                    };
                    t.Start();
                }
            }
            if (Settings.Default.RemindMessage != null)
            if (Settings.Default.RemindMessage.Count > 0)
            {
                ReminderControl0.ParentForm = this;
                ReminderControl0.Message = Settings.Default.RemindMessage[0];
                ReminderControl0.RemindTime = Settings.Default.RemindAt[0];
                ReminderControl0.Show();
                ReminderControl0.Tag = ReminderControl0.Location.Y + ReminderControl0.Size.Height;
                for (int i = 1; i < Settings.Default.RemindMessage.Count; i++)
                {
                    newReminderControl(i);
                }
            }
            if (Settings.Default.NoteCount > 0)
            {
                NoteLabel0.Text = Settings.Default.Notes[0];
                NoteLabel0.BackColor = NoteLabelColor(0);
                NoteLabel0.Tag = NoteLabel0.Location.Y + NoteLabel0.Size.Height;
                /*
                materialFlatButton9.Location = new Point(7,
                        NoteLabel0.Size.Height == 40 ? NoteLabel0.Location.Y + 2 : NoteLabel0.Location.Y + 12);
                materialFlatButton10.Location = new Point(237,
                        NoteLabel0.Size.Height == 40 ? NoteLabel0.Location.Y + 2 : NoteLabel0.Location.Y + 12);
                */
                for (int i = 1; i < Settings.Default.NoteCount; i++)
                {
                    newNoteLabel(i);
                }
            }
            else NoteLabel0.Hide();
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            if (Program.silentStart)
            {
                Hide();
                Notification welcome = new Notification(Mode.Message);
                welcome.Text = "Welcome back Sait. Have a nice day!";
                welcome.Done += (o) => { this.Show(); this.Activate(); };
                welcome.Show();
                if (File.Exists(@"C:\Windows\Media\Windows Logon.wav"))
                {
                    SoundPlayer welcomeSound = new SoundPlayer(@"C:\Windows\Media\Windows Logon.wav");
                    welcomeSound.Play();
                    welcomeSound.Dispose();
                }
                //PlaySound("Notification", UIntPtr.Zero, (uint)sndFlags.SND_SYNC);
            }
            if (Program.silentStart) 
            {
                Hide();
                Notification welcome = new Notification(Mode.Message);
                welcome.Text = "Welcome back Sait. Have a nice day!";
                welcome.Show();
                if (File.Exists(@"C:\Windows\Media\Windows Logon.wav"))
                {
                    SoundPlayer welcomeSound = new SoundPlayer(@"C:\Windows\Media\Windows Logon.wav");
                    welcomeSound.Play();
                    welcomeSound.Dispose();
                }
                //PlaySound("Notification", UIntPtr.Zero, (uint)sndFlags.SND_SYNC);
            }
            materialFlatButton3.Focus();
            lastActive = materialFlatButton3;
            materialSingleLineTextField1.Enabled = true;
            materialLabel1.Font = SkinManager.ROBOTO_MEDIUM_12;
            timePickerPanel1.timePicker.ClockMenu.SnapWindow(this.Handle, this.Handle, new SnapPoint
            {
                OffsetConstantX = this.Location.X + 180,
                OffsetConstantY = materialListView1.Location.Y + 104
            });
            //timePickerPanel1.timePicker.ValueChanged += TimePicker_ValueChanged;
            timePickerPanel1.timePicker.ClockMenu.ClockButtonOK.Click += TimePicker_Choosed;
            timePickerPanel1.timePicker.ClockMenu.Closed += delegate (object o, ToolStripDropDownClosedEventArgs evnt)
            {
                materialSingleLineTextField1.Enabled = true;
            };
            HandCursor = new Cursor(LoadCursor(IntPtr.Zero, idCursor.HAND));
            finishLoad = true;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (materialTabControl1.SelectedIndex == 1)
            {
                if (noteEditing)
                {
                    answer = MetroMessageBox.Show(this, "You have not saved Note. Are you sure to discard changes?",
                                                                "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 140);
                    if (answer == DialogResult.No)
                    {
                        Stop_AI_Asisstent = false;
                        e.Cancel = true;
                        materialSingleLineTextField1.SelectAll();
                        return;
                    }
                    else
                    {
                        noteEditing = false;
                        if (lastNoteLabelClick == null)
                        {
                            throw new Exception("lastNoteLabelClick is null");
                        }
                        int.TryParse(lastNoteLabelClick.Name.Remove(0, 9), out noteLabelID);
                        lastNoteLabelClick.Text = Settings.Default.Notes[noteLabelID];
                        materialSingleLineTextField1.TextChanged -= materialSingleLineTextField1_TextChanged;
                        materialSingleLineTextField1.Clear();
                        NoteLabel0.Focus();
                    }
                }
                if (lastNoteLabelClick != null)
                {
                    lastNoteLabelClick.BackColor = NoteLabelColor(0);
                    lastNoteLabelClick = null;
                }
                materialFlatButton10.Hide();
                materialFlatButton9.Hide();
            }
            else
            {
                if (remindCreating)
                {
                    answer = MetroMessageBox.Show(this, "You have not saved reminder. Are you sure to discard?",
                                                                "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 150);
                    if (answer == DialogResult.Yes)
                    {
                        panel3.Location = new Point(272, 0);
                        monthCalendar1.Hide();
                        if (timePickerPanel1.timePicker.ClockMenu.Visible)
                            timePickerPanel1.timePicker.ClockMenu.ClockButtonCancel.PerformClick();
                        materialSingleLineTextField1.TextChanged -= materialSingleLineTextField1_TextChanged;
                        materialSingleLineTextField1.Clear();
                        materialListView1.Items[0].Text = "Message to remind";
                        materialListView2.Items[0].Text = "Day";
                        materialListView3.Items[0].Text = "Time";
                        remindCreating = false;
                        remindMessageInputed = false;
                        remindAtTime = new DateTime();
                        materialFlatButton4.Icon = Properties.Resources.alarm_blue;
                        materialFlatButton5.Hide();
                        ReminderControl0.Focus();
                    }
                    else
                    {
                        if (remindMessageInputed) materialSingleLineTextField1.SelectAll();
                        Stop_AI_Asisstent = false;
                        e.Cancel = true;
                        return;
                    }
                }
                else if (ReminderControl.lastReminderClick != null)
                {
                    ReminderControl.lastReminderClick.LastState = ReminderMouseState.LostFocus;
                }
            }
            if (!Stop_AI_Asisstent)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void Main_BackColorChanged(object sender, EventArgs e)
        {
            UpdateMenuButton();
            if (finishLoad)
            UpdateNoteLabelColor();
            materialContextMenuStrip1.BackColor = this.BackColor;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.Activate();
        }

        private void materialToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.Show();
            this.Activate();
        }

        private void materialToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            materialFlatButton1_Click(materialFlatButton1, new EventArgs());
        }

        private void materialToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Stop_AI_Asisstent = true;
            this.Close();
            /*
            try
            {
                Environment.Exit(0);
            }
            catch (Exception exc)
            {
                string mess = string.Format("Exception error: {0}", exc.Message);
                MessageBox.Show(mess);
            }
            */
        }

        #endregion

        #region Main Buttons

        private void UpdateMenuButton()
        {
            if (SkinManager.Theme == MaterialSkinManager.Themes.DARK)
            {
                materialFlatButton1.BackColor = Color.FromArgb(71, 71, 71);
                materialFlatButton2.BackColor = Color.FromArgb(71, 71, 71);
                materialFlatButton3.BackColor = Color.FromArgb(71, 71, 71);
            }
            else
            {
                materialFlatButton1.BackColor = Color.FromArgb(211, 211, 211);
                materialFlatButton2.BackColor = Color.FromArgb(211, 211, 211);
                materialFlatButton3.BackColor = Color.FromArgb(211, 211, 211);
            }
            materialFlatButton1.UseCustomBackColor = true;
            materialFlatButton2.UseCustomBackColor = true;
            materialFlatButton3.UseCustomBackColor = true;
            if (lastActive != null) lastActive.UseCustomBackColor = false;
            else materialFlatButton3.UseCustomBackColor = false;
        }

        int animateProgress, animateStep;

        private void materialFlatButton_Click(object sender, EventArgs e)
        {
            if (lastActive == ((MaterialFlatButton)sender)) return;
            lastActive.UseCustomBackColor = true;
            lastActive.Refresh();
            ((MaterialFlatButton)sender).UseCustomBackColor = false;
            ((MaterialFlatButton)sender).Refresh();
            //panel1.Location = ((MaterialFlatButton)sender).Location;
            animateProgress = 10;
            animateStep = (((MaterialFlatButton)sender).Location.Y - lastActive.Location.Y) / 54;
            timer1.Start();
            if ((MaterialFlatButton)sender != materialFlatButton1) lastActive = (MaterialFlatButton)sender;
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (Visible)
            {
                if (materialTabControl1.SelectedIndex == 1)
                {
                    if (!noteEditing && lastNoteLabelClick != null)
                    {
                        lastNoteLabelClick.BackColor = NoteLabelColor(0);
                        lastNoteLabelClick = null;
                        speedButton = 2;
                        timer6.Start();
                    }
                }
                else
                {
                    if (ReminderControl.lastReminderClick != null)
                    {
                        ReminderControl.lastReminderClick.LastState = ReminderMouseState.LostFocus;
                    }
                }
                materialFlatButton_Click(sender, e);
            }
            if (_login._setting.Visible == true)
                _login._setting.Hide();
            _login._setting.materialCheckBox3.Checked = false;
            _login._setting.materialCheckBox3.Enabled = false;
            _login._setting.ShowDialog();
            _login._setting.materialCheckBox3.Enabled = true;
            if (Visible)
            {
                materialFlatButton1.UseCustomBackColor = true;
                materialFlatButton1.Refresh();
                lastActive.UseCustomBackColor = false;
                lastActive.Refresh();
                animateProgress = 10;
                animateStep = (lastActive.Location.Y - materialFlatButton1.Location.Y) / 54;
                timer1.Start();
                if (remindMessageInputed || noteEditing) materialSingleLineTextField1.SelectAll();
                //panel1.Location = lastActive.Location;
            }
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
            if (materialTabControl1.SelectedIndex == 1)
            {
                if (noteEditing)
                {
                    answer = MetroMessageBox.Show(this, "You have not saved Note. Are you sure to discard changes?",
                                                                "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 140);
                    if (answer == DialogResult.No)
                    {
                        materialSingleLineTextField1.SelectAll();
                        return;
                    }
                    else
                    {
                        noteEditing = false;
                        if (lastNoteLabelClick == null)
                        {
                            throw new Exception("lastNoteLabelClick is null");
                        }
                        int.TryParse(lastNoteLabelClick.Name.Remove(0, 9), out noteLabelID);
                        lastNoteLabelClick.Text = Settings.Default.Notes[noteLabelID];
                        materialSingleLineTextField1.TextChanged -= materialSingleLineTextField1_TextChanged;
                        materialSingleLineTextField1.Clear();
                        materialFlatButton3.Focus();
                    }
                }
                if (lastNoteLabelClick != null)
                {
                    lastNoteLabelClick.BackColor = NoteLabelColor(0);
                    lastNoteLabelClick = null;
                }
                materialFlatButton10.Hide();
                materialFlatButton9.Hide();
            }
            else
            {
                if (remindCreating)
                {
                    answer = MetroMessageBox.Show(this, "You have not saved reminder. Are you sure to discard?",
                                                                "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 150);
                    if (answer == DialogResult.Yes)
                    {
                        panel3.Location = new Point(272, 0);
                        monthCalendar1.Hide();
                        if (timePickerPanel1.timePicker.ClockMenu.Visible)
                            timePickerPanel1.timePicker.ClockMenu.ClockButtonCancel.PerformClick();
                        materialSingleLineTextField1.TextChanged -= materialSingleLineTextField1_TextChanged;
                        materialSingleLineTextField1.Clear();
                        materialListView1.Items[0].Text = "Message to remind";
                        materialListView2.Items[0].Text = "Day";
                        materialListView3.Items[0].Text = "Time";
                        remindCreating = false;
                        remindMessageInputed = false;
                        remindAtTime = new DateTime();
                        materialFlatButton4.Icon = Properties.Resources.alarm_blue;
                        materialFlatButton5.Hide();
                    }
                    else
                    {
                        if (remindMessageInputed) materialSingleLineTextField1.SelectAll();
                        return;
                    }
                }
                else if (ReminderControl.lastReminderClick != null)
                {
                    ReminderControl.lastReminderClick.LastState = ReminderMouseState.LostFocus;
                }
            }
            materialFlatButton_Click(sender, e);
            materialTabSelector1.Hide();
            materialTabControl1.Hide();
            materialFlatButton4.Hide();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (animateProgress == 1)
            {
                timer1.Enabled = false;
                return;
            }
            panel1.Location = new Point(panel1.Location.X, panel1.Location.Y + animateStep * animateProgress);
            animateProgress--;
        }

        #endregion

        private void materialTabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (materialTabControl1.SelectedIndex == 1)
            {
                if (remindCreating)
                {
                    answer = MetroMessageBox.Show(this, "You have not saved reminder. Are you sure to discard?",
                                                                "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 150);
                    if (answer == DialogResult.Yes)
                    {
                        //materialLabel1.Hide();
                        //materialListView1.Hide();
                        //materialListView2.Hide();
                        //materialListView3.Hide();
                        panel3.Location = new Point(272, 0);
                        monthCalendar1.Hide();
                        if (timePickerPanel1.timePicker.ClockMenu.Visible)
                            timePickerPanel1.timePicker.ClockMenu.ClockButtonCancel.PerformClick();
                        materialSingleLineTextField1.TextChanged -= materialSingleLineTextField1_TextChanged;
                        materialSingleLineTextField1.Clear();
                        materialListView1.Items[0].Text = "Message to remind";
                        materialListView2.Items[0].Text = "Day";
                        materialListView3.Items[0].Text = "Time";
                        remindCreating = false;
                        remindMessageInputed = false;
                        remindAtTime = new DateTime();
                        materialFlatButton4.Icon = Properties.Resources.alarm_blue;
                        materialFlatButton5.Hide();
                    }
                    else
                    {
                        e.Cancel = true;
                        if (remindMessageInputed) materialSingleLineTextField1.SelectAll();
                        //materialTabControl1.SelectTab(0);
                        //tabPage2.Hide();
                        //tabPage1.Show();
                    }
                }
                else if (ReminderControl.lastReminderClick != null)
                {
                    ReminderControl.lastReminderClick.LastState = ReminderMouseState.LostFocus;
                }
            }
            else
            {
                if (lastNoteLabelClick != null)
                {
                    if (noteEditing)
                    {
                        answer = MetroMessageBox.Show(this, "You have not saved Note. Are you sure to discard changes?",
                                                                "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 140);
                        if (answer == DialogResult.No)
                        {
                            e.Cancel = true;
                            //materialTabControl1.SelectedIndex = 1;
                            //tabPage1.Hide();
                            //tabPage2.Show();
                            materialSingleLineTextField1.SelectAll();
                            return;
                        }
                        else
                        {
                            noteEditing = false;
                            if (lastNoteLabelClick == null)
                            {
                                throw new Exception("lastNoteLabelClick is null");
                            }
                            int.TryParse(lastNoteLabelClick.Name.Remove(0, 9), out noteLabelID);
                            lastNoteLabelClick.Text = Settings.Default.Notes[noteLabelID];
                            materialSingleLineTextField1.TextChanged -= materialSingleLineTextField1_TextChanged;
                            materialSingleLineTextField1.Clear();
                            materialLabel1.Focus();
                        }
                    }
                    ((MaterialLabel)lastNoteLabelClick).BackColor = NoteLabelColor(0);
                    lastNoteLabelClick = null;
                    materialFlatButton10.Hide();
                    materialFlatButton9.Hide();
                }
            }
        }

        #region Reminder Module

        private void newReminderControl(int i)
        {
            if (i == 0)
            {
                ReminderControl0.Message = Settings.Default.RemindMessage[i];
                ReminderControl0.RemindTime = Settings.Default.RemindAt[i];
                ReminderControl0.RemindFinish = !Settings.Default.RemindCompleted[i];
                ReminderControl0.Show();
                ReminderControl0.Tag = ReminderControl0.Location.Y + ReminderControl0.Size.Height;
            }
            else
            {
                ReminderControl remindctrl = new ReminderControl()
                {
                    ParentForm = this,
                    Name = "ReminderControl" + i.ToString(),
                    MinimumSize = ReminderControl0.MinimumSize,
                    Location = new Point(5,
                               (int)((ReminderControl)panel2.Controls["ReminderControl" + (i - 1).ToString()]).Tag + 8),
                    Message = Settings.Default.RemindMessage[i],
                    RemindTime = Settings.Default.RemindAt[i],
                    RemindFinish = !Settings.Default.RemindCompleted[i],

                };
                panel2.Controls.Add(remindctrl);
                remindctrl.Tag = remindctrl.Location.Y + remindctrl.Size.Height;
            }
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            if (ReminderControl.lastReminderClick != null)
            {
                ReminderControl.lastReminderClick.LastState = ReminderMouseState.LostFocus;
            }
        }

        /*
        ReminderControl currentReminderHover;

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            currentReminderHover = (ReminderControl)panel2.GetChildAtPoint(e.Location);
            if (lastReminderHover != currentReminderHover)
            {
                lastReminderHover.LastState = ReminderControl.ReminderMouseState.Default;
                lastReminderHover = currentReminderHover;
            }
        }
        */

        private void TimePicker_Choosed(object sender, EventArgs e)
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
                materialListView1.Show();
                materialListView1.Items[0].ForeColor = SkinManager.GetDividersColor();
                materialListView2.Show();
                materialListView3.Show();
                speedPanel = 5;
                timer4.Start();
                speedButton = 2;
                timer2.Start();
                //materialFlatButton4.Icon = Properties.Resources.done_blue;
                //materialFlatButton5.Show();
                if (ReminderControl.lastReminderClick != null)
                {
                    ReminderControl.lastReminderClick.LastState = ReminderMouseState.LostFocus;
                }
                materialSingleLineTextField1.Clear();
                materialSingleLineTextField1.TextChanged += materialSingleLineTextField1_TextChanged;
                //materialSingleLineTextField1.Focus();
            }
            else
            {
                if (remindMessageInputed == false)
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
                if (Settings.Default.RemindMessage == null)
                    ReminderIndex = 0;
                else ReminderIndex = Settings.Default.RemindMessage.Count;
                Timer t = new Timer();
                //t.AutoReset = false;
                if (remindAtTime <= DateTime.Now)
                    t.Interval = 10;
                else t.Interval = (int)(remindAtTime - DateTime.Now).TotalMilliseconds; 
                string s = materialListView1.Items[0].Text; 
                t.Tick += delegate (object o, EventArgs evnt)
                   {
                       ((Timer)o).Stop();
                       Reminder rmd = new Reminder(ReminderIndex);
                       //MessageBox.Show(s);
                       Notification noti = new Notification();
                       noti.Text = s;
                       noti.Dismiss += delegate (Notification nt)
                       {
                           rmd.Dismiss = true;
                           Settings.Default.Save();
                       };
                       noti.OnRemindNotify += delegate (object obj, NotificationEventArgs ev)
                       {
                           ((Timer)o).Interval = (int)ev.RemindAfter.TotalMilliseconds;
                           ((Timer)o).Start();
                           rmd.RemindAfter = ev.RemindAfter;
                           Settings.Default.Save();
                       };
                       noti.Done += delegate (Notification nt)
                       {
                           //((System.Timers.Timer)o).Stop();
                           //((System.Timers.Timer)o).Enabled = false;
                           ((Timer)o).Dispose();
                           rmd.FinishTime = DateTime.Now;
                           rmd.Completed = true;
                           Settings.Default.Save();
                       };
                       noti.Show();
                       //rmd.Dispose();
                   };
                t.Start();
                //MessageBox.Show(remindAtTime.ToString());
                //MessageBox.Show(t.Interval.ToString());
                Reminder reminder = new Reminder(ReminderIndex);
                reminder.Message = materialListView1.Items[0].Text;
                reminder.Time = remindAtTime;
                Settings.Default.Save();
                newReminderControl(ReminderIndex);
                /*
                materialLabel1.Hide();
                materialListView1.Hide();
                materialListView2.Hide();
                materialListView3.Hide();
                */
                remindCreating = false;
                remindMessageInputed = false;
                speedPanel = 5;
                timer4.Start();
                materialListView1.Items[0].Text = "Message to remind";
                materialListView2.Items[0].Text = "Day";
                materialListView3.Items[0].Text = "Time";
                
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
            answer = MetroMessageBox.Show(this, "Are you sure to cancel this reminder?", "",
                                                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 140);
            if (answer == DialogResult.No) return;
            /*
            materialLabel1.Hide();
            materialListView1.Hide();
            materialListView2.Hide();
            materialListView3.Hide();
            monthCalendar1.Hide();
            */
            remindCreating = false;
            remindMessageInputed = false;
            speedPanel = 2;
            timer4.Start();
            materialFlatButton5.Enabled = false;
            speedButton = 2;
            timer2.Start();
            if (timePickerPanel1.timePicker.ClockMenu.Visible)
                timePickerPanel1.timePicker.ClockMenu.ClockButtonCancel.PerformClick();
            materialSingleLineTextField1.TextChanged -= materialSingleLineTextField1_TextChanged;
            materialSingleLineTextField1.Clear();
            //materialListView1.Items[0].Text = "Message to remind";
            //materialListView2.Items[0].Text = "Day";
            //materialListView3.Items[0].Text = "Time";
            remindAtTime = new DateTime();
            //materialFlatButton4.Icon = Properties.Resources.alarm_blue;
            //materialFlatButton5.Hide();
        }

        private void materialListView1_Click(object sender, EventArgs e)
        {
            if (monthCalendar1.Visible) monthCalendar1.Hide();
            else if (timePickerPanel1.timePicker.ClockMenu.Visible)
                timePickerPanel1.timePicker.ClockMenu.ClockButtonCancel.PerformClick();
            materialLabel1.Text = "WHAT do you want me to remind?";
            materialSingleLineTextField1.Enabled = true;
            if (remindMessageInputed) materialSingleLineTextField1.Text = materialListView1.Items[0].Text;
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
            if (remindAtTime.Date == temp.Date)
                materialListView2.Items[0].Text = "Today";
            else if (remindAtTime.AddDays(1).Date == temp.Date)
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

        private void timer2_Tick(object sender, EventArgs e)
        {
            materialFlatButton4.Location = new Point(materialFlatButton4.Location.X,
                                                             materialFlatButton4.Location.Y + speedButton);
            if (remindCreating)
            {
                if (materialFlatButton4.Location.Y <= 335)
                {
                    //materialFlatButton4.Location = new Point(materialFlatButton4.Location.X,
                    //                                         materialFlatButton4.Location.Y + speedButton);
                    speedButton++;
                }
                else
                {
                    timer2.Stop();
                    timer2.Enabled = false;
                    materialFlatButton4.Icon = Resources.done_blue;
                    materialFlatButton4.Location = new Point(materialFlatButton4.Location.X, 335);
                    materialFlatButton5.Location = new Point(materialFlatButton5.Location.X, 335);
                    materialFlatButton5.Show();
                    speedButton = 8;
                    timer3.Start();
                }
            }
            else
            {
                if (materialFlatButton4.Location.Y <= 335)
                {
                    //materialFlatButton4.Location = new Point(materialFlatButton4.Location.X, materialFlatButton4.Location.Y + speedButton);
                    materialFlatButton5.Location = new Point(materialFlatButton5.Location.X,
                                                             materialFlatButton5.Location.Y + speedButton);
                    speedButton++;
                }
                else
                {
                    timer2.Stop();
                    timer2.Enabled = false;
                    materialFlatButton4.Icon = Resources.alarm_blue;
                    materialFlatButton4.Location = new Point(materialFlatButton4.Location.X, 335);
                    materialFlatButton5.Location = new Point(materialFlatButton5.Location.X, 335);
                    speedButton = 8;
                    timer3.Start();
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (remindCreating)
            {
                if (materialFlatButton5.Location.Y > 299)
                {
                    materialFlatButton5.Location = new Point(materialFlatButton5.Location.X,
                                                         materialFlatButton5.Location.Y - speedButton);
                }
                else
                {
                    materialFlatButton5.Enabled = true;
                }
            }
            if (materialFlatButton4.Location.Y <= 299)
            {
                timer3.Stop();
                timer3.Enabled = false;
                if (!remindCreating)
                {
                    materialListView1.Items[0].Text = "Message to remind";
                    materialListView2.Items[0].Text = "Day";
                    materialListView3.Items[0].Text = "Time";
                }
            }
            else
            {
                materialFlatButton4.Location = new Point(materialFlatButton4.Location.X,
                                                         materialFlatButton4.Location.Y - speedButton);
                
                speedButton--;
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (remindCreating)
            {
                if (panel3.Location.X <= 272)
                {
                    if (panel3.Location.X <= 220)
                    {
                        if (panel3.Location.X <= 100)
                        {
                            if (panel3.Location.X <= 50)
                            {
                                speedPanel -= 1;
                                if (panel3.Location.X - speedPanel < 0)
                                    panel3.Location = new Point(0, 0);
                                else panel3.Location = new Point(panel3.Location.X - speedPanel, 0);
                                if (panel3.Location.X <= 0)
                                {
                                    timer4.Enabled = false;
                                    materialSingleLineTextField1.Focus();
                                }
                            }
                            else
                            {
                                speedPanel -= 4;
                                panel3.Location = new Point(panel3.Location.X - speedPanel, 0);
                            }
                        }
                        else
                        {
                            speedPanel += 3;
                            panel3.Location = new Point(panel3.Location.X - speedPanel, 0);
                        }
                    }
                    else
                    {
                        speedPanel += 1;
                        panel3.Location = new Point(panel3.Location.X - speedPanel, 0);
                    }
                }
            }
            else
            {
                if (panel3.Location.X >= 0)
                {
                    if (panel3.Location.X >= 60)
                    {
                        if (panel3.Location.X >= 180)
                        {
                            if (panel3.Location.X >= 220)
                            {
                                speedPanel -= 1;
                                if (panel3.Location.X + speedPanel > 272)
                                    panel3.Location = new Point(272, 0);
                                else panel3.Location = new Point(panel3.Location.X + speedPanel, 0);
                                if (panel3.Location.X == 272)
                                {
                                    timer4.Enabled = false;
                                    materialLabel1.Focus();
                                }
                            }
                            else
                            {
                                speedPanel -= 4;
                                panel3.Location = new Point(panel3.Location.X + speedPanel, 0);
                            }
                        }
                        else
                        {
                            speedPanel += 2;
                            panel3.Location = new Point(panel3.Location.X + speedPanel, 0);
                        }
                    }
                    else
                    {
                        speedPanel += 1;
                        panel3.Location = new Point(panel3.Location.X + speedPanel, 0);
                    }
                }
            }
        }

        #endregion

        #region Note Module

        private void materialFlatButton6_Click(object sender, EventArgs e)
        {
            tabPage2_Click(tabPage2, new EventArgs());
            _login.newNote();
        }

        public Color NoteLabelColor(NoteLabelStatus status)
        {
            switch (status)
            {
                case NoteLabelStatus.Default:
                    {
                        return SkinManager.Theme == MaterialSkinManager.Themes.DARK ? Color.FromArgb(60, 60, 60) 
                                                                       : SkinManager.GetFlatButtonHoverBackgroundColor();
                    }
                case NoteLabelStatus.Hover:
                    {
                        return SkinManager.Theme == MaterialSkinManager.Themes.DARK ?
                                             SkinManager.GetFlatButtonHoverBackgroundColor() : Color.FromArgb(205, 205, 205);
                    }
                case NoteLabelStatus.Clicked:
                    {
                        return SkinManager.Theme == MaterialSkinManager.Themes.DARK ? Color.FromArgb(100, 100, 100)
                                                                                    : Color.FromArgb(170, 170, 170);
                    }
                default: return Color.Transparent;
            }
        }

        public void newNoteLabel(int i)
        {
            if (i == 0)
            {
                NoteLabel0.Text = Settings.Default.Notes[0];
                NoteLabel0.Tag = NoteLabel0.Location.Y + NoteLabel0.Size.Height;
                NoteLabel0.Show();
                return;
            }
            MaterialLabel mlabel = new MaterialLabel();
            mlabel.Name = "NoteLabel" + i.ToString();
            mlabel.AutoEllipsis = true;
            mlabel.AutoSize = true;
            mlabel.Font = new Font("Roboto", 11F);
            mlabel.MaximumSize = new Size(270, 60);
            mlabel.MinimumSize = new Size(270, 40);
            mlabel.TextAlign = ContentAlignment.MiddleCenter;
            mlabel.Text = Settings.Default.Notes[i];
            mlabel.BackColor = NoteLabelColor(0);
            mlabel.Location = new Point(5,
                   (int)((MaterialLabel)(tabPage2.Controls["NoteLabel" + (i - 1).ToString()])).Tag + 8);
            mlabel.Size = new System.Drawing.Size(270, 40);
            mlabel.MouseEnter += NoteLabel_MouseEnter;
            mlabel.MouseLeave += NoteLabel_MouseLeave;
            mlabel.Click += NoteLabel_Click;
            tabPage2.Controls.Add(mlabel);      
            mlabel.Tag = mlabel.Location.Y + mlabel.Size.Height;
        }

        private void NoteLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = HandCursor;
            if (((MaterialLabel)sender) != lastNoteLabelClick)
                ((MaterialLabel)sender).BackColor = NoteLabelColor(NoteLabelStatus.Hover);
        }

        private void NoteLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            if (((MaterialLabel)sender) != lastNoteLabelClick)
                ((MaterialLabel)sender).BackColor = NoteLabelColor(0);
        }

        private void NoteLabel_Click(object sender, EventArgs e)
        {
            if (noteEditing) return;
            if (lastNoteLabelClick != null)
                lastNoteLabelClick.BackColor = NoteLabelColor(0);
            ((MaterialLabel)sender).BackColor = NoteLabelColor(NoteLabelStatus.Clicked);
            speedButton = 8;
            timer5.Start();
            lastNoteLabelClick = ((MaterialLabel)sender);
        }

        private void UpdateNoteLabelColor()
        {
            for (int i = 0; i < Settings.Default.NoteCount; i++)
            {
                ((MaterialLabel)tabPage2.Controls["NoteLabel" + i.ToString()]).BackColor = NoteLabelColor(0);
            }
            if (lastNoteLabelClick != null)
                lastNoteLabelClick.BackColor = NoteLabelColor(NoteLabelStatus.Clicked);
        }

        int noteLabelID;

        private void materialFlatButton9_Click(object sender, EventArgs e)
        {
            if (!noteEditing)
            {
                noteEditing = true;
                speedButton = 2;
                timer6.Start();
                materialSingleLineTextField1.Text = lastNoteLabelClick.Text;
                materialSingleLineTextField1.SelectAll();
                materialSingleLineTextField1.TextChanged += materialSingleLineTextField1_TextChanged;
            }
            else
            {
                if (lastNoteLabelClick == null)
                {
                    throw new Exception("lastNoteLabelClick is null");
                }
                int.TryParse(lastNoteLabelClick.Name.Remove(0, 9), out noteLabelID);
                _login.notes[noteLabelID].materialLabel1.Text = lastNoteLabelClick.Text;
                Settings.Default.Notes[noteLabelID] = lastNoteLabelClick.Text;
                Settings.Default.Save();
                materialSingleLineTextField1.TextChanged -= materialSingleLineTextField1_TextChanged;
                materialSingleLineTextField1.Clear();
                NoteLabel0.Focus();
                lastNoteLabelClick.BackColor = NoteLabelColor(0);
                lastNoteLabelClick = null;
                noteEditing = false;
                speedButton = 2;
                timer6.Start();
            }
        }

        private void materialFlatButton10_Click(object sender, EventArgs e)
        {
            if (lastNoteLabelClick == null)
            {
                throw new Exception("lastNoteLabelClick is null");
            }
            int.TryParse(lastNoteLabelClick.Name.Remove(0, 9), out noteLabelID);
            if (noteEditing)
            {
                lastNoteLabelClick.Text = Settings.Default.Notes[noteLabelID];
                materialSingleLineTextField1.TextChanged -= materialSingleLineTextField1_TextChanged;
                materialSingleLineTextField1.Clear();
                NoteLabel0.Focus();
                lastNoteLabelClick.BackColor = NoteLabelColor(0);
                lastNoteLabelClick = null;
                noteEditing = false;
                speedButton = 2;
                timer6.Start();
            }
            else
            {
                answer = MetroMessageBox.Show(this, Settings.Default.Notes[noteLabelID],
                                 string.Format("Are you sure to delete Note{0}", noteLabelID + 1),
                                 MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 125);
                if (answer == DialogResult.Yes)
                {
                    _login.notes[noteLabelID].close = true;
                    _login.notes[noteLabelID].Close();
                    lastNoteLabelClick = null;
                    speedButton = 2;
                    timer6.Start();
                }
            }
        }

        public void deleteNoteLabel(int i)
        {
            if (i >= Settings.Default.NoteCount)
            {
                MessageBox.Show(string.Format("Found an error! deleteNoteLabel({0})", i));
                return;
            }
            else
            {
                ((MaterialLabel)(tabPage2.Controls["NoteLabel" + i.ToString()])).Hide();
                if (lastNoteLabelClick == ((MaterialLabel)(tabPage2.Controls["NoteLabel" + i.ToString()])))
                {
                    speedButton = 2;
                    timer6.Start();
                }
                ((MaterialLabel)(tabPage2.Controls["NoteLabel" + i.ToString()])).Name = "NoteLabelDeleting";
                if (i + 1 < Settings.Default.NoteCount)
                {
                    var mlabel = ((MaterialLabel)(tabPage2.Controls["NoteLabel" + (i + 1).ToString()]));
                    mlabel.Location = ((MaterialLabel)(tabPage2.Controls["NoteLabelDeleting"])).Location;
                    mlabel.Tag = mlabel.Location.Y + mlabel.Size.Height;
                    mlabel.Name = "NoteLabel" + i.ToString();
                    for (int j = i + 2; j < Settings.Default.NoteCount; j++)
                    {
                        var mlabelAfter = ((MaterialLabel)(tabPage2.Controls["NoteLabel" + j.ToString()]));
                        mlabelAfter.Location = new Point(5, 
                                       (int)((MaterialLabel)(tabPage2.Controls["NoteLabel" + (j - 2).ToString()])).Tag + 8);
                        mlabelAfter.Tag = mlabelAfter.Location.Y + mlabelAfter.Height;
                        mlabelAfter.Name = "NoteLabel" + (j - 1).ToString();
                    }
                }
                ((MaterialLabel)(tabPage2.Controls["NoteLabelDeleting"])).Dispose();
                NoteLabel0.Focus();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            if (noteEditing) return;
            if (lastNoteLabelClick != null)
            {
                object objectTemp = lastNoteLabelClick as object;
                lastNoteLabelClick = null;
                NoteLabel_MouseLeave(objectTemp, new EventArgs());
                //lastNoteLabelClick.BackColor = BackColor = Color.FromArgb(60, 60, 60);
            }
            speedButton = 2;
            timer6.Start();
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            if (!materialFlatButton10.Visible)
            {
                materialFlatButton9.Location = new Point(200, 335);
                materialFlatButton10.Location = new Point(162, 335);
                if (!noteEditing)
                {
                    materialFlatButton9.Icon = Resources.edit_blue;
                    materialFlatButton10.Icon = Resources.delete_blue;
                }
                materialFlatButton9.Show();
                materialFlatButton10.Show();
            }
            if (materialFlatButton10.Location.Y <= 299)
            {
                //if (timer5.Enabled)
                //{
                    timer5.Stop();
                    timer5.Enabled = false;
                    materialFlatButton10.Enabled = true;
                    materialFlatButton9.Enabled = true;
                //}
            }
            else
            {
                materialFlatButton10.Location = new Point(materialFlatButton10.Location.X, materialFlatButton10.Location.Y - speedButton);
                materialFlatButton9.Location = new Point(materialFlatButton9.Location.X, materialFlatButton9.Location.Y - speedButton);
                speedButton--;
            }
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            /*
            if (!materialFlatButton10.Visible)
            {
                timer6.Enabled = false;
                return;
            }
            */
            if (materialFlatButton10.Location.Y >= 335)
            {
                //if (timer6.Enabled)
                //{
                    timer6.Stop();
                    timer6.Enabled = false;
                    materialFlatButton10.Hide();
                    materialFlatButton9.Hide();
                //}
                if (noteEditing)
                {
                    materialFlatButton9.Icon = Resources.done_blue;
                    materialFlatButton10.Icon = Resources.clear_blue;
                    speedButton = 8;
                    timer5.Start();
                }
                else
                {
                    materialFlatButton10.Enabled = false;
                    materialFlatButton9.Enabled = false;
                }
            }
            else
            {
                materialFlatButton10.Location = new Point(materialFlatButton10.Location.X, materialFlatButton10.Location.Y + speedButton);
                materialFlatButton9.Location = new Point(materialFlatButton9.Location.X, materialFlatButton9.Location.Y + speedButton);
                speedButton++;
            }
        }

        public void UpdateNoteLabel(int i)
        {
            var mlabel = ((MaterialLabel)(tabPage2.Controls["NoteLabel" + i.ToString()]));
            mlabel.Tag = mlabel.Location.Y + mlabel.Size.Height;
            for (int j = i + 1; j < Settings.Default.NoteCount; j++)
            {
                var mlabelAfter = ((MaterialLabel)(tabPage2.Controls["NoteLabel" + j.ToString()]));
                mlabelAfter.Location = new Point(5,
                            (int)((MaterialLabel)(tabPage2.Controls["NoteLabel" + (j - 1).ToString()])).Tag + 8);
                mlabelAfter.Tag = mlabelAfter.Location.Y + mlabelAfter.Size.Height;
            }
        }

        public enum NoteLabelStatus
        {
            Default,
            Hover,
            Clicked
        }

        #endregion

        private void materialSingleLineTextField1_TextChanged(object sender, EventArgs e)
        {
            if (remindCreating)
            {
                remindMessageInputed = true;
                materialListView1.Items[0].Text = materialSingleLineTextField1.Text;
            }
            else if (noteEditing)
            {
                if (lastNoteLabelClick == null)
                {
                    throw new Exception("lastNoteLabelClick is null");
                }
                lastNoteLabelClick.Text = materialSingleLineTextField1.Text;
            }
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
                TimeSpan ts = new TimeSpan();
                TimeSpan.TryParse(materialSingleLineTextField1.Text, out ts);
                MessageBox.Show(ts.ToString());
            }
        }

        #region Extension user32.dll function

        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOACTIVATE = 0x0010;
        private const int SWP_NOMOVE = 0x0002;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, 
                                                 int X, int Y, int cx, int cy, uint uFlags);

        enum idCursor
        {
            HAND = 32649,
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr LoadCursor(IntPtr hInstance, idCursor cursor);

        [DllImport("winmm.dll", SetLastError = true)]
        static extern bool PlaySound(string pszSound, UIntPtr hmod, uint fdwSound);
        [Flags]
        public enum sndFlags
        {
            SND_SYNC = 0x0000,
            SND_ASYNC = 0x0001,
            SND_NODEFAULT = 0x0002,
            SND_LOOP = 0x0008,
            SND_NOSTOP = 0x0010,
            SND_NOWAIT = 0x00002000,
            SND_FILENAME = 0x00020000,
            SND_RESOURCE = 0x00040004
        }

        #endregion

    }
}
