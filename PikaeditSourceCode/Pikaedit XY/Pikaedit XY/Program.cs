using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Pikaedit_XY
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
            ////Embeded language dll's
            ////File.WriteAllBytes("PikaeditLib.dll", Properties.Resources.PikaeditLib);
            if (Directory.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "x86"))
            {
                if (!File.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "x86" + Path.DirectorySeparatorChar + "SQLite.Interop.dll"))
                {
                    File.WriteAllBytes(Application.StartupPath + Path.DirectorySeparatorChar + "x86" + Path.DirectorySeparatorChar + "SQLite.Interop.dll", Properties.Resources.x86_SQLite_Interop);
                }
            }
            else
            {
                Directory.CreateDirectory(Application.StartupPath + Path.DirectorySeparatorChar + "x86");
                File.WriteAllBytes(Application.StartupPath + Path.DirectorySeparatorChar + "x86" + Path.DirectorySeparatorChar + "SQLite.Interop.dll", Properties.Resources.x86_SQLite_Interop);
            }
            if (Directory.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "x64"))
            {
                if (!File.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "x64" + Path.DirectorySeparatorChar + "SQLite.Interop.dll"))
                {
                    File.WriteAllBytes(Application.StartupPath + Path.DirectorySeparatorChar + "x64" + Path.DirectorySeparatorChar + "SQLite.Interop.dll", Properties.Resources.x64_SQLite_Interop);
                }
            }
            else
            {
                Directory.CreateDirectory(Application.StartupPath + Path.DirectorySeparatorChar + "x64");
                File.WriteAllBytes(Application.StartupPath + Path.DirectorySeparatorChar + "x64" + Path.DirectorySeparatorChar + "SQLite.Interop.dll", Properties.Resources.x64_SQLite_Interop);
            }
            if (File.Exists("PikaeditLib.dll"))
            {
                File.Delete("PikaeditLib.dll");
            }
            Application.Run(new Form1());
        }
    }
}
