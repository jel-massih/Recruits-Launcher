using RecruitsLauncher.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Resources;
using System.Windows.Forms;

namespace RecruitsLauncher
{
    public partial class MainForm : Form
    {
        public enum Languages
        {
            English,
            German
        };

        private enum ErrorType
        {
            ER_NOCONNECTION,
            ER_WRONG_ACCOUNT_INFO,
            ER_NO_OWN_GAME,
            ER_CORRUPT,
            ER_NOINSTALL,
            ER_NONE
        };

        private enum LauncherState
        {
            LS_SIGNIN,
            LS_UPDATING,
            LS_REPAIRING,
            LS_ONLINE,
            LS_OFFLINE
        };

        public static readonly string MANIFEST_URL = "http://YOURURL.com/fileManifest.txt";
        public static readonly string VERSION_URL = "http://YOURURL.com/downloads/versionInfo.txt";
        public static readonly string AUTHORIZE_URL = "http://YOURURL.com/downloads/installerAuth.php";
        public static readonly string[] DOWNLOAD_URLS = { "http://YOURURL.com/downloads/download.php", "http://localhost:247/YOURURL/download/download.php" }; //Array of Mirror URL's
        public static string DOWNLOAD_URL = ""; //Active Mirror URL
        public static readonly string[] VERIFY_IGNORE_DIRECTORIES = { "/Config/", "/Logs/", "/Localization/", "/UserCode/" }; //Ignore These Directories
        public static readonly string[] VERIFY_IGNORE_FILES = { "InstallInfo.xml" }; //Ignore this File

        public String m_localInstallDir;
        public String m_localVersionNumber;
        public String m_remoteVersionNumber;

        public String m_currentUsername;
        public String m_currentPassword;

        private FileDownloader m_downloader;
        private System.Drawing.Point m_dragOffset;
        private ErrorType m_errorType;
        private LauncherState m_launcherState;
        private System.Windows.Forms.Timer m_retryTimer = new System.Windows.Forms.Timer();
        private int m_retryCount = 0;
        private OptionsForm m_optionsForm;
        private ResourceManager LocRM = new ResourceManager("RecruitsLauncher.Resources.Localization.WinFormStrings", typeof(MainForm).Assembly);
        private System.Drawing.Text.PrivateFontCollection pfc;

        public MainForm()
        {
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            try
            {
                m_downloader = new FileDownloader(this);
                InitializeComponent();
                ClientSize = new System.Drawing.Size(800, 600);
                LoadCustomFont();
                Icon = Properties.Resources.recruitsicon;
                m_optionsForm = new OptionsForm(this);
                InitializeLauncher();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message, "ERROR!");
                Close();
            }
        }

        private void InitializeLauncher()
        {
            ShowSignIn();

            CheckNetwork();
            m_localVersionNumber = GetLocalVersionNumber();
            GetRemoteVersionNumber();
        }

