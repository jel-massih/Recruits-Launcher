using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RecruitsLauncher
{
    public partial class OptionsForm : Form
    {
        private MainForm mainForm;


        public OptionsForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            installBrowser.RootFolder = Environment.SpecialFolder.MyComputer;
            installTF.Text = ProgramFilesx86() + "/Commotion Games/Recruits";
            languageCombo.Items.Add(MainForm.Languages.English);
            languageCombo.Items.Add(MainForm.Languages.German);
            languageCombo.SelectedIndex = 0;
        }

        private void installBrowseBtn_Click(object sender, EventArgs e)
        {
            if (installBrowser.ShowDialog() == DialogResult.OK)
            {
                installTF.Text = installBrowser.SelectedPath.Replace('\\', '/') + "/Recruits";
            }
        }

        private void repairBtn_Click(object sender, EventArgs e)
        {
            mainForm.ForceRepair();
            Close();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void installTF_TextChanged(object sender, EventArgs e)
        {
            if (mainForm.CanUpdateLocalPath())
            {
                mainForm.UpdateLocalPath(installTF.Text);
            }
            else
            {
                installTF.Text = mainForm.m_localInstallDir;
            }
        }

        static string ProgramFilesx86()
        {
            if (8 == IntPtr.Size || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)").Replace('\\', '/');
            }

            return Environment.GetEnvironmentVariable("ProgramFiles").Replace('\\', '/');
        }

        private void languageCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (languageCombo.SelectedItem.GetType() == typeof(MainForm.Languages))
            {
                switch ((MainForm.Languages)languageCombo.SelectedItem)
                {
                    case MainForm.Languages.English:
                        mainForm.ChangeLanguage("en-US");
                        break;
                    case MainForm.Languages.German:
                        mainForm.ChangeLanguage("de-DE");
                        break;
                }
            }
        }

        
    }
}
