using UnityEngine;
using Dolberth.Player.Models;

namespace Dolberth.Managers.GameManager
{
    public class GameManager
    {
        private PlayerData playerData;
        private Dolberth.Maze.Models.Maze maze;


        public GameManager()
        {
            Debug.Log("init GameManager");
            InitGame();
        }

        void InitGame()
        {
            playerData = new PlayerData
            {
                Speed = 5
            };

            maze = new Dolberth.Maze.Models.Maze(21, 17);
        }

        public Dolberth.Maze.Models.Maze GetMaze()
        {
            return maze;
        }

        public PlayerData GetPlayerData()
        {
            return playerData;
        }
    }
}