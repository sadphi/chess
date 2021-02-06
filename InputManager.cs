using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    public static class InputManager
    {
        private static MouseState _curMouseState;
        private static MouseState _prevMouseState;

        private static KeyboardState _curKeyboardState;
        private static KeyboardState _prevKeyboardState;

        private static Rectangle _mouseRectangle;

        public static void Update(GameManager gameManager, GameTime gameTime)
        {
            HandleMouse(gameManager);
        }

        private static void HandleMouse(GameManager gameManager)
        {
            _curMouseState = Mouse.GetState();
            _mouseRectangle = new Rectangle(_curMouseState.X, _curMouseState.Y, 1, 1);
            Player curPlayer = gameManager.CurrentPlayer;

            // If the player hasn't selected a piece
            if (!curPlayer.HasSelectedPiece && _curMouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
            {
                foreach (Tile t in Board.Tiles)
                {
                    if (t.Position.Contains(_mouseRectangle))
                    {
                        //Find the piece standing on this tile (Should only contain one element)
                        List<Piece> sr = curPlayer.Pieces.Where(p => p.TileCoordinate == t.Coordinate).ToList();
                        if (sr.Count > 1)
                        {
                            throw new Exception("More than one piece exists on the same tile!");
                        } 
                        else if (sr.Count == 1)
                        {
                            curPlayer.SelectedPiece = sr[0];
                            curPlayer.HasSelectedPiece = true;
                            break;
                        }
                        //Do nothing if none of this player's piece at the tile.
                    }
                }
            }

            //If the player has selected a piece
            else if (curPlayer.HasSelectedPiece && _curMouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
            {
                foreach (Tile t in Board.Tiles)
                {
                    if (t.Position.Contains(_mouseRectangle))
                    {
                        bool isSuccess = curPlayer.SelectedPiece.Move(t.Coordinate);

                        //Check if move was successful, and unselect piece if it was
                        if (isSuccess)
                        {
                            curPlayer.SelectedPiece = null;
                            curPlayer.HasSelectedPiece = false;
                            break;
                        }
                    }
                }
            }


            _prevMouseState = _curMouseState;
        }
    }
}
