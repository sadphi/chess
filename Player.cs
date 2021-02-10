using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    public class Player
    {
        private List<Piece> _pieces = new List<Piece>();
        private List<Piece> _piecesCaptured = new List<Piece>();
        private Piece _selectedPiece;
        private bool  _hasSelectedPiece = false;
        private bool _isMyTurn;
        private int _points;

        public Player(Piece test)
        {
            _pieces.Add(test);
        }

        /// <summary>
        /// Capture a piece from the other player. Removes the piece from the other player's available pieces.
        /// </summary>
        /// <param name="other">The other player.</param>
        /// <param name="piece">The piece to capture.</param>
        /// <returns>The value of the captured piece.</returns>
        private int CapturePiece(Player other, Piece piece)
        {
            other.Pieces.Remove(piece);
            _piecesCaptured.Add(piece);
            return piece.Value;
        }

        public void Update(GameTime gameTime)
        {
            foreach (Piece p in _pieces)
            {
                p.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Piece p in _pieces)
            {
                p.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Try selecting a piece on a tile.
        /// </summary>
        /// <param name="tile">The tile where the piece (is possibly) standing.</param>
        /// <returns>true if a piece could be selected, false otherwise.</returns>
        public bool SelectPiece(Tile tile)
        {
            //Search if a piece standing on this tile.
            Piece toSelect = _pieces.Find(piece => piece.TileCoordinate == tile.Coordinate);
           
            if (toSelect != null)
            {
                Board.SelectedTile = tile;
                Board.HighlightPossibleMoves(toSelect.PossibleMoves, removeHighlight: false);
                _selectedPiece = toSelect;
                _hasSelectedPiece = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Try moving a piece to a tile. Will capture an enemy piece if it already occupies the tile.
        /// </summary>
        /// <param name="tile">The tile to move to.</param>
        /// <param name="other">The other player.</param>
        /// <returns>true if a move was possible, false otherwise.</returns>
        public bool MovePiece(Tile tile, Player other)
        {
            bool isSuccess = _selectedPiece.Move(tile.Coordinate);

            if (isSuccess && other != null)
            {
                //Check if the destination tile has an enemy piece.
                //Capture the piece if there is one.

                Piece toCapture = other.Pieces.Find(piece => piece.TileCoordinate == tile.Coordinate);
                if (toCapture != null) _points += CapturePiece(other, toCapture);
            }

            //Remove highlight from tile, and unselect piece.
            Board.SelectedTile = null;
            Board.HighlightPossibleMoves(_selectedPiece.PossibleMoves, removeHighlight: true);
            _selectedPiece = null;
            _hasSelectedPiece = false;

            return isSuccess;
        }

        public Piece SelectedPiece
        {
            get => _selectedPiece;
            set => _selectedPiece = value;
        }

        public bool HasSelectedPiece
        {
            get => _hasSelectedPiece;
            set => _hasSelectedPiece = value;
        }

        public bool IsMyTurn
        {
            get => _isMyTurn;
            set => _isMyTurn = value;
        }

        public List<Piece> Pieces
        {
            get => _pieces;
        }
    }
}
