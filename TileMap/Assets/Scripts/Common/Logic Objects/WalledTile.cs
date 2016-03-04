using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class WalledTile:Tile
    {
        public List<Wall> Walls = new List<Wall>();
        public List<Neighboors> MyNeighboors = new List<Neighboors>();
    }
}
