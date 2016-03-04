using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using System;

public class TiltMaze : Scene {   

    public int rowCount = 10, columnCount = 10;
    public Vector3 origin = new Vector3(0, 0, 0);
    public Material solvedTileMaterial;
    public GameObject PlayerObject;
    public GameObject CeilingObject;
    public GameObject CollisionPlaneObject;

    TileMap Ceiling;
    List<WalledTile> tilesInSolvedPath;
    float rotationSpeed = 0.1f;
    float lastMouseX, lastMouseY;
    private GameObject Player,CollisionPlane;
    GameObject TopWall, RightWall;

    // Use this for initialization
    void Start () {
        ScaleObjects();
        World = new GameObject();
        World.transform.position = GetCenter();
        SetupMap();

        WalledTile start = Map.Tile(0, 0),
            end = Map.Tile(rowCount - 1, columnCount - 1);
        tilesInSolvedPath = (new AStarSolver()).Solve(Map, start, end);

        
        PutMapInWorld();
        UpdateMouseCoordinates();
        SetupCollisionPlane();
        SetupPlayer(start);
        SetupOuterWalls();
    }

    private void SetupOuterWalls()
    {
        Vector3 topWallPosition = new Vector3((rowCount-1) * tileSize/2, 
                                    WallObject.transform.position.y*2, 
                                    columnCount * tileSize -tileSize/2);
        TopWall = Instantiate(WallObject, topWallPosition, Quaternion.identity) as GameObject;
        TopWall.transform.localScale = new Vector3(rowCount * tileSize,
                                        WallObject.transform.localScale.y,
                                        WallObject.transform.localScale.z);
        TopWall.transform.SetParent(World.transform);

        Vector3 rightWallPosition = new Vector3(rowCount * tileSize -tileSize/2,
                                    WallObject.transform.position.y*3/2,
                                    (columnCount - 1)* tileSize/2);
        RightWall = Instantiate(WallObject, rightWallPosition, Quaternion.identity) as GameObject;
        RightWall.transform.localScale = new Vector3(WallObject.transform.localScale.z,
                                        WallObject.transform.localScale.y,
                                        columnCount * tileSize);
        RightWall.transform.SetParent(World.transform);


    }

    private Vector3 GetCenter()
    {
        return Helper.GetScreenVector(new Vector3(origin.x + rowCount / 2, origin.y, origin.z + columnCount / 2));
    }

    private void UpdateMouseCoordinates()
    {
        lastMouseX = Input.mousePosition.x;
        lastMouseY = Input.mousePosition.y;
    }

    private void SetupCollisionPlane()
    {
        CollisionPlane = Instantiate(CollisionPlaneObject, CollisionPlaneObject.transform.position, Quaternion.identity) as GameObject;
        CollisionPlane.transform.SetParent(World.transform);
    }

    private void SetupMap()
    {
        Map = new WalledMap();
        Map.CreateTiles(origin, rowCount, columnCount);
        MazeGenerator gen = new MazeGenerator();
        gen.GenerateMaze(Map);
    }

    private void SetupCeiling()
    {
        Ceiling = new TileMap();
        Ceiling.CreateTiles(origin, rowCount, columnCount);
    }

    private void SetupPlayer(WalledTile start)
    {        
        Vector3 position = Helper.GetScreenVector(start.vPosition);
        position.y = 2;
        Player = Instantiate(PlayerObject, position, Quaternion.identity) as GameObject;
        Player.transform.localScale = new Vector3(tileSize / 2, tileSize / 2, tileSize / 2);
        Player.transform.position = Helper.GetScreenVector(position);
    }

    

    void PutMapInWorld()
    {
        Helper.PutMapInWorld();

        foreach (WalledTile tile in tilesInSolvedPath)
        {
            Vector3 position = Helper.GetScreenVector(tile.vPosition);
            GameObject tileObject = Tiles.Find(n => n.transform.position == position);
            tileObject.GetComponent<MeshRenderer>().material = solvedTileMaterial;
        }
        
    }

    void ScaleObjects()
    {
        WallObject.transform.localScale =
            new Vector3(tileSize, WallObject.transform.localScale.y, WallObject.transform.localScale.z);
        TileObject.transform.localScale =
            new Vector3(tileSize, tileSize,TileObject.transform.localScale.z);
        CeilingObject.transform.localScale=
            new Vector3(tileSize, tileSize, CeilingObject.transform.localScale.z);
        CeilingObject.transform.position = new Vector3(CeilingObject.transform.position.x, CeilingObject.transform.position.y,
            WallObject.transform.localScale.y);
        CollisionPlaneObject.transform.localScale=new Vector3(columnCount * tileSize, 
            CollisionPlaneObject.transform.localScale.y,
            rowCount * tileSize);
    }
	
	// Update is called once per frame
	void Update () {
            float amountX = -(Input.mousePosition.x - lastMouseX),
            amountY = ( Input.mousePosition.y - lastMouseY);
            UpdateMouseCoordinates();
            Vector3 currentAngles = World.transform.eulerAngles;
        amountX = amountX>0?Math.Min(5, amountX):Math.Max(-5,amountX);
        amountY = amountY > 0 ? Math.Min(5, amountY) : Math.Max(-5, amountY);

        World.transform.eulerAngles = new Vector3(currentAngles.x + amountY * rotationSpeed,
            currentAngles.y, currentAngles.z + amountX * rotationSpeed);

    }
}

internal interface IScene
{
}