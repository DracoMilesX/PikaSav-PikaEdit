using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace Pikaedit_Gen4
{
    public static class Updater
    {
        #region variables

        private static readonly string url = "https://dl.dropboxusercontent.com/s/rg45gd8drb2dh0h/version%20Gen%204%20C%23.txt?dl=1";
        private static WebClient checkVersion = new WebClient();
        private static WebClient downloadUpdate = new WebClient();
        private static bool initialized = false;
        private static string log = "";
        private static string file = "";
        private static List<Pikaedit_Lib.IPlugin> plugins;
        private static List<string> pluginFiles;

        #endregion

        #region methods
        public static void Initialize()
        {
            if (!initialized)
            {
                initialized = true;
                log = "";
                file = "";
                if (File.Exists("updater.exe"))
                {
                    File.Delete("updater.exe");
                }
                if (File.Exists("langupdater.exe"))
                {
                    File.Delete("langupdater.exe");
                }
                checkVersion.DownloadStringCompleted += checkVersion_DownloadStringCompleted;
                downloadUpdate.DownloadFileCompleted += downloadUpdate_DownloadFileCompleted;
                try
                {
                    check();
                }
                catch (Exception e)
                {
                }
            }
        }

        public static void Initialize(List<Pikaedit_Lib.IPlugin> p, List<string> files)
        {
            if (!initialized)
            {
                plugins = p;
                pluginFiles = files;
                initialized = true;
                log = "";
                file = "";
                if (File.Exists("updater.exe"))
                {
                    File.Delete("updater.exe");
                }
                if (File.Exists("langupdater.exe"))
                {
                    File.Delete("langupdater.exe");
                }
                checkVersion.DownloadStringCompleted += checkVersion_DownloadStringCompleted;
                downloadUpdate.DownloadFileCompleted += downloadUpdate_DownloadFileCompleted;
                try
                {
                    check();
                }
                catch (Exception e)
                {
                }
            }
        }

        private static void downloadUpdate_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string ver = System.Reflection.Assembly.GetEntryAssembly().Location + "\n" + file;
            //Update Plugins
            updatePlugins();
            System.Windows.Forms.MessageBox.Show("Update Downloaded! Pikaedit will restart", "Update Downloaded", System.Windows.Forms.MessageBoxButtons.OK);
            File.WriteAllText("info.bin", ver);
            File.WriteAllBytes("updater.exe", Properties.Resources.Updater);
            //File.WriteAllBytes("langupdater.exe", Properties.Resources.Language_Updater);
            Process.Start("updater.exe", "\"Pikaedit Gen 4\" info.bin");
            System.Threading.Thread.Sleep(500);
            Environment.Exit(1);
        }

        private static void updatePlugins()
        {
            if (Directory.GetFiles("Plugins").Length != 0)
            {
                WebClient downloader = new System.Net.WebClient();
                string extraPlugins = downloader.DownloadString("https://dl.dropboxusercontent.com/u/87538979/Pikaedit/Plugins/PluginUpdater.txt");
                Dictionary<string, string> pluginslist = new Dictionary<string, string>();
                string[] readlist = extraPlugins.Split('\n');
                for (int i = 0; i < readlist.Length; i++)
                {
                    readlist[i] = readlist[i].TrimEnd('\r');
                    if (!string.IsNullOrEmpty(readlist[i]))
                    {
                        pluginslist.Add(readlist[i].Split('|')[0], readlist[i].Split('|')[1]);
                    }
                }
                List<string> pluginsName = new List<string>();
                int s = plugins.Count;
                if (s != 0)
                {
                    List<Pikaedit_Lib.IPlugin> temp = new List<Pikaedit_Lib.IPlugin>();
                    temp.AddRange(plugins.ToArray());
                    foreach (Pikaedit_Lib.IPlugin plugin in plugins)
                    {
                        if (pluginslist.Keys.Contains(plugin.getName()))
                        {
                            pluginsName.Add(plugin.getName());
                            int i = temp.IndexOf(plugin);
                            temp.Remove(plugin);
                            if (File.Exists(pluginFiles[i]))
                            {
                                File.Delete(pluginFiles[i]);
                            }
                            pluginFiles.RemoveAt(i);
                        }
                    }

                    plugins = temp;
                    string args = "";
                    foreach (string name in pluginsName)
                    {
                        args += "\"" + name + "\" ";
                    }
                    if (args != string.Empty)
                    {
                        File.WriteAllBytes("PluginUpdater.exe", Properties.Resources.PluginUpdater);
                        Process process = Process.Start("PluginUpdater.exe", args.TrimEnd(' '));
                        process.WaitForExit();
                        File.Delete("PluginUpdater.exe");
                    }
                }
            }
        }

        private static void checkVersion_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error!=null)
            {
                return;
            }
            string v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string[] s = e.Result.Split('\n'); //Line 0 contains version, line 1-X contains version changelog, remove \r by trimming end!
            for (int i = 2; i < s.Length; i++)
            {
                log += s[i] + "\n";
            }
            log = log.TrimEnd('\n', '\r');
            s[0] = s[0].TrimEnd('\r').Replace("PikaEdit ", "");
            s[1] = s[1].TrimEnd('\r');
            //tempFile = Path.GetTempPath() + "\\PikaEdit " + s[0] + ".exe";
            file = "PikaEdit Gen4 " + s[0] + ".exe";
            if (!s[0].Equals(v))
            {
                log = "Pikaedit Gen4 has been updated to version " + s[0].TrimEnd('.', '0')/*.TrimStart('0','.')*/ + "\nVersion changes:\n" + log;
                System.Windows.Forms.DialogResult res = System.Windows.Forms.MessageBox.Show(log + "\nDownload Update?", "Update Available", System.Windows.Forms.MessageBoxButtons.OKCancel);
                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    downloadUpdate.DownloadFileAsync(new Uri(s[1]), file);
                }
            }
        }

        public static void check()
        {
            try
            {
                checkVersion.DownloadStringAsync(new Uri(url));
            }
            catch (Exception e)
            {
                //throw e;
            }
        }
        #endregion

    }
}
