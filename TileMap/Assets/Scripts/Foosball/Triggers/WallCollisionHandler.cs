using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Foosball.Triggers
{
    public class WallCollisionHandler:FoosballScript
    {
        static WallCollisionHandler instance;
        public static WallCollisionHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (new GameObject()).AddComponent<WallCollisionHandler>();
                }
                return instance;
            }
        }

        public bool[,] bumperAtWallWhite, bumperAtWallBlack;

        private WallCollisionHandler()
        {
            bumperAtWallBlack = new bool[4, 2];
            bumperAtWallWhite = new bool[4, 2];
        }
        public void RodHitWall(Collider collider, GameObject bumper)
        {
            Transform rod = bumper.transform.parent;
            Transform wall = collider.transform;
            int rodIndex = getRodIndex(rod), wallIndex = getWallIndex(wall);
            if (rod.name.Contains("Black"))
                bumperAtWallBlack[rodIndex, wallIndex] = true;
            else if (rod.name.Contains("White"))
                bumperAtWallWhite[rodIndex, wallIndex] = true;

            Manager.MoveRodToEdge(bumper.transform.parent, wallIndex);
        }

        private int getWallIndex(Transform rod)
        {
            switch (rod.name)
            {
                case "TopWall":
                    return 0;
                case "BottomWall":
                    return 1;
                default:
                    return -1;
            }
        }

        public int getRodIndex(Transform rod)
        {
            string name = rod.name;
            name = name.Replace("White ", "").Replace("Black ", "");
            return Int32.Parse(name) - 1;
        }
    }
}
