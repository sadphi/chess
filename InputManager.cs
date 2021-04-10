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
                        //Try selecting a piece at tile 't'
                        if (curPlayer.SelectPiece(t)) break;
                    }
                }
            }

            // If the player has selected a piece
            else if (curPlayer.HasSelectedPiece && _curMouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
            {
                foreach (Tile t in Board.Tiles)
                {
                    if (t.Position.Contains(_mouseRectangle))
                    {
                        // Try moving the player's selected piece to tile 't'. Go to next turn if the move was successful.
                        if (curPlayer.MovePiece(t, gameManager.OtherPlayer))
                        {
                            gameManager.NextTurn();
                            break;
                        }

                        // Try selecting a new Piece instead. Exit loop if a new piece could be selected.
                        else if (curPlayer.SelectPiece(t)) break;
                        
                    }
                }
            }


            _prevMouseState = _curMouseState;
        }
    }
}
