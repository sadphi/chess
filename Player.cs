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
        private IList<Piece> _pieces = new List<Piece>();
        private Piece _selectedPiece;
        private bool  _hasSelectedPiece = false;
        private bool _isMyTurn;

        public Player(Piece test)
        {
            _pieces.Add(test);
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

        public IList<Piece> Pieces
        {
            get => _pieces;
        }
    }
}
