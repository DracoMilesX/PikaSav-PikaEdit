using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit_Lib
{
    /// <summary>
    /// Static class used on Gen 5 PID Generator, can be used to generate a Gen 5 PID
    /// </summary>
    public static class PidGen
    {
        private static uint seed=0;
        private static Random r = new Random();
        private static ushort pidLow;
        private static ushort pidHigh;
        private static GenderValue ratio;
        /// <summary>
        /// This variable will have the PID generated
        /// </summary>
        public static uint finalPID;

        #region seedMethods
        /// <summary>
        /// Initialize seed
        /// </summary>
        private static void srand(uint newSeed)
        {
            seed = newSeed;
        }

        /// <summary>
        /// Call the next RNG
        /// </summary>
        private static UInt32 rand()
        {
            seed = ((0x41c64e6d * seed + 0x6073)) & 0xFFFFFFFF;
            return seed >> 16;
        }

        /// <summary>
        /// Call the previous RNG
        /// </summary>
        private static UInt32 prand()
        {
            seed = (seed * 0xeeb9eb65 + 0xa3561a1) & 0xFFFFFFFF;
            return seed >> 16;
        }
        #endregion

        /// <summary>
        /// PID Generation Types
        /// </summary>
        public enum PIDTypes
        {
            Egg,
            Wild,
            Dw,
            Event,
            /// <summary>
            /// Dynamic shiny Wonder Cards
            /// </summary>
            Shiny_Event,
            /// <summary>
            /// Pokemon that cannot be shiny
            /// </summary>
            Shiny_Locked
        }

        /// <summary>
        /// Shiny generation possibilities
        /// </summary>
        public enum Shininess
        {
            Normal,
            Possible,
            Shiny
        }

        /// <summary>
        /// Values used to determine gender on the PID based on the species gender ratio
        /// </summary>
        public enum GenderValue
        {
            Genderless=255,
            Always_Male=254,
            Always_Female=0,
            Half=128, //50%
            Mostly_Male=64, //87.5% Male
            Almost_Male=32, //75% Male
            Mostly_Female=192, //75% Female
        }

        /// <summary>
        /// Generate a Gen 5 PID depending on the type
        /// </summary>
        /// <param name="type">Type of PID to generate (PidGen.PIDTypes)</param>
        /// <param name="shiny">Determine if the PID can result a shiny or normal pokemon</param>
        /// <param name="ability">Define ability index 0-1, 2 = DW and will give a random byte between of 0 or 1</param>
        /// <param name="gender">0 Male, 1 Female, 2 Genderless</param>
        /// <param name="pkm">Pokemon which will have the PID generated</param>
        public static uint generate(PIDTypes type, Shininess shiny, int ability, int gender, Pokemon pkm)
        {
            ratio = (GenderValue)Enum.Parse(typeof(GenderValue), Enum.GetName(typeof(PkmLib.GenderRatios), pkm.genderRatio));
            if (ability == 2)
            {
                ability = r.Next(0, 2);
            }
            switch (type)
            {
                case PIDTypes.Egg:
                    finalPID = eggType(shiny, ability, gender, pkm);
                    break;
                case PIDTypes.Wild:
                    finalPID = wildType(shiny, ability, gender, pkm);
                    break;
                case PIDTypes.Dw:
                    finalPID = dwType(shiny, ability, gender, pkm);
                    break;
                case PIDTypes.Event:
                    finalPID = eventType(shiny, ability, gender, pkm);
                    break;
                case PIDTypes.Shiny_Event:
                    finalPID = shinyDynamicType(shiny, ability, gender, pkm);
                    break;
                case PIDTypes.Shiny_Locked:
                    finalPID = shinyLockedType(shiny, ability, gender, pkm);
                    break;
            }
            return finalPID;
        }

        private static uint eggType(Shininess shiny, int ability, int gender,Pokemon pkm)
        {
            bool pass = false;
            uint pid = 0;
            while (!pass)
            {
                pid = (uint)(r.Next(0, 0x7FFFFFFF) | (r.Next(0, 2) << 31));
                if (ability == 0)
                {
                    pid = pid & 0xFFFEFFFF;
                }
                else
                {
                    pid = pid | 0x10000;
                }
                bool genderPass = false;
                switch (gender)
                {
                    case 0:
                        genderPass = ((pid % 0x100) > (int)ratio);
                        break;
                    case 1:
                        genderPass = ((pid % 0x100) <= (int)ratio);
                        break;
                    case 2:
                        genderPass = true;
                        break;
                }
                if (genderPass)
                {
                    //Check shiny stuff
                    if (shiny == Shininess.Possible)
                    {
                        pass = true;
                    }
                    else
                    {
                        pidLow = (ushort)(pid & 0xFFFF);
                        pidHigh = (ushort)(pid >> 16);
                        ushort sv = (ushort)(pidLow ^ pidHigh ^ pkm.id ^ pkm.sid);
                        if (shiny == Shininess.Shiny)
                        {
                            pass = (sv < 8);
                        }
                        if (shiny == Shininess.Normal)
                        {
                            pass = (sv >= 8);
                        }
                    }
                }
            }
            return pid;
        }

        private static uint xorPid(uint pid, ushort id, ushort sid)
        {
            byte uPid = (byte)(pid >> 31);
            byte lPid = (byte)(pid & 1);
            byte lId = (byte)(id & 1);
            byte lSid = (byte)(sid & 1);
            if ((uPid ^ lPid ^ lId ^ lSid) == 1)
            {
                return pid ^ 0x80000000;
            }
            return pid;
        }

        private static uint wildType(Shininess shiny, int ability, int gender, Pokemon pkm)
        {
            bool pass = false;
            uint pid = 0;
            while (!pass)
            {
                pid = (uint)(r.Next(0, 0x7FFFFFFF) | (r.Next(0, 2) << 31));
                if (ability == 0)
                {
                    pid = pid & 0xFFFEFFFF;
                }
                else
                {
                    pid = pid | 0x10000;
                }
                pid = xorPid(pid, pkm.id, pkm.sid);
                bool genderPass = false;
                switch (gender)
                {
                    case 0:
                        genderPass = ((pid % 0x100) > (int)ratio);
                        break;
                    case 1:
                        genderPass = ((pid % 0x100) <= (int)ratio);
                        break;
                    case 2:
                        genderPass = true;
                        break;
                }
                if (genderPass)
                {
                    //Check shiny stuff
                    if (shiny == Shininess.Possible)
                    {
                        pass = true;
                    }
                    else
                    {
                        pidLow = (ushort)(pid & 0xFFFF);
                        pidHigh = (ushort)(pid >> 16);
                        ushort sv = (ushort)(pidLow ^ pidHigh ^ pkm.id ^ pkm.sid);
                        if (shiny == Shininess.Shiny)
                        {
                            pass = (sv < 8);
                        }
                        if (shiny == Shininess.Normal)
                        {
                            pass = (sv >= 8);
                        }
                    }
                }
            }
            return pid;
        }

        private static uint dwType(Shininess shiny, int ability, int gender, Pokemon pkm)
        {
            bool pass = false;
            uint pid = 0;
            while (!pass)
            {
                pid = (uint)(r.Next(0, 0x7FFFFFFF) | (r.Next(0, 2) << 31));
                if (ability == 0)
                {
                    pid = pid & 0xFFFEFFFF;
                }
                else
                {
                    pid = pid | 0x10000;
                }
                //pid = pid ^ 0x10000;
                pid = xorPid(pid, pkm.id, pkm.sid);
                bool genderPass = false;
                switch (gender)
                {
                    case 0:
                        genderPass = ((pid % 0x100) > (int)ratio);
                        break;
                    case 1:
                        genderPass = ((pid % 0x100) <= (int)ratio);
                        break;
                    case 2:
                        genderPass = true;
                        break;
                }
                if (genderPass)
                {
                    //Check shiny stuff
                    if (shiny == Shininess.Possible)
                    {
                        pass = false;
                    }
                    else
                    {
                        pidLow = (ushort)(pid & 0xFFFF);
                        pidHigh = (ushort)(pid >> 16);
                        ushort sv = (ushort)(pidLow ^ pidHigh ^ pkm.id ^ pkm.sid);
                        if (shiny == Shininess.Shiny)
                        {
                            pass = false;
                        }
                        if (shiny == Shininess.Normal)
                        {
                            pass = (sv >= 8);
                        }
                    }
                }
            }
            return pid;
        }

        private static uint eventType(Shininess shiny, int ability, int gender, Pokemon pkm)
        {
            bool pass = false;
            uint pid = 0;
            while (!pass)
            {
                pid = (uint)(r.Next(0, 0x7FFFFFFF) | (r.Next(0, 2) << 31));
                if (ability == 0)
                {
                    pid = pid & 0xFFFEFFFF;
                }
                else
                {
                    pid = pid | 0x10000;
                }
                //pid = pid ^ 0x10000;
                pid = xorPid(pid, pkm.id, pkm.sid);
                bool genderPass = false;
                switch (gender)
                {
                    case 0:
                        genderPass = ((pid % 0x100) > (int)ratio);
                        break;
                    case 1:
                        genderPass = ((pid % 0x100) <= (int)ratio);
                        break;
                    case 2:
                        genderPass = true;
                        break;
                }
                if (genderPass)
                {
                    //Check shiny stuff
                    if (shiny == Shininess.Possible)
                    {
                        pass = true;
                    }
                    else
                    {
                        pidLow = (ushort)(pid & 0xFFFF);
                        pidHigh = (ushort)(pid >> 16);
                        ushort sv = (ushort)(pidLow ^ pidHigh ^ pkm.id ^ pkm.sid);
                        if (shiny == Shininess.Shiny)
                        {
                            pass = (sv < 8);
                        }
                        if (shiny == Shininess.Normal)
                        {
                            pass = (sv >= 8);
                        }
                    }
                }
            }
            return pid;
        }

        private static uint shinyDynamicType(Shininess shiny, int ability, int gender, Pokemon pkm)
        {
            bool pass = false;
            uint pid = 0;
            while (!pass)
            {
                pid = (uint)(pid | ((uint)(pkm.id & 0xFF00) << 16));
                pid = (uint)(pid ^ (r.Next(0, 256) << 16));
                pid = (uint)(pid | ((uint)(pkm.sid & 0xFF00) << 8));
                pid = (uint)(pid ^ (r.Next(0, 256)));
                if (ability == 0)
                {
                    pid = pid & 0xFFFEFFFF;
                }
                else
                {
                    pid = pid | 0x10000;
                }
                bool genderPass = false;
                switch (gender)
                {
                    case 0:
                        genderPass = ((pid % 0x100) > (int)ratio);
                        break;
                    case 1:
                        genderPass = ((pid % 0x100) <= (int)ratio);
                        break;
                    case 2:
                        genderPass = true;
                        break;
                }
                if (genderPass)
                {
                    //Check shiny stuff
                    if (shiny == Shininess.Possible)
                    {
                        pass = false;
                    }
                    else
                    {
                        pidLow = (ushort)(pid & 0xFFFF);
                        pidHigh = (ushort)(pid >> 16);
                        ushort sv = (ushort)(pidLow ^ pidHigh ^ pkm.id ^ pkm.sid);
                        if (shiny == Shininess.Shiny)
                        {
                            pass = (sv < 8);
                        }
                        if (shiny == Shininess.Normal)
                        {
                            pass = false;
                        }
                    }
                }
            }
            return pid;
        }

        private static uint shinyLockedType(Shininess shiny, int ability, int gender, Pokemon pkm)
        {
            bool pass = false;
            uint pid = 0;
            while (!pass)
            {
                pid = (uint)(r.Next(0, 0x7FFFFFFF) | (r.Next(0, 2) << 31));
                if (ability == 0)
                {
                    pid = pid & 0xFFFEFFFF;
                }
                else
                {
                    pid = pid | 0x10000;
                }
                pid = pid ^ 0xFFFF0000;
                pidLow = (ushort)(pid & 0xFFFF);
                pidHigh = (ushort)(pid >> 16);
                if ((pidLow ^ pidHigh ^ pkm.id ^ pkm.sid) < 8)
                {
                    pid = pid ^ 0x10000000;
                }
                bool genderPass = false;
                switch (gender)
                {
                    case 0:
                        genderPass = ((pid % 0x100) > (int)ratio);
                        break;
                    case 1:
                        genderPass = ((pid % 0x100) <= (int)ratio);
                        break;
                    case 2:
                        genderPass = true;
                        break;
                }
                if (genderPass)
                {
                    //Check shiny stuff
                    if (shiny == Shininess.Possible)
                    {
                        pass = false;
                    }
                    else
                    {
                        pidLow = (ushort)(pid & 0xFFFF);
                        pidHigh = (ushort)(pid >> 16);
                        ushort sv = (ushort)(pidLow ^ pidHigh ^ pkm.id ^ pkm.sid);
                        if (shiny == Shininess.Shiny)
                        {
                            pass = false;
                        }
                        if (shiny == Shininess.Normal)
                        {
                            pass = (sv >= 8);
                        }
                    }
                }
            }
            return pid;
        }
    }
}
