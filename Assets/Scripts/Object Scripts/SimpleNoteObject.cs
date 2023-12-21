using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimpleNoteObject : Object
{
    public int key;
    public List<Vector2Int> directions = new List<Vector2Int>();
    

    public override void CallOnStart()
    {
        base.CallOnStart();
        Metronome.instance.endTick += ResetState;
        ShowIndicator();
        if (silent)
        {
            GameObject cog = Instantiate(PrefabReferenceManager.instance.silent, transform.position + new Vector3(0, -0.25f, 0), Quaternion.identity);
            cog.transform.parent = gameObject.transform;
            return;
        }
        try
        {
            //only for simple not obj
            GetComponentInChildren<TextMeshProUGUI>().text = ((char)(key + 65)).ToString();
        }
        catch { }
        
    }

    private void ResetState() => state = ObjectState.active;

    public override void Trigger()
    {
        if (!silent)
        {
            RegisterNote();
            AudioManager.instance.PlayNote(key);
        }
        //state = ObjectState.inactive;
        
        CreatePulses(directions);
        
        anim.SetTrigger("Trigger");
    }

    public void CreatePulses(List<Vector2Int> directions)
    {
        foreach (Vector2Int dir in directions)
        {
            GameObject cog = Instantiate(PrefabReferenceManager.instance.pulse);
            cog.GetComponent<Pulse>().Initialize(cartesianPosition, dir);
        }
    }

    private void RegisterNote()
    {
        Note cog = new Note();
        cog.key = key;
        cog.beat = Metronome.instance.GetCurrentBeat();
        NoteManager.instance.PlayNote(cog);
    }

    public void ShowIndicator()
    {
        GameObject cog = Instantiate(PrefabReferenceManager.instance.directionIndicator, transform.position, Quaternion.identity);
        cog.transform.parent = transform;
        foreach (Vector2Int dir in directions)
        {
            if (dir == Vector2Int.up)
            {
                cog.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (dir == Vector2Int.down)
            {
                cog.transform.GetChild(1).gameObject.SetActive(true);
            }
            else if (dir == Vector2Int.left)
            {
                cog.transform.GetChild(2).gameObject.SetActive(true);
            }
            else if (dir == Vector2Int.right)
            {
                cog.transform.GetChild(3).gameObject.SetActive(true);
            }
        }
        //yandere sim LETS GOOOOOOOOOOOOOO
        
    }

    public override void PickUp() => anim.SetBool("Picking Up", true);
    
    public override void PutDown() => anim.SetBool("Picking Up", false);
}
