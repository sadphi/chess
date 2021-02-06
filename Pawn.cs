﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    class Pawn : Piece
    {
        public Pawn(Texture2D texture, PieceColor color, (int, int) pos) : base(texture, color, pos)
        {
            name = "Pawn";
            notation = "P";
            value = 1;
        }

        protected override List<(int, int)> FindPossibleMoves()
        {
            //throw new NotImplementedException();
            var r = new List<(int, int)>();
            r.Add((0, 0));
            return r;
        }
    }
}
