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

namespace MyAIAsisstent
{
    public partial class Main : MaterialForm
    {
        public MaterialSkinManager materialSkinManager;
        private Login _login;
        private MaterialFlatButton lastActive;

        public Main()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.LightBlue800, Primary.BlueGrey400, Accent.LightBlue400, TextShade.WHITE);
            if (Properties.Settings.Default.LightTheme)
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

        private void Main_Load(object sender, EventArgs e)
        {
            
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            lastActive = materialFlatButton3;
            //Form1 f = new Form1();
            //f.Show();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

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

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            materialLabel1.Visible = !materialLabel1.Visible;
            materialLabel1.ForeColor = SkinManager.ColorScheme.DarkPrimaryColor;
            materialListView1.Visible = !materialListView1.Visible;
            materialSingleLineTextField1.Focus();
            /*
            Notification noti = new Notification();
            noti.Dismiss += DismissNotification;
            noti.Remind += RemindNotification;
            noti.Done += DoneNotification;
            noti.Show(); */
        }

        private void materialTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (materialTabControl1.SelectedIndex == 1)
            {
                materialLabel1.Hide();
                materialListView1.Hide();
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
            _login.newNote(_login.noteCount);
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
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

        private void materialToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void Main_BackColorChanged(object sender, EventArgs e)
        {
            UpdateMenuButton();
            materialContextMenuStrip1.BackColor = this.BackColor;
            timePickerPanel1.timePicker.ClockMenu.BackColor = this.BackColor;
        }

        private void materialSingleLineTextField1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

            }
        }

        private void materialSingleLineTextField1_Enter(object sender, EventArgs e)
        {
            //MessageBox.Show(materialSingleLineTextField1.BackColor.ToString() + this.BackColor.ToString());
        }

        private void materialListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(materialListView1.SelectedIndices.IndexOf(0).ToString());
            //MessageBox.Show(materialListView1.SelectedItems.Count.ToString());
        }

        private void materialListView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            MessageBox.Show("check");
        }

        private void materialListView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            MessageBox.Show("checked");
        }

        private void materialListView1_ItemActivate(object sender, EventArgs e)
        {
            //MessageBox.Show("active");
        }

        private void materialListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            MessageBox.Show("column");
        }

        private void materialListView1_VirtualItemsSelectionRangeChanged(object sender, ListViewVirtualItemsSelectionRangeChangedEventArgs e)
        {
            MessageBox.Show("sh");
        }

        private void materialListView1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("click");
            //MessageBox.Show(materialListView1.SelectedItems.Count.ToString());
            //MessageBox.Show(materialListView1.SelectedItems[0].Text);
            
        }
    }
}
