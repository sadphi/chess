using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace chess
{
    public abstract class Piece
    {
        protected int id;
        protected int value;
        protected (int, int) pos;
        protected (int, int) oldPos;
        protected (int, int)[] potentialPos; //The positions this piece can move to
        protected Texture2D texture;
        protected PieceColor color;
        protected string name;
        protected string notation;

        public Piece(PieceColor color)
        {
            this.color = color;
        }

        public void Update(GameTime gameTime)
        {
            if (pos != oldPos)
            {
                potentialPos = CalculateMovePotential();
            }
        }

        abstract public void Move((int, int) pos);

        abstract public (int, int)[] CalculateMovePotential();

    }

    public struct PieceColor
    {
        public PieceColor(string color)
        {
            Color = color;
        }

        public string Color { get; private set; }
    }
}
