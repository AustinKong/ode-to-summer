using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[CustomEditor(typeof(JSONPackager))]
public class JSONPackagerEditor : Editor
{
    JSONPackager packager;

    void OnEnable()
    {
        packager = ((MonoBehaviour)target).gameObject.GetComponent<JSONPackager>();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        //SNAP ALL OBJECTS TO GRID
        if (GUILayout.Button("Snap all objects"))
        {
            List<GameObject> rootObjects = new List<GameObject>();
            rootObjects.AddRange(GameObject.FindGameObjectsWithTag("Object"));
            rootObjects.AddRange(GameObject.FindGameObjectsWithTag("Tile"));

            for (int i = 0; i < rootObjects.Count; ++i)
            {
                GameObject gameObject = rootObjects[i];
                gameObject.transform.position = IsometricMath.IsometricToWorldCoordinatesSnapped(IsometricMath.WorldToIsometricCoordinatesSnapped(gameObject.transform.position));
            }
        }

        //MAKE THE OBJECTS BE POSIITONED AT 0,0
        if (GUILayout.Button("Make array not pisyy cuz of arr[-1]"))
        {
            List<GameObject> rootObjects = new List<GameObject>();
            rootObjects.AddRange(GameObject.FindGameObjectsWithTag("Object"));
            rootObjects.AddRange(GameObject.FindGameObjectsWithTag("Tile"));
            float biggestX = 0;
            float biggestY = 0;
            float smallestX = 100;
            float smallestY = 100;
            foreach (GameObject tile in rootObjects)
            {
                Vector2 gear = IsometricMath.WorldToIsometricCoordinates(tile.transform.position);
                smallestX = gear.x < smallestX ? gear.x : smallestX;
                smallestY = gear.y < smallestY ? gear.y : smallestY;
                biggestX = gear.x > biggestX ? gear.x : biggestX;
                biggestY = gear.y > biggestY ? gear.y : biggestY;
            }
            Vector2 offset = -IsometricMath.IsometricToWorldCoordinates(new Vector2(smallestX, smallestY));
            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
            Vector2 bolt = IsometricMath.IsometricToWorldCoordinates(new Vector2((biggestX - smallestX) / 2, (biggestY - smallestY) / 2));
            cam.transform.position = new Vector3(bolt.x , bolt.y, -10);
            GameObject.Find("Manager").GetComponent<GridManager>().tilemapSize = Mathf.Max((int)(biggestX - smallestX + 1),(int)(biggestY - smallestY + 1));
            for (int i = 0; i < rootObjects.Count; ++i)
            {
                GameObject gameObject = rootObjects[i];
                gameObject.transform.Translate(offset);
            }
        }

        //MAKE OBJECT AND TILES CHILDS OF THE GROUP
        if (GUILayout.Button("Pacakage my filthy hierarchy into something decent"))
        {
            Transform tileParent = GameObject.Find("Tiles").transform;
            Transform objectParent = GameObject.Find("Objects").transform;
            foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
            {
                tile.transform.parent = tileParent;
            }
            foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Object"))
            {
                tile.transform.parent = objectParent;
            }
        }



        if (GUILayout.Button("Randomize wood tiles"))
        {
            List<GameObject> rootObjects = new List<GameObject>();
            rootObjects.AddRange(GameObject.FindGameObjectsWithTag("Tile"));

            for (int i = 0; i < rootObjects.Count; ++i)
            {
                rootObjects[i].GetComponent<SpriteRenderer>().sprite = packager.woodenTiles[Random.Range(0, packager.woodenTiles.Count)];

            }
        }

        
    }
}
