using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public class MapFactory
    {
        public int rowCount;
        public int columnCount;
        public Vector3 origin;
        
        public MapFactory(int rowCount,int columnCount,Vector3 origin)
        {
            this.rowCount = rowCount;
            this.columnCount = columnCount;
            this.origin = origin;
        }

        public TileMap CreateMap(MapType type=MapType.PerfectMaze)
        {
            WalledMap Map=new WalledMap();
            switch (type)
            {
                case MapType.PerfectMaze:
                    Map.CreateTiles(origin, rowCount, columnCount);
                    MazeGenerator gen = new MazeGenerator();
                    gen.GenerateMaze(Map);
                    break;
                case MapType.RandomMaze:
                    RandomMazeGenerator ranGen = new RandomMazeGenerator();
                    Map.CreateTiles(origin, rowCount, columnCount);
                    ranGen.GenerateMaze(Map);
                    break;

            }

            return Map;
        }
    }
}
