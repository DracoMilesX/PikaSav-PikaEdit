using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit
{
    /// <summary>
    /// Represents a Save file Day-Care data
    /// </summary>
    public class DayCarePKM
    {
        /// <summary>
        /// Pokemon data stored in the Day-Care
        /// </summary>
        public Pokemon[] pkmdata = new Pokemon[2];
        /// <summary>
        /// Represents if the Day-Care has an egg waiting to be delivered
        /// </summary>
        public bool hasEgg;

        public DayCarePKM()
        {
            pkmdata[0] = new Pokemon();
            pkmdata[1] = new Pokemon();
            hasEgg = false;
        }

        public DayCarePKM(Pokemon[] pkmdata, bool hasEgg=false)
        {
            this.pkmdata = pkmdata;
            this.hasEgg = hasEgg;
        }

        public DayCarePKM(Pokemon pkm1, Pokemon pkm2, bool hasEgg = false)
        {
            pkmdata[0] = pkm1;
            pkmdata[1] = pkm2;
            this.hasEgg = hasEgg;
        }

        public DayCarePKM(Pokemon pkm1, Pokemon pkm2, byte hasEgg = 0)
        {
            pkmdata[0] = pkm1;
            pkmdata[1] = pkm2;
            this.hasEgg = hasEgg==1;
        }

        /// <summary>
        /// Get the count of Pokemon stored
        /// </summary>
        /// <returns>Number of non-empty pokemon stored</returns>
        public byte getCount()
        {
            byte i = 0;
            for (int j = 0; j < 2; j++)
            {
                if (pkmdata[j] != null)
                {
                    if (!pkmdata[j].isEmpty)
                    {
                        i++;
                    }
                }
            }
            return i;
        }

        /// <summary>
        /// Represent hasEgg bool as a byte
        /// </summary>
        /// <returns>byte value of hasEgg as 1 or 0</returns>
        public byte getEggByte()
        {
            return (hasEgg ? (byte)1 : (byte)0);
        }
    }
}
