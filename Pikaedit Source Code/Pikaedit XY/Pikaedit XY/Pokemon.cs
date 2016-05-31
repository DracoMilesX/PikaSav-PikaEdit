using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit_XY
{
    /// <summary>
    /// Represents a XY Pokemon data as an object (All string data is in English)
    /// </summary>
    public class Pokemon
    {
        public enum AbilityIndex
        {
            Ability_0 = 1,
            Ability_1 = 2,
            HA_Ability = 4
        }
        /// <summary>
        /// byte[] representing a decrypted pokemon from a save file
        /// </summary>
        public byte[] data = new byte[260];
        /// <summary>
        /// Pokemon data is Empty flag
        /// </summary>
        public bool isEmpty = true;
        public bool isShiny = false;
        /// <summary>
        /// Pokemon data has a party block
        /// </summary>
        public bool partypkm = false;
        public uint pid;
        public ushort checksum;
        public string nick;
        /// <summary>
        /// Nickname with trash bytes
        /// </summary>
        public string nicktb;
        /// <summary>
        /// OT with trash bytes
        /// </summary>
        public string ottb;
        /// <summary>
        /// Pokemon pokedex number
        /// </summary>
        public ushort no;
        public string species;
        public string ability;
        public ushort id;
        public ushort sid;
        public byte markings;
        /// <summary>
        /// Represents the Ability the Pokemon has
        /// </summary>
        public AbilityIndex abilityIndex;
        public string OTtraded;
        public byte countryID;
        public byte regionID;
        public byte region3dsID;
        public MoveSet moveset;
        public MoveSet eggmoves;
        public string item;
        public string nature;
        public byte happiness;
        public string language;
        public string version;
        public byte level = 1;
        public uint exp;
        public string form;
        //public bool isEgg;
        //public bool isNick;
        public bool isFateful;
        public RibbonSet ribbon1;
        public RibbonSet ribbon2;
        public RibbonSet ribbon3;
        public ushort hp = 0;
        public ushort maxhp = 0;
        public ushort attack = 0;
        public ushort defense = 0;
        public ushort spatk = 0;
        public ushort spdef = 0;
        public ushort speed = 0;
        public byte hpev = 0;
        public byte atev = 0;
        public byte dfev = 0;
        public byte saev = 0;
        public byte sdev = 0;
        public byte spev = 0;
        public IVSet iv;
        //public byte hpiv = 0;
        //public byte ativ = 0;
        //public byte dfiv = 0;
        //public byte saiv = 0;
        //public byte sdiv = 0;
        //public byte spiv = 0;
        public string dateegg;
        public string datemet;
        public byte levelmet;
        public string locationmet;
        public string eggloc;
        public string status = "None";
        public byte female;
        public byte genderless;
        public string ot;
        public bool isHatched;
        public byte pokerus;
        public string pokeball;
        public byte genderot;
        //public byte encounter;
        //Amie
        public byte amieAffection;
        public byte amieFullness;
        public byte amieEnjoyment;

        public uint encryptionKey;
        public SuperTraining supertraining;

        //Public Properties for Pkm editor
        /// <summary>
        /// Hidden Power type
        /// </summary>
        public string hpType = "Fighting";
        public PkmLib.GenderRatios genderRatio;
        /// <summary>
        /// Gen the pokemon came from (based from version)
        /// </summary>
        public int gen;
        public ushort shinyvalue;
        public ushort trainershiny;

        public ushort PokemonXYshiny;
        //Private stuff
        private uint seed;
        private byte baseHP = 1;
        private byte baseAtk = 1;
        private byte baseDef = 1;
        private byte baseSpAtk = 1;
        private byte baseSpDef = 1;
        private byte baseSpd = 1;

        private string[] forms = new string[] { "None" };
        /// <summary>
        /// Contains the pokemon XY sprite
        /// </summary>
        public System.Drawing.Image sprite;

        /// <summary>
        /// Initialize seed
        /// </summary>
        private void srand(UInt32 newSeed)
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

        private void fillForms()
        {
            forms = PkmLib.getFormList(species);
        }
        /// <summary>
        /// Calculate Base Stats depending on species and form
        /// </summary>
        private void baseStats()
        {
            baseHP = PkmLib.baseStats[no][0];
            baseAtk = PkmLib.baseStats[no][1];
            baseDef = PkmLib.baseStats[no][2];
            baseSpAtk = PkmLib.baseStats[no][3];
            baseSpDef = PkmLib.baseStats[no][4];
            baseSpd = PkmLib.baseStats[no][5];
            if (PkmLib.getFormValue(species, form) != 0)
            {
                if (form == "Attack")
                {
                    baseHP = 50;
                    baseAtk = 180;
                    baseDef = 20;
                    baseSpAtk = 180;
                    baseSpDef = 20;
                    baseSpd = 150;
                }
                if (form == "Defense")
                {
                    baseHP = 50;
                    baseAtk = 70;
                    baseDef = 160;
                    baseSpAtk = 70;
                    baseSpDef = 160;
                    baseSpd = 90;
                }
                if (form == "Speed")
                {
                    baseHP = 50;
                    baseAtk = 95;
                    baseDef = 90;
                    baseSpAtk = 95;
                    baseSpDef = 90;
                    baseSpd = 180;
                }
                if (form == "Sandy" & species == "Wormadam")
                {
                    baseHP = 60;
                    baseAtk = 79;
                    baseDef = 105;
                    baseSpAtk = 59;
                    baseSpDef = 85;
                    baseSpd = 36;
                }
                if (form == "Trash" & species == "Wormadam")
                {
                    baseHP = 60;
                    baseAtk = 69;
                    baseDef = 95;
                    baseSpAtk = 69;
                    baseSpDef = 95;
                    baseSpd = 36;
                }
                if (species == "Rotom")
                {
                    baseHP = 50;
                    baseAtk = 65;
                    baseDef = 107;
                    baseSpAtk = 105;
                    baseSpDef = 107;
                    baseSpd = 86;
                }
                if (form == "Origin")
                {
                    baseHP = 150;
                    baseAtk = 120;
                    baseDef = 100;
                    baseSpAtk = 120;
                    baseSpDef = 100;
                    baseSpd = 90;
                }
                if (form == "Sky")
                {
                    baseHP = 100;
                    baseAtk = 103;
                    baseDef = 75;
                    baseSpAtk = 120;
                    baseSpDef = 75;
                    baseSpd = 127;
                }
                if (form == "Therian" & species == "Tornadus")
                {
                    baseHP = 79;
                    baseAtk = 100;
                    baseDef = 80;
                    baseSpAtk = 110;
                    baseSpDef = 90;
                    baseSpd = 121;
                }
                if (form == "Therian" & species == "Thundurus")
                {
                    baseHP = 79;
                    baseAtk = 105;
                    baseDef = 70;
                    baseSpAtk = 145;
                    baseSpDef = 80;
                    baseSpd = 101;
                }
                if (form == "Therian" & species == "Landorus")
                {
                    baseHP = 89;
                    baseAtk = 145;
                    baseDef = 90;
                    baseSpAtk = 105;
                    baseSpDef = 80;
                    baseSpd = 91;
                }
                if (form == "Black")
                {
                    baseHP = 125;
                    baseAtk = 170;
                    baseDef = 100;
                    baseSpAtk = 120;
                    baseSpDef = 90;
                    baseSpd = 95;
                }
                if (form == "White")
                {
                    baseHP = 125;
                    baseAtk = 120;
                    baseDef = 90;
                    baseSpAtk = 170;
                    baseSpDef = 100;
                    baseSpd = 95;
                }
                if (form == "Pirouette")
                {
                    baseHP = 100;
                    baseAtk = 128;
                    baseDef = 90;
                    baseSpAtk = 77;
                    baseSpDef = 77;
                    baseSpd = 128;
                }
                if (form.Equals("Mega") || form.Equals("Mega X") || form.Equals("Mega Y"))
                {
                    if (form.Equals("Mega X"))
                    {
                        baseHP = PkmLib.megaBaseStats[species + " X"][0];
                        baseAtk = PkmLib.megaBaseStats[species + " X"][1];
                        baseDef = PkmLib.megaBaseStats[species + " X"][2];
                        baseSpAtk = PkmLib.megaBaseStats[species + " X"][3];
                        baseSpDef = PkmLib.megaBaseStats[species + " X"][4];
                        baseSpd = PkmLib.megaBaseStats[species + " X"][5];
                    }
                    else
                    {
                        if (form.Equals("Mega Y"))
                        {
                            baseHP = PkmLib.megaBaseStats[species + " Y"][0];
                            baseAtk = PkmLib.megaBaseStats[species + " Y"][1];
                            baseDef = PkmLib.megaBaseStats[species + " Y"][2];
                            baseSpAtk = PkmLib.megaBaseStats[species + " Y"][3];
                            baseSpDef = PkmLib.megaBaseStats[species + " Y"][4];
                            baseSpd = PkmLib.megaBaseStats[species + " Y"][5];
                        }
                        else
                        {
                            baseHP = PkmLib.megaBaseStats[species][0];
                            baseAtk = PkmLib.megaBaseStats[species][1];
                            baseDef = PkmLib.megaBaseStats[species][2];
                            baseSpAtk = PkmLib.megaBaseStats[species][3];
                            baseSpDef = PkmLib.megaBaseStats[species][4];
                            baseSpd = PkmLib.megaBaseStats[species][5];
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Delete pkm data and make the array have zeros
        /// </summary>
        public void clear()
        {
            int z = 0;
            while (z < data.Length)
            {
                data[z] = 0;
                z += 1;
            }
            species = "None";
            isEmpty = true;
        }

        /// <summary>
        /// Calculate Hidden Power power and type
        /// </summary>
        public void hiddenPower()
        {
            int hp2 = iv.hp;
            int at = iv.atk;
            int de = iv.def;
            int sp = iv.spe;
            int sa = iv.spa;
            int sd = iv.spd;
            if (iv.hp % 2 == 0)
            {
                hp2 = 0;
            }
            else
            {
                hp2 = 1;
            }
            if (iv.atk % 2 == 0)
            {
                at = 0;
            }
            else
            {
                at = 1;
            }
            if (iv.def % 2 == 0)
            {
                de = 0;
            }
            else
            {
                de = 1;
            }
            if (iv.spe % 2 == 0)
            {
                sp = 0;
            }
            else
            {
                sp = 1;
            }
            if (iv.spa % 2 == 0)
            {
                sa = 0;
            }
            else
            {
                sa = 1;
            }
            if (iv.spd % 2 == 0)
            {
                sd = 0;
            }
            else
            {
                sd = 1;
            }

            //Calculate Type
            at = at * 2;
            de = de * 4;
            sp = sp * 8;
            sa = sa * 16;
            sd = sd * 32;
            double temp = hp2 + at + de + sp + sa + sd;
            int type = 0;
            temp = temp * 15;
            temp = temp / 63;
            type = (int)Math.Floor(temp);
            string type2 = "";
            if (type == 0)
            {
                type2 = "Fighting";
            }
            if (type == 1)
            {
                type2 = "Flying";
            }
            if (type == 2)
            {
                type2 = "Poison";
            }
            if (type == 3)
            {
                type2 = "Ground";
            }
            if (type == 4)
            {
                type2 = "Rock";
            }
            if (type == 5)
            {
                type2 = "Bug";
            }
            if (type == 6)
            {
                type2 = "Ghost";
            }
            if (type == 7)
            {
                type2 = "Steel";
            }
            if (type == 8)
            {
                type2 = "Fire";
            }
            if (type == 9)
            {
                type2 = "Water";
            }
            if (type == 10)
            {
                type2 = "Grass";
            }
            if (type == 11)
            {
                type2 = "Electric";
            }
            if (type == 12)
            {
                type2 = "Psychic";
            }
            if (type == 13)
            {
                type2 = "Ice";
            }
            if (type == 14)
            {
                type2 = "Dragon";
            }
            if (type == 15)
            {
                type2 = "Dark";
            }
            hpType = type2;
        }

        public void checksumUpdate()
        {
            int c = 0;
            for (int i = 8; i < 232; i += 2)
            {
                c += BitConverter.ToUInt16(data, i);
            }
            ushort checksum = (ushort)(c & 0xFFFF);
            data[6] = (byte)(checksum & 0xFF);
            data[7] = (byte)((checksum >> 8) & 0xFF);
        }

        /// <summary>
        /// Initialize variables using pkx data array
        /// </summary>
        private void Initialize()
        {
            int z = 0;
            encryptionKey = BitConverter.ToUInt32(data, 0x0);
            checksum = BitConverter.ToUInt16(data, 0x6);
            try
            {
                species = PkmLib.species[BitConverter.ToUInt16(data, 0x8)];
                no = BitConverter.ToUInt16(data, 0x8);
            }
            catch (Exception ex)
            {
                encryptionKey = 0;
                checksum = 0;
                z = 0;
                while (z < data.Length)
                {
                    data[z] = 0;
                    z += 1;
                }
                return;
            }
            if (no == 0)
            {
                clear();
                return;
            }
            item = PkmLib.items[BitConverter.ToUInt16(data, 0xa)];
            id = BitConverter.ToUInt16(data, 0xc);
            sid = BitConverter.ToUInt16(data, 0xe);
            exp = BitConverter.ToUInt32(data, 0x10);
            if (data[0x14] < PkmLib.abilities.Count)
            {
                ability = PkmLib.abilities[data[0x14]];
            }
            else
            {
                ability = Convert.ToString(data[0x14]);
            }
            abilityIndex = (AbilityIndex)data[0x15];
            //4 for DW
            pid = BitConverter.ToUInt32(data, 0x18);
            //markings = pkm(&H16)
            switch (data[0xe3])
            {
                case 1:
                    language = "Japan";
                    break;
                case 2:
                    language = "English";
                    break;
                case 3:
                    language = "French";
                    break;
                case 4:
                    language = "Italian";
                    break;
                case 5:
                    language = "German";
                    break;
                case 7:
                    language = "Spanish";
                    break;
                case 8:
                    language = "Korean";
                    break;
                default:
                    language = "English";
                    break;
            }
            hpev = data[0x1e];
            atev = data[0x1f];
            dfev = data[0x20];
            spev = data[0x21];
            saev = data[0x22];
            sdev = data[0x23];
            markings = data[0x2a];
            supertraining = new SuperTraining(BitConverter.ToUInt32(data, 0x2c));
            ribbon1 = new RibbonSet(BitConverter.ToUInt16(data, 0x30));
            ribbon2 = new RibbonSet(BitConverter.ToUInt16(data, 0x32));
            ribbon3 = new RibbonSet(BitConverter.ToUInt16(data, 0x34));
            moveset = new MoveSet(new Move(BitConverter.ToUInt16(data, 0x5a), data[0x62], data[0x66]), new Move(BitConverter.ToUInt16(data, 0x5c), data[0x63], data[0x67]),
                new Move(BitConverter.ToUInt16(data, 0x5e), data[0x64], data[0x68]), new Move(BitConverter.ToUInt16(data, 0x60), data[0x65], data[0x69]));
            eggmoves = new MoveSet(new Move(BitConverter.ToUInt16(data, 0x6a), 0, 0), new Move(BitConverter.ToUInt16(data, 0x6c), 0, 0),
                new Move(BitConverter.ToUInt16(data, 0x6e), 0, 0), new Move(BitConverter.ToUInt16(data, 0x70), 0, 0));
            iv = new IVSet(BitConverter.ToUInt32(data, 0x74));
            //ribbon3 = BitConverter.ToUInt16(data, &H3C)
            //ribbon4 = BitConverter.ToUInt16(data, &H3E)
            byte x40 = data[0x1d];
            isFateful = ((x40 >> (0)) & 0x1) == 1;
            female = (byte)((x40 >> (1)) & 0x1);
            genderless = (byte)((x40 >> (2)) & 0x1);
            int f = x40 & 248;
            form = "None";
            fillForms();
            form = forms[f / 8];
            nature = PkmLib.natures[data[0x1c]];
            //DW = pkm(66)
            switch (data[0xdf])
            {
                case 1:
                    version = "Ruby";
                    gen = 3;
                    break;
                case 2:
                    version = "Sapphire";
                    gen = 3;
                    break;
                case 3:
                    version = "Emerald";
                    gen = 3;
                    break;
                case 4:
                    version = "Fire Red";
                    gen = 3;
                    break;
                case 5:
                    version = "Leaf Green";
                    gen = 3;
                    break;
                case 7:
                    version = "Heart Gold";
                    gen = 4;
                    break;
                case 8:
                    version = "Soul Silver";
                    gen = 4;
                    break;
                case 10:
                    version = "Diamond";
                    gen = 4;
                    break;
                case 11:
                    version = "Pearl";
                    gen = 4;
                    break;
                case 12:
                    version = "Platinum";
                    gen = 4;
                    break;
                case 15:
                    version = "Colosseum/XD";
                    gen = 3;
                    break;
                case 20:
                    version = "White";
                    gen = 5;
                    break;
                case 21:
                    version = "Black";
                    gen = 5;
                    break;
                case 22:
                    version = "White 2";
                    gen = 5;
                    break;
                case 23:
                    version = "Black 2";
                    gen = 5;
                    break;
                case 24:
                    version = "X";
                    gen = 6;
                    break;
                case 25:
                    version = "Y";
                    gen = 6;
                    break;
            }
            //ribbon5 = BitConverter.ToUInt16(data, 96)
            //ribbon6 = BitConverter.ToUInt16(data, 98)
            int y = 0x40;
            ushort value = 0;
            string sequencet = "";
            string nickseq = "";
            bool seqte = false;
            //string testring = "";
            string tempnick = "";
            string nick2 = "";
            string n1 = "";
            string n2 = "";
            while (z < 13)
            {
                value = BitConverter.ToUInt16(data, y);
                z = z + 1;
                if (value == 65535)
                {
                    sequencet = "\\FFFF";
                    tempnick = "";
                    seqte = true;
                }
                else
                {
                    if (value == 0)
                    {
                        sequencet = "\\0000";
                        tempnick = "";
                        seqte = true;
                    }
                    else
                    {
                        if (seqte == false)
                        {
                            sequencet = "";
                            tempnick = char.ConvertFromUtf32(value);
                        }
                        else
                        {
                            n1 = Func.zeros((value & 255).ToString("X"));
                            n2 = Func.zeros((value >> 8).ToString("X"));
                            sequencet = "\\" + n1 + n2;
                            tempnick = "";
                        }
                    }
                }
                nick2 = nick2 + tempnick;
                nickseq = nickseq + sequencet;
                y = y + 2;
            }
            nick = nick2;
            nicktb = nick2 + nickseq;
            z = 0;
            y = 0xb0;
            string otseq = "";
            bool seqteot = false;
            sequencet = "";
            string tempot = "";
            string ot2 = "";
            while (z < 12)
            {
                z = z + 1;
                value = BitConverter.ToUInt16(data, y);
                if (value == 65535)
                {
                    sequencet = "\\FFFF";
                    tempot = "";
                    seqteot = true;
                }
                else
                {
                    if (value == 0)
                    {
                        sequencet = "\\0000";
                        tempot = "";
                    }
                    else
                    {
                        if (seqteot == false)
                        {
                            sequencet = "";
                            tempot = char.ConvertFromUtf32(value);
                        }
                        else
                        {
                            sequencet = "\\" + value.ToString("X");
                            tempot = "";
                        }
                    }
                }
                ot2 = ot2 + tempot;
                otseq = otseq + sequencet;
                y = y + 2;
            }
            ot = ot2;
            ottb = ot2 + otseq;
            y = 0x78;
            otseq = "";
            seqteot = false;
            sequencet = "";
            tempot = "";
            ot2 = "";
            z = 0;
            while (z < 12)
            {
                z = z + 1;
                value = BitConverter.ToUInt16(data, y);
                if (value == 65535)
                {
                    sequencet = "\\FFFF";
                    tempot = "";
                    seqteot = true;
                }
                else
                {
                    if (value == 0)
                    {
                        sequencet = "\\0000";
                        tempot = "";
                    }
                    else
                    {
                        if (seqteot == false)
                        {
                            sequencet = "";
                            tempot = char.ConvertFromUtf32(value);
                        }
                        else
                        {
                            sequencet = "\\" + value.ToString("X");
                            tempot = "";
                        }
                    }
                }
                ot2 = ot2 + tempot;
                otseq = otseq + sequencet;
                y = y + 2;
            }
            OTtraded = ot2;
            byte eggry = data[0xd1];
            byte eggrm = data[0xd2];
            byte eggrd = data[0xd3];
            if (eggrd == 0 & eggrm == 0 & eggry == 0)
            {
                dateegg = "";
            }
            else
            {
                dateegg = String.Format("{0}", eggrd < 10 ? "0" : "") + eggrd + "/" + String.Format("{0}", eggrm < 10 ? "0" : "") + eggrm + "/" + String.Format("{0}", eggry < 10 ? "0" : "") + eggry;
            }
            byte datey = data[0xd4];
            byte datem = data[0xd5];
            byte dated = data[0xd6];
            datemet = String.Format("{0}", dated < 10 ? "0" : "") + dated + "/" + String.Format("{0}", datem < 10 ? "0" : "") + datem + "/" + String.Format("{0}", datey < 10 ? "0" : "") + datey;
            if (BitConverter.ToUInt16(data, 0xd8) == 0)
            {
                isHatched = false;
                if (PkmLib.locationValues.Contains(BitConverter.ToUInt16(data, 0xd8)))
                {
                    eggloc = PkmLib.locations[PkmLib.locationValues.IndexOf(BitConverter.ToUInt16(data, 0xd8))];
                }
                else
                {
                    eggloc = Convert.ToString(BitConverter.ToUInt16(data, 0xd8));
                }
                if (PkmLib.locationValues.Contains(BitConverter.ToUInt16(data, 0xda)))
                {
                    locationmet = PkmLib.locations[PkmLib.locationValues.IndexOf(BitConverter.ToUInt16(data, 0xda))];
                }
                else
                {
                    locationmet = Convert.ToString(BitConverter.ToUInt16(data, 0xda));
                }
            }
            else
            {
                isHatched = true;
                if (PkmLib.locationValues.Contains(BitConverter.ToUInt16(data, 0xda)))
                {
                    eggloc = PkmLib.locations[PkmLib.locationValues.IndexOf(BitConverter.ToUInt16(data, 0xda))];
                }
                else
                {
                    eggloc = Convert.ToString(BitConverter.ToUInt16(data, 0xda));
                }
                if (PkmLib.locationValues.Contains(BitConverter.ToUInt16(data, 0xd8)))
                {
                    locationmet = PkmLib.locations[PkmLib.locationValues.IndexOf(BitConverter.ToUInt16(data, 0xd8))];
                }
                else
                {
                    locationmet = Convert.ToString(BitConverter.ToUInt16(data, 0xd8));
                }
            }
            pokerus = data[0x2b];
            amieFullness = data[0xae];
            amieEnjoyment = data[0xaf];
            amieAffection = data[0xcb];
            happiness = data[0xca];
            pokeball = PkmLib.pokeballs[data[0xdc]];
            byte x84 = data[0xdd];
            levelmet = (byte)(x84 & 127);
            genderot = (byte)(x84 >> (7));
            countryID = data[0xe0];
            regionID = data[0xe1];
            region3dsID = data[0xe2];
            //encounter = pkm(133)
            //pokestar = pkm(135)
            if (partypkm)
            {
                byte x88 = data[0xe8];
                byte sleep = (byte)((x88) & 0x7);
                byte poison = (byte)((x88 >> (3)) & 0x1);
                byte burn = (byte)((x88 >> (4)) & 0x1);
                byte frozen = (byte)((x88 >> (5)) & 0x1);
                byte para = (byte)((x88 >> (6)) & 0x1);
                byte toxic = (byte)((x88 >> (7)) & 0x1);
                if (sleep > 0)
                {
                    status = "Asleep " + sleep + " Turn(s)";
                }
                else
                {
                    status = "None";
                }
                if (poison == 1)
                {
                    status = "Poisoned";
                }
                if (burn == 1)
                {
                    status = "Burned";
                }
                if (frozen == 1)
                {
                    status = "Frozen";
                }
                if (para == 1)
                {
                    status = "Paralyzed";
                }
                if (toxic == 1)
                {
                    status = "Badly Poisoned";
                }
                level = data[0xec];
                hp = BitConverter.ToUInt16(data, 0xf0);
                maxhp = BitConverter.ToUInt16(data, 0xf2);
                attack = BitConverter.ToUInt16(data, 0xf4);
                defense = BitConverter.ToUInt16(data, 0xf6);
                speed = BitConverter.ToUInt16(data, 0xf8);
                spatk = BitConverter.ToUInt16(data, 0xfa);
                spdef = BitConverter.ToUInt16(data, 0xfc);
            }
            else
            {
            }
            ushort pid1 = 0;
            ushort pid2 = 0;
            pid1 = (ushort)(pid & 0xffff);
            pid2 = (ushort)((pid >> 16) & 0xffff);
            int a = id ^ sid;
            trainershiny = (ushort)(a >> 4);
            int b = pid1 ^ pid2;
            PokemonXYshiny = (ushort)(b >> 4);
            shinyvalue = (ushort)(a ^ b);
            if ((a ^ b) < 16 & species != PkmLib.species[0])
            {
                isShiny = true;
            }
            else
            {
                isShiny = false;
            }
            hiddenPower();
            if (no == 0)
            {
                isEmpty = true;
            }
            else
            {
                isEmpty = false;
            }
            updateSprite();
            updateStats();
        }

        /// <summary>
        /// Initialize pkm data without setting variables (moving pkm)
        /// </summary>

        public Pokemon()
        {
        }

        /// <summary>
        /// Initialize pkm data with variables data and no array
        /// </summary>
        public Pokemon(UInt32 _pid, string _species, string _item, string _ability, string _nature, UInt32 _level, byte[] _iv, byte[] _ev, string[] _moves, byte[] _ppups,
            string _locationmet, string _datemet, string _egglocation, string _eggdate, string _version, string _language, string _form, byte _pokerus)
        {
        }

        /// <summary>
        /// Load a PokemonXY into the class and initialize variables
        /// </summary>
        public Pokemon(byte[] data, bool decryptData = false)
        {
            isEmpty = true;
            int z = 0;
            while (z < data.Length)
            {
                this.data[z] = data[z];
                z += 1;
            }
            if (data.Length > 232)
            {
                partypkm = true;
            }
            else
            {
                partypkm = false;
            }
            bool zero = true;
            z = 0;
            while (z < data.Length)
            {
                if ((data[z] != 0))
                {
                    zero = false;
                    break; // TODO: might not be correct. Was : Exit While
                }
                z += 1;
            }
            if (decryptData == true)
            {
                //Check if data is not zerod
                if (zero == false)
                {
                    decrypt();
                    try
                    {
                        Initialize();
                    }
                    catch (Exception ex)
                    {
                        z = 0;
                        while (z < data.Length)
                        {
                            data[z] = 0;
                            z += 1;
                        }
                        isEmpty = true;
                    }
                    return;
                }
                else
                {
                    isEmpty = true;
                }
            }
            else
            {
                if (data[52] > 3)
                {
                    decrypt();
                }
                else
                {
                    if (data[53] > 3)
                    {
                        decrypt();
                    }
                    else
                    {
                        if (data[54] > 3)
                        {
                            decrypt();
                        }
                        else
                        {
                            if (data[55] > 3)
                            {
                                decrypt();
                            }
                        }
                    }
                }
                if (zero == false)
                {
                    isEmpty = false;
                    try
                    {
                        Initialize();
                    }
                    catch (Exception ex)
                    {
                        z = 0;
                        while (z < data.Length)
                        {
                            data[z] = 0;
                            z += 1;
                        }
                        isEmpty = true;
                    }
                    return;
                }
                else
                {
                    isEmpty = true;
                }
            }
        }


        /// <summary>
        /// Decrypt pkm data
        /// </summary>
        public void decrypt()
        {
            //int we = 0;
            pid = BitConverter.ToUInt32(data, 0);
            checksum = BitConverter.ToUInt16(data, 6);
            uint order = (pid >> (13) & 31) % 24;
            string firstblock = null;
            string secondblock = null;
            string thirdblock = null;
            string fourthblock = null;
            switch (order)
            {
                case 0:
                    firstblock = "A";
                    secondblock = "B";
                    thirdblock = "C";
                    fourthblock = "D";
                    break;
                case 1:
                    firstblock = "A";
                    secondblock = "B";
                    thirdblock = "D";
                    fourthblock = "C";
                    break;
                case 2:
                    firstblock = "A";
                    secondblock = "C";
                    thirdblock = "B";
                    fourthblock = "D";
                    break;
                case 3:
                    firstblock = "A";
                    secondblock = "C";
                    thirdblock = "D";
                    fourthblock = "B";
                    break;
                case 4:
                    firstblock = "A";
                    secondblock = "D";
                    thirdblock = "B";
                    fourthblock = "C";
                    break;
                case 5:
                    firstblock = "A";
                    secondblock = "D";
                    thirdblock = "C";
                    fourthblock = "B";
                    break;
                case 6:
                    firstblock = "B";
                    secondblock = "A";
                    thirdblock = "C";
                    fourthblock = "D";
                    break;
                case 7:
                    firstblock = "B";
                    secondblock = "A";
                    thirdblock = "D";
                    fourthblock = "C";
                    break;
                case 8:
                    firstblock = "B";
                    secondblock = "C";
                    thirdblock = "A";
                    fourthblock = "D";
                    break;
                case 9:
                    firstblock = "B";
                    secondblock = "C";
                    thirdblock = "D";
                    fourthblock = "A";
                    break;
                case 10:
                    firstblock = "B";
                    secondblock = "D";
                    thirdblock = "A";
                    fourthblock = "C";
                    break;
                case 11:
                    firstblock = "B";
                    secondblock = "D";
                    thirdblock = "C";
                    fourthblock = "A";
                    break;
                case 12:
                    firstblock = "C";
                    secondblock = "A";
                    thirdblock = "B";
                    fourthblock = "D";
                    break;
                case 13:
                    firstblock = "C";
                    secondblock = "A";
                    thirdblock = "D";
                    fourthblock = "B";
                    break;
                case 14:
                    firstblock = "C";
                    secondblock = "B";
                    thirdblock = "A";
                    fourthblock = "D";
                    break;
                case 15:
                    firstblock = "C";
                    secondblock = "B";
                    thirdblock = "D";
                    fourthblock = "A";
                    break;
                case 16:
                    firstblock = "C";
                    secondblock = "D";
                    thirdblock = "A";
                    fourthblock = "B";
                    break;
                case 17:
                    firstblock = "C";
                    secondblock = "D";
                    thirdblock = "B";
                    fourthblock = "A";
                    break;
                case 18:
                    firstblock = "D";
                    secondblock = "A";
                    thirdblock = "B";
                    fourthblock = "C";
                    break;
                case 19:
                    firstblock = "D";
                    secondblock = "A";
                    thirdblock = "C";
                    fourthblock = "B";
                    break;
                case 20:
                    firstblock = "D";
                    secondblock = "B";
                    thirdblock = "A";
                    fourthblock = "C";
                    break;
                case 21:
                    firstblock = "D";
                    secondblock = "B";
                    thirdblock = "C";
                    fourthblock = "A";
                    break;
                case 22:
                    firstblock = "D";
                    secondblock = "C";
                    thirdblock = "A";
                    fourthblock = "B";
                    break;
                case 23:
                    firstblock = "D";
                    secondblock = "C";
                    thirdblock = "B";
                    fourthblock = "A";
                    break;
            }
            UInt16[] unencryptedbyte = new UInt16[113];
            srand(pid);
            int v = 8;
            int z = 0;
            while (z < 112)
            {
                unencryptedbyte[z] = (ushort)(BitConverter.ToUInt16(data, v) ^ (rand()));
                v = v + 2;
                z = z + 1;
            }
            z = 0;
            switch (firstblock)
            {
                case "A":
                    v = 8;
                    break;
                case "B":
                    v = 0x40;
                    break;
                case "C":
                    v = 0x78;
                    break;
                case "D":
                    v = 0xb0;
                    break;
            }
            while (z < 28)
            {
                data[v] = (byte)(unencryptedbyte[z] & 255);
                data[v + 1] = (byte)(unencryptedbyte[z] >> 8);
                v = v + 2;
                z = z + 1;
            }
            switch (secondblock)
            {
                case "A":
                    v = 8;
                    break;
                case "B":
                    v = 0x40;
                    break;
                case "C":
                    v = 0x78;
                    break;
                case "D":
                    v = 0xb0;
                    break;
            }
            while (z < 56)
            {
                data[v] = (byte)(unencryptedbyte[z] & 255);
                data[v + 1] = (byte)(unencryptedbyte[z] >> 8);
                v = v + 2;
                z = z + 1;
            }
            switch (thirdblock)
            {
                case "A":
                    v = 8;
                    break;
                case "B":
                    v = 0x40;
                    break;
                case "C":
                    v = 0x78;
                    break;
                case "D":
                    v = 0xb0;
                    break;
            }
            while (z < 84)
            {
                data[v] = (byte)(unencryptedbyte[z] & 255);
                data[v + 1] = (byte)(unencryptedbyte[z] >> 8);
                v = v + 2;
                z = z + 1;
            }
            switch (fourthblock)
            {
                case "A":
                    v = 8;
                    break;
                case "B":
                    v = 0x40;
                    break;
                case "C":
                    v = 0x78;
                    break;
                case "D":
                    v = 0xb0;
                    break;
            }
            while (z < 112)
            {
                data[v] = (byte)(unencryptedbyte[z] & 255);
                data[v + 1] = (byte)(unencryptedbyte[z] >> 8);
                v = v + 2;
                z = z + 1;
            }
            //Party
            if (partypkm == true)
            {
                srand(pid);
                z = 0;
                v = 232;
                UInt16[] partybytes = new UInt16[15];
                while (z < 14)
                {
                    partybytes[z] = (ushort)(BitConverter.ToUInt16(data, v) ^ (rand()));
                    z = z + 1;
                    v = v + 2;
                }
                z = 0;
                v = 232;
                while (z < 14)
                {
                    data[v] = (byte)(partybytes[z] & 255);
                    data[v + 1] = (byte)(partybytes[z] >> 8);
                    v = v + 2;
                    z = z + 1;
                }
            }
        }

        /// <summary>
        /// Encrypt pkm data
        /// </summary>
        public void encrypt()
        {
            pid = BitConverter.ToUInt32(data, 0);
            checksum = BitConverter.ToUInt16(data, 6);
            uint order = (pid >> (13) & 31) % 24;
            string firstblock = null;
            string secondblock = null;
            string thirdblock = null;
            string fourthblock = null;
            switch (order)
            {
                case 0:
                    firstblock = "A";
                    secondblock = "B";
                    thirdblock = "C";
                    fourthblock = "D";
                    break;
                case 1:
                    firstblock = "A";
                    secondblock = "B";
                    thirdblock = "D";
                    fourthblock = "C";
                    break;
                case 2:
                    firstblock = "A";
                    secondblock = "C";
                    thirdblock = "B";
                    fourthblock = "D";
                    break;
                case 3:
                    firstblock = "A";
                    secondblock = "C";
                    thirdblock = "D";
                    fourthblock = "B";
                    break;
                case 4:
                    firstblock = "A";
                    secondblock = "D";
                    thirdblock = "B";
                    fourthblock = "C";
                    break;
                case 5:
                    firstblock = "A";
                    secondblock = "D";
                    thirdblock = "C";
                    fourthblock = "B";
                    break;
                case 6:
                    firstblock = "B";
                    secondblock = "A";
                    thirdblock = "C";
                    fourthblock = "D";
                    break;
                case 7:
                    firstblock = "B";
                    secondblock = "A";
                    thirdblock = "D";
                    fourthblock = "C";
                    break;
                case 8:
                    firstblock = "B";
                    secondblock = "C";
                    thirdblock = "A";
                    fourthblock = "D";
                    break;
                case 9:
                    firstblock = "B";
                    secondblock = "C";
                    thirdblock = "D";
                    fourthblock = "A";
                    break;
                case 10:
                    firstblock = "B";
                    secondblock = "D";
                    thirdblock = "A";
                    fourthblock = "C";
                    break;
                case 11:
                    firstblock = "B";
                    secondblock = "D";
                    thirdblock = "C";
                    fourthblock = "A";
                    break;
                case 12:
                    firstblock = "C";
                    secondblock = "A";
                    thirdblock = "B";
                    fourthblock = "D";
                    break;
                case 13:
                    firstblock = "C";
                    secondblock = "A";
                    thirdblock = "D";
                    fourthblock = "B";
                    break;
                case 14:
                    firstblock = "C";
                    secondblock = "B";
                    thirdblock = "A";
                    fourthblock = "D";
                    break;
                case 15:
                    firstblock = "C";
                    secondblock = "B";
                    thirdblock = "D";
                    fourthblock = "A";
                    break;
                case 16:
                    firstblock = "C";
                    secondblock = "D";
                    thirdblock = "A";
                    fourthblock = "B";
                    break;
                case 17:
                    firstblock = "C";
                    secondblock = "D";
                    thirdblock = "B";
                    fourthblock = "A";
                    break;
                case 18:
                    firstblock = "D";
                    secondblock = "A";
                    thirdblock = "B";
                    fourthblock = "C";
                    break;
                case 19:
                    firstblock = "D";
                    secondblock = "A";
                    thirdblock = "C";
                    fourthblock = "B";
                    break;
                case 20:
                    firstblock = "D";
                    secondblock = "B";
                    thirdblock = "A";
                    fourthblock = "C";
                    break;
                case 21:
                    firstblock = "D";
                    secondblock = "B";
                    thirdblock = "C";
                    fourthblock = "A";
                    break;
                case 22:
                    firstblock = "D";
                    secondblock = "C";
                    thirdblock = "A";
                    fourthblock = "B";
                    break;
                case 23:
                    firstblock = "D";
                    secondblock = "C";
                    thirdblock = "B";
                    fourthblock = "A";
                    break;
            }
            int z = 0;
            int v = 8;
            //Block A
            UInt16[] blocka = new UInt16[29];
            while (z < 28)
            {
                blocka[z] = BitConverter.ToUInt16(data, v);
                z = z + 1;
                v = v + 2;
            }
            z = 0;
            v = 0x40;
            //Block B
            UInt16[] blockb = new UInt16[29];
            while (z < 28)
            {
                blockb[z] = BitConverter.ToUInt16(data, v);
                z = z + 1;
                v = v + 2;
            }
            z = 0;
            //Block C
            v = 0x78;
            UInt16[] blockc = new UInt16[29];
            while (z < 28)
            {
                blockc[z] = BitConverter.ToUInt16(data, v);
                z = z + 1;
                v = v + 2;
            }
            z = 0;
            //Block D
            UInt16[] blockd = new UInt16[29];
            v = 0xb0;
            while (z < 28)
            {
                blockd[z] = BitConverter.ToUInt16(data, v);
                z = z + 1;
                v = v + 2;
            }
            z = 0;
            UInt16[] partyb = new UInt16[15];
            if (partypkm == true)
            {
                v = 232;
                z = 0;
                while (z < 14)
                {
                    partyb[z] = BitConverter.ToUInt16(data, v);
                    z = z + 1;
                    v = v + 2;
                }
            }
            z = 0;
            srand(pid);
            UInt16[] byter = new UInt16[29];
            z = 0;
            v = 8;
            switch (firstblock)
            {
                case "A":
                    while (z < 28)
                    {
                        byter[z] = (ushort)(blocka[z] ^ rand());
                        z = z + 1;
                    }
                    break;
                case "B":
                    while (z < 28)
                    {
                        byter[z] = (ushort)(blockb[z] ^ rand());
                        z = z + 1;
                    }
                    break;
                case "C":
                    while (z < 28)
                    {
                        byter[z] = (ushort)(blockc[z] ^ rand());
                        z = z + 1;
                    }
                    break;
                case "D":
                    while (z < 28)
                    {
                        byter[z] = (ushort)(blockd[z] ^ rand());
                        z = z + 1;
                    }
                    break;
            }
            z = 0;
            v = 8;
            while (z < 28)
            {
                data[v] = (byte)(byter[z] & 255);
                data[v + 1] = (byte)(byter[z] >> 8);
                z = z + 1;
                v = v + 2;
            }
            z = 0;
            v = 0x40;
            switch (secondblock)
            {
                case "A":
                    while (z < 28)
                    {
                        byter[z] = (ushort)(blocka[z] ^ rand());
                        z = z + 1;
                    }
                    break;
                case "B":
                    while (z < 28)
                    {
                        byter[z] = (ushort)(blockb[z] ^ rand());
                        z = z + 1;
                    }
                    break;
                case "C":
                    while (z < 28)
                    {
                        byter[z] = (ushort)(blockc[z] ^ rand());
                        z = z + 1;
                    }
                    break;
                case "D":
                    while (z < 28)
                    {
                        byter[z] = (ushort)(blockd[z] ^ rand());
                        z = z + 1;
                    }
                    break;
            }
            z = 0;
            while (z < 28)
            {
                data[v] = (byte)(byter[z] & 255);
                data[v + 1] = (byte)(byter[z] >> 8);
                z = z + 1;
                v = v + 2;
            }
            z = 0;
            v = 0x78;
            switch (thirdblock)
            {
                case "A":
                    while (z < 28)
                    {
                        byter[z] = (ushort)(blocka[z] ^ rand());
                        z = z + 1;
                    }
                    break;
                case "B":
                    while (z < 28)
                    {
                        byter[z] = (ushort)(blockb[z] ^ rand());
                        z = z + 1;
                    }
                    break;
                case "C":
                    while (z < 28)
                    {
                        byter[z] = (ushort)(blockc[z] ^ rand());
                        z = z + 1;
                    }
                    break;
                case "D":
                    while (z < 28)
                    {
                        byter[z] = (ushort)(blockd[z] ^ rand());
                        z = z + 1;
                    }
                    break;
            }
            z = 0;
            while (z < 28)
            {
                data[v] = (byte)(byter[z] & 255);
                data[v + 1] = (byte)(byter[z] >> 8);
                z = z + 1;
                v = v + 2;
            }
            z = 0;
            v = 0xb0;
            switch (fourthblock)
            {
                case "A":
                    while (z < 28)
                    {
                        byter[z] = (ushort)(blocka[z] ^ rand());
                        z = z + 1;
                    }
                    break;
                case "B":
                    while (z < 28)
                    {
                        byter[z] = (ushort)(blockb[z] ^ rand());
                        z = z + 1;
                    }
                    break;
                case "C":
                    while (z < 28)
                    {
                        byter[z] = (ushort)(blockc[z] ^ rand());
                        z = z + 1;
                    }
                    break;
                case "D":
                    while (z < 28)
                    {
                        byter[z] = (ushort)(blockd[z] ^ rand());
                        z = z + 1;
                    }
                    break;
            }
            z = 0;
            while (z < 28)
            {
                data[v] = (byte)(byter[z] & 255);
                data[v + 1] = (byte)(byter[z] >> 8);
                z = z + 1;
                v = v + 2;
            }
            z = 0;
            //Party
            v = 232;
            if (partypkm == true)
            {
                srand(pid);
                z = 0;
                UInt16[] partybytes = new UInt16[15];
                while (z < 14)
                {
                    partybytes[z] = (ushort)((partyb[z]) ^ (rand()));
                    z = z + 1;
                }
                z = 0;
                v = 232;
                while (z < 14)
                {
                    data[v] = (byte)(partybytes[z] & 255);
                    data[v + 1] = (byte)(partybytes[z] >> 8);
                    z = z + 1;
                    v = v + 2;
                }
            }
        }

        /// <summary>
        /// Prepare pkx data array
        /// </summary>
        public void save(bool isParty = true)
        {
            if (isEmpty)
            {
                clear();
                return;
            }
            Array.Copy(BitConverter.GetBytes(encryptionKey), 0, data, 0, 4);
            Array.Copy(BitConverter.GetBytes(checksum), 0, data, 6, 2);
            Array.Copy(BitConverter.GetBytes(PkmLib.species.IndexOf(species)), 0, data, 8, 2);
            Array.Copy(BitConverter.GetBytes(PkmLib.items.IndexOf(item)), 0, data, 0xa, 2);
            Array.Copy(BitConverter.GetBytes(id), 0, data, 0xc, BitConverter.GetBytes(id).Length);
            Array.Copy(BitConverter.GetBytes(sid), 0, data, 0xe, BitConverter.GetBytes(sid).Length);
            Array.Copy(BitConverter.GetBytes(exp), 0, data, 0x10, BitConverter.GetBytes(exp).Length);
            data[0x14] = Convert.ToByte(PkmLib.abilities.IndexOf(ability));
            Array.Copy(BitConverter.GetBytes(pid), 0, data, 0x18, 4);
            data[0x15] = (byte)abilityIndex;
            data[0xe3] = Convert.ToByte((PkmLib.Languages)Enum.Parse(typeof(PkmLib.Languages), language));
            data[0x1e] = hpev;
            data[0x1f] = atev;
            data[0x20] = dfev;
            data[0x21] = spev;
            data[0x22] = saev;
            data[0x23] = sdev;
            data[0x2a] = markings;
            Array.Copy(BitConverter.GetBytes(supertraining.data), 0, data, 0x2c, 4);
            Array.Copy(BitConverter.GetBytes(ribbon1.data), 0, data, 0x30, 2);
            Array.Copy(BitConverter.GetBytes(ribbon2.data), 0, data, 0x32, 2);
            Array.Copy(BitConverter.GetBytes(ribbon3.data), 0, data, 0x34, 2);
            Array.Copy(BitConverter.GetBytes(moveset.move1.move), 0, data, 0x5a, 2);
            Array.Copy(BitConverter.GetBytes(moveset.move2.move), 0, data, 0x5c, 2);
            Array.Copy(BitConverter.GetBytes(moveset.move3.move), 0, data, 0x5e, 2);
            Array.Copy(BitConverter.GetBytes(moveset.move4.move), 0, data, 0x60, 2);
            Array.Copy(BitConverter.GetBytes(eggmoves.move1.move), 0, data, 0x6a, 2);
            Array.Copy(BitConverter.GetBytes(eggmoves.move2.move), 0, data, 0x6c, 2);
            Array.Copy(BitConverter.GetBytes(eggmoves.move3.move), 0, data, 0x6e, 2);
            Array.Copy(BitConverter.GetBytes(eggmoves.move4.move), 0, data, 0x70, 2);
            data[0x62] = moveset.move1.pp;
            data[0x63] = moveset.move2.pp;
            data[0x64] = moveset.move3.pp;
            data[0x65] = moveset.move4.pp;
            data[0x66] = moveset.move1.ppUp;
            data[0x67] = moveset.move2.ppUp;
            data[0x68] = moveset.move3.ppUp;
            data[0x69] = moveset.move4.ppUp;
            Array.Copy(BitConverter.GetBytes(iv.getIV()), 0, data, 0x74, 4);
            //Array.Copy(BitConverter.GetBytes(ribbon3), 0, data, &H3C, BitConverter.GetBytes(ribbon3).Length)
            //Array.Copy(BitConverter.GetBytes(ribbon4), 0, data, &H3E, BitConverter.GetBytes(ribbon4).Length)
            byte x40 = 0;
            if (isFateful)
            {
                x40 = (byte)(x40 | 1);
            }
            if (female != 0)
            {
                x40 = (byte)(x40 | 2);
            }
            if (genderless != 0)
            {
                x40 = (byte)(x40 | 4);
            }
            int f = 0;
            fillForms();
            f = Array.IndexOf(forms, form) * 8;
            x40 += (byte)f;
            data[0x1d] = x40;
            data[0x1c] = (byte)PkmLib.natures.IndexOf(nature);
            //pkm(&H42) = DW
            int l = 0;
            int z = 0;
            int n = 0x40;
            l = nick.Length;
            //string n1 = "";
            //string n2 = "";
            //string[] temp9 = null;
            //string temp2 = "";
            UInt16 nicku = 0;
            string temp = nick;
            while (z < 12)
            {
                if (string.IsNullOrEmpty(temp))
                {
                    Array.Copy(BitConverter.GetBytes(Convert.ToUInt16(0)), 0, data, n, 2);
                    z = z + 1;
                }
                else
                {
                    nicku = (ushort)temp.First();
                    Array.Copy(BitConverter.GetBytes(nicku), 0, data, n, BitConverter.GetBytes(nicku).Length);
                    z = z + 1;
                    temp = temp.Remove(0, 1);
                }
                n += 2;
            }
            z = 0;
            //temp2 = "";
            UInt16 otu = 0;
            l = ot.Length;
            n = 0xb0;
            temp = ot;
            while (z < 12)
            {
                if (string.IsNullOrEmpty(temp))
                {
                    Array.Copy(BitConverter.GetBytes(Convert.ToUInt16(0)), 0, data, n, 2);
                    z = z + 1;
                }
                else
                {
                    otu = (ushort)temp.First();
                    Array.Copy(BitConverter.GetBytes(otu), 0, data, n, BitConverter.GetBytes(otu).Length);
                    z = z + 1;
                    temp = temp.Remove(0, 1);
                }
                n += 2;
            }
            z = 0;
            otu = 0;
            l = OTtraded.Length;
            n = 0x78;
            temp = OTtraded;
            while (z < 12)
            {
                if (string.IsNullOrEmpty(temp))
                {
                    Array.Copy(BitConverter.GetBytes(Convert.ToUInt16(0)), 0, data, n, 2);
                    z = z + 1;
                }
                else
                {
                    otu = (ushort)temp.First();
                    Array.Copy(BitConverter.GetBytes(otu), 0, data, n, BitConverter.GetBytes(otu).Length);
                    z = z + 1;
                    temp = temp.Remove(0, 1);
                }
                n += 2;
            }
            switch (version)
            {
                case "Ruby":
                    data[0xdf] = 1;
                    break;
                case "Sapphire":
                    data[0xdf] = 2;
                    break;
                case "Emerald":
                    data[0xdf] = 3;
                    break;
                case "Fire Red":
                    data[0xdf] = 4;
                    break;
                case "Leaf Green":
                    data[0xdf] = 5;
                    break;
                case "Heart Gold":
                    data[0xdf] = 7;
                    break;
                case "Soul Silver":
                    data[0xdf] = 8;
                    break;
                case "Diamond":
                    data[0xdf] = 10;
                    break;
                case "Pearl":
                    data[0xdf] = 11;
                    break;
                case "Platinum":
                    data[0xdf] = 12;
                    break;
                case "Colosseum/XD":
                    data[0xdf] = 15;
                    break;
                case "White":
                    data[0xdf] = 20;
                    break;
                case "Black":
                    data[0xdf] = 21;
                    break;
                case "White 2":
                    data[0xdf] = 22;
                    break;
                case "Black 2":
                    data[0xdf] = 23;
                    break;
                case "X":
                    data[0xdf] = 24;
                    break;
                case "Y":
                    data[0xdf] = 25;
                    break;
                default:
                    data[0xdf] = 0;
                    break;
            }
            //Array.Copy(BitConverter.GetBytes(ribbon5), 0, data, &H60, BitConverter.GetBytes(ribbon5).Length)
            //Array.Copy(BitConverter.GetBytes(ribbon6), 0, data, &H62, BitConverter.GetBytes(ribbon6).Length)
            if (isHatched == false)
            {
                data[0xd3] = 0;
                data[0xd2] = 0;
                data[0xd1] = 0;
            }
            else
            {
                string[] eggdt = dateegg.Split('/');
                data[0xd3] = Convert.ToByte(eggdt[0]);
                data[0xd2] = Convert.ToByte(eggdt[1]);
                data[0xd1] = Convert.ToByte(eggdt[2]);
            }
            string[] datem = datemet.Split('/');
            data[0xd6] = Convert.ToByte(datem[0]);
            data[0xd5] = Convert.ToByte(datem[1]);
            data[0xd4] = Convert.ToByte(datem[2]);
            ushort parseTemp;
            if (isHatched == false)
            {
                data[0xd8] = 0;
                data[0xd9] = 0;
            }
            else
            {
                if (ushort.TryParse(eggloc, out parseTemp))
                {
                    Array.Copy(BitConverter.GetBytes(Convert.ToUInt16(eggloc)), 0, data, 0xda, 2);
                }
                else
                {
                    Array.Copy(BitConverter.GetBytes(Convert.ToUInt16(PkmLib.locationValues[PkmLib.locations.IndexOf(eggloc)])), 0, data, 0xda, 2);
                }
            }
            if (isHatched)
            {
                if (ushort.TryParse(locationmet, out parseTemp))
                {
                    Array.Copy(BitConverter.GetBytes(Convert.ToUInt16(locationmet)), 0, data, 0xd8, 2);
                }
                else
                {
                    Array.Copy(BitConverter.GetBytes(Convert.ToUInt16(PkmLib.locationValues[PkmLib.locations.IndexOf(locationmet)])), 0, data, 0xd8, 2);
                }
            }
            else
            {
                if (ushort.TryParse(locationmet, out parseTemp))
                {
                    Array.Copy(BitConverter.GetBytes(Convert.ToUInt16(locationmet)), 0, data, 0xda, 2);
                }
                else
                {
                    Array.Copy(BitConverter.GetBytes(Convert.ToUInt16(PkmLib.locationValues[PkmLib.locations.IndexOf(locationmet)])), 0, data, 0xda, 2);
                }
            }
            data[0x2b] = pokerus;
            switch (pokeball)
            {
                case "Master Ball":
                    data[0xdc] = 1;
                    break;
                case "Ultra Ball":
                    data[0xdc] = 2;
                    break;
                case "Great Ball":
                    data[0xdc] = 3;
                    break;
                case "Poké Ball":
                    data[0xdc] = 4;
                    break;
                case "Safari Ball":
                    data[0xdc] = 5;
                    break;
                case "Net Ball":
                    data[0xdc] = 6;
                    break;
                case "Dive Ball":
                    data[0xdc] = 7;
                    break;
                case "Nest Ball":
                    data[0xdc] = 8;
                    break;
                case "Repeat Ball":
                    data[0xdc] = 9;
                    break;
                case "Timer Ball":
                    data[0xdc] = 10;
                    break;
                case "Luxury Ball":
                    data[0xdc] = 11;
                    break;
                case "Premier Ball":
                    data[0xdc] = 12;
                    break;
                case "Dusk Ball":
                    data[0xdc] = 13;
                    break;
                case "Heal Ball":
                    data[0xdc] = 14;
                    break;
                case "Quick Ball":
                    data[0xdc] = 15;
                    break;
                case "Cherish Ball":
                    data[0xdc] = 16;
                    break;
                case "Fast Ball":
                    data[0xdc] = 17;
                    break;
                case "Level Ball":
                    data[0xdc] = 18;
                    break;
                case "Lure Ball":
                    data[0xdc] = 19;
                    break;
                case "Heavy Ball":
                    data[0xdc] = 20;
                    break;
                case "Love Ball":
                    data[0xdc] = 21;
                    break;
                case "Sport Ball":
                    data[0xdc] = 24;
                    break;
                case "Moon Ball":
                    data[0xdc] = 23;
                    break;
                case "Friend Ball":
                    data[0xdc] = 22;
                    break;
                case "Dream Ball":
                    data[0xdc] = 25;
                    break;
                default:
                    data[0xdc] = 4;
                    break;
            }
            //pkm(&H85) = encounter
            data[0xae] = amieFullness;
            data[0xaf] = amieEnjoyment;
            data[0xca] = happiness;
            data[0xcb] = amieAffection;
            data[0xdd] = (byte)(levelmet + (genderot * 128));
            data[0xe0] = countryID;
            data[0xe1] = regionID;
            data[0xe2] = region3dsID;
            if (isParty)
            {
                partypkm = true;
                if (level == 0)
                {
                    level = 1;
                }
                updateStats();
            }
            updateSprite();
            checksumUpdate();
        }

        /// <summary>
        /// Convert to Party PKM and calculate stats
        /// </summary>
        public void updateStats()
        {
            partypkm = true;
            if (status == "None")
            {
                status = "None";
                data[0xe8] = 0;
            }
            else
            {
                //Status
            }
            baseStats();
            level = PkmLib.calculateLevel(species, exp);
            data[0xec] = level;
            double atnature = 1;
            double dfnature = 1;
            double sanature = 1;
            double sdnature = 1;
            double spnature = 1;
            switch (nature)
            {
                case "Adamant":
                    atnature = 1.1;
                    sanature = 0.9;
                    break;
                case "Bashful":

                    break;
                case "Bold":
                    atnature = 0.9;
                    dfnature = 1.1;
                    break;
                case "Brave":
                    atnature = 1.1;
                    spnature = 0.9;
                    break;
                case "Calm":
                    sdnature = 1.1;
                    atnature = 0.9;
                    break;
                case "Careful":
                    sdnature = 1.1;
                    sanature = 0.9;
                    break;
                case "Docile":

                    break;
                case "Gentle":
                    sdnature = 1.1;
                    dfnature = 0.9;
                    break;
                case "Hardy":

                    break;
                case "Hasty":
                    spnature = 1.1;
                    dfnature = 0.9;
                    break;
                case "Impish":
                    dfnature = 1.1;
                    sanature = 0.9;
                    break;
                case "Jolly":
                    spnature = 1.1;
                    sanature = 0.9;
                    break;
                case "Lax":
                    dfnature = 1.1;
                    sdnature = 0.9;
                    break;
                case "Lonely":
                    atnature = 1.1;
                    dfnature = 0.9;
                    break;
                case "Mild":
                    sanature = 1.1;
                    dfnature = 0.9;
                    break;
                case "Modest":
                    sanature = 1.1;
                    atnature = 0.9;
                    break;
                case "Naive":
                    spnature = 1.1;
                    sdnature = 0.9;
                    break;
                case "Naughty":
                    atnature = 1.1;
                    sdnature = 0.9;
                    break;
                case "Quiet":
                    sanature = 1.1;
                    spnature = 0.9;
                    break;
                case "Quirky":

                    break;
                case "Rash":
                    sanature = 1.1;
                    sdnature = 0.9;
                    break;
                case "Relaxed":
                    dfnature = 1.1;
                    spnature = 0.9;
                    break;
                case "Sassy":
                    sdnature = 1.1;
                    spnature = 0.9;
                    break;
                case "Serious":

                    break;
                case "Timid":
                    spnature = 1.1;
                    atnature = 0.9;
                    break;
            }
            if (baseHP == 1)
            {
                hp = 1;
            }
            else
            {
                hp = (ushort)Math.Floor((double)(((iv.hp + (2 * baseHP) + (hpev / 4) + 100) * level) / 100) + 10);
            }
            maxhp = hp;
            data[0xf0] = (byte)(hp & 255);
            data[0xf1] = (byte)(hp >> 8);
            data[0xf2] = (byte)(hp & 255);
            data[0xf3] = (byte)(hp >> 8);
            attack = (ushort)Math.Floor(((((iv.atk + (2 * baseAtk) + (atev / 4)) * level) / 100) + 5) * atnature);
            data[0xf4] = (byte)(attack & 255);
            data[0xf5] = (byte)(attack >> 8);
            defense = (ushort)Math.Floor(((((iv.def + (2 * baseDef) + (dfev / 4)) * level) / 100) + 5) * dfnature);
            data[0xf6] = (byte)(defense & 255);
            data[0xf7] = (byte)(defense >> 8);
            speed = (ushort)Math.Floor(((((iv.spe + (2 * baseSpd) + (spev / 4)) * level) / 100) + 5) * spnature);
            data[0xf8] = (byte)(speed & 255);
            data[0xf9] = (byte)(speed >> 8);
            spatk = (ushort)Math.Floor(((((iv.spa + (2 * baseSpAtk) + (saev / 4)) * level) / 100) + 5) * sanature);
            data[0xfa] = (byte)(spatk & 255);
            data[0xfb] = (byte)(spatk >> 8);
            spdef = (ushort)Math.Floor(((((iv.spd + (2 * baseSpDef) + (sdev / 4)) * level) / 100) + 5) * sdnature);
            data[0xfc] = (byte)(spdef & 255);
            data[0xfd] = (byte)(spdef >> 8);
        }

        /// <summary>
        /// Updates the Pokemon sprite based on its species
        /// </summary>
        public void updateSprite()
        {
            no = (ushort)PkmLib.species.IndexOf(species);
            //genderRatio = (PkmLib.GenderRatios)PkmLib.genderRatio[no];
            sprite = PkmLib.getSprite(no, form);
        }

        /// <summary>
        /// Convert to PC PKM and remove status and stats
        /// </summary>
        public void convertToPC()
        {
            partypkm = false;
            int z = 232;
            while (z < data.Length)
            {
                data[z] = 0;
                z += 1;
            }
            Initialize();
        }

        /// <summary>
        /// Return a copy of the PokemonXY object
        /// </summary>
        /// <returns>Clone of a PokemonXY object</returns>
        public Pokemon Clone()
        {
            return this.MemberwiseClone() as Pokemon;
        }

        /// <summary>
        /// Receive a byte[] of this PokemonXY without Party Block
        /// </summary>
        /// <returns>Returns a byte[232] of the PokemonXY as PC pkm</returns>
        public byte[] getPC()
        {
            return Func.subArray(data, 0, 232);
        }

        /// <summary>
        /// Returns a Copy of the PokemonXY encrypted without encrypting this object data
        /// </summary>
        /// <param name="party">Include party block</param>
        /// <returns>byte[] of the PokemonXY encrypted</returns>
        public byte[] getEncrypted(bool party = true)
        {
            save();
            Pokemon temp = this;
            temp.encrypt();
            if (party)
            {
                return temp.data;
            }
            else
            {
                return temp.getPC();
            }
        }
    }
}
