using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabReferenceManager : MonoBehaviour
{
    public static PrefabReferenceManager instance; private void Awake() => instance = this;
    public GameObject pulse;
    public GameObject directionIndicator;
    public GameObject padlock;
    public GameObject silent;
}