        private void LoadCustomFont()
        {

            byte[] myFont = Properties.Resources.CPMono_v07_Bold;
            using (var ms = new MemoryStream(myFont))
            {
                pfc = new System.Drawing.Text.PrivateFontCollection();

                byte[] buffer = new byte[ms.Length];
                ms.Read(buffer, 0, (int)ms.Length);

                System.Runtime.InteropServices.GCHandle handle = System.Runtime.InteropServices.GCHandle.Alloc(buffer, System.Runtime.InteropServices.GCHandleType.Pinned);
                
                checked
                {
                    pfc.AddMemoryFont(handle.AddrOfPinnedObject(), (int)ms.Length);

                    WebsiteLink.Font = new System.Drawing.Font(pfc.Families[0], WebsiteLink.Font.Size, System.Drawing.FontStyle.Bold);
                    ForumsLink.Font = new System.Drawing.Font(pfc.Families[0], ForumsLink.Font.Size, System.Drawing.FontStyle.Bold);
                    OptionsLink.Font = new System.Drawing.Font(pfc.Families[0], OptionsLink.Font.Size, System.Drawing.FontStyle.Bold);
                    eulaLink.Font = new System.Drawing.Font(pfc.Families[0], eulaLink.Font.Size, System.Drawing.FontStyle.Bold);
                    registerLink.Font = new System.Drawing.Font(pfc.Families[0], registerLink.Font.Size, System.Drawing.FontStyle.Bold);
                    forgotPassLink.Font = new System.Drawing.Font(pfc.Families[0], forgotPassLink.Font.Size, System.Drawing.FontStyle.Bold);

                    infoLBL.Font = new System.Drawing.Font(pfc.Families[0], infoLBL.Font.Size, System.Drawing.FontStyle.Bold);
                    passLBL.Font = new System.Drawing.Font(pfc.Families[0], passLBL.Font.Size, System.Drawing.FontStyle.Bold);
                    middleLBL.Font = new System.Drawing.Font(pfc.Families[0], middleLBL.Font.Size, System.Drawing.FontStyle.Bold);

                    actionBtn.Font = new System.Drawing.Font(pfc.Families[0], actionBtn.Font.Size, System.Drawing.FontStyle.Bold);
                    downloadBtn.Font = new System.Drawing.Font(pfc.Families[0], downloadBtn.Font.Size, System.Drawing.FontStyle.Bold);
                }
                
            }
        }

        public void ChangeLanguage(string languageCode)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(languageCode);
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            resources.ApplyResources(this, "$this");

            UpdateLanguage(resources);

