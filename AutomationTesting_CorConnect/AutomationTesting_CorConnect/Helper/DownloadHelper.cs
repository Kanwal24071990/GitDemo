using OpenQA.Selenium;
using System.IO;

namespace AutomationTesting_CorConnect.Helper
{
    internal class DownloadHelper
    {
        IWebDriver driver;
        private string lastDownloadedFilePath;

        internal DownloadHelper(IWebDriver driver)
        {
            this.driver = driver;
        }

        internal string WaitForDownload(string folder, string extension, By button, int milsToStartdownload, int milstoRenameTmp)
        {
            if (!extension.StartsWith('.'))
            {
                extension.Insert(0, ".");
            }

            string fileName = string.Empty;
            using (var watcher = new FileSystemWatcher(folder))
            {

                // Setting the watcher to listen in case of file creations on the directory
                watcher.EnableRaisingEvents = true;
                watcher.Created += new FileSystemEventHandler(fsw_created);
                watcher.Changed += new FileSystemEventHandler(fsw_changed);
                lastDownloadedFilePath = "";

                driver.FindElement(button).Click();

                // 50 seconds to start download since click
                var resultFile = watcher.WaitForChanged(WatcherChangeTypes.All, 80000);

                while (!resultFile.Name.EndsWith(extension))
                {
                    // 20 seconds rename the file from .tmp to correct extension
                    resultFile = watcher.WaitForChanged(WatcherChangeTypes.All, milstoRenameTmp);
                    if (resultFile.TimedOut)
                    {
                        if (lastDownloadedFilePath.EndsWith(extension))
                        {
                            fileName = lastDownloadedFilePath.Substring(lastDownloadedFilePath.LastIndexOf("\\") + 1);
                            watcher.Dispose();
                            return fileName;
                        }
                        else
                        {
                            watcher.Dispose();
                            return string.Empty;
                        }
                    }
                }
                watcher.Dispose();
                fileName = resultFile.Name;
                return fileName;

            }

        }

        internal string DownloadFile(string folder, string extension, By button)
        {
            if (!extension.StartsWith('.'))
            {
                extension.Insert(0, ".");
            }

            string fileName = string.Empty;
            using (var watcher = new FileSystemWatcher(folder))
            {

                // Setting the watcher to listen in case of file creations on the directory
                watcher.EnableRaisingEvents = true;
                watcher.Created += new FileSystemEventHandler(fsw_created);
                watcher.Changed += new FileSystemEventHandler(fsw_changed);
                lastDownloadedFilePath = "";

                driver.FindElement(button).Click();
                var resultFile = watcher.WaitForChanged(WatcherChangeTypes.All);

                while (!resultFile.Name.EndsWith(extension))
                {
                    // 80 seconds rename the file from .tmp to correct extension
                    resultFile = watcher.WaitForChanged(WatcherChangeTypes.All, 80000);
                    if (resultFile.TimedOut)
                    {
                        if (lastDownloadedFilePath.EndsWith(extension))
                        {
                            fileName = lastDownloadedFilePath.Substring(lastDownloadedFilePath.LastIndexOf("\\") + 1);
                            watcher.Dispose();

                            return fileName;
                        }
                        else
                        {
                            watcher.Dispose();
                            return string.Empty;
                        }
                    }
                }


                watcher.Dispose();
                fileName = resultFile.Name;
                return fileName;

            }

        }

        private void fsw_created(object sender, FileSystemEventArgs e)
        {
            //teporary file
            lastDownloadedFilePath = e.FullPath;
        }
        private void fsw_changed(object sender, FileSystemEventArgs e)
        {
            //teporary or final file
            lastDownloadedFilePath = e.FullPath;
        }
    }
}
