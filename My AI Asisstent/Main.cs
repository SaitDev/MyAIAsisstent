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
        private bool remindCreating = false, remindMessageInputed, noteEditing = false, finishLoad = false;
        private DialogResult answer;
        private Cursor HandCursor;
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
                this.Activate();
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
            if (Settings.Default.NoteCount > 0)
            {
                NoteLabel0.Text = Settings.Default.Notes[0];
                NoteLabel0.BackColor = NoteLabelColor(0);
                //MessageBox.Show(SkinManager.GetFlatButtonHoverBackgroundColor().RemoveAlpha().ToString());
                //MessageBox.Show(NoteLabel0.BackColor.ToString());
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
            lastActive = materialFlatButton3;
            lastActive.Focus();
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
            HandCursor = new Cursor(LoadCursor(IntPtr.Zero, idCursor.HAND));
            finishLoad = true;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (materialTabControl1.SelectedIndex == 1 && lastNoteLabelClick != null)
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
                        NoteLabel0.Focus();
                    }
                }
                lastNoteLabelClick.BackColor = NoteLabelColor(0);
                lastNoteLabelClick = null;
                materialFlatButton10.Hide();
                materialFlatButton9.Hide();
            }
            this.Hide();
            e.Cancel = true;
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

        private void materialToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Show();
            this.Activate();
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
            if (materialTabControl1.SelectedIndex == 1 && !noteEditing)
            {
                if (lastNoteLabelClick != null)
                {
                    lastNoteLabelClick.BackColor = NoteLabelColor(0);
                    lastNoteLabelClick = null;
                }
                speed = 2;
                timer3.Start();
            }
            materialFlatButton_Click(sender, e);
            if (_login._setting.Visible == true)
                _login._setting.Hide();
            _login._setting.materialCheckBox3.Checked = false;
            _login._setting.materialCheckBox3.Enabled = false;
            _login._setting.ShowDialog();
            _login._setting.materialCheckBox3.Enabled = true;
            materialFlatButton1.UseCustomBackColor = true;
            materialFlatButton1.Refresh();
            lastActive.UseCustomBackColor = false;
            lastActive.Refresh();
            animateProgress = 10;
            animateStep = (lastActive.Location.Y - materialFlatButton1.Location.Y) / 54;
            timer1.Start();
            if (noteEditing) materialSingleLineTextField1.SelectAll();
            //panel1.Location = lastActive.Location;
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
            if (materialTabControl1.SelectedIndex == 1 && lastNoteLabelClick != null)
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
                        NoteLabel0.Focus();
                    }
                }
                lastNoteLabelClick.BackColor = NoteLabelColor(0);
                lastNoteLabelClick = null;
                materialFlatButton10.Hide();
                materialFlatButton9.Hide();
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

        private void materialTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (materialTabControl1.SelectedIndex == 1)
            {
                if (remindCreating)
                {
                    answer = MetroMessageBox.Show(this, "You have not saved reminder. Are you sure to discard?",
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
                        remindMessageInputed = false;
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
            else
            {
                if (lastNoteLabelClick != null)
                {
                    ((MaterialLabel)lastNoteLabelClick).BackColor = NoteLabelColor(0);
                    lastNoteLabelClick = null;
                    materialFlatButton10.Hide();
                    materialFlatButton9.Hide();
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
                materialSingleLineTextField1.Clear();
                materialSingleLineTextField1.TextChanged += materialSingleLineTextField1_TextChanged;
                materialSingleLineTextField1.Focus();
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
                remindMessageInputed = false;
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
            remindMessageInputed = false;
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

        #endregion

        #region Note Module

        private void materialFlatButton6_Click(object sender, EventArgs e)
        {
            tabPage2_Click(tabPage2, new EventArgs());
            _login.newNote();
        }

        private Color NoteLabelColor(NoteLabelStatus status)
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
            MaterialLabel mlabel = new MaterialLabel();
            mlabel.Name = "NoteLabel" + i.ToString();
            mlabel.AutoEllipsis = true;
            mlabel.AutoSize = true;
            mlabel.Font = new System.Drawing.Font("Roboto", 11F);
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
            if (lastNoteLabelClick != null)
                lastNoteLabelClick.BackColor = NoteLabelColor(0);
            ((MaterialLabel)sender).BackColor = NoteLabelColor(NoteLabelStatus.Clicked);
            speed = 8;
            timer2.Start();
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
                speed = 2;
                timer3.Start();
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
                speed = 2;
                timer3.Start();
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
                speed = 2;
                timer3.Start();
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
            speed = 2;
            timer3.Start();
        }

        int speed;

        private void timer2_Tick(object sender, EventArgs e)
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
                timer2.Enabled = false;
                materialFlatButton10.Enabled = true;
                materialFlatButton9.Enabled = true;
                return;
            }
            materialFlatButton10.Location = new Point(materialFlatButton10.Location.X, materialFlatButton10.Location.Y - speed);
            materialFlatButton9.Location = new Point(materialFlatButton9.Location.X, materialFlatButton9.Location.Y - speed);
            speed--;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (!materialFlatButton10.Visible)
            {
                timer3.Enabled = false;
                return;
            }
            if (materialFlatButton10.Location.Y >= 335)
            {
                timer3.Enabled = false;
                materialFlatButton10.Hide();
                materialFlatButton9.Hide();
                if (noteEditing)
                {
                    materialFlatButton9.Icon = Resources.done_blue;
                    materialFlatButton10.Icon = Resources.clear_blue;
                    speed = 8;
                    timer2.Start();
                    return;
                }
                materialFlatButton10.Enabled = false;
                materialFlatButton9.Enabled = false;
                return;
            }
            materialFlatButton10.Location = new Point(materialFlatButton10.Location.X, materialFlatButton10.Location.Y + speed);
            materialFlatButton9.Location = new Point(materialFlatButton9.Location.X, materialFlatButton9.Location.Y + speed);
            speed++;
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

        enum NoteLabelStatus
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
            }
        }
        

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
    }
}
