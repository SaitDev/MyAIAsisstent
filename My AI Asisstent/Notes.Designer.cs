﻿namespace MyAIAsisstent
{
    partial class Notes
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
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.metroTextBox1 = new MetroFramework.Controls.MetroTextBox();
            this.metroLink1 = new MetroFramework.Controls.MetroLink();
            this.metroLink2 = new MetroFramework.Controls.MetroLink();
            this.materialContextMenuStrip1 = new MaterialSkin.Controls.MaterialContextMenuStrip();
            this.materialToolStripMenuItem1 = new MaterialSkin.Controls.MaterialToolStripMenuItem();
            this.materialToolStripMenuItem2 = new MaterialSkin.Controls.MaterialToolStripMenuItem();
            this.materialToolStripMenuItem3 = new MaterialSkin.Controls.MaterialToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.opacityToolStripMenuItem = new MaterialSkin.Controls.MaterialToolStripMenuItem();
            this.toolStripMenuItem1 = new MaterialSkin.Controls.MaterialToolStripMenuItem();
            this.toolStripMenuItem2 = new MaterialSkin.Controls.MaterialToolStripMenuItem();
            this.toolStripMenuItem3 = new MaterialSkin.Controls.MaterialToolStripMenuItem();
            this.toolStripMenuItem4 = new MaterialSkin.Controls.MaterialToolStripMenuItem();
            this.toolStripMenuItem5 = new MaterialSkin.Controls.MaterialToolStripMenuItem();
            this.materialContextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialLabel1
            // 
            this.materialLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MyAIAsisstent.Properties.Settings.Default, "Note1", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(12, 85);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(193, 166);
            this.materialLabel1.TabIndex = 0;
            this.materialLabel1.Text = global::MyAIAsisstent.Properties.Settings.Default.Note1;
            this.materialLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.materialLabel1.DoubleClick += new System.EventHandler(this.materialLabel1_DoubleClick);
            // 
            // metroTextBox1
            // 
            this.metroTextBox1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.metroTextBox1.CustomButton.Image = null;
            this.metroTextBox1.CustomButton.Location = new System.Drawing.Point(29, 2);
            this.metroTextBox1.CustomButton.Name = "";
            this.metroTextBox1.CustomButton.Size = new System.Drawing.Size(161, 161);
            this.metroTextBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox1.CustomButton.TabIndex = 1;
            this.metroTextBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox1.CustomButton.UseSelectable = true;
            this.metroTextBox1.CustomButton.Visible = false;
            this.metroTextBox1.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.metroTextBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.metroTextBox1.Lines = new string[] {
        "metroTextBox1"};
            this.metroTextBox1.Location = new System.Drawing.Point(12, 76);
            this.metroTextBox1.MaxLength = 32767;
            this.metroTextBox1.Multiline = true;
            this.metroTextBox1.Name = "metroTextBox1";
            this.metroTextBox1.PasswordChar = '\0';
            this.metroTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox1.SelectedText = "";
            this.metroTextBox1.SelectionLength = 0;
            this.metroTextBox1.SelectionStart = 0;
            this.metroTextBox1.Size = new System.Drawing.Size(193, 166);
            this.metroTextBox1.TabIndex = 2;
            this.metroTextBox1.Text = "metroTextBox1";
            this.metroTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.metroTextBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTextBox1.UseCustomBackColor = true;
            this.metroTextBox1.UseCustomForeColor = true;
            this.metroTextBox1.UseSelectable = true;
            this.metroTextBox1.Visible = false;
            this.metroTextBox1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLink1
            // 
            this.metroLink1.AutoSize = true;
            this.metroLink1.BackColor = System.Drawing.Color.Gray;
            this.metroLink1.Image = global::MyAIAsisstent.Properties.Resources.save_white;
            this.metroLink1.ImageSize = 34;
            this.metroLink1.Location = new System.Drawing.Point(174, 30);
            this.metroLink1.Name = "metroLink1";
            this.metroLink1.Size = new System.Drawing.Size(41, 31);
            this.metroLink1.TabIndex = 3;
            this.metroLink1.UseCustomBackColor = true;
            this.metroLink1.UseSelectable = true;
            this.metroLink1.Click += new System.EventHandler(this.metroLink1_Click);
            // 
            // metroLink2
            // 
            this.metroLink2.AutoSize = true;
            this.metroLink2.BackColor = System.Drawing.Color.Gray;
            this.metroLink2.Image = global::MyAIAsisstent.Properties.Resources.setting_white;
            this.metroLink2.ImageSize = 34;
            this.metroLink2.Location = new System.Drawing.Point(133, 30);
            this.metroLink2.Name = "metroLink2";
            this.metroLink2.Size = new System.Drawing.Size(41, 31);
            this.metroLink2.TabIndex = 4;
            this.metroLink2.UseCustomBackColor = true;
            this.metroLink2.UseSelectable = true;
            this.metroLink2.Click += new System.EventHandler(this.metroLink2_Click);
            // 
            // materialContextMenuStrip1
            // 
            this.materialContextMenuStrip1.BackColor = System.Drawing.Color.White;
            this.materialContextMenuStrip1.Depth = 0;
            this.materialContextMenuStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.materialToolStripMenuItem1,
            this.materialToolStripMenuItem2,
            this.materialToolStripMenuItem3,
            this.toolStripSeparator2,
            this.opacityToolStripMenuItem});
            this.materialContextMenuStrip1.Margin = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.materialContextMenuStrip1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialContextMenuStrip1.Name = "materialContextMenuStrip1";
            this.materialContextMenuStrip1.Size = new System.Drawing.Size(127, 130);
            // 
            // materialToolStripMenuItem1
            // 
            this.materialToolStripMenuItem1.AutoSize = false;
            this.materialToolStripMenuItem1.Name = "materialToolStripMenuItem1";
            this.materialToolStripMenuItem1.Size = new System.Drawing.Size(125, 30);
            this.materialToolStripMenuItem1.Text = "New";
            // 
            // materialToolStripMenuItem2
            // 
            this.materialToolStripMenuItem2.AutoSize = false;
            this.materialToolStripMenuItem2.Name = "materialToolStripMenuItem2";
            this.materialToolStripMenuItem2.Size = new System.Drawing.Size(120, 30);
            this.materialToolStripMenuItem2.Text = "Edit";
            this.materialToolStripMenuItem2.Click += new System.EventHandler(this.materialToolStripMenuItem2_Click);
            // 
            // materialToolStripMenuItem3
            // 
            this.materialToolStripMenuItem3.AutoSize = false;
            this.materialToolStripMenuItem3.Name = "materialToolStripMenuItem3";
            this.materialToolStripMenuItem3.Size = new System.Drawing.Size(120, 30);
            this.materialToolStripMenuItem3.Text = "Delete";
            this.materialToolStripMenuItem3.Click += new System.EventHandler(this.materialToolStripMenuItem3_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(123, 6);
            // 
            // opacityToolStripMenuItem
            // 
            this.opacityToolStripMenuItem.AutoSize = false;
            this.opacityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5});
            this.opacityToolStripMenuItem.Name = "opacityToolStripMenuItem";
            this.opacityToolStripMenuItem.Size = new System.Drawing.Size(125, 30);
            this.opacityToolStripMenuItem.Text = "Opacity";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.AutoSize = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(140, 30);
            this.toolStripMenuItem1.Text = "100%";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.AutoSize = false;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(140, 30);
            this.toolStripMenuItem2.Text = "90%";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.AutoSize = false;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(140, 30);
            this.toolStripMenuItem3.Text = "80%";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.AutoSize = false;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(140, 30);
            this.toolStripMenuItem4.Text = "70%";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.AutoSize = false;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(140, 30);
            this.toolStripMenuItem5.Text = "60%";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // Notes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(217, 254);
            this.ContextMenuStrip = this.materialContextMenuStrip1;
            this.Controls.Add(this.metroLink2);
            this.Controls.Add(this.metroLink1);
            this.Controls.Add(this.metroTextBox1);
            this.Controls.Add(this.materialLabel1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::MyAIAsisstent.Properties.Settings.Default, "Location1", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::MyAIAsisstent.Properties.Settings.Default.Location1;
            this.MaximizeBox = false;
            this.Name = "Notes";
            this.Opacity = 0.7D;
            this.ShowInTaskbar = false;
            this.Sizable = false;
            this.Text = "Notes";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Notes_FormClosing);
            this.Shown += new System.EventHandler(this.Notes_Shown);
            this.ResizeEnd += new System.EventHandler(this.Notes_ResizeEnd);
            this.Move += new System.EventHandler(this.Notes_Move);
            this.materialContextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MetroFramework.Controls.MetroTextBox metroTextBox1;
        private MetroFramework.Controls.MetroLink metroLink1;
        private MetroFramework.Controls.MetroLink metroLink2;
        private MaterialSkin.Controls.MaterialContextMenuStrip materialContextMenuStrip1;
        private MaterialSkin.Controls.MaterialToolStripMenuItem opacityToolStripMenuItem;
        private MaterialSkin.Controls.MaterialToolStripMenuItem toolStripMenuItem1;
        private MaterialSkin.Controls.MaterialToolStripMenuItem toolStripMenuItem2;
        private MaterialSkin.Controls.MaterialToolStripMenuItem toolStripMenuItem3;
        private MaterialSkin.Controls.MaterialToolStripMenuItem toolStripMenuItem4;
        private MaterialSkin.Controls.MaterialToolStripMenuItem toolStripMenuItem5;
        private MaterialSkin.Controls.MaterialToolStripMenuItem materialToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private MaterialSkin.Controls.MaterialToolStripMenuItem materialToolStripMenuItem2;
        private MaterialSkin.Controls.MaterialToolStripMenuItem materialToolStripMenuItem3;
    }
}