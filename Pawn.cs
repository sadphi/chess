using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace chess
{
    class Pawn : Piece
    {
        private bool _hasMoved;
        
        public Pawn(Player owner, Texture2D texture, (int, int) pos, ColorChess color) : base(owner, texture, pos, color)
        {
            name = "Pawn";
            notation = "P";
            value = 1;
            _hasMoved = false;
            tile = pos;
        }

        protected override List<(int, int)> FindPossibleMoves()
        {
            List<(int, int)> res = new List<(int, int)>();
            int possibleSteps;

            if (!_hasMoved) possibleSteps = 2;
            else possibleSteps = 1;
            
            
            (int, int) next = (tile.Item1 - 1, tile.Item2);
            (int, int) sideRight = (next.Item1, next.Item2 + 1);
            (int, int) sideLeft = (next.Item1, next.Item2 - 1);

            if (!owner.Opponent.HasPieceAtCoordinate(next))
            {
                (int, int) nextNext = (next.Item1 - 1, next.Item2);
                res.Add(next);

                if (possibleSteps == 2 && !owner.Opponent.HasPieceAtCoordinate(nextNext))
                    res.Add(nextNext);
            }

            // Check if a piece can be captured to the right
            if (owner.Opponent.HasPieceAtCoordinate(sideRight))
            {
                res.Add(sideRight);
            }

            // Check if a piece can be captured to the left
            if (owner.Opponent.HasPieceAtCoordinate(sideLeft))
            {
                res.Add(sideLeft);
            }
            

            return res;
        }

        public override bool Move((int, int) pos)
        {
            if (base.Move(pos))
            {
                _hasMoved = true;
                return true;
            }
            return false;
        }
    }
}
