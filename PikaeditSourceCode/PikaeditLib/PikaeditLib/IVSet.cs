using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit_Lib
{
    /// <summary>
    /// Represents the uint that represent ivs, isEgg and isNicknamed flags
    /// </summary>
    public class IVSet
    {
        public byte hp;
        public byte atk;
        public byte def;
        public byte spa;
        public byte spd;
        public byte spe;
        /// <summary>
        /// Pokemon is an egg flag
        /// </summary>
        public bool isEgg;

        /// <summary>
        /// Pokemon is Nicknamed flag
        /// </summary>
        public bool isNick;

        public IVSet()
        {

        }

        public IVSet(uint iv)
        {
            this.hp = (byte)((iv >> (0)) & 0x1f);
            this.atk = (byte)((iv >> (5)) & 0x1f);
            this.def = (byte)((iv >> (10)) & 0x1f);
            this.spe = (byte)((iv >> (15)) & 0x1f);
            this.spa = (byte)((iv >> (20)) & 0x1f);
            this.spd = (byte)((iv >> (25)) & 0x1f);
            this.isEgg = ((iv >> (30)) & 0x1) == 1;
            this.isNick = ((iv >> (31)) & 0x1) == 1;
        }

        public IVSet(byte hp, byte atk, byte def, byte spa, byte spd, byte spe, bool isEgg=false, bool isNick =false)
        {
            this.hp = hp;
            this.atk = atk;
            this.def = def;
            this.spa = spa;
            this.spd = spd;
            this.spe = spe;
            this.isEgg = isEgg;
            this.isNick = isNick;
        }

        /// <summary>
        /// Return ivs and flags as a uint used in pkm files
        /// </summary>
        /// <returns>uint value representing the ivs and flags stored</returns>
        public uint getIV()
        {
            return (uint)((isNick ? 1 << 31 : 0) | (isEgg ? 1 << 30 : 0) | (spd << 25) | (spa << 20) | (spe << 15) | (def << 10) | (atk << 5) | hp);
        }
    }
}
