using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace chess
{
    public class Player
    {
        private Player _opponent;
        private ColorChess _color;
        private List<Piece> _pieces = new List<Piece>();
        private List<Piece> _piecesCaptured = new List<Piece>();
        private Piece _selectedPiece;
        private bool  _hasSelectedPiece = false;
        private bool _isMyTurn;
        private int _points;

        /// <summary>
        /// When a Player is created, two methods must be called to completed initialization:
        /// 
        /// CreatePieces() must be called to create all the pieces.
        /// 
        /// SetOpponent() must be called to set the opponent to this Player.
        /// </summary>
        public Player(ColorChess color)
        {
            _color = color;
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
        /// Creates all pieces for this player.
        /// </summary>
        public void CreatePieces(Microsoft.Xna.Framework.Content.ContentManager cm)
        {
            switch (_color)
            {
                case ColorChess.Light:
                    
                    CreatePiecesLight(cm);
                    break;
                case ColorChess.Dark:
                    CreatePiecesDark(cm);
                    break;
            }
        }

        /// <summary>
        /// Creates all pieces for the "Light" Player.
        /// </summary>
        private void CreatePiecesLight(Microsoft.Xna.Framework.Content.ContentManager cm)
        {
            string colorSuffix = "light";

            for (int col = 0; col < 8; col++)
            {
                _pieces.Add(new Pawn(this, cm.Load<Texture2D>("pawn_" + colorSuffix), (6, col), _color));
            }
        }

        /// <summary>
        /// Creates all pieces for the "Dark" Player.
        /// </summary>
        private void CreatePiecesDark(Microsoft.Xna.Framework.Content.ContentManager cm)
        {
            string colorSuffix = "dark";

            for (int col = 0; col < 8; col++)
            {
                _pieces.Add(new Pawn(this, cm.Load<Texture2D>("pawn_" + colorSuffix), (1, col), _color));
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
        /// Check if this Player has a Piece at the specified coordinate.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns>'true' if a piece is found, 'false' otherwise.</returns>
        public bool HasPieceAtCoordinate((int, int) pos)
        {
            return _pieces.Exists(p => pos ==  p.TileCoordinate);
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

        /// <summary>
        /// Sets the opponent to this Player.
        /// </summary>
        /// <param name="p">The opponent.</param>
        public void SetOpponent(Player p) 
        {
            _opponent = p;
        }

        public Player Opponent
        {
            get 
            {
                if (_opponent == null)
                {
                    throw new NotImplementedException(
                        "An opponent to this Player has not been set!" + 
                        " Use SetOpponent() to set the opponent."
                        );
                }
                return _opponent;
            }
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

        public ColorChess Color
        {
            get => _color;
        }
    }
}
