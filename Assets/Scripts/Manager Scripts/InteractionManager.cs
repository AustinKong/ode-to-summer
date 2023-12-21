using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager instance; private void Awake() => instance = this;

    Transform selectedObject;
    Vector3 mousePosition;
    [SerializeField] new Camera camera;
    Vector2Int lastFramePos;

    void Update()
    {
        mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);

        if (selectedObject)
        {
            Vector2Int cog = IsometricMath.WorldToIsometricCoordinatesSnapped(mousePosition);
            if (GridManager.instance.ReadFromPosition(cog).type == TileType.empty && (cog - lastFramePos).magnitude == 1)
            {
                selectedObject.position = IsometricMath.IsometricToWorldCoordinatesSnapped(cog);
                lastFramePos = cog;
                AudioManager.instance.PlayClick();
            }
            else
            {
                //cannot move
            }
        }

        if (Input.GetMouseButtonDown(0))
        {

            if (GridManager.instance.ReadFromPosition(IsometricMath.WorldToIsometricCoordinatesSnapped(mousePosition)).type == TileType.filled)
            {
                selectedObject = GridManager.instance.ReadFromPosition(IsometricMath.WorldToIsometricCoordinatesSnapped(mousePosition)).obj.transform;
                if (selectedObject.GetComponent<Object>().movable)
                {
                    selectedObject.GetComponent<Object>().PickUp();
                    lastFramePos = IsometricMath.WorldToIsometricCoordinatesSnapped(mousePosition);
                    GridManager.instance.RemoveFromPosition(lastFramePos);
                }
                else
                {
                    //immovable
                    selectedObject.GetComponent<Object>().ErrorShake();
                    selectedObject = null;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            //selectedObject.GetComponentInChildren<CustomAnimator>().Drop();
            //selectedObject.GetComponent<Object>().Activate();
            selectedObject.GetComponent<Object>().PutDown();
            GridManager.instance.AddToPosition(lastFramePos, selectedObject.gameObject, selectedObject.GetComponent<Object>().type);
            selectedObject.GetComponent<Object>().cartesianPosition = lastFramePos;
            selectedObject = null;
        }
    }
}
