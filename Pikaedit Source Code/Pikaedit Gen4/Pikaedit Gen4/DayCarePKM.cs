using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit_Gen4
{
    public class DayCarePKM
    {
        public Pokemon[] pkmdata = new Pokemon[2];
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
    }
}
