using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private Transform shadowCaster;

    private void Update()
    {
        float cog = 1f-shadowCaster.transform.localPosition.y;
        transform.localScale = new Vector3(cog, cog, 1);
    }
}
