using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using Assets.Scripts.Common;

public class WeaponMaze : Scene {

    public int rowCount = 10, columnCount = 10;
    public Vector3 origin = new Vector3(0, 0, 0);
    public float wallThickness=4;
    public float wallHeight = 3;
    public GameObject Player;

    public WeaponMaze()
    {
        Map = (new MapFactory(rowCount,columnCount,origin)).CreateMap(MapType.RandomMaze) as WalledMap;
    }

    // Use this for initialization
    void Start () {
        World = new GameObject();
        ScaleObjects();
        Helper.PutMapInWorld();
    }

    // Update is called once per frame
    void Update () {
    }

    void ScaleObjects()
    {
        WallObject.transform.localScale =
            new Vector3(tileSize, wallHeight, wallThickness);
        TileObject.transform.localScale =
            new Vector3(tileSize, tileSize, TileObject.transform.localScale.z);
        MainCamera.transform.position = new Vector3(5 * tileSize, 10 * tileSize, 5 * tileSize);
        Player.transform.position = new Vector3(0, Player.transform.localScale.y, 0);
            
    }
}
