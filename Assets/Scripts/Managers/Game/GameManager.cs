using UnityEngine;
using Dolberth.Player.Models;

namespace Dolberth.Managers.GameManager
{
    public class GameManager : IGameManager
    {
        private PlayerData playerData;
        private Maze.Models.Maze maze;

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
           
            playerData = new PlayerData();
            playerData.MaxHealth = 100;
            playerData.Health = playerData.MaxHealth;
            maze = new Maze.Models.Maze(diameter: 21, coins: 17);
        }

        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <returns>The maze.</returns>
        public Maze.Models.Maze GetMaze()
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