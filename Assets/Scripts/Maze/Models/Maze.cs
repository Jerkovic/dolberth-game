using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Dolberth.Maze.Models;

namespace Dolberth.Maze.Models 
{
	public class Maze  {

		ArrayList walls = new ArrayList ();
		ArrayList cells = new ArrayList ();

		public int Diameter { get; set; }
		public int Radius { get; set; }
		public int Coins { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Maze"/> class.
		/// </summary>
		/// <param name="diameter">Diameter.</param>
		/// <param name="coins">Coins.</param>
		public Maze(int diameter, int coins) 
		{

			this.Diameter = diameter;
			this.Radius = (diameter - 1) / 2;
			this.Coins = coins;
			Generate ();
		}

		/// <summary>
		/// Gets all cells in maze.
		/// </summary>
		/// <returns>The cells.</returns>
		public ArrayList getCells() {

			return cells;
		}

		/// <summary>
		/// Changes the type of the cell.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="cellType">Cell type.</param>
		public void ChangeCellType(int x, int y, CellType cellType) {

			getCellAt (x, y).cellType = cellType;
		}

		/// <summary>
		/// Spawns the enemies.
		/// </summary>
		private void SpawnEnemies() {

			for (int i = 0; i < 3; i++)
			{
				FindRandomEmptyCell ().cellType = CellType.ENEMYSPAWN;
			}
		}


		/// <summary>
		/// Picks up coin and updates maze state
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public bool PickUpCoin(int x, int y) {

			if (getCellAt (x, y).cellType.Equals (CellType.COIN))
			{
				ChangeCellType (x, y, CellType.HALL);
				return true;
			}
			return false;
		}
			
		/// <summary>
		/// Generates a maze using Prim's algorithm
		/// </summary>
		private void Generate ()
		{
			
			for (int x = -Radius; x <= Radius; x++) {
				for (int z = -Radius; z <= Radius; z++) {
					cells.Add (new Cell (x, z));
				}
			}

			Cell startingCell = getCellAt (0, 0);
			walls.Add (startingCell);
			int max = 300;

			while (true) 
			{
				Cell wall = (Cell)walls [Random.Range (0, walls.Count)];
				processWall (wall);

				if (walls.Count <= 0)
					break;
				
				if (--max < 0)
					break;

			}

			getCellAt (0, 0).cellType = CellType.START;
				
			Cell randomFreeCell = FindRandomEmptyCell ();
			randomFreeCell.cellType = CellType.GOAL;

			SpawnCoins ();
			SpawnEnemies ();
		}
			
		/// <summary>
		/// Generates the coins scattered in our maze.
		/// </summary>
		private void SpawnCoins() {

			for (int i = 0; i < Coins; i++)
			{
				FindRandomEmptyCell ().cellType = CellType.COIN;
			}
		}

		/// <summary>
		/// Finds a random empty cell.
		/// </summary>
		/// <returns>The random empty cell.</returns>
		public Cell FindRandomEmptyCell() {
			
			while(true) {
				
				int x = Random.Range (-Radius, Radius);
				int z = Random.Range (-Radius, Radius);
				Cell cell = getCellAt (x, z);
					
				if (cell.cellType == CellType.HALL) {
					return cell;
				}
			}
		}

		/// <summary>
		/// Processes the wall.
		/// </summary>
		/// <param name="cell">Cell.</param>
		private void processWall (Cell cell)
		{
			int x = cell.x;
			int z = cell.z;
			if (cell.from == null)
			{
				if (Random.Range (0, 2) == 0)
				{
					x += Random.Range (0, 2) - 1;
				}
				else
				{
					z += Random.Range (0, 2) - 1;
				}
			}
			else
			{
				x += (cell.x - cell.from.x);
				z += (cell.z - cell.from.z);
			}

			Cell next = getCellAt (x, z);
			if (next == null || next.cellType != CellType.WALL)
				return;
			
			cell.cellType = CellType.HALL;
			next.cellType = CellType.HALL;

			foreach (Cell process in getWallsAroundCell (next))
			{
				process.from = next;
				walls.Add (process);
			}
			walls.Remove (cell);

		}

		/// <summary>
		/// Gets the cell at x and z.
		/// </summary>
		/// <returns>The <see cref="Cell"/>.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		private Cell getCellAt (int x, int z)
		{
			foreach (Cell cell in cells)
			{
				if (cell.x == x && cell.z == z)
					return cell;
			}
			return null;
		}

		/// <summary>
		/// Gets the walls around cell.
		/// </summary>
		/// <returns>The walls around cell.</returns>
		/// <param name="cell">Cell.</param>
		private ArrayList getWallsAroundCell (Cell cell)
		{

			ArrayList near = new ArrayList ();
			ArrayList check = new ArrayList ();

			check.Add (getCellAt (cell.x + 1, cell.z));
			check.Add (getCellAt (cell.x - 1, cell.z));
			check.Add (getCellAt (cell.x, cell.z + 1));
			check.Add (getCellAt (cell.x, cell.z - 1));

			foreach (Cell checking in check)
			{
				if (checking != null && checking.cellType == CellType.WALL)
					near.Add (checking);
			}
			return near;
		}			
	}
}