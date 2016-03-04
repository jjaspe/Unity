using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Common
{
    public class RandomMazeGenerator
    {
        int xSize, zSize;
        Random r = new Random();

        public void GenerateMaze(WalledMap map)
        {
            xSize = map.Tiles.GetUpperBound(0) + 1;
            zSize = map.Tiles.GetUpperBound(1) + 1;

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < zSize; j++)
                {
                    bool addDownNeighbor = r.Next(2) > 0;
                    if(i>0 && addDownNeighbor)
                    {
                        map.ConnectTiles(map.Tile(i, j), map.Tile(i - 1, j));
                    }
                    bool addLeftNeighbor = r.Next(2) > 0 ;
                    if(j>0 && addLeftNeighbor)
                    {
                        map.ConnectTiles(map.Tile(i, j), map.Tile(i , j-1));
                    }
                }
            }
        }
    }
}
