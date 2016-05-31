using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit_Lib
{
    /// <summary>
    /// Represents a uint in XY pkx data which contains Super Training Regiments completed Flags
    /// </summary>
    public class SuperTraining
    {
        public uint data;

        public SuperTraining()
        {

        }

        public SuperTraining(uint data)
        {
            this.data = data;
        }

        /// <summary>
        /// Set flags and calculate data value
        /// </summary>
        /// <param name="flags">bool[] containing which regiments have been completed</param>
        public void setChanges(bool[] flags)
        {
            uint[] c = new uint[flags.Length];
            for (int i = 0; i < flags.Length; i++)
            {
                c[i] = (flags[i] ? (uint)1 : (uint)0);
            }
            for (int i = 0; i < flags.Length; i++)
            {
                this.data = (uint)(this.data | (c[i] << (i + 2)));
            }
        }

        /// <summary>
        /// Get Super Training flags from the data value
        /// </summary>
        /// <returns>bool[] representing which bits are active</returns>
        public bool[] getFlags()
        {
            bool[] flags = new bool[30];
            for (int i = 0; i < 30; i++)
            {
                flags[i] = ((data >> (i + 2)) & 1) == 1;
            }
            return flags;
        }
    }
}
