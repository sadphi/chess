using Microsoft.Xna.Framework;
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
        public Pawn(Texture2D texture, PieceColor color) : base(texture, color)
        {
            name = "Pawn";
            notation = "P";
            value = 1;
        }

        public override Vector2[] CalculateMovePotential()
        {
            throw new NotImplementedException();
        }
    }
}
