using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    public class GameManager
    {
        private Player _curPlayer;
        private Player _playerOne;
        private Player _playerTwo;

        /// <summary>
        /// Sets up the game manager. Must be called first, before any other call.
        /// </summary>
        /// <param name="firstPlayer">Optional argument: Specify which player takes the first turn.</param>
        public GameManager(Player playerOne, Player playerTwo, Player firstPlayer = null)
        {
            if (firstPlayer == null)
            {
                Random r = new Random();
                int v = r.Next(1);
                if (v == 0) _curPlayer = playerOne;
                else _curPlayer = playerTwo;
            }
            else
            {
                _curPlayer = firstPlayer;
            }

            _curPlayer = firstPlayer;
            _playerOne = playerOne;
            _playerTwo = playerTwo;
        }

        public Player CurrentPlayer
        {
            get => _curPlayer;
        }

        public Player PlayerOne
        {
            get => _playerOne;
        }

        public Player PlayerTwo
        {
            get => _playerTwo;
        }
    }

    public enum PlayerTurn
    {
        Dark,
        Light
    }
}
