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
using Microsoft.Win32;
using System.Threading.Tasks;

namespace MyAIAsisstent
{
    public partial class Setting : MaterialForm
    {
        private Login _login;
        private MaterialSkinManager materialSkinManager;
        RegistryKey regApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        private string bindadress;
        private int noteIndex;
        public bool fromMain = false;

        public Setting(Login login)
        {
            InitializeComponent();;
            _login = login;
            materialSkinManager = _login._main.materialSkinManager;
            materialSkinManager.AddFormToManage(this);
        }


        private void Setting_Load(object sender, EventArgs e)
        {
            this.Opacity = 1;
            if (materialSkinManager.Theme == MaterialSkinManager.Themes.DARK)
            {
                materialRadioButton2.Checked = true;
            }
        }


        private void Setting_Shown(object sender, EventArgs e)
        {
            //_login.Hide();
        }

        private void Setting_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            //this.materialCheckBox3.DataBindings.Remove(new System.Windows.Forms.Binding("Checked", global::MyAIAsisstent.Properties.Settings.Default, bindadress, true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            e.Cancel = true;
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
            if (Properties.Settings.Default.AutoStart)
            {
                regApp.SetValue("MyAIAsisstent", Application.ExecutablePath.ToString());
            }
            else regApp.DeleteValue("MyAIAsisstent", false);
            if (!fromMain)
            {
                Properties.Settings.Default.NoteOnTop[noteIndex] = materialCheckBox3.Checked;
                Properties.Settings.Default.Save();
                _login.notes[noteIndex].TopMost = materialCheckBox3.Checked;
            }
            //Application.DoEvents();
            DelayClose(400);
        }

        private async void DelayClose(int i)
        {
            await Task.Delay(i);
            Close();
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            DelayClose(200);
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            //_login.Close();
            //_login.appClose = true;
            DelayExit();
        }

        private async void DelayExit()
        {
            await Task.Delay(300);
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

        public void bindSetting (int index)
        {
            noteIndex = index;
            materialCheckBox3.Checked = Properties.Settings.Default.NoteOnTop[noteIndex];
            //bindadress = "NoteOnTop[" + noteIndex.ToString() + "]";
            //materialCheckBox3.DataBindings.Add(new Binding("Checked", Properties.Settings.Default, bindadress, true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            //this.materialCheckBox3.DataBindings.Add(new Binding())
        }      
    }
}
