using System;
using System.Drawing;
using System.Windows.Forms;

using MaterialSkin;
using MaterialSkin.Controls;
using System.Collections.Generic;
using System.Linq;

namespace MyAIAsisstent
{
    public partial class Notes : MaterialForm
    {
        private Main _main;
        public int index;
        private bool moved, close = false;
        Random rnd = new Random();

        public Notes(Main main)
        {
            InitializeComponent();
            main.materialSkinManager.AddFormToManage(this);
            _main = main;
            //metroLink1.BackColor = ColorTranslator.FromHtml("#42a5f5");
            metroLink1.Enabled = false;
        }

        private void Notes_Load(object sender, EventArgs e)
        {
            if ((index < Properties.Settings.Default.NoteCount)&&(Properties.Settings.Default.NoteCount!=0))
            {
                Location = Properties.Settings.Default.Locations[index];
                Opacity = Properties.Settings.Default.Opacitys[index];
                materialLabel1.Text = Properties.Settings.Default.Notes[index];
                metroTextBox1.Text = materialLabel1.Text;
                TopMost = Properties.Settings.Default.NoteOnTop[index];
           }
            else
            {
                Point[] temp1 = Properties.Settings.Default.Locations;
                Array.Resize<Point>(ref temp1, _main.noteCount);
                Properties.Settings.Default.Locations = temp1;
                Double[] temp2 = Properties.Settings.Default.Opacitys;
                Array.Resize<Double>(ref temp2, _main.noteCount);
                Properties.Settings.Default.Opacitys = temp2;
                Boolean[] temp3 = Properties.Settings.Default.NoteOnTop;
                Array.Resize<Boolean>(ref temp3, _main.noteCount);
                Properties.Settings.Default.NoteOnTop = temp3;

                Properties.Settings.Default.Locations[index] = new Point(rnd.Next(300), rnd.Next(300));
                Properties.Settings.Default.Opacitys[index] = 0.9;
                Properties.Settings.Default.NoteOnTop[index] = false;
                Properties.Settings.Default.Notes.Add("Double click to edit");
                Properties.Settings.Default.NoteCount++;
                Properties.Settings.Default.Save();

                Location = Properties.Settings.Default.Locations[index];
                Opacity = Opacity = Properties.Settings.Default.Opacitys[index];
                materialLabel1.Text = Properties.Settings.Default.Notes[index];
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
                this.Opacity = Properties.Settings.Default.Opacitys[index];
                Properties.Settings.Default.Locations[index] = Location;
                Properties.Settings.Default.Save();
                moved = false;
            }
            //else this.Opacity = Properties.Settings.Default.Opacity1;
        }

        private void Notes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close) return;
            string t = this.Text;
            string s = "Are you sure want to delete " + t;
            if ((MessageBox.Show(this, s, t, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)) == DialogResult.Yes)
            {
                //materialLabel1.Text = "";
                Properties.Settings.Default.NoteCount--;
                var temp1 = new List<Point>(Properties.Settings.Default.Locations);
                temp1.RemoveAt(index);
                Properties.Settings.Default.Locations = temp1.ToArray();
                var temp2 = new List<Double>(Properties.Settings.Default.Opacitys);
                temp2.RemoveAt(index);
                Properties.Settings.Default.Opacitys = temp2.ToArray();
                Properties.Settings.Default.Notes.RemoveAt(index);
                Properties.Settings.Default.Save();
                _main.noteCount--;
                close = true;
                if (_main.noteCount == 0) Environment.Exit(0);
                else
                {
                    //Properties.Settings.Default.NoteCount = _main.noteCount;
                    //Properties.Settings.Default.Save();
                    this.Close();
                }
            }
            else e.Cancel = true;
        }

        private void materialLabel1_DoubleClick(object sender, EventArgs e)
        {
            if (_main.materialSkinManager.Theme == MaterialSkinManager.Themes.DARK)
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
            Properties.Settings.Default.Notes.RemoveAt(index);
            Properties.Settings.Default.Notes.Insert(index, materialLabel1.Text);
            Properties.Settings.Default.Save();
        }

        private void metroLink2_Click(object sender, EventArgs e)
        {
            _main._setting.bindSetting(index);
            _main._setting.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Opacity = Convert.ToDouble(1.0);
            Properties.Settings.Default.Opacitys[index] = 1;
            Properties.Settings.Default.Save();
            //MessageBox.Show(Properties.Settings.Default.Opacity1.ToString());
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Opacity = (double)0.9;
            Properties.Settings.Default.Opacitys[index] = 0.9;
            Properties.Settings.Default.Save();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //Properties.Settings.Default.Opacitys[index] = Convert.ToDouble(0.8);
            Opacity = (double)0.8;
            Properties.Settings.Default.Opacitys[index] = 0.8;
            Properties.Settings.Default.Save();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            this.Opacity = (double)0.7;
            Properties.Settings.Default.Opacitys[index] = 0.7;
            Properties.Settings.Default.Save();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.Opacity = (double)0.6;
            Properties.Settings.Default.Opacitys[index] = 0.6;
            Properties.Settings.Default.Save();
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
            if (_main.noteCount == 10)
            {
                MessageBox.Show("Reached limitation of note");
            }
            else */
            _main.newNote(_main.noteCount);
            /*
            _main.noteCount++;
            var i = _main.noteCount - 1;
            _main.notes[i] = new Notes(_main);
            _main.notes[i].index = i;
            _main.notes[i].Text = "Note " + _main.noteCount.ToString();
            _main.notes[i].Show();
            */
        }

        private void Notes_Shown(object sender, EventArgs e)
        {
            this.materialContextMenuStrip1.AutoSize = false;
            this.materialContextMenuStrip1.Size = new Size(127, this.materialContextMenuStrip1.Size.Height - 20);
        }

        private void materialToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
            Properties.Settings.Default.Save();
        }

        private void materialToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            metroLink3.PerformClick();
        }
    }
}
