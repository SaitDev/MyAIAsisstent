namespace MyAIAsisstent
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("Time");
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("Day");
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem("Message to remind");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.materialSingleLineTextField1 = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.panel1 = new System.Windows.Forms.Panel();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.materialFlatButton5 = new MaterialSkin.Controls.MaterialFlatButton();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.timePickerPanel1 = new Opulos.Core.UI.TimePickerPanel();
            this.materialListView3 = new MaterialSkin.Controls.MaterialListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.materialListView2 = new MaterialSkin.Controls.MaterialListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialListView1 = new MaterialSkin.Controls.MaterialListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.materialFlatButton4 = new MaterialSkin.Controls.MaterialFlatButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.materialFlatButton6 = new MaterialSkin.Controls.MaterialFlatButton();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.materialContextMenuStrip1 = new MaterialSkin.Controls.MaterialContextMenuStrip();
            this.materialToolStripMenuItem2 = new MaterialSkin.Controls.MaterialToolStripMenuItem();
            this.materialToolStripMenuItem1 = new MaterialSkin.Controls.MaterialToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.materialFlatButton3 = new MaterialSkin.Controls.MaterialFlatButton();
            this.materialFlatButton2 = new MaterialSkin.Controls.MaterialFlatButton();
            this.materialRaisedButton1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialFlatButton1 = new MaterialSkin.Controls.MaterialFlatButton();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.materialContextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialSingleLineTextField1
            // 
            this.materialSingleLineTextField1.Depth = 0;
            this.materialSingleLineTextField1.Hint = "What can I do for you?";
            this.materialSingleLineTextField1.Location = new System.Drawing.Point(51, 395);
            this.materialSingleLineTextField1.MaxLength = 32767;
            this.materialSingleLineTextField1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSingleLineTextField1.Name = "materialSingleLineTextField1";
            this.materialSingleLineTextField1.PasswordChar = '\0';
            this.materialSingleLineTextField1.SelectedText = "";
            this.materialSingleLineTextField1.SelectionLength = 0;
            this.materialSingleLineTextField1.SelectionStart = 0;
            this.materialSingleLineTextField1.Size = new System.Drawing.Size(279, 23);
            this.materialSingleLineTextField1.TabIndex = 0;
            this.materialSingleLineTextField1.TabStop = false;
            this.materialSingleLineTextField1.UseSystemPasswordChar = false;
            this.materialSingleLineTextField1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.materialSingleLineTextField1_KeyPress);
            // 
            // materialDivider1
            // 
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(0, 59);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(50, 360);
            this.materialDivider1.TabIndex = 2;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Location = new System.Drawing.Point(0, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(4, 46);
            this.panel1.TabIndex = 5;
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Controls.Add(this.tabPage2);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(50, 59);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(280, 337);
            this.materialTabControl1.TabIndex = 0;
            this.materialTabControl1.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.materialFlatButton5);
            this.tabPage1.Controls.Add(this.monthCalendar1);
            this.tabPage1.Controls.Add(this.timePickerPanel1);
            this.tabPage1.Controls.Add(this.materialListView3);
            this.tabPage1.Controls.Add(this.materialListView2);
            this.tabPage1.Controls.Add(this.materialLabel1);
            this.tabPage1.Controls.Add(this.materialListView1);
            this.tabPage1.Controls.Add(this.materialFlatButton4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(272, 311);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Remind";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // materialFlatButton5
            // 
            this.materialFlatButton5.AutoUpper = true;
            this.materialFlatButton5.Depth = 0;
            this.materialFlatButton5.Icon = global::MyAIAsisstent.Properties.Resources.clear_blue;
            this.materialFlatButton5.IconSize = new System.Drawing.Size(25, 25);
            this.materialFlatButton5.Location = new System.Drawing.Point(198, 300);
            this.materialFlatButton5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFlatButton5.Name = "materialFlatButton5";
            this.materialFlatButton5.Primary = false;
            this.materialFlatButton5.Size = new System.Drawing.Size(40, 35);
            this.materialFlatButton5.TabIndex = 9;
            this.materialFlatButton5.UseCustomBackColor = false;
            this.materialFlatButton5.UseVisualStyleBackColor = true;
            this.materialFlatButton5.Visible = false;
            this.materialFlatButton5.Click += new System.EventHandler(this.materialFlatButton5_Click);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.AllowDrop = true;
            this.monthCalendar1.CausesValidation = false;
            this.monthCalendar1.Location = new System.Drawing.Point(4, 159);
            this.monthCalendar1.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 8;
            this.monthCalendar1.TabStop = false;
            this.monthCalendar1.TitleBackColor = System.Drawing.Color.Transparent;
            this.monthCalendar1.TitleForeColor = System.Drawing.Color.Transparent;
            this.monthCalendar1.TrailingForeColor = System.Drawing.Color.Transparent;
            this.monthCalendar1.Visible = false;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // timePickerPanel1
            // 
            this.timePickerPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.timePickerPanel1.Location = new System.Drawing.Point(171, 167);
            this.timePickerPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.timePickerPanel1.Name = "timePickerPanel1";
            this.timePickerPanel1.Size = new System.Drawing.Size(100, 38);
            this.timePickerPanel1.TabIndex = 4;
            // 
            // materialListView3
            // 
            this.materialListView3.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.materialListView3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialListView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.materialListView3.Depth = 0;
            this.materialListView3.Font = new System.Drawing.Font("Roboto", 24F);
            this.materialListView3.FullRowSelect = true;
            this.materialListView3.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.materialListView3.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem13});
            this.materialListView3.Location = new System.Drawing.Point(140, 115);
            this.materialListView3.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialListView3.MouseState = MaterialSkin.MouseState.OUT;
            this.materialListView3.Name = "materialListView3";
            this.materialListView3.OwnerDraw = true;
            this.materialListView3.Size = new System.Drawing.Size(136, 50);
            this.materialListView3.TabIndex = 6;
            this.materialListView3.UseCompatibleStateImageBehavior = false;
            this.materialListView3.View = System.Windows.Forms.View.Details;
            this.materialListView3.Visible = false;
            this.materialListView3.Click += new System.EventHandler(this.materialListView3_Click);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Time";
            this.columnHeader3.Width = 136;
            // 
            // materialListView2
            // 
            this.materialListView2.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.materialListView2.AutoArrange = false;
            this.materialListView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialListView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.materialListView2.Depth = 0;
            this.materialListView2.Font = new System.Drawing.Font("Roboto", 24F);
            this.materialListView2.FullRowSelect = true;
            this.materialListView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.materialListView2.ImeMode = System.Windows.Forms.ImeMode.On;
            listViewItem14.StateImageIndex = 0;
            this.materialListView2.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem14});
            this.materialListView2.Location = new System.Drawing.Point(4, 115);
            this.materialListView2.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialListView2.MouseState = MaterialSkin.MouseState.OUT;
            this.materialListView2.MultiSelect = false;
            this.materialListView2.Name = "materialListView2";
            this.materialListView2.OwnerDraw = true;
            this.materialListView2.Size = new System.Drawing.Size(136, 50);
            this.materialListView2.TabIndex = 5;
            this.materialListView2.UseCompatibleStateImageBehavior = false;
            this.materialListView2.View = System.Windows.Forms.View.Details;
            this.materialListView2.Visible = false;
            this.materialListView2.Click += new System.EventHandler(this.materialListView2_Click);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Day";
            this.columnHeader2.Width = 136;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(15, 16);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Padding = new System.Windows.Forms.Padding(8);
            this.materialLabel1.Size = new System.Drawing.Size(244, 35);
            this.materialLabel1.TabIndex = 2;
            this.materialLabel1.Text = "What do you want me to remind?";
            this.materialLabel1.Visible = false;
            // 
            // materialListView1
            // 
            this.materialListView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.materialListView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.materialListView1.Depth = 0;
            this.materialListView1.Font = new System.Drawing.Font("Roboto", 24F);
            this.materialListView1.FullRowSelect = true;
            this.materialListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.materialListView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem15});
            this.materialListView1.Location = new System.Drawing.Point(4, 70);
            this.materialListView1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialListView1.MouseState = MaterialSkin.MouseState.OUT;
            this.materialListView1.MultiSelect = false;
            this.materialListView1.Name = "materialListView1";
            this.materialListView1.OwnerDraw = true;
            this.materialListView1.Scrollable = false;
            this.materialListView1.Size = new System.Drawing.Size(272, 45);
            this.materialListView1.TabIndex = 1;
            this.materialListView1.UseCompatibleStateImageBehavior = false;
            this.materialListView1.View = System.Windows.Forms.View.Details;
            this.materialListView1.Visible = false;
            this.materialListView1.Click += new System.EventHandler(this.materialListView1_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Message";
            this.columnHeader1.Width = 272;
            // 
            // materialFlatButton4
            // 
            this.materialFlatButton4.AutoUpper = true;
            this.materialFlatButton4.Depth = 0;
            this.materialFlatButton4.Icon = global::MyAIAsisstent.Properties.Resources.alarm_blue;
            this.materialFlatButton4.IconSize = new System.Drawing.Size(28, 26);
            this.materialFlatButton4.Location = new System.Drawing.Point(239, 300);
            this.materialFlatButton4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFlatButton4.Name = "materialFlatButton4";
            this.materialFlatButton4.Primary = false;
            this.materialFlatButton4.Size = new System.Drawing.Size(40, 35);
            this.materialFlatButton4.TabIndex = 0;
            this.materialFlatButton4.UseCustomBackColor = false;
            this.materialFlatButton4.UseVisualStyleBackColor = true;
            this.materialFlatButton4.Visible = false;
            this.materialFlatButton4.Click += new System.EventHandler(this.materialFlatButton4_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.materialFlatButton6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(272, 311);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Note";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // materialFlatButton6
            // 
            this.materialFlatButton6.AutoUpper = true;
            this.materialFlatButton6.Depth = 0;
            this.materialFlatButton6.Icon = ((System.Drawing.Image)(resources.GetObject("materialFlatButton6.Icon")));
            this.materialFlatButton6.IconSize = new System.Drawing.Size(28, 26);
            this.materialFlatButton6.Location = new System.Drawing.Point(239, 300);
            this.materialFlatButton6.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFlatButton6.Name = "materialFlatButton6";
            this.materialFlatButton6.Primary = false;
            this.materialFlatButton6.Size = new System.Drawing.Size(40, 35);
            this.materialFlatButton6.TabIndex = 2;
            this.materialFlatButton6.UseCustomBackColor = false;
            this.materialFlatButton6.UseVisualStyleBackColor = true;
            this.materialFlatButton6.Click += new System.EventHandler(this.materialFlatButton6_Click);
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Location = new System.Drawing.Point(80, 24);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(250, 35);
            this.materialTabSelector1.TabIndex = 0;
            this.materialTabSelector1.Text = "materialTabSelector1";
            this.materialTabSelector1.UseCustomHightlight = true;
            this.materialTabSelector1.Visible = false;
            this.materialTabSelector1.TabIndexChanged += new System.EventHandler(this.materialTabSelector1_TabIndexChanged);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.materialContextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "My AI Asisstent";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // materialContextMenuStrip1
            // 
            this.materialContextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialContextMenuStrip1.Depth = 0;
            this.materialContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.materialToolStripMenuItem2,
            this.materialToolStripMenuItem1});
            this.materialContextMenuStrip1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialContextMenuStrip1.Name = "materialContextMenuStrip1";
            this.materialContextMenuStrip1.Size = new System.Drawing.Size(104, 64);
            // 
            // materialToolStripMenuItem2
            // 
            this.materialToolStripMenuItem2.AutoSize = false;
            this.materialToolStripMenuItem2.Name = "materialToolStripMenuItem2";
            this.materialToolStripMenuItem2.Size = new System.Drawing.Size(120, 30);
            this.materialToolStripMenuItem2.Text = "Show";
            this.materialToolStripMenuItem2.Click += new System.EventHandler(this.materialToolStripMenuItem2_Click);
            // 
            // materialToolStripMenuItem1
            // 
            this.materialToolStripMenuItem1.AutoSize = false;
            this.materialToolStripMenuItem1.Name = "materialToolStripMenuItem1";
            this.materialToolStripMenuItem1.Size = new System.Drawing.Size(120, 30);
            this.materialToolStripMenuItem1.Text = "Exit";
            this.materialToolStripMenuItem1.Click += new System.EventHandler(this.materialToolStripMenuItem1_Click);
            // 
            // materialFlatButton3
            // 
            this.materialFlatButton3.AutoUpper = true;
            this.materialFlatButton3.Depth = 0;
            this.materialFlatButton3.Icon = ((System.Drawing.Image)(resources.GetObject("materialFlatButton3.Icon")));
            this.materialFlatButton3.IconSize = new System.Drawing.Size(36, 36);
            this.materialFlatButton3.Location = new System.Drawing.Point(1, 66);
            this.materialFlatButton3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFlatButton3.Name = "materialFlatButton3";
            this.materialFlatButton3.Primary = false;
            this.materialFlatButton3.Size = new System.Drawing.Size(49, 46);
            this.materialFlatButton3.TabIndex = 6;
            this.materialFlatButton3.UseCustomBackColor = false;
            this.materialFlatButton3.UseVisualStyleBackColor = false;
            this.materialFlatButton3.Click += new System.EventHandler(this.materialFlatButton3_Click);
            // 
            // materialFlatButton2
            // 
            this.materialFlatButton2.AutoUpper = true;
            this.materialFlatButton2.Depth = 0;
            this.materialFlatButton2.Icon = ((System.Drawing.Image)(resources.GetObject("materialFlatButton2.Icon")));
            this.materialFlatButton2.IconSize = new System.Drawing.Size(36, 36);
            this.materialFlatButton2.Location = new System.Drawing.Point(1, 120);
            this.materialFlatButton2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFlatButton2.Name = "materialFlatButton2";
            this.materialFlatButton2.Primary = false;
            this.materialFlatButton2.Size = new System.Drawing.Size(49, 46);
            this.materialFlatButton2.TabIndex = 4;
            this.materialFlatButton2.UseCustomBackColor = false;
            this.materialFlatButton2.UseVisualStyleBackColor = false;
            this.materialFlatButton2.Click += new System.EventHandler(this.materialFlatButton2_Click);
            // 
            // materialRaisedButton1
            // 
            this.materialRaisedButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialRaisedButton1.Depth = 0;
            this.materialRaisedButton1.Icon = global::MyAIAsisstent.Properties.Resources.setting_blue;
            this.materialRaisedButton1.Location = new System.Drawing.Point(0, 270);
            this.materialRaisedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.Size = new System.Drawing.Size(49, 46);
            this.materialRaisedButton1.TabIndex = 3;
            this.materialRaisedButton1.UseCustomBackColor = false;
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            this.materialRaisedButton1.Visible = false;
            // 
            // materialFlatButton1
            // 
            this.materialFlatButton1.AutoUpper = false;
            this.materialFlatButton1.Depth = 0;
            this.materialFlatButton1.Icon = global::MyAIAsisstent.Properties.Resources.setting_blue;
            this.materialFlatButton1.IconSize = new System.Drawing.Size(36, 36);
            this.materialFlatButton1.Location = new System.Drawing.Point(1, 174);
            this.materialFlatButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFlatButton1.Name = "materialFlatButton1";
            this.materialFlatButton1.Primary = false;
            this.materialFlatButton1.Size = new System.Drawing.Size(49, 46);
            this.materialFlatButton1.TabIndex = 1;
            this.materialFlatButton1.UseCustomBackColor = false;
            this.materialFlatButton1.UseVisualStyleBackColor = false;
            this.materialFlatButton1.Click += new System.EventHandler(this.materialFlatButton1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(330, 420);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.materialFlatButton3);
            this.Controls.Add(this.materialFlatButton2);
            this.Controls.Add(this.materialRaisedButton1);
            this.Controls.Add(this.materialFlatButton1);
            this.Controls.Add(this.materialSingleLineTextField1);
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this.materialTabControl1);
            this.Location = new System.Drawing.Point(80, 275);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.BackColorChanged += new System.EventHandler(this.Main_BackColorChanged);
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.materialContextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField materialSingleLineTextField1;
        private MaterialSkin.Controls.MaterialFlatButton materialFlatButton1;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton1;
        private MaterialSkin.Controls.MaterialFlatButton materialFlatButton2;
        private System.Windows.Forms.Panel panel1;
        public MaterialSkin.Controls.MaterialFlatButton materialFlatButton3;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private MaterialSkin.Controls.MaterialFlatButton materialFlatButton4;
        private MaterialSkin.Controls.MaterialFlatButton materialFlatButton6;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private MaterialSkin.Controls.MaterialContextMenuStrip materialContextMenuStrip1;
        private MaterialSkin.Controls.MaterialToolStripMenuItem materialToolStripMenuItem1;
        private MaterialSkin.Controls.MaterialToolStripMenuItem materialToolStripMenuItem2;
        private System.Windows.Forms.Timer timer1;
        private MaterialSkin.Controls.MaterialListView materialListView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private Opulos.Core.UI.TimePickerPanel timePickerPanel1;
        private MaterialSkin.Controls.MaterialListView materialListView2;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private MaterialSkin.Controls.MaterialListView materialListView3;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private MaterialSkin.Controls.MaterialFlatButton materialFlatButton5;
    }
}