using UnityEngine;
using Dolberth.Player.Models;

namespace Dolberth.Managers.GameManager
{
    public class GameManager
    {
        private PlayerData playerData;
        private Dolberth.Maze.Models.Maze maze;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Dolberth.Managers.GameManager.GameManager"/> class.
        /// </summary>
        public GameManager()
        {
            Debug.Log("init GameManager");
            InitGame();
        }

        /// <summary>
        /// Inits the game.
        /// </summary>
        void InitGame()
        {
            playerData = new PlayerData
            {
                Speed = 5
            };

            maze = new Dolberth.Maze.Models.Maze(21, 17);
        }

        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <returns>The maze.</returns>
        public Dolberth.Maze.Models.Maze GetMaze()
        {
            return maze;
        }

        /// <summary>
        /// Gets the player data.
        /// </summary>
        /// <returns>The player data.</returns>
        public PlayerData GetPlayerData()
        {
            return playerData;
        }
    }
}