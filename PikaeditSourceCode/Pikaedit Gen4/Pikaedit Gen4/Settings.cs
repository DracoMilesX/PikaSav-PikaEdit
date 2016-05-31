using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Pikaedit_Gen4
{
    public static class Settings
    {
        # region gts

        /// <summary>
        /// The GTS System is initialized?
        /// </summary>
        public static bool gtsStarted;
        /// <summary>
        /// Path to receive pkm from the GTS System
        /// </summary>
        public static string gtsFolderR;
        #endregion
        #region editor
        /// <summary>
        /// The legal mode pkm editor is enabled or not
        /// </summary>
        public static bool legalMode;
        #endregion

        public static void Initialize()
        {
            gtsStarted = false;
            gtsFolderR = "";
            legalMode = true;
        }

        public static void load()
        {
            if (File.Exists("settings.bin"))
            {
                FileStream fs = new FileStream("settings.bin", FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                PkmLib.lang = br.ReadByte();
                if (br.PeekChar() > 0)
                {
                    legalMode = br.ReadBoolean();
                }
                fs.Close();
                br.Close();
                if (PkmLib.lang > 11)
                {
                    PkmLib.lang = Convert.ToByte(char.ConvertFromUtf32(PkmLib.lang));
                }
            }
            else
            {
                PkmLib.lang = 0;
                legalMode = true;
            }
        }

        public static void save()
        {
            if (File.Exists("settings.bin"))
            {
                File.Delete("settings.bin");
            }
            FileStream fs = new FileStream("settings.bin", FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(PkmLib.lang);
            bw.Write(legalMode);
            fs.Close();
            bw.Close();
        }
    }
}
