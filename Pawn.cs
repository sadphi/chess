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
            // Set to either 1 or -1:
            // 1 is for Light pieces.
            // -1 is for Dark pieces.
            int direction = (owner.Color == ColorChess.Light) ? 1 : -1;
            List<(int, int)> res = new List<(int, int)>();
            
            
            (int, int) next = (tile.Item1 - direction, tile.Item2);
            (int, int) sideRight = (next.Item1, next.Item2 + 1);
            (int, int) sideLeft = (next.Item1, next.Item2 - 1);

            // A move forward is only possible if this piece isn't blocked, by either the owner or the opponent.
            if (!owner.Opponent.HasPieceAtCoordinate(next) && !owner.HasPieceAtCoordinate(next))
            {
                (int, int) nextNext = (next.Item1 - direction, next.Item2);
                res.Add(next);

                // This checks if a second move forward is possible.
                if (!_hasMoved && !owner.Opponent.HasPieceAtCoordinate(nextNext) && !owner.HasPieceAtCoordinate(nextNext))
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
