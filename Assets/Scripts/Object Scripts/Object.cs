using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectState
{
    active,
    inactive
}
public class Object : MonoBehaviour
{
    public bool silent = false;
    public bool movable = true;
    public TileType type;
    [HideInInspector] public Vector2Int cartesianPosition;
    [HideInInspector] public ObjectState state = ObjectState.active;
    [HideInInspector] public Animator anim;
    public virtual void Trigger() { }

    public virtual void PickUp() { }

    public virtual void PutDown() { }

    public void ErrorShake() => anim.SetTrigger("Shake");

    public virtual void CallOnStart()
    {
        anim = GetComponentInChildren<Animator>();
        ShowPadlockIndicator();
    }

    public void ShowPadlockIndicator()
    {
        if (!movable)
        {
            GameObject cog = Instantiate(PrefabReferenceManager.instance.padlock, transform.position + new Vector3(0.54f,-0.2f), Quaternion.identity);
            cog.transform.parent = gameObject.transform;
        }
    }

    private void Start() => CallOnStart();
}
