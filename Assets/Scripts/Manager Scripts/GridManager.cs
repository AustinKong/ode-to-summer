using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    empty, obstacle, none, filled, outOfBounds
}
public class Tile
{
    public GameObject obj;
    public TileType type;
}

public class GridManager : MonoBehaviour
{
    public static GridManager instance; private void Awake() => instance = this;

    public int tilemapSize;
    private Tile[,] tilemap;

    public bool AddToPosition(Vector2Int pos, GameObject obj, TileType type)
    {
        if (tilemap[pos.x, pos.y].type != TileType.empty)
        {
            return false;
        }
        tilemap[pos.x, pos.y].obj = obj;
        tilemap[pos.x, pos.y].type = type;
        return true;
    }

    public void RemoveFromPosition(Vector2Int pos)
    {
        tilemap[pos.x, pos.y].obj = null;
        tilemap[pos.x, pos.y].type = TileType.empty;
    }

    public Tile ReadFromPosition(Vector2Int pos)
    {
        
        if (pos.x >= tilemapSize || pos.y >= tilemapSize || pos.x < 0 || pos.y < 0)
        {
            Tile cog = new Tile();
            cog.type = TileType.outOfBounds;
            return cog;
        }
        else return tilemap[pos.x, pos.y];
    }

    public void BuildLevel()
    {
        #region Fill tilemap with empty
        tilemap = new Tile[tilemapSize, tilemapSize];
        for (int i = 0; i < tilemapSize; i++)
        {
            for (int k = 0; k < tilemapSize; k++)
            {
                Tile nut = new Tile
                {
                    obj = null,
                    type = TileType.none
                };
                tilemap[i, k] = nut;
            }
        }
        #endregion
        #region Fill tilemap with tiles read from scene
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        
        foreach (GameObject tile in tiles)
        {
            Tile nut = new Tile
            {
                obj = null,
                type = TileType.empty
            };
            Vector2Int gear = IsometricMath.WorldToIsometricCoordinatesSnapped(tile.transform.position);
            tilemap[gear.x, gear.y] = nut;
        }
        #endregion
        #region Fill tilemap with objects read from scene
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Object");
        foreach(GameObject k in objects)
        {
            Tile nut = new Tile
            {
                obj = k,
                type = k.GetComponent<Object>().type
            };
            k.GetComponent<Object>().cartesianPosition = IsometricMath.WorldToIsometricCoordinatesSnapped(k.transform.position);
            Vector2Int gear = IsometricMath.WorldToIsometricCoordinatesSnapped(k.transform.position);
            tilemap[gear.x, gear.y] = nut;
        }
        #endregion
    }

    private void Start()
    {
        BuildLevel();
    }
}