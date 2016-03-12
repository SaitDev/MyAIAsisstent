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
//using Windows.UI.Notifications;
//using Windows.Data.Xml.Dom;
//using System.IO;


namespace MyAIAsisstent
{
    public partial class Login : MaterialForm
    {
        private const string pass = "120d0a9f6c11649cea1c8eaeccb1e1d3";
        public Setting _setting;
        public Main _main;
        public Notes[] notes;
        public int noteCount;
        private bool silentStart = Program.silentStart;

        public Login(Main main)
        {
            InitializeComponent();
            _main = main;
            _main.materialSkinManager.AddFormToManage(this);
            _setting = new Setting(this);
            notes = new Notes[10];
            noteCount = Properties.Settings.Default.NoteCount;
            if (Properties.Settings.Default.RequiredPassword == false)
            {
                if (noteCount == 0)
                {
                    newNote();
                }
                else
                {
                    int i;
                    if (silentStart) System.Threading.Thread.Sleep(3000);
                    for (i = 0; i < noteCount; i++)
                    {
                        createNote(i);
                    }
                }
            }
            else Visible = true;
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            materialFlatButton2.AutoSize = false;
            materialFlatButton2.Size = new Size(72, 36);
            this.Opacity = 1;
        }

        private void Main_Move(object sender, EventArgs e)
        {
            this.Opacity = .5;
        }

        private void Main_ResizeEnd(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text == "")
            {
                MessageBox.Show("You forget to enter username", "Invaild input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (materialSingleLineTextField2.Text == "")
            {
                MessageBox.Show("You forget to enter password", "Invaild input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (materialSingleLineTextField1.Text.ToUpper() == "SAIT")
                using (MD5 md5Hash = MD5.Create())
                {
                    string hash = GetMd5Hash(md5Hash, materialSingleLineTextField2.Text);
                    if (VerifyMd5Hash(md5Hash, materialSingleLineTextField2.Text, pass))
                    {
                        int i;
                        for (i = 0; i < noteCount; i++)
                        {
                            createNote(i);
                        }
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("User or password is incorrect.", "Login failed");
                        /*
                        // Get a toast XML template
                        XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);

                        // Fill in the text elements
                        XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
                        for (int i = 0; i < stringElements.Length; i++)
                        {
                            stringElements[i].AppendChild(toastXml.CreateTextNode("Line " + i));
                        }

                        // Specify the absolute path to an image
                        String imagePath = "file:///" + Path.GetFullPath("setting_white.png");
                        XmlNodeList imageElements = toastXml.GetElementsByTagName("image");
                        imageElements[0].Attributes.GetNamedItem("src").NodeValue = imagePath;

                        // Create the toast and attach event listeners
                        ToastNotification toast = new ToastNotification(toastXml);
                        //toast.Activated += ToastActivated;
                        //toast.Dismissed += ToastDismissed;
                        //toast.Failed += ToastFailed;

                        // Show the toast. Be sure to specify the AppUserModelId on your application's shortcut!
                        ToastNotificationManager.CreateToastNotifier("Sait's AI Test").Show(toast);
                        */
                    }
                }
            else MessageBox.Show("User or password is incorrect.", "Login failed");
        }
        /*
        private void ToastActivated(ToastNotification sender, object e)
        {
            Dispatcher.Invoke(() =>
            {
                Activate();
                Output.Text = "The user activated the toast.";
            });
        }

        private void ToastDismissed(ToastNotification sender, ToastDismissedEventArgs e)
        {
            String outputText = "";
            switch (e.Reason)
            {
                case ToastDismissalReason.ApplicationHidden:
                    outputText = "The app hid the toast using ToastNotifier.Hide";
                    break;
                case ToastDismissalReason.UserCanceled:
                    outputText = "The user dismissed the toast";
                    break;
                case ToastDismissalReason.TimedOut:
                    outputText = "The toast has timed out";
                    break;
            }

            Dispatcher.Invoke(() =>
            {
                Output.Text = outputText;
            });
        }

        private void ToastFailed(ToastNotification sender, ToastFailedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Output.Text = "The toast encountered an error.";
            });
        }
        */
        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            //this.Close();
            Environment.Exit(0);
        }

        #region MD5 Hash

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

        #endregion 

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

        public void createNote (int i)
        {
            notes[i] = new Notes(this);
            notes[i].index = i;
            notes[i].Text = "Note " + (i+1).ToString();
            notes[i].Show();
        }

        public void newNote ()
        {
            if (noteCount > 9)
            {
                MessageBox.Show("Limit exceeded. Can not create new Note. Please update to the newest version.");
                return;
            }
            createNote(noteCount);
            _main.newNoteLabel(noteCount);
            noteCount++;
        }
    }
}
