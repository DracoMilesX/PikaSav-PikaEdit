using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Pikaedit
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
            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            Application.Run(new Form1());
        }

        //static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        //{
        //    try
        //    {
        //        using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(".dll"))
        //        {
        //            byte[] assembyData = new byte[stream.Length];
        //            stream.Read(assembyData, 0, assembyData.Length);
        //            return Assembly.Load(assembyData);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
    }


}
