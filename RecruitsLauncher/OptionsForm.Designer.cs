namespace RecruitsLauncher
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.installTF = new System.Windows.Forms.TextBox();
            this.installLBL = new System.Windows.Forms.Label();
            this.installBrowseBtn = new System.Windows.Forms.Button();
            this.installBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.repairBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.languageCombo = new System.Windows.Forms.ComboBox();
            this.languageLBL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // installTF
            // 
            resources.ApplyResources(this.installTF, "installTF");
            this.installTF.Name = "installTF";
            this.installTF.TextChanged += new System.EventHandler(this.installTF_TextChanged);
            // 
            // installLBL
            // 
            resources.ApplyResources(this.installLBL, "installLBL");
            this.installLBL.Name = "installLBL";
            // 
            // installBrowseBtn
            // 
            resources.ApplyResources(this.installBrowseBtn, "installBrowseBtn");
            this.installBrowseBtn.Name = "installBrowseBtn";
            this.installBrowseBtn.UseVisualStyleBackColor = true;
            this.installBrowseBtn.Click += new System.EventHandler(this.installBrowseBtn_Click);
            // 
            // repairBtn
            // 
            resources.ApplyResources(this.repairBtn, "repairBtn");
            this.repairBtn.Name = "repairBtn";
            this.repairBtn.UseVisualStyleBackColor = true;
            this.repairBtn.Click += new System.EventHandler(this.repairBtn_Click);
            // 
            // closeBtn
            // 
            resources.ApplyResources(this.closeBtn, "closeBtn");
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // languageCombo
            // 
            this.languageCombo.FormattingEnabled = true;
            resources.ApplyResources(this.languageCombo, "languageCombo");
            this.languageCombo.Name = "languageCombo";
            this.languageCombo.SelectedIndexChanged += new System.EventHandler(this.languageCombo_SelectedIndexChanged);
            // 
            // languageLBL
            // 
            resources.ApplyResources(this.languageLBL, "languageLBL");
            this.languageLBL.Name = "languageLBL";
            // 
            // OptionsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.languageLBL);
            this.Controls.Add(this.languageCombo);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.repairBtn);
            this.Controls.Add(this.installBrowseBtn);
            this.Controls.Add(this.installLBL);
            this.Controls.Add(this.installTF);
            this.Name = "OptionsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label installLBL;
        private System.Windows.Forms.Button installBrowseBtn;
        private System.Windows.Forms.FolderBrowserDialog installBrowser;
        private System.Windows.Forms.Button repairBtn;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.TextBox installTF;
        private System.Windows.Forms.Label languageLBL;
        public System.Windows.Forms.ComboBox languageCombo;
    }
}