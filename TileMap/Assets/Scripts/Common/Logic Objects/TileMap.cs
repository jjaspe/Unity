using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class TileMap
    {
        public virtual Tile[,] Tiles { get; set; }

        public TileMap()
        {
            Tiles = new Tile[0, 0];
        }

        protected virtual void createTiles(int rows, int columns)
        {
            Tiles = new Tile[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Tiles[i, j] = new Tile();
                }
            }
        }

        public void CreateTiles(Vector3 origin, int rows, int columns)
        {
            createTiles(rows, columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Tiles[i, j].Position = new Position(origin.x + i, origin.y, origin.z + j);
                }
            }
        }
    }
}
