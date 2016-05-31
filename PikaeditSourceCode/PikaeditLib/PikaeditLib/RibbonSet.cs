using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit_Lib
{
    /// <summary>
    /// Represents a Ribbon structure ushort from the pkm data
    /// </summary>
    public class RibbonSet
    {
        public ushort data = 0;

        public RibbonSet()
        {

        }

        public RibbonSet(ushort data)
        {
            this.data = data;
        }

        /// <summary>
        /// Set flags and calculate data value
        /// </summary>
        /// <param name="flags">bool[] containing which ribbons are active</param>
        public void setChanges(bool[] flags)
        {
            ushort[] c = new ushort[flags.Length];
            for (int i = 0; i < flags.Length; i++)
            {
                c[i] = (flags[i] ? (ushort)1 : (ushort)0);
            }
            for (int i = 0; i < flags.Length; i++)
            {
                this.data = (ushort)(this.data | (c[i] << i));
            }
        }


        /// <summary>
        /// Get Ribbon flags from the data value
        /// </summary>
        /// <returns>bool[] representing which bits are active</returns>
        public bool[] getFlags()
        {
            bool[] flags = new bool[16];
            for (int i = 0; i < 16; i++)
            {
                flags[i] = ((data >> i) & 1) == 1;
            }
            return flags;
        }
    }
}
