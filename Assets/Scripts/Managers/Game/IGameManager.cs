using Dolberth.Player.Models;

namespace Dolberth.Managers.GameManager
{
    public interface IGameManager
    {
        Maze.Models.Maze GetMaze();
        PlayerData GetPlayerData();
    }
}
