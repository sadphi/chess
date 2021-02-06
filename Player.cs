using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    public class Player
    {
        private Piece _selectedPiece;
        private bool  _hasSelectedPiece = false;
        private IList<Piece> _pieces;

        public Player()
        {

        }

        public Piece SelectedPiece
        {
            get => _selectedPiece;
            set => _selectedPiece = value;
        }

        public bool HasSelectedPiece
        {
            get => _hasSelectedPiece;
            set => _hasSelectedPiece = true;
        }

        public IList<Piece> Pieces
        {
            get => _pieces;
        }
    }
}
