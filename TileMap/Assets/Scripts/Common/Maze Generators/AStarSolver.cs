using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class AStarSolver
    {
        public List<WalledTile> Solve(WalledMap map, WalledTile start, WalledTile end)
        {
            int[,] distance = new int[map.Tiles.GetUpperBound(0) + 1, map.Tiles.GetUpperBound(1) + 1];
            distance[start.Position.X, start.Position.Z] = 1;
            List<WalledTile> tilesToCheck = new List<WalledTile>() { start },
                tempList = new List<WalledTile>(), currentNeighbors, Path = new List<WalledTile>();
            bool stay = true;

            while (stay)
            {
                foreach (WalledTile tile in tilesToCheck)
                {
                    //prevent adding previously checked neighboors
                    currentNeighbors = map.GetConnectedTiles(tile).Where
                        (n => distance[n.Position.X, n.Position.Z] < 1).ToList();
                    foreach (WalledTile neighboor in currentNeighbors)
                    {
                        distance[neighboor.Position.X, neighboor.Position.Z] = 
                            distance[tile.Position.X, tile.Position.Z] + 1;
                        tempList.Add(neighboor);
                        if (neighboor.Equals(end))
                            stay = false;
                    }
                }
                tilesToCheck.RemoveAll(n => true);
                tilesToCheck.AddRange(tempList);
                tempList.RemoveAll(n => true);
            }

            //Walk back from end
            Path.Add(end);
            WalledTile current = end;
            while (!current.Equals(start))
            {
                current = map.GetConnectedTiles(current)
                    .Where(n => distance[n.Position.X, n.Position.Z] == 
                    distance[current.Position.X, current.Position.Z] - 1)
                    .First();
                Path.Add(current);
            }

            return Path.Reverse<WalledTile>().ToList();
        }
    }
}
