using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginObject : SimpleNoteObject
{
    public override void CallOnStart()
    {
        base.CallOnStart();
        Metronome.instance.RegisterOriginObject(this);
    }

    public override void Trigger()
    {
        state = ObjectState.inactive;
        CreatePulses(directions);
    }
}
