using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MaterialSkin;
using MaterialSkin.Controls;

namespace MyAIAsisstent
{
    public partial class Setting : MaterialForm
    {
        private Login _login;
        private MaterialSkinManager materialSkinManager;
        public Setting(Login lgi)
        {
            InitializeComponent();;
            _login = lgi;
            materialSkinManager = _login.materialSkinManager;
            materialSkinManager.AddFormToManage(this);
        }


        private void Setting_Load(object sender, EventArgs e)
        {
            materialRaisedButton1.PerformClick();
            this.Opacity = 1;
        }


        private void Setting_Shown(object sender, EventArgs e)
        {
            _login.Hide();
        }

        private void Setting_FormClosed(object sender, FormClosedEventArgs e)
        {
            _login.Show();
        }

        private void Setting_Layout(object sender, LayoutEventArgs e)
        {
            //this.Opacity = 1;
        }

        private void Setting_Move(object sender, EventArgs e)
        {
            this.Opacity = .5;
        }

        private void Setting_ResizeEnd(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void materialRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //Properties.Settings.Default.LightTheme = materialRadioButton1.Checked;
            //MessageBox.Show("shit");
        }

        private void materialRadioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.LightTheme)
            {
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                materialRadioButton1.Checked = true;
            }
            else
            {
                materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
                materialRadioButton2.Checked = true;
            }
            Properties.Settings.Default.Save();
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
