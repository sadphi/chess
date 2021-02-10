using Microsoft.Xna.Framework;
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
        private Player _otherPlayer;
        private Player _playerOne;
        private Player _playerTwo;
        private List<Player> _players = new List<Player>();

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

            _players.Add(playerOne);
            _players.Add(playerTwo);

            //Set the who is the other player (not taking its turn).
            _otherPlayer = _players.Find(p => p != _curPlayer);

            _curPlayer.IsMyTurn = true;
            _playerOne = playerOne;
            _playerTwo = playerTwo;
        }

        /// <summary>
        /// Updates all game logic, including each player.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            foreach (Player p in _players)
            {
                p.Update(gameTime);
            }
        }

        /// <summary>
        /// Begin next turn (switches current player to the other player)
        /// </summary>
        public void NextTurn()
        {
            _otherPlayer = _curPlayer;
            if (_curPlayer == _playerOne) _curPlayer = _playerTwo;
            else _curPlayer = _playerOne;
        }

        /// <summary>
        /// Gets the player that is taking its turn.
        /// </summary>
        public Player CurrentPlayer
        {
            get => _curPlayer;
        }

        /// <summary>
        /// Gets the player that IS NOT taking its turn.
        /// </summary>
        public Player OtherPlayer
        {
            get => _otherPlayer;
        }

        public Player PlayerOne
        {
            get => _playerOne;
        }

        public Player PlayerTwo
        {
            get => _playerTwo;
        }

        public List<Player> Players
        {
            get => _players;
        }
    }

    public enum PlayerTurn
    {
        Dark,
        Light
    }
}
