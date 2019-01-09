using UnityEngine;
using UnityEngine.AI;
using Dolberth.Maze.Models;
using Zenject;
using Dolberth.Managers.GameManager;

namespace Dolberth.Maze
{
    public class MazeController : MonoBehaviour
    {

        public GameObject coinPrefab;
        public GameObject enemyPrefab;
        public GameObject wallPrefab;
        public GameObject goalPrefab;

        public NavMeshSurface navMeshSurface;

        private Models.Maze maze;
        private GameManager _gameManager;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        /// <summary>
        /// Start this instance.
        /// </summary>
        void Start()
        {
            maze = _gameManager.GetMaze();
            _generateMazeMeshes();
        }

        /// <summary>
        /// Generates the maze related meshes.
        /// </summary>
        private void _generateMazeMeshes()
        {
            foreach (Cell cell in maze.GetCells())
            {
                if (cell.cellType == CellType.WALL)
                {
                    Instantiate(wallPrefab, new Vector3((float)cell.x, (float).5, (float)cell.z), Quaternion.identity);
                }
                else if (cell.cellType == CellType.COIN)
                {
                    Instantiate(coinPrefab, new Vector3((float)cell.x, (float).5, (float)cell.z), coinPrefab.transform.rotation);
                }
                else if (cell.cellType == CellType.GOAL)
                {
                    Instantiate(goalPrefab, new Vector3((float)cell.x, (float).5, (float)cell.z), Quaternion.identity);
                }
                else if (cell.cellType == CellType.ENEMYSPAWN)
                {
                    // The prefab must have the Zen auto Injecter component
                    Instantiate(enemyPrefab, new Vector3((float)cell.x, (float).5, (float)cell.z), Quaternion.identity);
                }
            }

            _generateWallAround();
            navMeshSurface.BuildNavMesh();
        }

        /// <summary>
        /// Ugly generation of the steady wall around the maze.
        /// </summary>
        private void _generateWallAround()
        {

            Vector3 sw1Pos = new Vector3(0, .5f, maze.Radius + 1);
            GameObject wall1 = Instantiate(wallPrefab, sw1Pos, Quaternion.identity);
            wall1.transform.localScale += new Vector3(maze.Diameter + 2, 0, 0);

            Vector3 sw2Pos = new Vector3(0, .5f, -(maze.Radius + 1));
            GameObject wall2 = Instantiate(wallPrefab, sw2Pos, Quaternion.identity);
            wall2.transform.localScale += new Vector3(maze.Diameter + 2, 0, 0);

            Vector3 sw3Pos = new Vector3(maze.Radius + 1, .5f, 0);
            GameObject wall3 = Instantiate(wallPrefab, sw3Pos, Quaternion.identity);
            wall3.transform.localScale += new Vector3(0, 0, maze.Diameter + 2);

            Vector3 sw4Pos = new Vector3(-(maze.Radius + 1), .5f, 0);
            GameObject wall4 = Instantiate(wallPrefab, sw4Pos, Quaternion.identity);
            wall4.transform.localScale += new Vector3(0, 0, maze.Diameter + 2);
        }
    }
}