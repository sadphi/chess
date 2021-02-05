using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace chess
{
    public abstract class Piece
    {
        protected int id;
        protected int value;
        protected (int, int) tile;                        //Coordinate of current tile
        protected Vector2 boardPos;                       //The "real" position (used to move/draw tile)
        protected (int, int) prevTile;                    //The tile where this piece was previously standing
        protected ICollection<(int, int)> potentialTiles; //The tiles this piece can move to
        protected Texture2D texture;
        protected PieceColor color;
        protected string name;
        protected string notation;

        public Piece(Texture2D texture, PieceColor color, (int, int) pos)
        {
            this.color = color;
            this.texture = texture;
            tile = pos;
            Move(pos);

            potentialTiles = new List<(int, int)>();
        }

        public void Update(GameTime gameTime)
        {
            if (tile != prevTile)
            {
                potentialTiles = CalculatePotentialMoves();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //TODO: Implement usage of PieceColor
            spriteBatch.Draw(texture, boardPos, Color.White);
        }

        public void Move((int, int) pos)
        {
            if (potentialTiles == null) //Collection is always null when the piece spawns for the first time
            {
                SetBoardPosition(pos);
            }

            else if (potentialTiles.Contains(pos))
            {
                SetBoardPosition(pos);
            }
        }

        private void SetBoardPosition((int, int) pos)
        {
            Rectangle t = Board.GetTileAtIndex(pos); //Get the center point of this tile
            boardPos = new Vector2(t.Center.X - texture.Width / 2, t.Center.Y - texture.Height / 2);
        }

        abstract public (int, int)[] CalculatePotentialMoves();

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
