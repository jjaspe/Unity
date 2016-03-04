using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class WalledMap:TileMap
    {
        public WalledMap()
        {
            Tiles = new WalledTile[0, 0];
        }

        protected override void createTiles(int rows, int columns)
        {
            Tiles = new WalledTile[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Tiles[i, j] = new WalledTile();
                }
            }
        }

        public new void CreateTiles(Vector3 origin,int rowCount, int columnCount)
        {
            base.CreateTiles(origin,rowCount, columnCount);
            createWalls();
        }

        protected virtual void createWalls()
        {
            foreach(WalledTile tile in Tiles)
            {
                tile.Walls.Add(new Wall() { position = tile.vPosition, orientation = WallOrientation.X });
                tile.Walls.Add(new Wall() { position = tile.vPosition, orientation = WallOrientation.Z });
            }
        }

        public WalledTile Tile(int i, int j)
        {
            return Tiles[i, j] as WalledTile;
        }

        internal List<WalledTile> GetConnectedTiles(WalledTile tile)
        {
            List<WalledTile> connectedNeighboors = new List<WalledTile>();
            foreach (Neighboors neighbor in tile.MyNeighboors)
            {
                switch (neighbor)
                {
                    case Neighboors.Right:
                        connectedNeighboors.Add(Tile(tile.Position.X + 1, tile.Position.Z));
                        break;
                    case Neighboors.Up:
                        connectedNeighboors.Add(Tile(tile.Position.X, tile.Position.Z + 1));
                        break;
                    case Neighboors.Left:
                        connectedNeighboors.Add(Tile(tile.Position.X - 1, tile.Position.Z));
                        break;
                    case Neighboors.Down:
                        connectedNeighboors.Add(Tile(tile.Position.X, tile.Position.Z - 1));
                        break;
                }
            }
            return connectedNeighboors;
        }

        internal List<WalledTile> GetNeighboorTiles(WalledTile tile)
        {
            List<WalledTile> neighbors = new List<WalledTile>();
            if (tile.Position.X + 1 <= Tiles.GetUpperBound(0))
                neighbors.Add(Tile(tile.Position.X + 1, tile.Position.Z));
            if (tile.Position.Z + 1 <= Tiles.GetUpperBound(1))
                neighbors.Add(Tile(tile.Position.X, tile.Position.Z + 1) );
            if (tile.Position.X > 0)
                neighbors.Add(Tile(tile.Position.X - 1, tile.Position.Z) );
            if (tile.Position.Z > 0)
                neighbors.Add(Tile(tile.Position.X, tile.Position.Z - 1) );
            return neighbors;
        }

        internal void ConnectTiles(WalledTile tile1, WalledTile tile2)
        {
            if (tile1.Position.X == tile2.Position.X)
            {
                if (tile1.Position.Z > tile2.Position.Z)
                {
                    tile1.MyNeighboors.Add(Neighboors.Down);
                    tile2.MyNeighboors.Add(Neighboors.Up);
                    tile1.Walls.RemoveAll(n => n.orientation == WallOrientation.X);
                }
                else if (tile2.Position.Z > tile1.Position.Z)
                {
                    tile1.MyNeighboors.Add(Neighboors.Up);
                    tile2.MyNeighboors.Add(Neighboors.Down);
                    tile2.Walls.RemoveAll(n => n.orientation == WallOrientation.X);
                }
            }

            if (tile1.Position.Z == tile2.Position.Z)
            {
                if (tile1.Position.X < tile2.Position.X)
                {
                    tile1.MyNeighboors.Add(Neighboors.Right);
                    tile2.MyNeighboors.Add(Neighboors.Left);

                    tile2.Walls.RemoveAll(n => n.orientation == WallOrientation.Z);
                }
                else if (tile2.Position.X < tile1.Position.X)
                {
                    tile1.MyNeighboors.Add(Neighboors.Left);
                    tile2.MyNeighboors.Add(Neighboors.Right);

                    tile1.Walls.RemoveAll(n => n.orientation == WallOrientation.Z);
                }
            }
        }
    }
}
