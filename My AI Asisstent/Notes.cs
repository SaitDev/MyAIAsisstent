using System;
using System.Drawing;
using System.Windows.Forms;

using MaterialSkin;
using MaterialSkin.Controls;
using System.Collections.Generic;
using System.Linq;
using MyAIAsisstent.Properties;

namespace MyAIAsisstent
{
    public partial class Notes : MaterialForm
    {
        private Login _login;
        public int index;
        private bool moved;
        public bool close = false, systemEndSession = false;
        Random rnd = new Random();

        public Notes(Login login)
        {
            InitializeComponent();
            login._main.materialSkinManager.AddFormToManage(this);
            _login = login;
            //metroLink1.BackColor = ColorTranslator.FromHtml("#42a5f5");
            metroLink1.Enabled = false;
        }

        private static int WM_QUERYENDSESSION = 0x11;
        private static int ENDSESSION_CLOSEAPP = 0x1;
        private static uint ENDSESSION_CRITICAL = 0x40000000;
        private static uint ENDSESSION_LOGOFF = 0x80000000;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_QUERYENDSESSION)
            {
                if (m.LParam == (IntPtr)ENDSESSION_LOGOFF)
                {
                    systemEndSession = true;
                    Close();
                }
            }
            // If this is WM_QUERYENDSESSION, the closing event should be
            // raised in the base WndProc.
            base.WndProc(ref m);
        }

        private void Notes_Load(object sender, EventArgs e)
        {
            if ((index < Settings.Default.NoteCount) && (Settings.Default.NoteCount != 0))
            {
                Location = Settings.Default.Locations[index];
                Opacity = Settings.Default.Opacitys[index];
                materialLabel1.Text = Settings.Default.Notes[index];
                metroTextBox1.Text = materialLabel1.Text;
                TopMost = Settings.Default.NoteOnTop[index];
                if (TopMost) materialToolStripMenuItem4.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                else materialToolStripMenuItem4.DisplayStyle = ToolStripItemDisplayStyle.Text;
            }
            else
            {
                if (Settings.Default.NoteCount == 0)
                {
                    Settings.Default.Locations = new Point[1];
                    Settings.Default.Opacitys = new double[1];
                    Settings.Default.NoteOnTop = new bool[1];
                }
                else
                {
                    Point[] temp1 = Settings.Default.Locations;
                    Array.Resize<Point>(ref temp1, index + 1);
                    Settings.Default.Locations = temp1;
                    double[] temp2 = Settings.Default.Opacitys;
                    Array.Resize<Double>(ref temp2, index + 1);
                    Settings.Default.Opacitys = temp2;
                    bool[] temp3 = Settings.Default.NoteOnTop;
                    Array.Resize<Boolean>(ref temp3, index + 1);
                    Settings.Default.NoteOnTop = temp3;
                }
                Settings.Default.Locations[index] = new Point(rnd.Next(600), rnd.Next(250));
                Settings.Default.Opacitys[index] = 0.9;
                Settings.Default.NoteOnTop[index] = false;
                if (Settings.Default.Notes == null)
                    Settings.Default.Notes = new System.Collections.Specialized.StringCollection();
                Settings.Default.Notes.Add("Double click to edit");
                Settings.Default.NoteCount++;
                Settings.Default.Save();
                Location = Settings.Default.Locations[index];
                Opacity = Opacity = Settings.Default.Opacitys[index];
                materialLabel1.Text = Settings.Default.Notes[index];
                metroTextBox1.Text = materialLabel1.Text;
                materialToolStripMenuItem4.DisplayStyle = ToolStripItemDisplayStyle.Text;
            }
        }

        private void Notes_Shown(object sender, EventArgs e)
        {
            this.materialContextMenuStrip1.AutoSize = false;
            this.materialContextMenuStrip1.Size = new Size(127, this.materialContextMenuStrip1.Size.Height);
            //Microsoft.Win32.SystemEvents.SessionEnding += (o, evnt) => { close = true; Close(); };
        }

        private void Notes_Move(object sender, EventArgs e)
        {
            Opacity = 0.5;
            moved = true;
        }

        private void Notes_ResizeEnd(object sender, EventArgs e)
        {
            if (moved)
            {
                this.Opacity = Settings.Default.Opacitys[index];
                Settings.Default.Locations[index] = Location;
                Settings.Default.Save();
                moved = false;
            }
        }

        private void Notes_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                e.Cancel = false;
                //Close();
                return;
            }
            */
            if (close || systemEndSession)
            {
                if (close) DeleteNote();
                e.Cancel = false;
                return;
            }
            string t = this.Text;
            string s = "Are you sure want to delete " + t;
            if ((MessageBox.Show(this, s, t, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)) == DialogResult.Yes)
            {
                DeleteNote();
                e.Cancel = false;
            }
            else e.Cancel = true;
        }

        public void DeleteNote()
        {
            _login._main.deleteNoteLabel(index);
            Settings.Default.NoteCount--;
            var temp1 = new List<Point>(Settings.Default.Locations);
            temp1.RemoveAt(index);
            Settings.Default.Locations = temp1.ToArray();
            var temp2 = new List<Double>(Settings.Default.Opacitys);
            temp2.RemoveAt(index);
            Settings.Default.Opacitys = temp2.ToArray();
            var temp3 = new List<Boolean>(Settings.Default.NoteOnTop);
            temp3.RemoveAt(index);
            Settings.Default.NoteOnTop = temp3.ToArray();
            Settings.Default.Notes.RemoveAt(index);
            Settings.Default.Save();
            _login.noteCount = Settings.Default.NoteCount;
            if (index < _login.noteCount)
            {
                for (int i = index; i < _login.noteCount; i++)
                {
                    _login.notes[i] = _login.notes[i + 1];
                    _login.notes[i].index = i;
                    _login.notes[i].Text = "Note " + (i + 1).ToString();
                    _login.notes[i].Refresh();
                }
            }
        }

        private void materialLabel1_DoubleClick(object sender, EventArgs e)
        {
            if (_login._main.materialSkinManager.Theme == MaterialSkinManager.Themes.DARK)
            {
                metroTextBox1.BackColor = Color.FromArgb(255, 51, 51, 51);
                metroTextBox1.ForeColor = ColorTranslator.FromHtml("#42a5f5");
            }
            else metroTextBox1.BackColor = Color.FromArgb(255, 255, 255, 255);
            metroTextBox1.Show();
            metroLink1.Enabled = true;
        }

        private void metroLink1_Click(object sender, EventArgs e)
        {
            materialLabel1.Text = metroTextBox1.Text;
            metroTextBox1.Hide();
            metroLink1.Enabled = false;
            Size temp = _login._main.tabPage2.Controls["NoteLabel" + index.ToString()].Size;
            _login._main.tabPage2.Controls["NoteLabel" + index.ToString()].Text = materialLabel1.Text;
            if (_login._main.tabPage2.Controls["NoteLabel" + index.ToString()].Size != temp)
                _login._main.UpdateNoteLabel(index);
            Settings.Default.Notes[index] = materialLabel1.Text;
            Settings.Default.Save();
        }

        private void metroLink2_Click(object sender, EventArgs e)
        {
            _login._setting.bindSetting(index);
            _login._setting.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Opacity = Convert.ToDouble(1.0);
            Settings.Default.Opacitys[index] = 1;
            Settings.Default.Save();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Opacity = (double)0.9;
            Settings.Default.Opacitys[index] = 0.9;
            Settings.Default.Save();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Opacity = (double)0.8;
            Settings.Default.Opacitys[index] = 0.8;
            Settings.Default.Save();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            this.Opacity = (double)0.7;
            Settings.Default.Opacitys[index] = 0.7;
            Settings.Default.Save();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.Opacity = (double)0.6;
            Settings.Default.Opacitys[index] = 0.6;
            Settings.Default.Save();
        }

        private void materialToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.materialLabel1_DoubleClick(sender, e);
        }

        private void materialToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void metroLink3_Click(object sender, EventArgs e)
        {
            _login.newNote();
        }

        private void materialToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
            Settings.Default.NoteOnTop[index] = TopMost;
            Settings.Default.Save();
            if (TopMost) materialToolStripMenuItem4.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            else materialToolStripMenuItem4.DisplayStyle = ToolStripItemDisplayStyle.Text;
        }

        private void materialToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            metroLink3.PerformClick();
        }
    }
}
