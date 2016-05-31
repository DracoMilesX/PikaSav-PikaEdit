using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit_Lib
{
    /// <summary>
    /// Represents a Save file Day-Care data
    /// </summary>
    public class DayCareGen4
    {
        /// <summary>
        /// Pokemon data stored in the Day-Care
        /// </summary>
        public PokemonGen4[] pkmdata = new PokemonGen4[2];
        /// <summary>
        /// Represents if the Day-Care has an egg waiting to be delivered
        /// </summary>
        public bool hasEgg;

        public DayCareGen4()
        {
            pkmdata[0] = new PokemonGen4();
            pkmdata[1] = new PokemonGen4();
            hasEgg = false;
        }

        public DayCareGen4(PokemonGen4[] pkmdata, bool hasEgg = false)
        {
            this.pkmdata = pkmdata;
            this.hasEgg = hasEgg;
        }

        public DayCareGen4(PokemonGen4 pkm1, PokemonGen4 pkm2, bool hasEgg = false)
        {
            pkmdata[0] = pkm1;
            pkmdata[1] = pkm2;
            this.hasEgg = hasEgg;
        }

        public DayCareGen4(PokemonGen4 pkm1, PokemonGen4 pkm2, byte hasEgg = 0)
        {
            pkmdata[0] = pkm1;
            pkmdata[1] = pkm2;
            this.hasEgg = hasEgg == 1;
        }

        /// <summary>
        /// Get the count of Pokemon stored
        /// </summary>
        /// <returns>Number of non-empty Pokemon stored</returns>
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
