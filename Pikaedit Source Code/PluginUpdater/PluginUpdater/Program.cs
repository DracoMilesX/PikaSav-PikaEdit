using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace PluginUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string,string> url = new Dictionary<string,string>();
            WebClient downloader = new WebClient();
            Console.WriteLine("Downloading additional plugin list...");
            string extraPlugins = downloader.DownloadString("https://dl.dropboxusercontent.com/u/87538979/Pikaedit/Plugins/PluginUpdater.txt");
            string allPlugins = Properties.Resources.File + "\n" + extraPlugins;
            Dictionary<string, string> plugins = new Dictionary<string, string>();
            string[] readlist = allPlugins.Split('\n');
            if (args.Length != 0)
            {
                for (int i = 0; i < readlist.Length; i++)
                {
                    readlist[i] = readlist[i].TrimEnd('\r');
                    if (!string.IsNullOrEmpty(readlist[i]))
                    {
                        if (!plugins.Keys.Contains(readlist[i].Split('|')[0]))
                        {
                            plugins.Add(readlist[i].Split('|')[0], readlist[i].Split('|')[1]);
                        }
                    }
                }
                if (!Directory.Exists("Plugins"))
                {
                    Directory.CreateDirectory("Plugins");
                }
                foreach (string name in args)
                {
                    Console.WriteLine("Reading plugin list...");
                    if (plugins.Keys.Contains(name))
                    {
                        Console.WriteLine("Downloading " + name + " Plugin...");
                        if (File.Exists(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Plugins" + Path.DirectorySeparatorChar + name + ".dll"))
                        {
                            File.Delete(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Plugins" + Path.DirectorySeparatorChar + name + ".dll");
                        }
                        downloader.DownloadFile(plugins[name], Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Plugins" + Path.DirectorySeparatorChar + name + ".dll");
                        Console.WriteLine(name + " Plugin Download Complete...\n");
                    }
                }
                Console.WriteLine("Plugins have been updated!\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
