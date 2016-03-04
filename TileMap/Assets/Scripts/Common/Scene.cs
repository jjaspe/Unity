using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Scene:MonoBehaviour
    {
        protected ScreenHelper Helper;
        public float tileSize = 10;
        public GameObject TileObject;
        public GameObject WallObject;
        public GameObject MainCamera;
        internal GameObject World;
        internal WalledMap Map;
        public List<GameObject> Tiles = new List<GameObject>();
        public List<GameObject> Walls = new List<GameObject>();

        public Scene()
        {
            Helper = new ScreenHelper(this);
        }
    }
}
