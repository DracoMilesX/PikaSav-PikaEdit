using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit_Lib
{
    /// <summary>
    /// Contains Gen 4 Save file Trainer Data
    /// </summary>
    public class TrainerInfoGen4
    {
        public readonly int NAMEMAXLENGTH = 7;
        public enum TrainerGender
        {
            Male,
            Female
        }
        public string name;
        public ushort id;
        public ushort sid;
        public uint money;
        public TrainerGender gender;
        public byte badges;
        public byte hgssbadges;

        public TrainerInfoGen4()
        {
            name = "";
            id = 1;
            sid = 0;
            money = 0;
            gender = TrainerGender.Male;
            badges = 0;
        }

        public TrainerInfoGen4(string name, ushort id, ushort sid, uint money, byte gender, byte badges, byte hgssbadges = 0)
        {
            this.name = name;
            this.id = id;
            this.sid = sid;
            this.money = money;
            this.gender = (gender == 0 ? TrainerGender.Male : TrainerGender.Female);
            this.badges = badges;
            this.hgssbadges = hgssbadges;
        }

        public byte[] getName()
        {
            return Func.getfromStringIngame(name, NAMEMAXLENGTH);
        }

        public byte getGenderByte()
        {
            if (gender == TrainerGender.Male)
            {
                return 0;
            }
            return 1;
        }


        public bool[] getBadgesObtained()
        {
            return new bool[] { (badges & 1) == 1, ((badges >> 1) & 1) == 1, ((badges >> 2) & 1) == 1, ((badges >> 3) & 1) == 1, ((badges >> 4) & 1) == 1, ((badges >> 5) & 1) == 1, ((badges >> 6) & 1) == 1, ((badges >> 7) & 1) == 1 };
        }

        public bool[] getHGSSBadgesObtained()
        {
            return new bool[] { (hgssbadges & 1) == 1, ((hgssbadges >> 1) & 1) == 1, ((hgssbadges >> 2) & 1) == 1, ((hgssbadges >> 3) & 1) == 1, ((hgssbadges >> 4) & 1) == 1, ((hgssbadges >> 5) & 1) == 1, ((hgssbadges >> 6) & 1) == 1, ((hgssbadges >> 7) & 1) == 1 };
        }

        public bool[] getAllBadgesObtained()
        {
            return new bool[] { (badges & 1) == 1, ((badges >> 1) & 1) == 1, ((badges >> 2) & 1) == 1, ((badges >> 3) & 1) == 1, ((badges >> 4) & 1) == 1, ((badges >> 5) & 1) == 1, ((badges >> 6) & 1) == 1, ((badges >> 7) & 1) == 1, (hgssbadges & 1) == 1, ((hgssbadges >> 1) & 1) == 1, ((hgssbadges >> 2) & 1) == 1, ((hgssbadges >> 3) & 1) == 1, ((hgssbadges >> 4) & 1) == 1, ((hgssbadges >> 5) & 1) == 1, ((hgssbadges >> 6) & 1) == 1, ((hgssbadges >> 7) & 1) == 1 };
        }

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
            b = getHGSSBadgesObtained();
            for (int i = 0; i < 8; i++)
            {
                if (b[i])
                {
                    c++;
                }
            }
            return c;
        }

        public void setBadges(bool[] b)
        {
            badges = 0;
            hgssbadges = 0;
            byte[] c = new byte[b.Length];
            for (int i = 0; i < b.Length; i++)
            {
                c[i] = (b[i] ? (byte)1 : (byte)0);
            }
            if (b.Length == 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    badges = (byte)(badges | (c[i] << i));
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    badges = (byte)(badges | (c[i] << i));
                }
                for (int i = 0; i < 8; i++)
                {
                    hgssbadges = (byte)(hgssbadges | (c[i + 8] << i));
                }
            }
        }
    }
}
