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
        public bool close = false;
        Random rnd = new Random();

        public Notes(Login login)
        {
            InitializeComponent();
            login._main.materialSkinManager.AddFormToManage(this);
            _login = login;
            //metroLink1.BackColor = ColorTranslator.FromHtml("#42a5f5");
            metroLink1.Enabled = false;
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

                Settings.Default.Locations[index] = new Point(rnd.Next(300), rnd.Next(300));
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
            }
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
            //else this.Opacity = Properties.Settings.Default.Opacity1;
        }

        private void Notes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close)
            {
                DeleteNote();
                return;
            }
            string t = this.Text;
            string s = "Are you sure want to delete " + t;
            if ((MessageBox.Show(this, s, t, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)) == DialogResult.Yes)
            {
                DeleteNote();
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
            //Properties.Settings.Default.Notes.RemoveAt(index);
            //Properties.Settings.Default.Notes.Insert(index, materialLabel1.Text);
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
            //MessageBox.Show(Properties.Settings.Default.Opacity1.ToString());
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Opacity = (double)0.9;
            Settings.Default.Opacitys[index] = 0.9;
            Settings.Default.Save();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //Properties.Settings.Default.Opacitys[index] = Convert.ToDouble(0.8);
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
            /*
            if (_login.noteCount == 10)
            {
                MessageBox.Show("Reached limitation of note");
            }
            else */
            _login.newNote();
            /*
            _login.noteCount++;
            var i = _login.noteCount - 1;
            _login.notes[i] = new Notes(_login);
            _login.notes[i].index = i;
            _login.notes[i].Text = "Note " + _login.noteCount.ToString();
            _login.notes[i].Show();
            */
        }

        private void Notes_Shown(object sender, EventArgs e)
        {
            this.materialContextMenuStrip1.AutoSize = false;
            this.materialContextMenuStrip1.Size = new Size(127, this.materialContextMenuStrip1.Size.Height);
        }

        private void materialToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
            Settings.Default.NoteOnTop[index] = TopMost;
            Settings.Default.Save();
        }

        private void materialToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            metroLink3.PerformClick();
        }
    }
}
