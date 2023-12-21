using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricMath : MonoBehaviour
{
    
    public static Vector2 WorldToIsometricCoordinates(Vector2 coord) => new Vector2(coord.x / 2f + coord.y, coord.y - coord.x / 2f);
    public static Vector2 IsometricToWorldCoordinatesSnapped(Vector2Int coord) => new Vector2(coord.x - coord.y, (coord.x + coord.y) / 2f);
    public static Vector2 IsometricToWorldCoordinates(Vector2 coord) => new Vector2(coord.x - coord.y, (coord.x + coord.y) / 2f);
    public static Vector2Int WorldToIsometricCoordinatesSnapped(Vector2 coord) => new Vector2Int(Mathf.RoundToInt(coord.x / 2f + coord.y), Mathf.RoundToInt(coord.y - coord.x / 2f));
}
