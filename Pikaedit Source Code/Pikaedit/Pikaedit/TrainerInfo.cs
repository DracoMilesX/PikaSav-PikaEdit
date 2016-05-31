using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit
{
    /// <summary>
    /// Contains Save file Trainer Data
    /// </summary>
    public class TrainerInfo
    {
        /// <summary>
        /// Maximum character length for trainer name
        /// </summary>
        public readonly int NAMEMAXLENGTH = 7;

        /// <summary>
        /// Represents trainer gender
        /// </summary>
        public enum TrainerGender
        {
            Male,
            Female
        }
        /// <summary>
        /// Trainer name
        /// </summary>
        public string name;
        /// <summary>
        /// Trainer ID
        /// </summary>
        public ushort id;
        /// <summary>
        /// Trainer Secret ID
        /// </summary>
        public ushort sid;
        /// <summary>
        /// Trainer's money
        /// </summary>
        public uint money;
        /// <summary>
        /// Trainer's gender
        /// </summary>
        public TrainerGender gender;
        /// <summary>
        /// Obtained badges as byte, each bit represents a badge
        /// </summary>
        public byte badges;
        /// <summary>
        /// Playtime hours
        /// </summary>
        public ushort playHours;
        /// <summary>
        /// Playtime minutes
        /// </summary>
        public byte playMin;
        /// <summary>
        /// Playtime seconds
        /// </summary>
        public byte playSec;

        public TrainerInfo()
        {
            name = "";
            id = 1;
            sid = 0;
            money = 0;
            gender = TrainerGender.Male;
            badges = 0;
        }

        /// <summary>
        /// Initialize TrainerInfo object
        /// </summary>
        /// <param name="name">Trainer name</param>
        /// <param name="id">Trainer ID</param>
        /// <param name="sid">Trainer SID</param>
        /// <param name="money">Money held</param>
        /// <param name="gender">Trainer gender</param>
        /// <param name="badges">Current badges</param>
        /// <param name="playHours">Playtime hours (Max 999)</param>
        /// <param name="playMin">Playtime minutes (Max 59)</param>
        /// <param name="playSec">Playtime seconds (Max 59)</param>
        public TrainerInfo(string name, ushort id, ushort sid, uint money, byte gender, byte badges, ushort playHours, byte playMin, byte playSec)
        {
            this.name = name;
            this.id = id;
            this.sid = sid;
            this.money = money;
            this.gender = (gender == 0 ? TrainerGender.Male : TrainerGender.Female);
            this.badges = badges;
            this.playHours = playHours;
            this.playMin = playMin;
            this.playSec = playSec;
        }

        /// <summary>
        /// Get Trainer name as byte array
        /// </summary>
        /// <returns>byte[] containing trainer name</returns>
        public byte[] getName()
        {
            return Func.getfromString(name, NAMEMAXLENGTH);
        }

        /// <summary>
        /// Get Trainer gender represented as a byte
        /// </summary>
        /// <returns>0 if Trainer gender is male, 1 if it is female</returns>
        public byte getGenderByte()
        {
            if (gender == TrainerGender.Male)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// Get badges obtained
        /// </summary>
        /// <returns>bool[] indicating which badges have been obtained and which not</returns>
        public bool[] getBadgesObtained()
        {
            return new bool[] { (badges & 1) == 1, ((badges >> 1) & 1) == 1, ((badges >> 2) & 1) == 1, ((badges >> 3) & 1) == 1, ((badges >> 4) & 1) == 1, ((badges >> 5) & 1) == 1, ((badges >> 6) & 1) == 1, ((badges >> 7) & 1) == 1 };
        }

        /// <summary>
        /// Get badges obtained count
        /// </summary>
        /// <returns>Number of badges obtained</returns>
        public int badgeCount()
        {
            int c = 0;
            bool[] b = getBadgesObtained();
            for (int i = 0; i < 8; i++)
            {
                if (b[i])
                {
                    c++;
                }
            }
            return c;
        }

        /// <summary>
        /// Set the badges byte using a bool[]
        /// </summary>
        /// <param name="b">Badges flags</param>
        public void setBadges(bool[] b)
        {
            badges = 0;
            byte[] c = new byte[b.Length];
            for (int i = 0; i < b.Length; i++)
            {
                c[i] = (b[i] ? (byte)1 : (byte)0);
            }
            for (int i = 0; i < 8; i++)
            {
                badges = (byte)(badges | (c[i] << i));
            }
        }
    }
}
