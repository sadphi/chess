using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace chess
{
    public abstract class Piece
    {
        protected int id;
        protected int value;
        protected Vector2 pos;
        protected Vector2 oldPos;
        protected ICollection<Vector2> potentialPos; //The positions this piece can move to
        protected Texture2D texture;
        protected PieceColor color;
        protected string name;
        protected string notation;

        public Piece(Texture2D texture, PieceColor color)
        {
            this.color = color;
            this.texture = texture;
        }

        public void Update(GameTime gameTime)
        {
            if (pos != oldPos)
            {
                potentialPos = CalculateMovePotential();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, Color.White);
        }

        public void SetPosition(Vector2 newPosition)
        {
            pos = newPosition;
        }

        public void Move(Vector2 pos)
        {
            if (potentialPos.Contains(pos))
                this.pos = pos;
        }

        abstract public Vector2[] CalculateMovePotential();

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
