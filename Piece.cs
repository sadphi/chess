using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace chess
{
    /// <summary>
    /// An abstract class describing a chess piece.
    /// </summary>
    public abstract class Piece
    {
        protected int id;
        protected int value;
        protected (int, int) tile;                   //Coordinate of current tile
        protected Vector2 boardPos;                  //The "real" position (used to move/draw tile)
        protected (int, int) prevTile;               //The tile where this piece was previously standing
        protected List<(int, int)> possibleMoves;   //The tiles this piece can move to
        protected Texture2D texture;
        protected PieceColor color;
        protected string name;
        protected string notation;

        public Piece(Texture2D texture, PieceColor color, (int, int) pos)
        {
            this.color = color;
            this.texture = texture;
            Move(pos);

            possibleMoves = new List<(int, int)>();
        }

        /// <summary>
        /// Update this piece.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            if (tile != prevTile)
            {
                possibleMoves = FindPossibleMoves();
            }
        }

        /// <summary>
        /// Draw this piece.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //TODO: Implement usage of PieceColor
            spriteBatch.Draw(texture, boardPos, Color.White);
        }

        /// <summary>
        /// Move this piece to a specified tile, if possible.
        /// Does nothing otherwise.
        /// </summary>
        /// <param name="pos">The tile coordinate to move to.</param>
        /// <returns>true if move was made, false otherwise.</returns> 
        public bool Move((int, int) pos)
        {
            if (possibleMoves == null) //Collection is always null when the piece spawns for the first time
            {
                SetBoardPosition(pos);
                return true;
            }

            else if (possibleMoves.Contains(pos))
            {
                SetBoardPosition(pos);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Set the Vector2 position of this piece. ("Real" position)
        /// </summary>
        private void SetBoardPosition((int, int) pos)
        {
            tile = pos;
            Rectangle t = Board.GetTileAtIndex(pos).Position; //Get the position of this tile
            boardPos = new Vector2(t.Center.X - texture.Width / 2, t.Center.Y - texture.Height / 2);
        }

        /// <summary>
        /// Calculate which tiles this piece can move to.
        /// </summary>
        /// <returns>A collection of possible tiles. Is empty if no moves can be made.</returns>
        abstract protected List<(int, int)> FindPossibleMoves();

        public Vector2 Pos
        {
            get => boardPos;
        }

        public (int, int) TileCoordinate
        {
            get => tile;
        }

        public List<(int, int)> PossibleMoves
        {
            get => possibleMoves;
        }

        public int Value
        {
            get => value;
        }

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