            if (m_optionsForm != null)
            {
                resources = new ComponentResourceManager(typeof(OptionsForm));
                resources.ApplyResources(m_optionsForm, m_optionsForm.Name);

                foreach (Control c in m_optionsForm.Controls)
                {
                    resources.ApplyResources(c, c.Name);
                }
            }
        }

        public void UpdateLanguage(ComponentResourceManager resources)
        {
            resources.ApplyResources(WebsiteLink, WebsiteLink.Name);
            resources.ApplyResources(ForumsLink, ForumsLink.Name);
            resources.ApplyResources(OptionsLink, OptionsLink.Name);
            resources.ApplyResources(passLBL, passLBL.Name);
            LoadCustomFont();
        }

        private void UpdateLauncherState(LauncherState newState)
        {
            m_launcherState = newState;

            switch (m_launcherState)
            {
                case LauncherState.LS_SIGNIN:
                    passTF.Show();
                    userTF.Show();
                    passLBL.Show();
                    infoLBL.Show();
                    infoLBL.Text = "E-Mail";
                    registerLink.Show();
                    forgotPassLink.Show();
                    actionBtn.Enabled = true;
                    actionBtn.Text = LocRM.GetString("actionBtnSignIn");
                    pauseBtn.Hide();
                    playBtn.Hide();
                    totalProgressBar.Hide();
                    middleLBL.Hide();
                    break;
                case LauncherState.LS_ONLINE:
                    actionBtn.Enabled = true;
                    infoLBL.Text = LocRM.GetString("updateComplete"); ;
                    pauseBtn.Hide();
                    playBtn.Hide();
                    actionBtn.Text = LocRM.GetString("actionBtnOnline");
                    downloadBtn.Hide();
                    middleLBL.Show();
                    middleLBL.Text = String.Format(LocRM.GetString("updated") + " - V{0}", m_localVersionNumber);
                    break;
                case LauncherState.LS_OFFLINE:
                    passTF.Hide();
                    userTF.Hide();
                    passLBL.Hide();
                    totalProgressBar.Hide();
                    infoLBL.Hide();
                    downloadBtn.Hide();
                    middleLBL.Show();
                    registerLink.Hide();
                    forgotPassLink.Hide();
                    playBtn.Hide();
                    pauseBtn.Hide();
                    actionBtn.Text = LocRM.GetString("actionBtnOffline");
                    actionBtn.Enabled = true;
                    break;
                case LauncherState.LS_UPDATING:
                    pauseBtn.Show(); 
                    actionBtn.Enabled = false;
                    middleLBL.Hide();
                    downloadBtn.Hide();
                    totalProgressBar.Show();
                    infoLBL.Show();
                    infoLBL.Text = LocRM.GetString("initializingUpdate");
                    break;
            }
        }

        private void ShowSignIn()
        {
            userTF.Text = (String)Settings.Default["rememberedUser"];
            UpdateLauncherState(LauncherState.LS_SIGNIN);
            m_errorType = ErrorType.ER_NONE;
        }

        private void ElevatePrivileges()
        {
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.UseShellExecute = true;
            proc.WorkingDirectory = Environment.CurrentDirectory;
            proc.FileName = Application.ExecutablePath;
            proc.Verb = "runas";

            try
            {
                Process.Start(proc);
            }
            catch
            {
                return;
            }

            Application.Exit();  // Quit itself
        }

        public void DownloadComplete()
        {
            UpdateLauncherState(LauncherState.LS_ONLINE);
            totalProgressBar.Hide();
        }

        public void UpdateProgressBar()
        {
            int totalPercent = (int)m_downloader.TotalPercentage();
            if(totalPercent > 100) {totalPercent = 100;}
            if (totalPercent < 0) { totalPercent = 0; }
            totalProgressBar.Value = totalPercent;
            infoLBL.Text = String.Format(LocRM.GetString("downloadingUpdate") + " - {0} / {1} ({2})",
                FileDownloader.FormatSizeBinary(m_downloader.m_totalProgress),
                FileDownloader.FormatSizeBinary(m_downloader.m_totalSize),
                FileDownloader.FormatSizeBinary(m_downloader.m_downloadSpeed*10)+"/s");
        }

        public void DownloadFailed()
        {
            m_downloader.Pause();
            ShowError(ErrorType.ER_NOCONNECTION);
        }

        private void CheckNetwork()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                ShowError(ErrorType.ER_NOCONNECTION);
            }
        }

        private bool CheckConnection()
        {
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(VERSION_URL);
                stream.Close();
                return true;
            }
            catch
            {
            }
            return false;
        }

        public void ForceRepair()
        {
            m_launcherState = LauncherState.LS_REPAIRING;
            UpdateGame();
        }

        private void ShowError(ErrorType errType)
        {
            m_errorType = errType;

            switch (m_errorType)
            {
                case ErrorType.ER_NOCONNECTION:
                    ConnectionError();
                    break;

                case ErrorType.ER_WRONG_ACCOUNT_INFO:
                    infoLBL.Text = LocRM.GetString("incorrectInfo");
                    infoLBL.ForeColor = System.Drawing.Color.Red;
                    registerLink.Show();
                    passLBL.Show();
                    break;

                case ErrorType.ER_NO_OWN_GAME:
                    infoLBL.Text = LocRM.GetString("noOwn");
                    infoLBL.ForeColor = System.Drawing.Color.Red;
                    registerLink.Hide();
                    passLBL.Hide();
                    break;
            }
        }

        private void ConnectionError()
        {
            UpdateLauncherState(LauncherState.LS_OFFLINE);
            middleLBL.Left = 15;
            middleLBL.Text = LocRM.GetString("retryTXT_p1") + (5 - m_retryCount) + LocRM.GetString("seconds") + "...";
            m_retryCount = 0;
            if (!m_retryTimer.Enabled)
            {
                m_retryTimer.Tick += m_retryTimer_Tick;
                m_retryTimer.Interval = 1000;
                m_retryTimer.Start();
            }
        }

        void m_retryTimer_Tick(object sender, EventArgs e)
        {
            if (m_retryCount > 5)
            {
                if (CheckConnection())
                {
                    middleLBL.Left = 200;
                    m_retryTimer.Stop();
                    ShowSignIn();
                }
                m_retryCount = 0;
            }
            else
            {
                middleLBL.Text = LocRM.GetString("retryTXT_p1") + (5 - m_retryCount) + LocRM.GetString("seconds") + "...";
                m_retryCount++;
            }
        }

        public bool CanUpdateLocalPath()
        {
            if (m_launcherState != LauncherState.LS_REPAIRING && m_launcherState != LauncherState.LS_UPDATING)
            {
                return true;
            }

            return false;
        }

        public void UpdateLocalPath(String newPath)
        {
            m_localInstallDir = newPath;
            if (m_launcherState == LauncherState.LS_ONLINE || m_launcherState == LauncherState.LS_OFFLINE)
            {
                UserLoggedIn();
            }
        }

        private void actionBtn_Click(object sender, EventArgs e)
        {
            switch (m_launcherState)
            {
                case LauncherState.LS_SIGNIN:
                    registerLink.Hide();
                    infoLBL.ForeColor = System.Drawing.Color.Silver;
                    infoLBL.Text = LocRM.GetString("attemptingSignIn");
                    TryLogin(userTF.Text, passTF.Text);
                    break;

                case LauncherState.LS_OFFLINE:
                case LauncherState.LS_ONLINE:
                    LaunchGame();
                    break;
            }
        }

        private void LaunchGame()
        {
            try
            {
                Process.Start(m_localInstallDir + "/Binaries/Win32/Recruits.exe");
                Close();
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + "\n\n" + LocRM.GetString("launchError_p1") + "\n\n" + LocRM.GetString("launchError_p2") + "\n\n" + LocRM.GetString("launchError_p3"), "ERROR", MessageBoxButtons.OKCancel);
                if(result == DialogResult.OK)
                {
                    ForceRepair();
                }
            }
        }

        private void TryLogin(String username, String password)
        {
            Settings.Default["rememberedUser"] = username;
            Settings.Default.Save();
            m_currentUsername = username;
            m_currentPassword = password;

            if (username == "" || password == "")
            {
                ShowError(ErrorType.ER_WRONG_ACCOUNT_INFO);
                return;
            }

            username = username.ToLower();

            WebClient client = new WebClient();
            Encryption enc = new Encryption();
            String ParamString = enc.Encrypt3DES(password);
            try 
            {
                String ConnString = AUTHORIZE_URL+"?user="+username+"&params="+ParamString;
                String line = client.DownloadString(ConnString);
                if (line == "VALID")
                {
                    UserLoggedIn();
                    return;
                }
                else if (line == "NO GAME")
                {
                    ShowError(ErrorType.ER_NO_OWN_GAME);
                    return;
                }
                else if (line.Contains("INVALID"))
                {
                    ShowError(ErrorType.ER_WRONG_ACCOUNT_INFO);
                    return;
                }
            }
            catch (Exception e)
            {
                #if DEBUG
                    MessageBox.Show("Unable to Connect to Recruits Login Servers: " + e.Message);
                #endif
            }
            ShowError(ErrorType.ER_NOCONNECTION);
        }

        private void UserLoggedIn()
        {
            UpdateLauncherState(LauncherState.LS_OFFLINE);
            actionBtn.Text = LocRM.GetString("actionBtnOffline");
            middleLBL.Text = LocRM.GetString("scanningInstall");
            CheckForInstall();
        }

        private void CheckForInstall()
        {
            try {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(MANIFEST_URL);
                
                StreamReader resStream = new StreamReader(stream);

                while (!resStream.EndOfStream)
                {
                    String filePath = resStream.ReadLine();
                    string[] splitFile = filePath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (splitFile.Length >= 2)
                    {
                        if (!File.Exists(m_localInstallDir + splitFile[0]) || (new FileInfo(m_localInstallDir + splitFile[0]).Length != Int64.Parse(splitFile[1])))
                        {
                            bool bIgnore = false;

                            foreach (string str in VERIFY_IGNORE_DIRECTORIES)
                            {
                                if (splitFile[0].Contains(str))
                                {
                                    bIgnore = true;
                                }
                            }

                            foreach (string str in VERIFY_IGNORE_FILES)
                            {
                                if (splitFile[0].Contains(str))
                                {
                                    bIgnore = true;
                                }
                            }
                            System.Diagnostics.Debug.WriteLine("YAY");
                            if (!bIgnore)
                            {
                                System.Diagnostics.Debug.WriteLine("FAILED");
                                System.Diagnostics.Debug.WriteLine(splitFile[0]);
                                ErrorCheckingInstall();
                                stream.Close();
                                resStream.Dispose();
                                return;
                            }
                        }
                    }
                }
                stream.Close();
                resStream.Dispose();

                SuccessCheckingInstall();
                return;
            }
            catch (Exception e) 
            {
                MessageBox.Show("Turd");
                System.Diagnostics.Debug.WriteLine("Tured");
                #if DEBUG
                    MessageBox.Show("Unable to Connect to Recruits Servers for Install Verification: " + e.Message);
                #endif
            }
            ShowError(ErrorType.ER_NOCONNECTION);
            return;
        }

        private void SuccessCheckingInstall()
        {
            if (m_localVersionNumber != m_remoteVersionNumber)
            {
                Int64 downloadSize = GetDownloadSize();
                actionBtn.Text = LocRM.GetString("actionBtnOffline");
                middleLBL.Text = LocRM.GetString("updateAvailable") + String.Format(" - V{0}  |  {1}", m_remoteVersionNumber, FileDownloader.FormatSizeBinary(downloadSize));
                downloadBtn.Show();
            }
            else
            {
                UpdateLauncherState(LauncherState.LS_ONLINE);
                infoLBL.Hide();
            }
        }

        private void ErrorCheckingInstall()
        {
            Int64 downloadSize = GetDownloadSize();
            middleLBL.Text = LocRM.GetString("needDownload") + String.Format(" - V{0}  |  {1}", m_remoteVersionNumber, FileDownloader.FormatSizeBinary(downloadSize));
            downloadBtn.Show();
        }

        private string GetLocalVersionNumber()
        {
            String filePath = "/UDKGame/versionInfo.txt";
            String version = "-1";
            Debug.WriteLine(m_localInstallDir + filePath + " _ " + File.Exists(m_localInstallDir + filePath));
            if (File.Exists(m_localInstallDir + filePath))
            {
                
                string[] lines = File.ReadAllLines(m_localInstallDir + filePath);
                foreach (string line in lines)
                {
                    if (line.IndexOf(':') != -1)
                    {
                        try
                        {
                            String key = line.Substring(0, line.IndexOf(':'));
                            String value = line.Substring(line.IndexOf(':') + 1);

                            if (key == "VER")
                            {
                                version = value;
                            }
                        }
                        catch (Exception) { }
                    }
                }
            }

            return version;
        }

        private void GetRemoteVersionNumber()
        {
            string version = "-2";
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(VERSION_URL);
                StreamReader resStream = new StreamReader(stream);

                while (!resStream.EndOfStream)
                {
                    String line = resStream.ReadLine();
                    if (line.IndexOf(':') != -1)
                    {
                        try
                        {
                            String key = line.Substring(0, line.IndexOf(':'));
                            String value = line.Substring(line.IndexOf(':') + 1);

                            if (key == "VER")
                            {
                                version = value;
                            }
                        }
                        catch (Exception) { }
                    }
                }
                stream.Close();
                resStream.Dispose();
                m_remoteVersionNumber = version;
                return;
            }
            catch (Exception e)
            {
                #if DEBUG
                    MessageBox.Show("Unable to Connect to Recruits Servers for Version Authentication: " + e.Message);
                #endif
            }
            
            ShowError(ErrorType.ER_NOCONNECTION);
            m_remoteVersionNumber = "-2";
        }

        private void downloadBtn_Click(object sender, EventArgs e)
        {
            UpdateGame();
        }

        private void UpdateGame()
        {
            bool connection = CheckConnection();

            try
            {
                if (!Directory.Exists(m_localInstallDir))
                {
                    Directory.CreateDirectory(m_localInstallDir);
                }


                using (FileStream fstream = new FileStream(m_localInstallDir+"/rubbish.txt", FileMode.Create))
                using (TextWriter writer = new StreamWriter(fstream))
                {
                    writer.WriteLine("sometext");
                }

                File.Delete(m_localInstallDir + "/rubbish.txt");
            }
            catch (Exception)
            {
                DialogResult result = MessageBox.Show(m_localInstallDir + LocRM.GetString("privError_p1"), "ERROR", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    ElevatePrivileges();
                }
                return;                
            }
            UpdateLauncherState(LauncherState.LS_UPDATING);
            
            SetDownloadServer();
        }

        private void SetupDownloader()
        {
            m_downloader.m_localDirectory = m_localInstallDir;
            m_downloader.m_files.Clear();
            GetFileList();
        }

        private void SetDownloadServer()
        {
            Int64 lowestTime = Int64.MaxValue;
            foreach (String serverURL in DOWNLOAD_URLS)
            {
                Int64 time = PingServer(serverURL + "?loc=UDKGame/PCTOC.txt");
                if (time < lowestTime)
                {
                    lowestTime = time;
                    DOWNLOAD_URL = serverURL;
                    m_downloader.m_downloadURL = serverURL;
                }
            }

            if (DOWNLOAD_URL != "")
            {
                SetupDownloader();
            }
        }

        private Int64 PingServer(String ServerURL)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                WebClient client = new WebClient();
                watch.Start();
                Stream stream = client.OpenRead(ServerURL);
                stream.Close();
                watch.Stop();
            } catch {
                return Int64.MaxValue;
            }

            return watch.ElapsedMilliseconds;
        }

        private void GetFileList()
        {
            List<FileDownloader.FileInfo> result = new List<FileDownloader.FileInfo>();
            result.Clear();
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(MANIFEST_URL);
                StreamReader resStream = new StreamReader(stream);

                while (!resStream.EndOfStream)
                {
                    String filePath = resStream.ReadLine();
                    string[] splitFile = filePath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (splitFile.Length >= 2)
                    {
                        Int64 size = Int64.Parse(splitFile[1]);
                        try
                        {
                            result.Add(new FileDownloader.FileInfo(splitFile[0], splitFile[0].Substring(0, splitFile[0].LastIndexOf('/')), Int64.Parse(splitFile[1]), DateTime.Now));
                        }
                        catch (Exception)
                        {
                            result.Add(new FileDownloader.FileInfo(splitFile[0], splitFile[0].Substring(0, splitFile[0].LastIndexOf('/'))));
                        }
                    }
                    else
                    {
                        result.Add(new FileDownloader.FileInfo(splitFile[0], splitFile[0].Substring(0, splitFile[0].LastIndexOf('/'))));
                    }
                }
                stream.Close();
                resStream.Dispose();
                m_downloader.m_files.AddRange(result);
                m_downloader.Start();
                return;
            }
            catch (Exception) { }
        }

        private Int64 GetDownloadSize()
        {
            Int64 totalSize = 0;

            totalSize = 0;
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(MANIFEST_URL);

                StreamReader resStream = new StreamReader(stream);

                while (!resStream.EndOfStream)
                {
                    String filePath = resStream.ReadLine();
                    string[] splitFile = filePath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (splitFile.Length >= 2)
                    {
                        if (!File.Exists(m_localInstallDir + splitFile[0]) || (new FileInfo(m_localInstallDir + splitFile[0]).Length != Int64.Parse(splitFile[1])))
                        {
                            bool bIgnore = false;

                            foreach (string str in VERIFY_IGNORE_DIRECTORIES)
                            {
                                if (splitFile[0].Contains(str))
                                {
                                    bIgnore = true;
                                }
                            }

                            foreach (string str in VERIFY_IGNORE_FILES)
                            {
                                if (splitFile[0].Contains(str))
                                {
                                    bIgnore = true;
                                }
                            }

                            if (!bIgnore)
                            {
                                totalSize += Int64.Parse(splitFile[1]);
                            }
                        }
                    }
                }
                stream.Close();
                resStream.Dispose();
                return totalSize;
            }
            catch (Exception e)
            {
                #if DEBUG
                MessageBox.Show(e.Message);
                #endif
            }
            
            ShowError(ErrorType.ER_NOCONNECTION);
            return totalSize;
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
                pauseBtn.Hide();
                playBtn.Show();
                m_downloader.Pause();
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
                pauseBtn.Show();
                playBtn.Hide();
                m_downloader.Resume();
        }

        #region TopBar Stuff

        private void WebsiteLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://recruitsgame.com");

        }

        private void ForumsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://forums.recruitsgame.com");
        }

        private void OptionsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_optionsForm.ShowDialog();
        }

        private void eulaLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://recruitsgame.com/license");
        }

        private void registerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://recruitsgame.com/TestSite/register/");
        }

        private void forgotPassLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://recruitsgame.com/TestSite/password-reset/");
        }

        private void movebar_picBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_dragOffset = PointToScreen(e.Location);
                var formLocation = FindForm().Location;
                m_dragOffset.X -= formLocation.X;
                m_dragOffset.Y -= formLocation.Y;
            }
        }

        private void movebar_picBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                System.Drawing.Point newLoc = PointToScreen(e.Location);

                newLoc.X -= m_dragOffset.X;
                newLoc.Y -= m_dragOffset.Y;
                FindForm().Location = newLoc;
            }
        }

        private void closeLBL_Click(object sender, EventArgs e)
        {
            m_downloader.Pause();
            Close();
        }

        private void minimizeLBL_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        System.Drawing.Color offColor = System.Drawing.Color.FromArgb(190, 190, 190);
        System.Drawing.Color hoverColor = System.Drawing.Color.FromArgb(255, 255, 255);

        private void WebsiteLink_MouseEnter(object sender, EventArgs e)
        {
            WebsiteLink.LinkColor = hoverColor;
        }

        private void WebsiteLink_MouseLeave(object sender, EventArgs e)
        {
            WebsiteLink.LinkColor = offColor;
        }

        private void ForumsLink_MouseEnter(object sender, EventArgs e)
        {
            ForumsLink.LinkColor = hoverColor;
        }

        private void ForumsLink_MouseLeave(object sender, EventArgs e)
        {
            ForumsLink.LinkColor = offColor;
        }

        private void OptionsLink_MouseEnter(object sender, EventArgs e)
        {
            OptionsLink.LinkColor = hoverColor;
        }

        private void OptionsLink_MouseLeave(object sender, EventArgs e)
        {
            OptionsLink.LinkColor = offColor;
        }

        private void eulaLink_MouseEnter(object sender, EventArgs e)
        {
            eulaLink.LinkColor = hoverColor;
        }

        private void eulaLink_MouseLeave(object sender, EventArgs e)
        {
            eulaLink.LinkColor = offColor;
        }

        private void registerLink_MouseEnter(object sender, EventArgs e)
        {
            registerLink.LinkColor = hoverColor;
        }

        private void registerLink_MouseLeave(object sender, EventArgs e)
        {
            registerLink.LinkColor = offColor;
        }

        private void forgotPassLink_MouseEnter(object sender, EventArgs e)
        {
            forgotPassLink.LinkColor = hoverColor;
        }

        private void forgotPassLink_MouseLeave(object sender, EventArgs e)
        {
            forgotPassLink.LinkColor = offColor;
        }

        private void minimizeLBL_MouseEnter(object sender, EventArgs e)
        {
            minimizeLBL.ForeColor = hoverColor;
        }

        private void minimizeLBL_MouseLeave(object sender, EventArgs e)
        {
            minimizeLBL.ForeColor = offColor;
        }

        private void closeLBL_MouseEnter(object sender, EventArgs e)
        {
            closeLBL.ForeColor = hoverColor;
        }

        private void closeLBL_MouseLeave(object sender, EventArgs e)
        {
            closeLBL.ForeColor = offColor;
        }

        #endregion

        private void enLangBtn_Click(object sender, EventArgs e)
        {
            ChangeLanguage("en-US");
            m_optionsForm.languageCombo.SelectedIndex = m_optionsForm.languageCombo.Items.IndexOf(Languages.English);
        }

        private void deLangBtn_Click(object sender, EventArgs e)
        {
            ChangeLanguage("de-de");
            m_optionsForm.languageCombo.SelectedIndex = m_optionsForm.languageCombo.Items.IndexOf(Languages.German);
        }
    }
}
