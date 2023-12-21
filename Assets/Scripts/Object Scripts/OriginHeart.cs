using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginHeart : MonoBehaviour
{
    [SerializeField] private Transform originObject;
    private void Update()
    {
        transform.localPosition = new Vector3(0, 0.9f + Mathf.Sin(Time.time) * 0.1f + originObject.transform.localPosition.y/1.5f, 0);
    }
}
