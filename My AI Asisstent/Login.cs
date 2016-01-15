using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MaterialSkin;
using MaterialSkin.Controls;
using System.Security.Cryptography;

namespace MyAIAsisstent
{
    public partial class Login : MaterialForm
    {
        private const string pass = "120d0a9f6c11649cea1c8eaeccb1e1d3";
        public MaterialSkinManager materialSkinManager;
        private Setting _setting;
        public Login()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.LightBlue800, Primary.Red500, Accent.LightBlue400, TextShade.WHITE);
            
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            materialFlatButton2.AutoSize = false;
            materialFlatButton2.Size = new Size(72, 36);
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text == "")
            {
                MessageBox.Show("You forget to enter username", "Invaild input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                materialSingleLineTextField1.Focus();
                this.ActiveControl = materialSingleLineTextField1;
                materialSingleLineTextField1.Select();
                return;
            }
            else if (materialSingleLineTextField2.Text == "")
            {
                MessageBox.Show("You forget to enter password", "Invaild input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                materialSingleLineTextField2.Focus();
                this.ActiveControl = materialSingleLineTextField2;
                materialSingleLineTextField2.Select();
                return;
            }
            if (materialSingleLineTextField1.Text.ToUpper() == "SAIT")
                using (MD5 md5Hash = MD5.Create())
                {
                    string hash = GetMd5Hash(md5Hash, materialSingleLineTextField2.Text);
                    //materialSingleLineTextField1.Text = hash;
                    if (VerifyMd5Hash(md5Hash, materialSingleLineTextField2.Text, pass))
                    {
                        _setting = new Setting(this);
                        _setting.Show();
                    }
                }
            else MessageBox.Show("User or password is incorrect.");
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void materialSingleLineTextField1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                materialFlatButton1.PerformClick();
        }

        private void materialSingleLineTextField2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                materialFlatButton1.PerformClick();
        }
    }
}
