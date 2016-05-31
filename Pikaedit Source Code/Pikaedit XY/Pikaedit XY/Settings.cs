using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Pikaedit_XY
{
    public static class Settings
    {
        #region editor
        /// <summary>
        /// The legal mode pkm editor is enabled or not
        /// </summary>
        public static bool legalMode;
        public static bool injectionP;
        #endregion

        public static void Initialize()
        {
            legalMode = true;
            injectionP = false;
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
                if (br.PeekChar() > 0)
                {
                    injectionP = br.ReadBoolean();
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
            bw.Write(injectionP);
            fs.Close();
            bw.Close();
        }
    }
}
