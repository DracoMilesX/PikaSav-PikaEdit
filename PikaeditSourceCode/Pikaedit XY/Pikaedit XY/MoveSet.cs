using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikaedit_XY
{
    /// <summary>
    /// Represents a Pokemon move slot including PP and PP Ups
    /// </summary>
    public class Move
    {
        /// <summary>
        /// Pokemon move value
        /// </summary>
        public ushort move;
        /// <summary>
        /// Current PP
        /// </summary>
        public byte pp;
        /// <summary>
        /// PP Ups used on this move
        /// </summary>
        public byte ppUp;

        public Move()
        {
            move = 0;
            pp = 0;
            ppUp = 0;
        }

        public Move(ushort move, byte pp, byte ppUp)
        {
            this.move = move;
            this.pp = pp;
            this.ppUp = ppUp;
        }

        public Move(string move, byte pp, byte ppUp)
        {
            if (PkmLib.moves.IndexOf(move) < PkmLib.moves.Count)
            {
                this.move = (ushort)PkmLib.moves.IndexOf(move);
            }
            else
            {
                this.move = 0;
            }
            this.pp = pp;
            this.ppUp = ppUp;
        }
    }

    /// <summary>
    /// Contains a Pokemon moveset (Move index number, Current PP, PP Ups)
    /// </summary>
    public class MoveSet
    {
        public Move move1;
        public Move move2;
        public Move move3;
        public Move move4;

        public MoveSet()
        {
            this.move1 = new Move();
            this.move2 = new Move();
            this.move3 = new Move();
            this.move4 = new Move();
        }

        public MoveSet(Move move1, Move move2, Move move3, Move move4)
        {
            this.move1 = move1;
            this.move2 = move2;
            this.move3 = move3;
            this.move4 = move4;
        }

        /// <summary>
        /// Return a bool indicating if the pokemon moveset is empty (All moves are 0)
        /// </summary>
        /// <returns>true if all moves are "None", false if there is at least one move</returns>
        public bool isEmpty()
        {
            return (move1.move == 0 && move2.move == 0 && move3.move == 0 && move4.move == 0);
        }
    }
}
