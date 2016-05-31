using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit
{
    public class Pokedex
    {

        //public enum PokedexFunctions
        //{
        //    National,
        //    Form_Viewer,
        //    /// <summary>
        //    /// BW2 Only
        //    /// </summary>
        //    Habitat_Viewer
        //}

        public byte enabledFunctions;
        public byte[] caught;
        public byte[] seenMale;
        public byte[] seenFemale;
        public byte[] seenShinyMale;
        public byte[] seenShinyFemale;
        public byte[] formMale;
        public byte[] formFemale;
        public byte[] languages;

        public Pokedex()
        {

        }

        public Pokedex(byte enabledFunctions, byte[] caught, byte[] seenMale, byte[] seenFemale, byte[] seenShinyMale, byte[] seenShinyFemale, byte[] formMale, byte[] formFemale, byte[] languages)
        {
            this.enabledFunctions = enabledFunctions;
            this.caught = caught;
            this.seenMale = seenMale;
            this.seenFemale = seenFemale;
            this.seenShinyMale = seenShinyMale;
            this.seenShinyFemale = seenShinyFemale;
            this.formMale = formMale;
            this.formFemale = formFemale;
            this.languages = languages;
        }

        public bool[] getPokemonStatus(int index)
        {
            index--;
            bool[] status = new bool[5];
            status[0] = Func.convertFromBitChain(caught)[index];
            status[1] = Func.convertFromBitChain(seenMale)[index];
            status[2] = Func.convertFromBitChain(seenFemale)[index];
            status[3] = Func.convertFromBitChain(seenShinyMale)[index];
            status[4] = Func.convertFromBitChain(seenShinyFemale)[index];
            return status;
        }

        public bool[] getLanguages(int index)
        {
            index--;
            bool[] flags = Func.convertFromBitChain(languages);
            int i = index * 7;
            return Func.boolSubArray(flags, i, 7);
        }

        public bool[] getPokedexFunctions()
        {
            bool[] flags = new bool[3];
            for (int i = 0; i < 3; i++)
            {
                flags[i] = ((enabledFunctions >> i) & 1) == 1;
            }
            return flags;
        }
    }
}
