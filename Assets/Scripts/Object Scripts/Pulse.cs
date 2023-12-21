using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    private Vector2Int cartesianDirection;
    private Vector2Int cartesianPosition;

    private void Start() => Metronome.instance.tick += TickUpdate;

    private float translationTime = 0;

    private void TickUpdate()
    {
        cartesianPosition += cartesianDirection;
        StartCoroutine(Translate());
    }

    private void PositionCheck()
    {
        Tile cog = GridManager.instance.ReadFromPosition(cartesianPosition);
        if (cog.type == TileType.obstacle || cog.type == TileType.outOfBounds)
        {
            Deinitialize();
        }
        if (cog.type == TileType.filled && cog.obj.GetComponent<Object>().state == ObjectState.active)
        {
            cog.obj.GetComponent<Object>().Trigger();
            Deinitialize();
        }
    }

    private void Deinitialize()
    {
        Metronome.instance.tick -= TickUpdate;
        Destroy(gameObject);
    }

    public void Initialize(Vector2Int pos, Vector2Int dir)
    {
        cartesianPosition = pos;
        transform.position = IsometricMath.IsometricToWorldCoordinatesSnapped(cartesianPosition);
        cartesianDirection = dir;
        //StartCoroutine(Translate());
    }
    IEnumerator Translate()
    {
        
        #region jumping implementation
        Vector2 startingPos = transform.position;
        Vector2 targetPos = IsometricMath.IsometricToWorldCoordinatesSnapped(cartesianPosition);
        while (translationTime < 1f)
        {
            translationTime += Time.deltaTime * Metronome.instance.bps * 1.2f;
            transform.position = Vector3.Slerp(startingPos, targetPos, translationTime);
            yield return null;
        }
        #endregion
        
        /*
        #region sliding implementation
        Vector2 targetDir = IsometricMath.IsometricToWorldCoordinatesSnapped(cartesianDirection);
        while (translationTime < 1/Metronome.bps)
        {
            translationTime += Time.deltaTime;
            transform.Translate(targetDir * Time.deltaTime);
            yield return null;
        }
        #endregion
        */
        PositionCheck();
        translationTime = 0;
        
    }

}
