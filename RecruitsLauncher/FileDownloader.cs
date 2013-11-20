using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net;

namespace RecruitsLauncher
{
    class FileDownloader
    {
        public struct FileInfo
        {
            public string Path;
            public string LocalPath;
            public string Name;
            public Int64 FileSize;
            public DateTime LastModifyDate;

            public FileInfo(String path, String localPath)
            {
                this.Path = path;
                this.Name = this.Path.Split("/"[0])[this.Path.Split("/"[0]).Length - 1];
                this.LocalPath = localPath;
                this.FileSize = 0;
                this.LastModifyDate = DateTime.Now;
            }

            public FileInfo(String path, String localPath, Int64 fileSize, DateTime lastModified)
            {
                this.Path = path;
                this.Name = this.Path.Split("/"[0])[this.Path.Split("/"[0]).Length - 1];
                this.LocalPath = localPath;
                this.FileSize = fileSize;
                this.LastModifyDate = lastModified;
            }
        }

        private const Int32 DEFAULT_DECIMALS = 2;

        public string m_localDirectory, m_downloadURL;
        public List<FileInfo> m_files = new List<FileInfo>();

        public Queue<FileInfo> items = new Queue<FileInfo>();

        public Int64 m_totalSize, m_totalProgress, m_downloadSpeed;

        private DateTime m_lastUpdate;

        private MainForm m_mainForm;

        private Int64 m_lastBytesRecievedSize = 0;

        private FileInfo activeFile;

        private bool m_bPendingStop;

        public FileDownloader(MainForm owner)
        {
            m_mainForm = owner;
        }

        public void Start()
        {
            m_bPendingStop = false;
            PopulateItemsQueue();
            DownloadItem();
        }

        public void Pause()
        {
            m_bPendingStop = true;
        }

        public void Resume()
        {
            m_bPendingStop = false;
            DownloadItem();
        }

        public void PopulateItemsQueue()
        {
            m_totalSize = 0;

            for (Int32 fileNr = 1; fileNr < m_files.Count; fileNr++)
            {
                bool bFileGood = false;

                if (File.Exists(m_localDirectory + "/" + m_files[fileNr].LocalPath + "/" + m_files[fileNr].Name))
                {
                    bool bIgnore = false;

                    foreach (string str in MainForm.VERIFY_IGNORE_DIRECTORIES)
                    {
                        String path = m_files[fileNr].LocalPath + "/";
                        if (path.Contains(str))
                        {
                            bIgnore = true;
                        }
                    }

                    foreach (string str in MainForm.VERIFY_IGNORE_FILES)
                    {
                        if (m_files[fileNr].Name == str)
                        {
                            bIgnore = true;
                        }
                    }

                    System.IO.FileInfo localFileInfo = new System.IO.FileInfo(m_localDirectory + "/" + m_files[fileNr].LocalPath + "/" + m_files[fileNr].Name);
                    Int64 localSize = localFileInfo.Length;
                    Int64 hostSize = m_files[fileNr].FileSize;
                    if ((localSize == hostSize) || bIgnore)
                    {
                        bFileGood = true;
                    }
                }

                if (!bFileGood)
                {
                    items.Enqueue(m_files[fileNr]);
                    m_totalSize += m_files[fileNr].FileSize;
                }
            }
        }

        private void DownloadItem()
        {
            if (m_bPendingStop)
            {
                return;
            }

            if(items.Any()) 
            {
                var nextItem = items.Dequeue();
                activeFile = nextItem;

                if (!Directory.Exists(m_localDirectory + "/" + nextItem.LocalPath))
                {
                    Directory.CreateDirectory(m_localDirectory + "/" + nextItem.LocalPath);
                }

                var webClient = new WebClient();
                webClient.DownloadFileCompleted += OnGetDownloadFileCompleted;
                webClient.DownloadProgressChanged += OnDownloadProgressChanged;
                webClient.DownloadFileAsync(new Uri(MainForm.DOWNLOAD_URL + "?loc=" + nextItem.Path), m_localDirectory+nextItem.Path); 
                return;
            }
            m_mainForm.DownloadComplete();
        }

        void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (m_bPendingStop)
            {
                ((WebClient)sender).CancelAsync();
                items.Enqueue(activeFile);
                progressChanged(0);
                m_totalProgress -= m_lastBytesRecievedSize;
                m_lastBytesRecievedSize = 0;
                m_mainForm.UpdateProgressBar();
                return;
            }

            progressChanged(e.BytesReceived);
            m_totalProgress += e.BytesReceived - m_lastBytesRecievedSize;
            m_lastBytesRecievedSize = e.BytesReceived;

            m_mainForm.UpdateProgressBar();
        }

        private void progressChanged(long bytes)
        {
            if (m_lastBytesRecievedSize == 0)
            {
                m_lastUpdate = DateTime.Now;
                return;
            }

            var now = DateTime.Now;
            var timeSpan = now - m_lastUpdate;
            var bytesChange = bytes - m_lastBytesRecievedSize;
            if (timeSpan.Seconds != 0)
            {
                m_downloadSpeed = (long)(bytesChange / timeSpan.Seconds);
                m_lastUpdate = now;
            }
        }

        private void OnGetDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            m_lastBytesRecievedSize = 0;
            if (e.Error == null || m_bPendingStop)
            {
                DownloadItem();
            }
            else
            {
                m_mainForm.DownloadFailed();
            }
        }

        #region Size Formating Helpers

        public static string FormatSizeBinary(Int64 size)
        {
            return FormatSizeBinary(size, DEFAULT_DECIMALS);
        }

        public static string FormatSizeBinary(Int64 size, Int32 decimals)
        {
            String[] sizes = { "MB", "KB", "MB", "GB", "TB" };
            double formattedSize = size;
            Int32 sizeIndex = 0;
            while (formattedSize >= 1024 && sizeIndex < sizes.Length)
            {
                formattedSize /= 1024;
                sizeIndex++;
            }
            return Math.Round(formattedSize, decimals) + sizes[sizeIndex];
        }

        public static string FormatSizeDecimal(Int64 size)
        {
            return FormatSizeDecimal(size, DEFAULT_DECIMALS);
        }

        public static string FormatSizeDecimal(Int64 size, Int32 decimals)
        {
            String[] sizes = { "B", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB", "ZiB", "YiB" };
            double formattedSize = size;
            Int32 sizeIndex = 0;
            while (formattedSize >= 1000 && sizeIndex < sizes.Length)
            {
                formattedSize /= 1000;
                sizeIndex++;
            }
            return Math.Round(formattedSize, decimals) + sizes[sizeIndex];
        }

        #endregion

        public double TotalPercentage()
        {
            return Math.Round((Double)m_totalProgress / m_totalSize * 100, DEFAULT_DECIMALS);
        }

    }
}
