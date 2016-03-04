using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class ScreenHelper
    {
        float tileSize { get { return scene.tileSize; } }
        Scene scene;
        
        public ScreenHelper(Scene scene)
        {
            this.scene = scene;
        }

        public void PutMapInWorld()
        {
            foreach (WalledTile tile in scene.Map.Tiles)
            {
                foreach (Wall wall in tile.Walls)
                {
                    CreateWall(wall, scene);
                }
                CreateTile(tile, scene);                
            }
        }

        public Vector3 GetScreenVector(Vector3 gameVector)
        {
            return new Vector3(gameVector.x * tileSize, gameVector.y * tileSize, gameVector.z * tileSize);
        }

        public Vector3 GetRotationVector(Vector3 gameVector)
        {
            return new Vector3(gameVector.x * 90, gameVector.y * 90, gameVector.z * 90);
        }

        public Vector3 GetRotationVector(WallOrientation or)
        {
            switch (or)
            {
                case WallOrientation.X:
                    return new Vector3(0, 0, 0);
                case WallOrientation.Y:
                    return new Vector3(0, 0, 90);
                case WallOrientation.Z:
                    return new Vector3(0, 90, 0);
                default:
                    return new Vector3(0, 0, 0);
            }
        }

        public void CreateWall(Wall wall,Scene scene)
        {
            Vector3 wallPosition = GetScreenVector(wall.position);
            GameObject newWall = Scene.Instantiate(scene.WallObject, wallPosition,
                Quaternion.Euler(GetRotationVector(wall.orientation)))
                as GameObject;

            wallPosition.y += scene.WallObject.transform.localScale.y / 2;
            wallPosition.x -= wall.orientation == WallOrientation.Z ? tileSize / 2 : 0;
            wallPosition.z -= wall.orientation == WallOrientation.X ? tileSize / 2 : 0;
            newWall.transform.position = wallPosition;

            newWall.transform.SetParent(scene.World.transform);
            scene.Walls.Add(newWall);
        }

        public void CreateTile(Tile tile,Scene scene)
        {
            GameObject newTile = Scene.Instantiate(scene.TileObject, GetScreenVector(tile.vPosition),
                                    scene.TileObject.transform.rotation) as GameObject;
            scene.Tiles.Add(newTile);
            newTile.transform.SetParent(scene.World.transform);
        }
    }
}
