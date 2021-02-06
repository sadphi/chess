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
        protected (int, int) tile;                               //Coordinate of current tile
        protected Vector2 boardPos;                              //The "real" position (used to move/draw tile)
        protected (int, int) prevTile;                           //The tile where this piece was previously standing
        protected IList<(int, int)> potentialTiles;              //The tiles this piece can move to
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

        /// <summary>
        /// Update this piece.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            if (tile != prevTile)
            {
                potentialTiles = CalculatePotentialMoves();
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
            if (potentialTiles == null) //Collection is always null when the piece spawns for the first time
            {
                SetBoardPosition(pos);
                return true;
            }

            else if (potentialTiles.Contains(pos))
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
            Rectangle t = Board.GetTileAtIndex(pos).Position; //Get the position of this tile
            boardPos = new Vector2(t.Center.X - texture.Width / 2, t.Center.Y - texture.Height / 2);
        }

        public Vector2 Pos
        {
            get => boardPos;
        }

        public (int, int) TileCoordinate
        {
            get => tile;
        }

        /// <summary>
        /// Calculate which tiles this piece can move to.
        /// </summary>
        /// <returns>A collection of possible tiles.</returns>
        abstract public IList<(int, int)> CalculatePotentialMoves();

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
