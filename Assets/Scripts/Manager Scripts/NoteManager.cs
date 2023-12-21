using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Note
{
    public int key;
    public int beat;
}
public class NoteManager : MonoBehaviour
{
    public static NoteManager instance; private void Awake() => instance = this;

    [SerializeField] private GameObject noteStemUp;
    [SerializeField] private GameObject noteStemDown;
    public Transform noteStart; //Bottom left start
    public Transform noteEnd; //Top right end

    private List<GameObject> noteGameobjects = new List<GameObject>();
    [SerializeField]private List<Note> targetNotes = new List<Note>();
    private List<Note> playedNotes = new List<Note>();

    private int correctNotesPlayed = 0;

    public void PlayNote(Note note)
    {
        CheckNote(note);
        playedNotes.Add(note);
    }

    private void Start()
    {
        for(int i = 0; i < targetNotes.Count; i++)
        {
            GameObject cog;
            if (targetNotes[i].key > 1) cog = Instantiate(noteStemDown);
            else cog = Instantiate(noteStemUp);
            cog.transform.SetParent(noteStart.transform.parent, false);
            cog.GetComponent<RectTransform>().localPosition = new Vector3((noteEnd.localPosition.x - noteStart.localPosition.x)/22f * targetNotes[i].beat + noteStart.localPosition.x, (noteEnd.localPosition.y - noteStart.localPosition.y)/10f * (4 + targetNotes[i].key) + noteStart.localPosition.y);
            noteGameobjects.Add(cog);
        }
    }

    public void ResetCorrectNotes()
    {
        correctNotesPlayed = 0;
        foreach (GameObject obj in noteGameobjects)
        {
            obj.GetComponent<Image>().enabled = true;
        }
    }

    public void CheckNote(Note note)
    {
        if (note.key == targetNotes[correctNotesPlayed].key) //first check if notes are same
        {
            
            if (correctNotesPlayed == 0)
            {
                FadeNote();
                correctNotesPlayed++;
                return;
            }
            if (note.beat - playedNotes[playedNotes.Count - 1].beat == targetNotes[correctNotesPlayed].beat - targetNotes[correctNotesPlayed - 1].beat)
            {
                FadeNote();
                correctNotesPlayed++;
                if (correctNotesPlayed == targetNotes.Count)
                {
                    SceneTransition.instance.NextLevel();
                    Metronome.instance.enabled = false;
                }
                return;
            }
            
        }
        ResetCorrectNotes();

    }

    private void FadeNote()
    {
        noteGameobjects[correctNotesPlayed].GetComponent<Image>().enabled = false;
    }
}
