using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Pikaedit_XY
{
    public class SaveFile
    {
        public string filename;
        public byte[] data;
        public Pokemon[] party = new Pokemon[6];
        public Box[] boxes = new Box[31];
        public Version version; //0 XY

        private FileStream fs;
        private BinaryReader br;
        private BinaryWriter bw;

        public enum Version
        {
            XY
        }

        public ushort GetCheckSum(byte[] data)
        {
            int sum = 0xFFFF;

            for (int i = 0; i < data.Length; i++)
                sum = (sum << 8) ^ Func.SeedTable[(byte)(data[i] ^ (byte)(sum >> 8))];

            return (ushort)sum;
        }

        public SaveFile()
        {
            this.filename = "";
            this.data = null;
            for (int i = 0; i < 24; i++)
            {
                boxes[i] = new Box();
            }
            for (int i = 0; i < 6; i++)
            {
                party[i] = new Pokemon();
            }
            version = 0;
        }

        public SaveFile(string filename)
        {
            this.filename = filename;
            this.data = File.ReadAllBytes(filename);
            Initialize();
        }

        private void Initialize()
        {
            fs = new FileStream(filename, FileMode.Open);
            br = new BinaryReader(fs);
            determineVersion();
            extractParty();
            for (int i = 0; i < 24; i++)
            {
                fs.Seek(0x400 + (0x1000 * i), SeekOrigin.Begin);
                boxes[i] = new Box();
                for (int j = 0; j < 30; j++)
                {
                    boxes[i].pkmdata[j] = new Pokemon(br.ReadBytes(232));
                }
            }

            for (int i = 0; i < 24; i++)
            {
                fs.Seek(0x4 + (i * 40), SeekOrigin.Begin);
                boxes[i].name = Func.getString(br.ReadBytes(18), 0, 9);
            }
            fs.Seek(0x3C4, SeekOrigin.Begin);
            for (int i = 0; i < 24; i++)
            {
                boxes[i].wallpaper = br.ReadByte();
            }


            //Close file streams
            br.Close();
            fs.Close();
        }
        private void determineVersion()
        {
            version = Version.XY;
            //byte[] checksum = Func.subArray(data, 0x25FA2, 2);
            //byte[] block = Func.subArray(data, 0x25F00, 0x94);
            //if (Func.getUInt16(checksum) == GetCheckSum(block))
            //{
            //    version = Version.BW2;
            //}
            //else
            //{
            //    version = Version.BW;
            //}
        }

        private void extractParty()
        {
            fs.Seek(0x18E08, SeekOrigin.Begin);
            for (int i = 0; i < 6; i++)
            {
                this.party[i] = new Pokemon(br.ReadBytes(260));
            }
        }

        public void save(string filename)
        {
            Dictionary<string, ushort> checksums = new Dictionary<string, ushort>();
            File.WriteAllBytes(filename, data);
            fs = new FileStream(filename, FileMode.Open);
            bw = new BinaryWriter(fs);
            br = new BinaryReader(fs);
            for (int i = 0; i < 24; i++)
            {
                fs.Seek(0x400 + (0x1000 * i), SeekOrigin.Begin);
                for (int j = 0; j < 30; j++)
                {
                    bw.Write(boxes[i].pkmdata[j].getEncrypted(false)); ;
                }
            }
            for (int i = 0; i < 24; i++)
            {
                fs.Seek(0x400 + (0x1000 * i), SeekOrigin.Begin);
                checksums.Add("box" + i, GetCheckSum(br.ReadBytes(0xFF0)));
                fs.Seek(0x400 + (0x1000 * i) + 0xFF2, SeekOrigin.Begin);
                bw.Write(checksums["box" + i]);
            }
            fs.Seek(0x18E08, SeekOrigin.Begin);
            byte count = 0;
            for (int i = 0; i < 6; i++)
            {
                if (!party[i].isEmpty)
                {
                    count++;
                }
                bw.Write(party[i].getEncrypted());
            }
            fs.Seek(0x18E04, SeekOrigin.Begin);
            bw.Write(count);
            fs.Seek(0x18E00, SeekOrigin.Begin);
            checksums.Add("party", GetCheckSum(br.ReadBytes(0x534)));
            fs.Seek(0x19336, SeekOrigin.Begin);
            bw.Write(checksums["party"]);

            writeFinalChecksums(checksums);

            //Close streams
            br.Close();
            bw.Close();
            fs.Close();
        }

        private void writeFinalChecksums(Dictionary<string, ushort> checksums)
        {
            if (version == Version.XY)//BW2 Block 0x25F00
            {
                fs.Seek(0x25F02, SeekOrigin.Begin);
                for (int i = 0; i < 24; i++)
                {
                    bw.Write(checksums["box" + i]);
                }
                fs.Seek(0x25F34, SeekOrigin.Begin);
                bw.Write(checksums["party"]);
                fs.Seek(0x25F00, SeekOrigin.Begin);
                checksums.Add("final", GetCheckSum(br.ReadBytes(0x94)));
                fs.Seek(0x25FA2, SeekOrigin.Begin);
                bw.Write(checksums["final"]);
            }
            else //BW Block 0x23F00
            {
                fs.Seek(0x23F02, SeekOrigin.Begin);
                for (int i = 0; i < 24; i++)
                {
                    bw.Write(checksums["box" + i]);
                }
                fs.Seek(0x23F34, SeekOrigin.Begin);
                bw.Write(checksums["party"]);
                fs.Seek(0x23F00, SeekOrigin.Begin);
                checksums.Add("final", GetCheckSum(br.ReadBytes(0x8C)));
                fs.Seek(0x23F9A, SeekOrigin.Begin);
                bw.Write(checksums["final"]);
            }
        }

        #region editorExtras

        public string[] getBoxesNames()
        {
            string[] s = new string[24];
            for (int i = 0; i < 24; i++)
            {
                s[i] = boxes[i].name;
            }
            return s;
        }

        #endregion
    }
}
