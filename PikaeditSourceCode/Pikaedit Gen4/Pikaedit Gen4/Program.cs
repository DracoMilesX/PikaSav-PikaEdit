using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Pikaedit_Gen4
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Embeded language dll's
            //File.WriteAllBytes("PikaeditLib.dll", Properties.Resources.PikaeditLib);
            if (Directory.Exists(Application.StartupPath + "\\x86"))
            {
                if (!File.Exists(Application.StartupPath + "\\x86\\SQLite.Interop.dll"))
                {
                    File.WriteAllBytes(Application.StartupPath + "\\x86\\SQLite.Interop.dll", Properties.Resources.x86_SQLite_Interop);
                }
            }
            else
            {
                Directory.CreateDirectory(Application.StartupPath + "\\x86");
                File.WriteAllBytes(Application.StartupPath + "\\x86\\SQLite.Interop.dll", Properties.Resources.x86_SQLite_Interop);
            }
            if (Directory.Exists(Application.StartupPath + "\\x64"))
            {
                if (!File.Exists(Application.StartupPath + "\\x64\\SQLite.Interop.dll"))
                {
                    File.WriteAllBytes(Application.StartupPath + "\\x64\\SQLite.Interop.dll", Properties.Resources.x64_SQLite_Interop);
                }
            }
            else
            {
                Directory.CreateDirectory(Application.StartupPath + "\\x64");
                File.WriteAllBytes(Application.StartupPath + "\\x64\\SQLite.Interop.dll", Properties.Resources.x64_SQLite_Interop);
            }
            if (File.Exists("PikaeditLib.dll"))
            {
                File.Delete("PikaeditLib.dll");
            }
            Application.Run(new Form1());
        }
    }
}
