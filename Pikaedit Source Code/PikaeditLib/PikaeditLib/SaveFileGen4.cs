using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Pikaedit_Lib
{
    /// <summary>
    /// Represents a Gen 4 save file
    /// </summary>
    public class SaveFileGen4
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
        /// Represents save file Version
        /// </summary>
        public Version version;
        /// <summary>
        /// Represents the General Block that will be loaded in the game
        /// </summary>
        public SelectedBlock generalBlock;
        /// <summary>
        /// Represents the Storage Block that will be loaded in the game
        /// </summary>
        public SelectedBlock storageBlock;
        /// <summary>
        /// Represents save file Party as a PokemonGen4[]
        /// </summary>
        public PokemonGen4[] party = new PokemonGen4[6];
        /// <summary>
        /// Represents save file Boxes
        /// </summary>
        public BoxGen4[] boxes = new BoxGen4[18];
        /// <summary>
        /// Represents save file Pal Parked Pokemon
        /// </summary>
        public PokemonGen4[] palpark = new PokemonGen4[6];
        /// <summary>
        /// Represents save file Day-Care data
        /// </summary>
        public DayCareGen4 daycare;
        /// <summary>
        /// Represents save file Trainer Info
        /// </summary>
        public TrainerInfoGen4 trainer;

        private FileStream fs;
        private BinaryReader br;
        private BinaryWriter bw;
        /// <summary>
        /// This is summed to the first block if using second block (either General or Storage)
        /// </summary>
        public readonly int offsetblock = 0x40000;

        public enum Version
        {
            DP,
            Pt,
            HGSS,
            Unknown=100
        }

        public enum SelectedBlock
        {
            Block1,
            Block2
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
        public SaveFileGen4()
        {
            this.filename = "";
            this.data = null;
            for (int i = 0; i < 18; i++)
            {
                boxes[i] = new BoxGen4();
            }
            for (int i = 0; i < 6; i++)
            {
                party[i] = new PokemonGen4();
            }
            version = Version.Unknown;
        }

        /// <summary>
        /// Initialize a Save file using an existing save file
        /// </summary>
        /// <param name="filename">File name of the save file</param>
        public SaveFileGen4(string filename)
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
        public SaveFileGen4(byte[] data)
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
            determineBlock();
            extractParty();
            extractBoxes();
            trainer = extractTrainerData();
            
            //for (int i = 0; i < 18; i++)
            //{
            //    fs.Seek(0x4 + (i * 40), SeekOrigin.Begin);
            //    boxes[i].name = Func.getString(br.ReadBytes(18), 0, 9);
            //}
            //fs.Seek(0x3C4, SeekOrigin.Begin);
            //for (int i = 0; i < 18; i++)
            //{
            //    boxes[i].wallpaper = br.ReadByte();
            //}

            daycare = extractDayCare();
            

            //Close file streams
            br.Close();
            fs.Close();
        }
        private void determineVersion()
        {
            byte[] checksum = Func.subArray(data, 0xC0FE, 2);
            byte[] block = Func.subArray(data, 0x0, 0xC0EC);
            if (Func.getUInt16(checksum) == GetCheckSum(block))
            {
                version = Version.DP;
            }
            else
            {
                checksum = Func.subArray(data, 0xCF2A, 2);
                block = Func.subArray(data, 0x0, 0xCF18);
                if (Func.getUInt16(checksum) == GetCheckSum(block))
                {
                    version = Version.Pt;
                }
                else
                {
                    checksum = Func.subArray(data, 0xF626, 2);
                    block = Func.subArray(data, 0x0, 0xF618);
                    if (Func.getUInt16(checksum) == GetCheckSum(block))
                    {
                        version = Version.HGSS;
                    }
                    else
                    {
                        version = Version.Unknown;
                    }
                }
            }
        }

        private void determineBlock()
        {
            ushort block1 = 0;
            ushort block2 = 0;
            switch (version)
            {
                case Version.DP:
                    block1 = Func.getUInt16(data, 0xC0F0);
                    block2 = Func.getUInt16(data, 0x4C0F0);
                    break;
                case Version.Pt:
                    block1 = Func.getUInt16(data, 0xCF1C);
                    block2 = Func.getUInt16(data, 0x4CF1C);
                    break;
                case Version.HGSS:
                    block1 = Func.getUInt16(data, 0xF626);
                    block2 = Func.getUInt16(data, 0x4F626);
                    break;
            }
            if (block1 >= block2)
            {
                generalBlock = SelectedBlock.Block1;
            }
            else
            {
                generalBlock = SelectedBlock.Block2;
            }
            switch (version)
            {
                case Version.DP:
                    block1 = Func.getUInt16(data, 0x1E2D0);
                    block2 = Func.getUInt16(data, 0x5E2D0);
                    break;
                case Version.Pt:
                    block1 = Func.getUInt16(data, 0x1F100);
                    block2 = Func.getUInt16(data, 0x5F100);
                    break;
                case Version.HGSS:
                    block1 = Func.getUInt16(data, 0x21A00);
                    block2 = Func.getUInt16(data, 0x61A00);
                    break;
            }
            if (block1 >= block2)
            {
                storageBlock = SelectedBlock.Block1;
            }
            else
            {
                storageBlock = SelectedBlock.Block2;
            }

        }

        private void extractParty()
        {
            int bl = 0;
            if (generalBlock == SelectedBlock.Block2)
            {
                bl = offsetblock;
            }
            switch (version)
            {
                case Version.DP:
                    fs.Seek(0x98 +bl, SeekOrigin.Begin);
                    break;
                case Version.Pt:
                    fs.Seek(0xA0 +bl, SeekOrigin.Begin);
                    break;
                case Version.HGSS:
                    fs.Seek(0x98 + bl, SeekOrigin.Begin);
                    break;
            }
            for (int i = 0; i < 6; i++)
            {
                this.party[i] = new PokemonGen4(br.ReadBytes(236));
            }
        }

        private void extractBoxes()
        {
            int bl = 0;
            if (storageBlock == SelectedBlock.Block2)
            {
                bl = offsetblock;
            }
            bool emptybox = false;
            switch (version)
            {
                case Version.DP:
                    fs.Seek(0xC100 + bl, SeekOrigin.Begin);
                    break;
                case Version.Pt:
                    fs.Seek(0xCF30 + bl, SeekOrigin.Begin);
                    break;
                case Version.HGSS:
                    fs.Seek(0xF700 + bl, SeekOrigin.Begin);
                    break;
            }
            if (br.ReadUInt32() == 0xFFFFFFFF)
            {
                emptybox = true;
            }
            switch (version)
            {
                case Version.DP:
                    fs.Seek(0xC104 + bl, SeekOrigin.Begin);
                    break;
                case Version.Pt:
                    fs.Seek(0xCF30 + bl, SeekOrigin.Begin);
                    break;
            }
            for (int i = 0; i < 18; i++)
            {
                switch (version)
                {
                    case Version.HGSS:
                        fs.Seek(0xF700 + bl + (i * 0x1000), SeekOrigin.Begin);
                        break;
                }
                boxes[i] = new BoxGen4();
                boxes[i].name = "BOX " + (i + 1);
                for (int j = 0; j < 30; j++)
                {
                    if (!emptybox)
                    {
                        boxes[i].pkmdata[j] = new PokemonGen4(br.ReadBytes(136));
                    }
                    else
                    {
                        boxes[i].pkmdata[j] = new PokemonGen4();
                    }
                }
            }
        }

        private TrainerInfoGen4 extractTrainerData()
        {
            string name="";
            ushort id=1;
            ushort sid=0;
            uint money=0;
            byte gender=0;
            byte badges=0;
            byte hgssbadges = 0;
            int bl = 0;
            if (generalBlock == SelectedBlock.Block2)
            {
                bl = offsetblock;
            }
            switch (version)
            {
                case Version.DP:
                    fs.Seek(0x64 + bl, SeekOrigin.Begin);
                    break;
                case Version.Pt:
                    fs.Seek(0x68 + bl, SeekOrigin.Begin);
                    break;
                case Version.HGSS:
                    fs.Seek(0x64 + bl, SeekOrigin.Begin);
                    break;
            }
            name = Func.getStringIngame(br.ReadBytes(16), 0, 8);
            id = br.ReadUInt16();
            sid = br.ReadUInt16();
            money = br.ReadUInt32();
            gender = br.ReadByte();
            switch (version)
            {
                case Version.Pt:
                    fs.Seek(0x82 + bl, SeekOrigin.Begin);
                    break;
                case Version.HGSS:
                    fs.Seek(0x7e + bl, SeekOrigin.Begin);
                    break;
            }
            badges = br.ReadByte();
            switch (version)
            {
                case Version.HGSS:
                    fs.Seek(0x83 + bl, SeekOrigin.Begin);
                    hgssbadges = br.ReadByte();
                    break;
            }
            return new TrainerInfoGen4(name, id, sid, money, gender, badges, hgssbadges);
        }

        private DayCareGen4 extractDayCare()
        {
            int bl = 0;
            if (generalBlock == SelectedBlock.Block2)
            {
                bl = offsetblock;
            }
            switch (version)
            {
                case Version.DP:
                    fs.Seek(0x141C + bl, SeekOrigin.Begin);
                    break;
                case Version.Pt:
                    fs.Seek(0x1654 + bl, SeekOrigin.Begin);
                    break;
                case Version.HGSS:
                    fs.Seek(0x15FC + bl, SeekOrigin.Begin);
                    break;
            }
            return new DayCareGen4(new PokemonGen4(br.ReadBytes(232)), new PokemonGen4(br.ReadBytes(232)), false);
        }

        private void extractPalPark()
        {
            int bl = 0;
            if (generalBlock == SelectedBlock.Block2)
            {
                bl = offsetblock;
            }
            switch (version)
            {
                case Version.DP:
                    fs.Seek(0xba28 + bl, SeekOrigin.Begin);
                    break;
                case Version.Pt:
                    fs.Seek(0x1654 + bl, SeekOrigin.Begin);
                    break;
                case Version.HGSS:
                    fs.Seek(0x15FC + bl, SeekOrigin.Begin);
                    break;
            }
            for (int i = 0; i < 6; i++)
            {
                this.palpark[i] = new PokemonGen4(br.ReadBytes(236));
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
            int bl = 0;

            //StorageBlock

            if (storageBlock == SelectedBlock.Block2)
            {
                bl = offsetblock;
            }
            bool usedBoxes = false;
            for (int i = 0; i < 18 && !usedBoxes; i++)
            {
                if (!boxes[i].isEmpty())
                {
                    usedBoxes = true;
                }
            }
            switch (version)
            {
                case Version.DP:
                    fs.Seek(0xC104 + bl, SeekOrigin.Begin);
                    break;
                case Version.Pt:
                    fs.Seek(0xCF30 + bl, SeekOrigin.Begin);
                    break;
            }
            for (int i = 0; i < 18; i++)
            {
                switch (version)
                {
                    case Version.HGSS:
                        fs.Seek(0xF700 + bl + (i * 0x1000), SeekOrigin.Begin);
                        break;
                }
                for (int j = 0; j < 30; j++)
                {
                    if (usedBoxes)
                    {
                        bw.Write(boxes[i].pkmdata[j].getEncrypted(false));
                    }
                    else
                    {
                        bw.Write(Func.fillarray(136,0xFF));
                    }
                }
            }
            switch (version)
            {
                case Version.DP:
                    fs.Seek(0xc100 + bl, SeekOrigin.Begin);
                    checksums["storage"] = GetCheckSum(br.ReadBytes(0x121cc));
                    fs.Seek(0x1e2de + bl, SeekOrigin.Begin);
                    bw.Write(checksums["storage"]);
                    break;
                case Version.Pt:
                    fs.Seek(0xcf2c + bl, SeekOrigin.Begin);
                    checksums["storage"] = GetCheckSum(br.ReadBytes(0x121d0));
                    fs.Seek(0x1f10e + bl, SeekOrigin.Begin);
                    bw.Write(checksums["storage"]);
                    break;
                case Version.HGSS:
                    fs.Seek(0xf700 + bl, SeekOrigin.Begin);
                    checksums["storage"] = GetCheckSum(br.ReadBytes(0x12300));
                    fs.Seek(0x21a0e + bl, SeekOrigin.Begin);
                    bw.Write(checksums["storage"]);
                    break;
            }

            //General Block

            bl = 0;
            if (generalBlock == SelectedBlock.Block2)
            {
                bl = offsetblock;
            }
            switch (version)
            {
                case Version.DP:
                    fs.Seek(0x98 + bl, SeekOrigin.Begin);
                    break;
                case Version.Pt:
                    fs.Seek(0xA0 + bl, SeekOrigin.Begin);
                    break;
                case Version.HGSS:
                    fs.Seek(0x98 + bl, SeekOrigin.Begin);
                    break;
            }
            byte count = 0;
            for (int i = 0; i < 6; i++)
            {
                if (!party[i].isEmpty)
                {
                    count++;
                }
                bw.Write(party[i].getEncrypted());
            }
            switch (version)
            {
                case Version.DP:
                    fs.Seek(0x94 + bl, SeekOrigin.Begin);
                    break;
                case Version.Pt:
                    fs.Seek(0x9C + bl, SeekOrigin.Begin);
                    break;
                case Version.HGSS:
                    fs.Seek(0x94 + bl, SeekOrigin.Begin);
                    break;
            }
            bw.Write(count);
            //Day Care
            switch (version)
            {
                case Version.DP:
                    fs.Seek(0x141C + bl, SeekOrigin.Begin);
                    break;
                case Version.Pt:
                    fs.Seek(0x1654 + bl, SeekOrigin.Begin);
                    break;
                case Version.HGSS:
                    fs.Seek(0x15FC + bl, SeekOrigin.Begin);
                    break;
            }
            for (int i = 0; i < 2; i++)
            {
                bw.Write(daycare.pkmdata[i].getEncrypted());
            }
            //Trainer info
            switch (version)
            {
                case Version.DP:
                    fs.Seek(0x64 + bl, SeekOrigin.Begin);
                    break;
                case Version.Pt:
                    fs.Seek(0x68 + bl, SeekOrigin.Begin);
                    break;
                case Version.HGSS:
                    fs.Seek(0x64 + bl, SeekOrigin.Begin);
                    break;
            }
            bw.Write(Func.getfromStringIngame(trainer.name, trainer.NAMEMAXLENGTH+1));
            bw.Write(trainer.id);
            bw.Write(trainer.sid);
            bw.Write(trainer.money);
            bw.Write(trainer.getGenderByte());
            switch (version)
            {
                case Version.Pt:
                    fs.Seek(0x82 + bl, SeekOrigin.Begin);
                    break;
                case Version.HGSS:
                    fs.Seek(0x7e + bl, SeekOrigin.Begin);
                    break;
            }
            bw.Write(trainer.badges);
            switch (version)
            {
                case Version.HGSS:
                    fs.Seek(0x83 + bl, SeekOrigin.Begin);
                    bw.Write(trainer.hgssbadges);
                    break;
            }

            fs.Seek(bl, SeekOrigin.Begin);
            switch (version)
            {
                case Version.DP:
                    checksums["general"] = GetCheckSum(br.ReadBytes(0xc0ec));
                    fs.Seek(0xc0fe + bl, SeekOrigin.Begin);
                    bw.Write(checksums["general"]);
                    break;
                case Version.Pt:
                    checksums["general"] = GetCheckSum(br.ReadBytes(0xcf18));
                    fs.Seek(0xcf2a + bl, SeekOrigin.Begin);
                    bw.Write(checksums["general"]);
                    break;
                case Version.HGSS:
                    checksums["general"] = GetCheckSum(br.ReadBytes(0xf618));
                    fs.Seek(0xf626 + bl, SeekOrigin.Begin);
                    bw.Write(checksums["general"]);
                    break;
            }

            //Close streams
            br.Close();
            bw.Close();
            fs.Close();
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

        public string[] getBoxesNames()
        {
            string[] s = new string[18];
            for (int i = 0; i < 18; i++)
            {
                s[i] = boxes[i].name;
            }
            return s;
        }

        #endregion
    }
}
