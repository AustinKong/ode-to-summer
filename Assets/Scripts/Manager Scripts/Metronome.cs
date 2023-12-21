using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class Metronome : MonoBehaviour
{
    public static Metronome instance; private void Awake() => instance = this;

    [SerializeField] private RectTransform hand;
    public int bps = 2;

    public event UnityAction tick;
    public event UnityAction endTick;

    private int beat = 1;
    private bool doTick = false;
    float ticker = 0;

    private List<OriginObject> origins = new List<OriginObject>();

    public void RegisterOriginObject(OriginObject obj) => origins.Add(obj);

    private void Start()
    {
        tick += TickUpdate;
        endTick += EndTicking;
        GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

    private void Update()
    {
        if (doTick)
        {
            float sinValue = Mathf.Sin(Time.time * bps * Mathf.PI);
            hand.rotation = Quaternion.Euler(0, 0, sinValue * 35f);
        }
        
        ticker += Time.deltaTime;
        if (ticker >= 1f/bps)
        {
            if (doTick) tick();
            ticker = 0;
        }
    }

    public void StartTicking()
    {
        doTick = true;
        ticker = 0;
        InteractionManager.instance.enabled = false;
        hand.transform.parent.GetComponent<Button>().enabled = false;
    }

    private void EndTicking() 
    {
        hand.transform.parent.GetComponent<Button>().enabled = true;
        InteractionManager.instance.enabled = true;
    }
    

    private void TickUpdate()
    {
        if(tick.GetInvocationList().Length <= 1)
        {
            doTick = false;
            beat = 1;
            NoteManager.instance.ResetCorrectNotes();
            endTick();
        }
        else
        {
            beat++;
        }
    }

    public int GetCurrentBeat() => beat;

    private void ButtonClicked()
    {
        foreach(OriginObject obj in origins)
        {
            obj.Trigger();
        }
        StartTicking();
    }
}
