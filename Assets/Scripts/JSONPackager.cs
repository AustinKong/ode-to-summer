using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public string levelName;
    public int tilemapSize;
    public List<Note> track = new List<Note>();
    public List<Vector2Int> tiles = new List<Vector2Int>();
}


public class JSONPackager : MonoBehaviour
{
    public List<Sprite> woodenTiles = new List<Sprite>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    //USE IMAGE TO GET WIDTH AND HEIGHT OF TILEMAP
    //USE COLORED PIXELS TO GET TILED POSITION
    //SVAE TILES INTO 2D ARRAY AT RUNTIME
    /*
    public void PackageLevelToJSON()
    {
        Level cog = new Level();
        cog.tilemapSize = GridManager.instance.tilemapSize;
        cog.levelName = "1-1test";
        for(int i = 0; i < GridManager.instance.tilemapSize; i++)
        {
            for (int k = 0; k < GridManager.instance.tilemapSize; k++)
            {
                Tile nut = GridManager.instance.ReadFromPosition(new Vector2Int(i, k));
                if (nut.type != TileType.none) cog.tiles.Add(new Vector2Int(i, k));
            }
        }

        string json = JsonUtility.ToJson(cog);
        System.IO.File.WriteAllText("D:/Projects/An Ode/Ode/Assets/JSON/" + cog.levelName + ".json", json);
        Debug.Log("D:/Projects/An Ode/Ode/Assets/JSON/" + cog.levelName + ".json");
    }

    public Level LoadLevelFromJSON(string filename)
    {
        string json = System.IO.File.ReadAllText("D:/Projects/An Ode/Ode/Assets/JSON/" + filename + ".json");
        return JsonUtility.FromJson<Level>(json);
    }
    */
}
