using System.Collections;
using System.Collections.Generic;
using Dolberth.Player.Models;
using UnityEngine;

namespace Dolberth.Managers.GameManager
{
    public interface IGameManager
    {
        Dolberth.Maze.Models.Maze GetMaze();
        PlayerData GetPlayerData();
    }
}
