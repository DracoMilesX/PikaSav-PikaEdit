using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit
{
    /// <summary>
    /// Represents a save file WonderCard
    /// </summary>
    public class WonderCard
    {
        /// <summary>
        /// Wonder Card byte[] in the save file decrypted
        /// </summary>
        public byte[] data = new byte[204];

        /// <summary>
        /// Represents Card Type, what will the player will receive
        /// </summary>
        public enum CardType
        {
            None,
            Pokemon,
            Item,
            /// <summary>
            /// Pass Power
            /// </summary>
            Power
        }

        /// <summary>
        /// Represents the ability index the pokemon will have or can have
        /// </summary>
        public enum Ability
        {
            /// <summary>
            /// Pokemon will have ability 0
            /// </summary>
            Ability_0,
            /// <summary>
            /// Pokemon will have ability 1
            /// </summary>
            Ability_1,
            /// <summary>
            /// Pokemon will have hidden ability
            /// </summary>
            HA,
            /// <summary>
            /// Pokemon can have ability 0 or 1, but not HA
            /// </summary>
            Random,
            /// <summary>
            /// Pokemon can have ability 0, 1 or HA
            /// </summary>
            Random_HA
        }

        /// <summary>
        /// Represents the shininess of the pokemon the Wonder Card has if PID is random
        /// </summary>
        public enum Shininess
        {
            /// <summary>
            /// The Pokemon will never be shiny
            /// </summary>
            Never,
            /// <summary>
            /// The pokemon can be shiny
            /// </summary>
            Possible,
            /// <summary>
            /// The pokemon will be shiny
            /// </summary>
            Always
        }

        /// <summary>
        /// Represents the OT Gender the pokemon will have
        /// </summary>
        public enum OTGender
        {
            Male,
            Female,
            Game = 3
        }

        /// <summary>
        /// Represents the Pokemon Gender
        /// </summary>
        public enum Gender
        {
            Male,
            Female,
            Random_Genderless
        }

        public WonderCard()
        {
            data = new byte[204];
        }

        public WonderCard(byte[] data)
        {
            this.data = data;
        }

        public string title
        {
            get
            {
                return Func.getString(data, 0x60, 37);
            }
            set
            {
                byte[] b = Func.getfromString(value, 37, true);
                Array.Copy(b, 0, data, 0x60, 74);
            }
        }

        public ushort dateYear
        {
            get
            {
                return Func.getUInt16(data, 0xae);
            }
            set
            {
                Array.Copy(BitConverter.GetBytes(value), 0, data, 0xae, 2);
            }
        }

        public byte dateMonth
        {
            get
            {
                return data[0xad];
            }
            set
            {
                data[0xad] = value;
            }
        }

        public byte dateDay
        {
            get
            {
                return data[0xac];
            }
            set
            {
                data[0xac] = value;
            }
        }

        public ushort cardId
        {
            get
            {
                return Func.getUInt16(data, 0xb0);
            }
            set
            {
                Array.Copy(BitConverter.GetBytes(value), 0, data, 0xb0, 2);
            }
        }

        public byte cardLocation
        {
            get
            {
                return data[0xb2];
            }
            set
            {
                data[0xb2] = value;
            }
        }

        public CardType type
        {
            get
            {
                switch (data[0xb3])
                {
                    case 0:
                        return CardType.None;
                    case 1:
                        return CardType.Pokemon;
                    case 2:
                        return CardType.Item;
                    case 3:
                        return CardType.Power;
                    default:
                        return CardType.None;
                }
            }
            set
            {
                data[0xb3] = (byte)value;
            }
        }

        public bool cardUsed
        {
            get
            {
                return data[0xb4] == 3;
            }
            set
            {
                data[0xb4] = (value ? (byte)3 : (byte)1);
            }
        }

        public bool staticPID
        {
            get
            {
                return pid == 0;
            }
        }

        public uint pid
        {
            get
            {
                return Func.getUInt32(data, 0x8);
            }
            set
            {
                Array.Copy(BitConverter.GetBytes(value), 0, data, 0x8, 4);
            }
        }

        public RibbonSet ribbons
        {
            get
            {
                return new RibbonSet(BitConverter.ToUInt16(data, 0xc));
            }
            set
            {
                Array.Copy(BitConverter.GetBytes(value.data), 0, data, 0xc, 2);
            }
        }

        public string nickname
        {
            get
            {
                return Func.getString(data, 0x1e, 11);
            }
            set
            {
                byte[] b = Func.getfromString(value, 11, true);
                Array.Copy(b, 0, data, 0x60, 22);
            }
        }

        public bool isNicknamed
        {
            get
            {
                return !string.IsNullOrEmpty(nickname);
            }
        }

        public string language
        {
            get
            {
                switch (data[0x1c])
                {
                    case 0:
                        return "Game received";
                    case 1:
                        return "Japan";
                    case 2:
                        return "English";
                    case 3:
                        return "French";
                    case 4:
                        return "Italian";
                    case 5:
                        return "German";
                    case 7:
                        return "Spanish";
                    case 8:
                        return "Korean";
                    default:
                        return "Game received";
                }
            }
            set
            {
                if (value == "Game received")
                {
                    data[0x1c] = 0;
                }
                else
                {
                    data[0x1c] = Convert.ToByte((PkmLib.Languages)Enum.Parse(typeof(PkmLib.Languages), value));
                }
            }
        }

        public Ability ability
        {
            get
            {
                return (Ability)data[0x36];
            }
            set
            {
                data[0x36] = (byte)value;
            }
        }

        public OTGender otGender
        {
            get
            {
                return (OTGender)data[0x5a];
            }
            set
            {
                data[0x5a] = (byte)value;
            }
        }

        public Gender gender
        {
            get
            {
                return (Gender)data[0x35];
            }
            set
            {
                data[0x35] = (byte)value;
            }
        }

        public string nature
        {
            get
            {
                if (data[0x34] == 0xFF)
                {
                    return "Random";
                }
                else
                {
                    return PkmLib.abilities[data[0x34]];
                }
            }
            set
            {
                if (value == "Random")
                {
                    data[0x34] = 0xFF;
                }
                else
                {
                    data[0x34] = (byte)PkmLib.abilities.IndexOf(value);
                }
            }
        }

        public Shininess isShiny
        {
            get
            {
                return (Shininess)data[0x37];
            }
            set
            {
                data[0x37] = (byte)value;
            }
        }

        public IVSet iv
        {
            get
            {
                return new IVSet(BitConverter.ToUInt32(data, 0x43));
            }
            set
            {
                Array.Copy(BitConverter.GetBytes(Convert.ToUInt32(value.getIV())), 0, data, 0x43, 4);
            }
        }

        public string species
        {
            get
            {
                return PkmLib.species[BitConverter.ToUInt16(data, 0x1A)];
            }
            set
            {
                Array.Copy(BitConverter.GetBytes((ushort)PkmLib.species.IndexOf(value)), 0, data, 0x1A, 2);
            }
        }

        public string pkmItem
        {
            get
            {
                return PkmLib.items[BitConverter.ToUInt16(data, 0x10)];
            }
            set
            {
                Array.Copy(BitConverter.GetBytes((ushort)PkmLib.items.IndexOf(value)), 0, data, 0x10, 2);
            }
        }

        public string form
        {
            get
            {
                return PkmLib.getFormFromValue(species, data[0x1c]);
            }
            set
            {
                data[0x1c] = PkmLib.getFormValue(species, value);
            }
        }

        public string pokeball
        {
            get
            {
                return PkmLib.pokeballs[data[0xe]];
            }
            set
            {
                data[0xe] = (byte)PkmLib.pokeballs.IndexOf(value);
            }
        }

        public string ot
        {
            get
            {
                return Func.getString(data, 0x4a, 8);
            }
            set
            {
                byte[] b = Func.getfromString(value, 8, true);
                Array.Copy(b, 0, data, 0x4a, 16);
            }
        }

        public byte level
        {
            get
            {
                return data[0x5b];
            }
            set
            {
                data[0x5b] = value;
                data[0x3c] = value; //Met level?
            }
        }

        public MoveSet moveset
        {
            get
            {
                return new MoveSet(new Move(BitConverter.ToUInt16(data, 0x12), 0, 0), new Move(BitConverter.ToUInt16(data, 0x14), 0, 0), new Move(BitConverter.ToUInt16(data, 0x16), 0, 0), new Move(BitConverter.ToUInt16(data, 0x18), 0, 0));
            }
            set
            {
                Array.Copy(BitConverter.GetBytes(value.move1.move), 0, data, 0x12, 2);
                Array.Copy(BitConverter.GetBytes(value.move2.move), 0, data, 0x14, 2);
                Array.Copy(BitConverter.GetBytes(value.move3.move), 0, data, 0x16, 2);
                Array.Copy(BitConverter.GetBytes(value.move4.move), 0, data, 0x18, 2);
            }
        }

        public string locationHatched
        {
            get
            {
                return PkmLib.locations[PkmLib.locationValues.IndexOf(BitConverter.ToUInt16(data, 0x38))];
            }
            set
            {
                Array.Copy(BitConverter.GetBytes(PkmLib.locationValues[PkmLib.locations.IndexOf(value)]), 0, data, 0x38, 2);
            }
        }

        public string locationMet
        {
            get
            {
                return PkmLib.locations[PkmLib.locationValues.IndexOf(BitConverter.ToUInt16(data, 0x3A))];
            }
            set
            {
                Array.Copy(BitConverter.GetBytes(PkmLib.locationValues[PkmLib.locations.IndexOf(value)]), 0, data, 0x3A, 2);
            }
        }

        public bool isEgg
        {
            get
            {
                return data[0x5c] == 1;
            }
            set
            {
                data[0x5c] = (value ? (byte)1 : (byte)0);
            }
        }

        public string version
        {
            get
            {
                switch (data[0x4])
                {
                    case 0:
                        return "Game received";
                    case 1:
                        return "Ruby";
                    case 2:
                        return "Sapphire";
                    case 3:
                        return "Emerald";
                    case 4:
                        return "Fire Red";
                    case 5:
                        return "Leaf Green";
                    case 7:
                        return "Heart Gold";
                    case 8:
                        return "Soul Silver";
                    case 10:
                        return "Diamond";
                    case 11:
                        return "Pearl";
                    case 12:
                        return "Platinum";
                    case 15:
                        return "Colosseum/XD";
                    case 20:
                        return "White";
                    case 21:
                        return "Black";
                    case 22:
                        return "White 2";
                    case 23:
                        return "Black 2";
                    case 24:
                        return "X";
                    case 25:
                        return "Y";
                    default:
                        return "Game received";
                }
            }
            set
            {
                switch (value)
                {
                    case "Game received":
                        data[0x4] = 0;
                        break;
                    case "Ruby":
                        data[0x4] = 1;
                        break;
                    case "Sapphire":
                        data[0x4] = 2;
                        break;
                    case "Emerald":
                        data[0x4] = 3;
                        break;
                    case "Fire Red":
                        data[0x4] = 4;
                        break;
                    case "Leaf Green":
                        data[0x4] = 5;
                        break;
                    case "Heart Gold":
                        data[0x4] = 7;
                        break;
                    case "Soul Silver":
                        data[0x4] = 8;
                        break;
                    case "Diamond":
                        data[0x4] = 10;
                        break;
                    case "Pearl":
                        data[0x4] = 11;
                        break;
                    case "Platinum":
                        data[0x4] = 12;
                        break;
                    case "Colosseum/XD":
                        data[0x4] = 15;
                        break;
                    case "White":
                        data[0x4] = 20;
                        break;
                    case "Black":
                        data[0x4] = 21;
                        break;
                    case "White 2":
                        data[0x4] = 22;
                        break;
                    case "Black 2":
                        data[0x4] = 23;
                        break;
                    case "X":
                        data[0x4] = 24;
                        break;
                    case "Y":
                        data[0x4] = 25;
                        break;
                    default:
                        data[0x4] = 0;
                        break;
                }
            }
        }

        public ushort sid
        {
            get
            {
                return Func.getUInt16(data, 0x2);
            }
            set
            {
                Array.Copy(BitConverter.GetBytes(value), 0, data, 0x2, 2);
            }
        }

        public ushort id
        {
            get
            {
                return Func.getUInt16(data, 0);
            }
            set
            {
                Array.Copy(BitConverter.GetBytes(value), 0, data, 0, 2);
            }
        }

        public string cardItem
        {
            get
            {
                return PkmLib.items[id];
            }
            set
            {
                Array.Copy(BitConverter.GetBytes((ushort)PkmLib.items.IndexOf(value)), 0, data, 0, 2);
            }
        }

        public string cardPower
        {
            get
            {
                return PkmLib.passPowers[id];
            }
            set
            {
                Array.Copy(BitConverter.GetBytes((ushort)PkmLib.passPowers.IndexOf(value)), 0, data, 0, 2);
            }
        }

        public bool isEmpty
        {
            get
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] != 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Get Wonder Card data as a string[] log
        /// </summary>
        /// <returns>string[] log containing Wonder Card data</returns>
        public string[] getLog()
        {
            switch (type)
            {
                case CardType.Pokemon:
                    return new string[] { "Wonder Card contains Pokemon data:", species, "Level: " + (level == 0 ? "Random" : Convert.ToString(level)), "Item: " + pkmItem, "Nature: " + nature, "ID/SID " + id + "/" + sid, "Version: " + version, "OT: " + ot, "Location " + locationMet, (cardUsed ? "This Wonder Card has been used" : "This Wonder Card hasn't been used") };
                case CardType.Item:
                    return new string[] { "Wonder Card contains Item data:", "Item to be delivered: " + cardItem };
                case CardType.Power:
                    return new string[] { "Wonder Card contains Pass Power data:", "Pass Power to be delivered: " + cardPower };
                default:
                    return new string[] { "Empty Slot" };
            }
        }
    }
}
