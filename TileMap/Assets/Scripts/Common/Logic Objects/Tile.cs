using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Position
    {
        public Position(int x,int y,int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Position(float x, float y, float z)
        {
            X = (int)x;
            Y = (int)y;
            Z = (int)z;
        }
        public int X, Y, Z;
    }
    public class Tile
    {
        public Position Position;
        public Vector3 vPosition
        {
            get { return new Vector3(Position.X, Position.Y, Position.Z); }
        }
    }
}
