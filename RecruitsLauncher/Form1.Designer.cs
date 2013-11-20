namespace RecruitsLauncher
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.downloadBtn = new System.Windows.Forms.Button();
            this.middleLBL = new System.Windows.Forms.Label();
            this.pauseBtn = new System.Windows.Forms.PictureBox();
            this.playBtn = new System.Windows.Forms.PictureBox();
            this.passLBL = new System.Windows.Forms.Label();
            this.infoLBL = new System.Windows.Forms.Label();
            this.movebar_picBox = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.actionBtn = new System.Windows.Forms.Button();
            this.minimizeLBL = new System.Windows.Forms.Label();
            this.closeLBL = new System.Windows.Forms.Label();
            this.OptionsLink = new System.Windows.Forms.LinkLabel();
            this.WebsiteLink = new System.Windows.Forms.LinkLabel();
            this.ForumsLink = new System.Windows.Forms.LinkLabel();
            this.registerLink = new System.Windows.Forms.LinkLabel();
            this.forgotPassLink = new System.Windows.Forms.LinkLabel();
            this.eulaLink = new System.Windows.Forms.LinkLabel();
            this.passTF = new System.Windows.Forms.TextBox();
            this.userTF = new System.Windows.Forms.TextBox();
            this.totalProgressBar = new RecruitsLauncherControls.SmoothProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pauseBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.movebar_picBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // downloadBtn
            // 
            this.downloadBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
            resources.ApplyResources(this.downloadBtn, "downloadBtn");
            this.downloadBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.downloadBtn.Name = "downloadBtn";
            this.downloadBtn.UseCompatibleTextRendering = true;
            this.downloadBtn.UseVisualStyleBackColor = false;
            this.downloadBtn.Click += new System.EventHandler(this.downloadBtn_Click);
            // 
            // middleLBL
            // 
            resources.ApplyResources(this.middleLBL, "middleLBL");
            this.middleLBL.BackColor = System.Drawing.Color.Transparent;
            this.middleLBL.ForeColor = System.Drawing.Color.Silver;
            this.middleLBL.Name = "middleLBL";
            this.middleLBL.UseCompatibleTextRendering = true;
            // 
            // pauseBtn
            // 
            this.pauseBtn.BackColor = System.Drawing.Color.Transparent;
            this.pauseBtn.BackgroundImage = global::RecruitsLauncher.Properties.Resources.pause;
            resources.ApplyResources(this.pauseBtn, "pauseBtn");
            this.pauseBtn.Name = "pauseBtn";
            this.pauseBtn.TabStop = false;
            this.pauseBtn.Click += new System.EventHandler(this.PauseBtn_Click);
            // 
            // playBtn
            // 
            this.playBtn.BackColor = System.Drawing.Color.Transparent;
            this.playBtn.BackgroundImage = global::RecruitsLauncher.Properties.Resources.play;
            resources.ApplyResources(this.playBtn, "playBtn");
            this.playBtn.Name = "playBtn";
            this.playBtn.TabStop = false;
            this.playBtn.Click += new System.EventHandler(this.playBtn_Click);
            // 
            // passLBL
            // 
            resources.ApplyResources(this.passLBL, "passLBL");
            this.passLBL.BackColor = System.Drawing.Color.Transparent;
            this.passLBL.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.passLBL.Name = "passLBL";
            this.passLBL.UseCompatibleTextRendering = true;
            // 
            // infoLBL
            // 
            resources.ApplyResources(this.infoLBL, "infoLBL");
            this.infoLBL.BackColor = System.Drawing.Color.Transparent;
            this.infoLBL.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.infoLBL.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.infoLBL.Name = "infoLBL";
            this.infoLBL.UseCompatibleTextRendering = true;
            // 
            // movebar_picBox
            // 
            this.movebar_picBox.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.movebar_picBox, "movebar_picBox");
            this.movebar_picBox.Name = "movebar_picBox";
            this.movebar_picBox.TabStop = false;
            this.movebar_picBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.movebar_picBox_MouseDown);
            this.movebar_picBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.movebar_picBox_MouseMove);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::RecruitsLauncher.Properties.Resources.launcher_img1;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // actionBtn
            // 
            this.actionBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            resources.ApplyResources(this.actionBtn, "actionBtn");
            this.actionBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.actionBtn.Name = "actionBtn";
            this.actionBtn.UseCompatibleTextRendering = true;
            this.actionBtn.UseVisualStyleBackColor = false;
            this.actionBtn.Click += new System.EventHandler(this.actionBtn_Click);
            // 
            // minimizeLBL
            // 
            resources.ApplyResources(this.minimizeLBL, "minimizeLBL");
            this.minimizeLBL.BackColor = System.Drawing.Color.Transparent;
            this.minimizeLBL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.minimizeLBL.Name = "minimizeLBL";
            this.minimizeLBL.Click += new System.EventHandler(this.minimizeLBL_Click);
            this.minimizeLBL.MouseEnter += new System.EventHandler(this.minimizeLBL_MouseEnter);
            this.minimizeLBL.MouseLeave += new System.EventHandler(this.minimizeLBL_MouseLeave);
            // 
            // closeLBL
            // 
            resources.ApplyResources(this.closeLBL, "closeLBL");
            this.closeLBL.BackColor = System.Drawing.Color.Transparent;
            this.closeLBL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.closeLBL.Name = "closeLBL";
            this.closeLBL.Click += new System.EventHandler(this.closeLBL_Click);
            this.closeLBL.MouseEnter += new System.EventHandler(this.closeLBL_MouseEnter);
            this.closeLBL.MouseLeave += new System.EventHandler(this.closeLBL_MouseLeave);
            // 
            // OptionsLink
            // 
            this.OptionsLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
            resources.ApplyResources(this.OptionsLink, "OptionsLink");
            this.OptionsLink.BackColor = System.Drawing.Color.Transparent;
            this.OptionsLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.OptionsLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.OptionsLink.Name = "OptionsLink";
            this.OptionsLink.TabStop = true;
            this.OptionsLink.UseCompatibleTextRendering = true;
            this.OptionsLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.OptionsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OptionsLink_LinkClicked);
            this.OptionsLink.MouseEnter += new System.EventHandler(this.OptionsLink_MouseEnter);
            this.OptionsLink.MouseLeave += new System.EventHandler(this.OptionsLink_MouseLeave);
            // 
            // WebsiteLink
            // 
            this.WebsiteLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
            resources.ApplyResources(this.WebsiteLink, "WebsiteLink");
            this.WebsiteLink.BackColor = System.Drawing.Color.Transparent;
            this.WebsiteLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.WebsiteLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.WebsiteLink.Name = "WebsiteLink";
            this.WebsiteLink.TabStop = true;
            this.WebsiteLink.UseCompatibleTextRendering = true;
            this.WebsiteLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.WebsiteLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WebsiteLink_LinkClicked);
            this.WebsiteLink.MouseEnter += new System.EventHandler(this.WebsiteLink_MouseEnter);
            this.WebsiteLink.MouseLeave += new System.EventHandler(this.WebsiteLink_MouseLeave);
            // 
            // ForumsLink
            // 
            this.ForumsLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
            resources.ApplyResources(this.ForumsLink, "ForumsLink");
            this.ForumsLink.BackColor = System.Drawing.Color.Transparent;
            this.ForumsLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.ForumsLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.ForumsLink.Name = "ForumsLink";
            this.ForumsLink.TabStop = true;
            this.ForumsLink.UseCompatibleTextRendering = true;
            this.ForumsLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.ForumsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ForumsLink_LinkClicked);
            this.ForumsLink.MouseEnter += new System.EventHandler(this.ForumsLink_MouseEnter);
            this.ForumsLink.MouseLeave += new System.EventHandler(this.ForumsLink_MouseLeave);
            // 
            // registerLink
            // 
            this.registerLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
            resources.ApplyResources(this.registerLink, "registerLink");
            this.registerLink.BackColor = System.Drawing.Color.Transparent;
            this.registerLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.registerLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.registerLink.Name = "registerLink";
            this.registerLink.TabStop = true;
            this.registerLink.UseCompatibleTextRendering = true;
            this.registerLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.registerLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.registerLink_LinkClicked);
            this.registerLink.MouseEnter += new System.EventHandler(this.registerLink_MouseEnter);
            this.registerLink.MouseLeave += new System.EventHandler(this.registerLink_MouseLeave);
            // 
            // forgotPassLink
            // 
            this.forgotPassLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
            resources.ApplyResources(this.forgotPassLink, "forgotPassLink");
            this.forgotPassLink.BackColor = System.Drawing.Color.Transparent;
            this.forgotPassLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.forgotPassLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.forgotPassLink.Name = "forgotPassLink";
            this.forgotPassLink.TabStop = true;
            this.forgotPassLink.UseCompatibleTextRendering = true;
            this.forgotPassLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.forgotPassLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.forgotPassLink_LinkClicked);
            this.forgotPassLink.MouseEnter += new System.EventHandler(this.forgotPassLink_MouseEnter);
            this.forgotPassLink.MouseLeave += new System.EventHandler(this.forgotPassLink_MouseLeave);
            // 
            // eulaLink
            // 
            this.eulaLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
            resources.ApplyResources(this.eulaLink, "eulaLink");
            this.eulaLink.BackColor = System.Drawing.Color.Transparent;
            this.eulaLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.eulaLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.eulaLink.Name = "eulaLink";
            this.eulaLink.TabStop = true;
            this.eulaLink.UseCompatibleTextRendering = true;
            this.eulaLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            // 
            // passTF
            // 
            this.passTF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.passTF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.passTF, "passTF");
            this.passTF.ForeColor = System.Drawing.Color.Gainsboro;
            this.passTF.Name = "passTF";
            this.passTF.UseSystemPasswordChar = true;
            // 
            // userTF
            // 
            this.userTF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.userTF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.userTF, "userTF");
            this.userTF.ForeColor = System.Drawing.Color.Gainsboro;
            this.userTF.Name = "userTF";
            // 
            // totalProgressBar
            // 
            this.totalProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.totalProgressBar.BarBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.totalProgressBar.Cursor = System.Windows.Forms.Cursors.Arrow;
            resources.ApplyResources(this.totalProgressBar, "totalProgressBar");
            this.totalProgressBar.Maximum = 100;
            this.totalProgressBar.Minimum = 0;
            this.totalProgressBar.Name = "totalProgressBar";
            this.totalProgressBar.ProgressBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(213)))), ((int)(((byte)(110)))));
            this.totalProgressBar.Value = 0;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Controls.Add(this.eulaLink);
            this.Controls.Add(this.pauseBtn);
            this.Controls.Add(this.playBtn);
            this.Controls.Add(this.actionBtn);
            this.Controls.Add(this.movebar_picBox);
            this.Controls.Add(this.minimizeLBL);
            this.Controls.Add(this.closeLBL);
            this.Controls.Add(this.OptionsLink);
            this.Controls.Add(this.WebsiteLink);
            this.Controls.Add(this.ForumsLink);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.passLBL);
            this.Controls.Add(this.registerLink);
            this.Controls.Add(this.forgotPassLink);
            this.Controls.Add(this.downloadBtn);
            this.Controls.Add(this.middleLBL);
            this.Controls.Add(this.totalProgressBar);
            this.Controls.Add(this.passTF);
            this.Controls.Add(this.userTF);
            this.Controls.Add(this.infoLBL);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.pauseBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.movebar_picBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RecruitsLauncherControls.SmoothProgressBar totalProgressBar;
        private System.Windows.Forms.Button downloadBtn;
        private System.Windows.Forms.Label middleLBL;
        private System.Windows.Forms.PictureBox pauseBtn;
        private System.Windows.Forms.PictureBox playBtn;
        private System.Windows.Forms.Label passLBL;
        private System.Windows.Forms.Label infoLBL;
        private System.Windows.Forms.PictureBox movebar_picBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button actionBtn;
        private System.Windows.Forms.Label minimizeLBL;
        private System.Windows.Forms.Label closeLBL;
        private System.Windows.Forms.LinkLabel OptionsLink;
        private System.Windows.Forms.LinkLabel WebsiteLink;
        private System.Windows.Forms.LinkLabel ForumsLink;
        private System.Windows.Forms.LinkLabel registerLink;
        private System.Windows.Forms.LinkLabel forgotPassLink;
        private System.Windows.Forms.LinkLabel eulaLink;
        private System.Windows.Forms.TextBox passTF;
        private System.Windows.Forms.TextBox userTF;

    }
}