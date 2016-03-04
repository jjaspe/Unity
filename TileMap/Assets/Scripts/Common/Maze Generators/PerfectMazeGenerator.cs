using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class MazeGenerator
    {
        int xSize, zSize;
        public WalledTile currentCell;
        Random r = new Random();
        private int totalCells;
        private int totalVisitedCells = 0;
        private List<WalledTile> visitedCells = new List<WalledTile>();
        private bool[,] visited;
        private int lastVisitedCell;
        public int steps = 0;
        WalledTile neighbor;
        WalledMap map;
        Tile[,] WalledTiles { get { return map.Tiles; } }


        public void GenerateMaze(WalledMap map)
        {
            this.map = map;
            Setup();

            int X = r.Next(xSize - 1),
            Z = r.Next(zSize - 1);

            currentCell = (WalledTile)WalledTiles[X, Z];
            visited[X, Z] = true;
            totalVisitedCells++;

            //Continue going until we have visited every cell
            while (totalVisitedCells < totalCells)
            {
                neighbor = GetNeighboor(map, visited, currentCell);
                if (visited[neighbor.Position.X, neighbor.Position.Z] == false 
                    && visited[currentCell.Position.X, currentCell.Position.Z] == true)
                {
                    RemoveWallBetweenNeighbors(currentCell, neighbor);
                    visited[neighbor.Position.X, neighbor.Position.Z] = true;
                    totalVisitedCells++;
                    visitedCells.Add(currentCell);
                    currentCell = neighbor;
                    lastVisitedCell = visitedCells.Count - 1;
                }
            }
        }

        public WalledMap DoStep()
        {
            if (totalVisitedCells < totalCells)
            {
                neighbor = GetNeighboor(map, visited, currentCell);
                if (visited[neighbor.Position.X, neighbor.Position.Z] == false
                    && visited[currentCell.Position.X, currentCell.Position.Z] == true)
                {
                    RemoveWallBetweenNeighbors(currentCell, neighbor);
                    visited[neighbor.Position.X, neighbor.Position.Z] = true;
                    totalVisitedCells++;
                    visitedCells.Add(currentCell);
                    currentCell = neighbor;
                    lastVisitedCell = visitedCells.Count - 1;
                }
            }
            return map;
        }

        private WalledTile GetNeighboor(WalledMap map, bool[,] visited, WalledTile tile)
        {
            currentCell = tile;
            List<WalledTile> neighboors = map.GetNeighboorTiles(tile).
                Where(n => visited[n.Position.X, n.Position.Z] == false).ToList();

            //If we have neighbors set our current neighbor to a random neighbor and set the wall we want to break between those neighbors
            if (neighboors != null && neighboors.Count != 0)
            {
                int randomNeighbor = r.Next(neighboors.Count);
                return neighboors[randomNeighbor];
            }
            else
            {
                //Move back one cell in path
                if (lastVisitedCell > 0)
                {
                    return GetNeighboor(map, visited, visitedCells[lastVisitedCell--]);
                }
                else
                {
                    return null;
                }
            }
        }

        private void RemoveWallBetweenNeighbors(WalledTile tile1, WalledTile tile2)
        {
            map.ConnectTiles(tile1, tile2);
        }

        private void Setup()
        {
            xSize = map.Tiles.GetUpperBound(0) + 1;
            zSize = map.Tiles.GetUpperBound(1) + 1;
            visited = new bool[xSize, zSize];

            totalCells = xSize * zSize;
            totalVisitedCells = 0;

            visitedCells = new List<WalledTile>();
            lastVisitedCell = 0;
        }
    }
}
