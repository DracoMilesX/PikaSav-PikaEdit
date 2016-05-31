using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Pikaedit_Lib
{
    /// <summary>
    /// Represents a Gen 5 save file
    /// </summary>
    public class SaveFile
    {
        /// <summary>
        /// Save file File to access using FileStream
        /// </summary>
        private string filename;
        /// <summary>
        /// Save file extension
        /// </summary>
        public FileExtension extension;

        /// <summary>
        /// Original save file data
        /// </summary>
        public byte[] data;

        /// <summary>
        /// Represents save file Party as a Pokemon[]
        /// </summary>
        public Pokemon[] party = new Pokemon[6];

        /// <summary>
        /// Represents save file Boxes
        /// </summary>
        public Box[] boxes = new Box[24];

        /// <summary>
        /// Represents save file Battle Box as a Pokemon[]
        /// </summary>
        public Pokemon[] battleBox = new Pokemon[6];

        /// <summary>
        /// Represents save file Day-Care data
        /// </summary>
        public DayCare daycare;

        /// <summary>
        /// Represents save file Trainer Info
        /// </summary>
        public TrainerInfo trainer;

        /// <summary>
        /// Represents BW2 Reshiram/Zekrom storage when fused with Kyurem
        /// </summary>
        public Pokemon fused;

        /// <summary>
        /// Represents save file Version
        /// </summary>
        public Version version;

        private uint wcSeed;

        /// <summary>
        /// Represents Wonder Cards stored in the save file
        /// </summary>
        public WonderCard[] wonderCards = new WonderCard[12];

        private FileStream fs;
        private BinaryReader br;
        private BinaryWriter bw;
        private uint seed;

        #region seedMethods
        /// <summary>
        /// Initialize seed
        /// </summary>
        private void srand(uint newSeed)
        {
            seed = newSeed;
        }

        /// <summary>
        /// Call the next RNG
        /// </summary>
        private UInt32 rand()
        {
            seed = ((0x41c64e6d * seed + 0x6073)) & 0xFFFFFFFF;
            return seed >> 16;
        }

        /// <summary>
        /// Call the previous RNG
        /// </summary>
        private UInt32 prand()
        {
            seed = (seed * 0xeeb9eb65 + 0xa3561a1) & 0xFFFFFFFF;
            return seed >> 16;
        }
        #endregion

        /// <summary>
        /// Save file version
        /// </summary>
        public enum Version
        {
            BW,
            BW2,
            Unknown = 100
        }

        /// <summary>
        /// Save file extension
        /// </summary>
        public enum FileExtension
        {
            SAV,
            DSV
        }

        /// <summary>
        /// Generate the ushort checksum of a byte[] block
        /// </summary>
        /// <param name="data">Block to get checksum as byte[]</param>
        /// <returns>Checksum generated from given byte[] block</returns>
        public ushort GetCheckSum(byte[] data)
        {
            int sum = 0xFFFF;

            for (int i = 0; i < data.Length; i++)
                sum = (sum << 8) ^ Func.SeedTable[(byte)(data[i] ^ (byte)(sum >> 8))];

            return (ushort)sum;
        }

        /// <summary>
        /// Initialize an empty save file (Do not use to create a save file)
        /// </summary>
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
            version = Version.Unknown;
            for (int i = 0; i < 12; i++)
            {
                wonderCards[i] = new WonderCard();
            }
        }

        /// <summary>
        /// Initialize a Save file using an existing save file
        /// </summary>
        /// <param name="filename">File name of the save file</param>
        public SaveFile(string filename)
        {
            this.filename = filename;
            this.data = File.ReadAllBytes(filename);
            if (Path.GetExtension(filename) == ".dsv")
            {
                this.extension = FileExtension.DSV;
            }
            else
            {
                this.extension = FileExtension.SAV;
            }
            Initialize();
        }

        /// <summary>
        /// Initialize a Save file using a byte[] representing a save file data
        /// </summary>
        /// <param name="data">A byte[] representing a save file data</param>
        public SaveFile(byte[] data)
        {
            this.data = data;
            File.WriteAllBytes("temp.sav", data);
            this.filename = "temp.sav";
            this.extension = FileExtension.SAV;
            Initialize();
            File.Delete("temp.sav");
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
                    boxes[i].pkmdata[j] = new Pokemon(br.ReadBytes(136));
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

            daycare = extractDayCare();
            trainer = extractTrainerData();
            //<TODO>
            if (version == Version.BW2)
            {
                //Seek offset
                fused = new Pokemon();
            }
            else
            {
                //Doesn't exist
                fused = new Pokemon();
            }
            //</TODO>

            extractWonderCards();

            //Close file streams
            br.Close();
            fs.Close();
        }
        private void determineVersion()
        {
            byte[] checksum = Func.subArray(data, 0x25FA2, 2);
            byte[] block = Func.subArray(data, 0x25F00, 0x94);
            if (Func.getUInt16(checksum) == GetCheckSum(block))
            {
                version = Version.BW2;
            }
            else
            {
                version = Version.BW;
            }
        }


        private void extractParty()
        {
            fs.Seek(0x18E08, SeekOrigin.Begin);
            for (int i = 0; i < 6; i++)
            {
                this.party[i] = new Pokemon(br.ReadBytes(220));
            }
        }

        private DayCare extractDayCare()
        {
            switch (version)
            {
                case Version.BW2:
                    fs.Seek(0x20D04, SeekOrigin.Begin);
                    break;
                case Version.BW:
                    fs.Seek(0x20E04, SeekOrigin.Begin);
                    break;
            }
            Pokemon p1 = new Pokemon(br.ReadBytes(220));
            switch (version)
            {
                case Version.BW2:
                    fs.Seek(0x20DE8, SeekOrigin.Begin);
                    break;
                case Version.BW:
                    fs.Seek(0x20EE8, SeekOrigin.Begin);
                    break;
            }
            Pokemon p2 = new Pokemon(br.ReadBytes(220));
            switch (version)
            {
                case Version.BW2:
                    fs.Seek(0x20EC8, SeekOrigin.Begin);
                    break;
                case Version.BW:
                    fs.Seek(0x20FC8, SeekOrigin.Begin);
                    break;
            }
            return new DayCare(p1, p2, br.ReadByte());
        }

        private TrainerInfo extractTrainerData()
        {
            string name = "";
            ushort id = 1;
            ushort sid = 0;
            uint money = 0;
            byte gender = 0;
            byte badges = 0;
            fs.Seek(0x19404, SeekOrigin.Begin);
            name = Func.getString(br.ReadBytes(16), 0, 8);
            id = br.ReadUInt16();
            sid = br.ReadUInt16();
            switch (version)
            {
                case Version.BW2:
                    fs.Seek(0x21100, SeekOrigin.Begin);
                    break;
                case Version.BW:
                    fs.Seek(0x21200, SeekOrigin.Begin);
                    break;
            }
            money = br.ReadUInt32();
            fs.Seek(0x19421, SeekOrigin.Begin);
            gender = br.ReadByte();
            switch (version)
            {
                case Version.BW2:
                    fs.Seek(0x21104, SeekOrigin.Begin);
                    break;
                case Version.BW:
                    fs.Seek(0x21204, SeekOrigin.Begin);
                    break;
            }
            badges = br.ReadByte();
            fs.Seek(0x19424, SeekOrigin.Begin);
            return new TrainerInfo(name, id, sid, money, gender, badges, br.ReadUInt16(), br.ReadByte(), br.ReadByte());
        }

        private void extractWonderCards()
        {
            fs.Seek(0x1d290, SeekOrigin.Begin);
            wcSeed = br.ReadUInt32();
            fs.Seek(0x1c800, SeekOrigin.Begin);
            srand(wcSeed);
            ushort[] wcmap = new ushort[0x548];
            for (int i = 0; i < 0x548; i++)
            {
                wcmap[i] = (ushort)(br.ReadUInt16() ^ rand());
            }
            byte[] c = Func.convertToByteArray(Func.ushortSubArray(wcmap, 0x80, 0x548 - 0x80));
            for (int i = 0; i < 12; i++)
            {
                wonderCards[i] = new WonderCard(Func.subArray(c, (i * 204), 204));
            }
        }

        /// <summary>
        /// Saves the edited save file
        /// </summary>
        /// <param name="filename">filename for the save file to save</param>
        /// <param name="type">File extension for the saved file</param>
        public void save(string filename, FileExtension type = FileExtension.SAV)
        {
            Dictionary<string, ushort> checksums = new Dictionary<string, ushort>();
            if (extension == FileExtension.SAV)
            {
                if (type == FileExtension.SAV)
                {
                    File.WriteAllBytes(filename, data);
                }
                else
                {
                    File.WriteAllBytes(filename, Func.mergeArray(data, PkmLib.dsvfooter));
                }
            }
            else
            {
                if (type == FileExtension.SAV)
                {
                    File.WriteAllBytes(filename, Func.subArray(data, 0, 0x80000));
                }
                else
                {
                    File.WriteAllBytes(filename, data);
                }
            }
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

            switch (version)
            {
                case Version.BW2:
                    fs.Seek(0x20D00, SeekOrigin.Begin);
                    break;
                case Version.BW:
                    fs.Seek(0x20E00, SeekOrigin.Begin);
                    break;
            }
            bw.Write((!daycare.pkmdata[0].isEmpty ? (byte)1 : (byte)0));
            switch (version)
            {
                case Version.BW2:
                    fs.Seek(0x20D04, SeekOrigin.Begin);
                    break;
                case Version.BW:
                    fs.Seek(0x20E04, SeekOrigin.Begin);
                    break;
            }
            bw.Write(daycare.pkmdata[0].getEncrypted());

            switch (version)
            {
                case Version.BW2:
                    fs.Seek(0x20DE4, SeekOrigin.Begin);
                    break;
                case Version.BW:
                    fs.Seek(0x20EE4, SeekOrigin.Begin);
                    break;
            }
            bw.Write((!daycare.pkmdata[1].isEmpty ? (byte)1 : (byte)0));
            switch (version)
            {
                case Version.BW2:
                    fs.Seek(0x20DE8, SeekOrigin.Begin);
                    break;
                case Version.BW:
                    fs.Seek(0x20EE8, SeekOrigin.Begin);
                    break;
            }
            bw.Write(daycare.pkmdata[1].getEncrypted());
            switch (version)
            {
                case Version.BW2:
                    fs.Seek(0x20EC8, SeekOrigin.Begin);
                    break;
                case Version.BW:
                    fs.Seek(0x20FC8, SeekOrigin.Begin);
                    break;
            }
            bw.Write(daycare.getEggByte());
            switch (version)
            {
                case Version.BW2:
                    fs.Seek(0x20D00, SeekOrigin.Begin);
                    checksums.Add("daycare", GetCheckSum(br.ReadBytes(0x1d4)));
                    fs.Seek(0x20ED6, SeekOrigin.Begin);
                    bw.Write(checksums["daycare"]);
                    break;
                case Version.BW:
                    fs.Seek(0x20E00, SeekOrigin.Begin);
                    checksums.Add("daycare", GetCheckSum(br.ReadBytes(0x1cc)));
                    fs.Seek(0x20FCE, SeekOrigin.Begin);
                    bw.Write(checksums["daycare"]);
                    break;
            }

            fs.Seek(0x19404, SeekOrigin.Begin);
            byte[] n = Func.getfromString(trainer.name, trainer.NAMEMAXLENGTH + 1);
            //n[n.Length - 1] = 0xFF;
            //n[n.Length - 2] = 0xFF;
            bw.Write(n);
            bw.Write(trainer.id);
            bw.Write(trainer.sid);
            switch (version)
            {
                case Version.BW2:
                    fs.Seek(0x21100, SeekOrigin.Begin);
                    break;
                case Version.BW:
                    fs.Seek(0x21200, SeekOrigin.Begin);
                    break;
            }
            bw.Write(trainer.money);
            fs.Seek(0x19421, SeekOrigin.Begin);
            bw.Write(trainer.getGenderByte());
            switch (version)
            {
                case Version.BW2:
                    fs.Seek(0x21104, SeekOrigin.Begin);
                    break;
                case Version.BW:
                    fs.Seek(0x21204, SeekOrigin.Begin);
                    break;
            }
            bw.Write(trainer.badges);
            fs.Seek(0x19424, SeekOrigin.Begin);
            bw.Write(trainer.playHours);
            bw.Write(trainer.playMin);
            bw.Write(trainer.playSec);

            fs.Seek(0x1C100, SeekOrigin.Begin);
            checksums.Add("card", GetCheckSum(br.ReadBytes(0x658)));
            fs.Seek(0x1C75A, SeekOrigin.Begin);
            bw.Write(checksums["card"]);

            fs.Seek(0x19400, SeekOrigin.Begin);
            switch (version)
            {
                case Version.BW2:
                    checksums.Add("trainer", GetCheckSum(br.ReadBytes(0xB0)));
                    fs.Seek(0x194B2, SeekOrigin.Begin);
                    bw.Write(checksums["trainer"]);
                    break;
                case Version.BW:
                    checksums.Add("trainer", GetCheckSum(br.ReadBytes(0x68)));
                    fs.Seek(0x1946A, SeekOrigin.Begin);
                    bw.Write(checksums["trainer"]);
                    break;
            }

            switch (version)
            {
                case Version.BW2:
                    fs.Seek(0x21100, SeekOrigin.Begin);
                    checksums.Add("money", GetCheckSum(br.ReadBytes(0xF0)));
                    fs.Seek(0x211F2, SeekOrigin.Begin);
                    bw.Write(checksums["money"]);
                    break;
                case Version.BW:
                    fs.Seek(0x21200, SeekOrigin.Begin);
                    checksums.Add("money", GetCheckSum(br.ReadBytes(0xEC)));
                    fs.Seek(0x212EE, SeekOrigin.Begin);
                    bw.Write(checksums["money"]);
                    break;
            }

            fs.Seek(0x1d290, SeekOrigin.Begin);
            bw.Write(wcSeed);
            fs.Seek(0x1c800, SeekOrigin.Begin);
            srand(wcSeed);
            ushort[] wcmap = new ushort[0x548];
            for (int i = 0; i < 0x548; i++)
            {
                wcmap[i] = (ushort)(br.ReadUInt16() ^ rand());
            }
            fs.Seek(0x1c900, SeekOrigin.Begin);
            for (int i = 0; i < 12; i++)
            {
                bw.Write(wonderCards[i].data);
            }
            fs.Seek(0x1c800, SeekOrigin.Begin);
            for (int i = 0; i < 0x548; i++)
            {
                wcmap[i] = br.ReadUInt16();
            }
            srand(wcSeed);
            fs.Seek(0x1c800, SeekOrigin.Begin);
            for (int i = 0; i < 0x548; i++)
            {
                bw.Write((ushort)(wcmap[i] ^ rand()));
            }
            fs.Seek(0x1c800, SeekOrigin.Begin);
            checksums.Add("mysterygift", GetCheckSum(br.ReadBytes(0xa94)));
            fs.Seek(0x1d296, SeekOrigin.Begin);
            bw.Write(checksums["mysterygift"]);


            writeFinalChecksums(checksums);

            //Close streams
            br.Close();
            bw.Close();
            fs.Close();
        }

        private void writeFinalChecksums(Dictionary<string, ushort> checksums)
        {
            if (version == Version.BW2)//BW2 Block 0x25F00
            {
                fs.Seek(0x25F02, SeekOrigin.Begin);
                for (int i = 0; i < 24; i++)
                {
                    bw.Write(checksums["box" + i]);
                }
                fs.Seek(0x25F34, SeekOrigin.Begin);
                bw.Write(checksums["party"]);
                bw.Write(checksums["trainer"]);
                fs.Seek(0x25F42, SeekOrigin.Begin);
                bw.Write(checksums["card"]);
                bw.Write(checksums["mysterygift"]);
                fs.Seek(0x25F64, SeekOrigin.Begin);
                bw.Write(checksums["daycare"]);
                fs.Seek(0x25F68, SeekOrigin.Begin);
                bw.Write(checksums["money"]);
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
                bw.Write(checksums["trainer"]);
                fs.Seek(0x23F42, SeekOrigin.Begin);
                bw.Write(checksums["card"]);
                bw.Write(checksums["mysterygift"]);
                fs.Seek(0x23F64, SeekOrigin.Begin);
                bw.Write(checksums["daycare"]);
                fs.Seek(0x23F68, SeekOrigin.Begin);
                bw.Write(checksums["money"]);
                fs.Seek(0x23F00, SeekOrigin.Begin);
                checksums.Add("final", GetCheckSum(br.ReadBytes(0x8C)));
                fs.Seek(0x23F9A, SeekOrigin.Begin);
                bw.Write(checksums["final"]);
            }
        }

        /// <summary>
        /// Saves the save file and gets the byte[] of the new save file
        /// </summary>
        /// <returns>byte[] of the saved save file</returns>
        public byte[] getSavedData()
        {
            save("temp.sav", 0);
            byte[] n = File.ReadAllBytes("temp.sav");
            File.Delete("temp.sav");
            return n;
        }

        #region editorExtras

        /// <summary>
        /// Return a string[] containing all boxes names
        /// </summary>
        /// <returns>string[] with all save file boxes names</returns>
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
