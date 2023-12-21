using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanddrawnAnimator : MonoBehaviour
{
    
    private float timePerFrame = 0.8f;
    private float time;
    private int currentFrame = 0;
    [SerializeField] private List<Sprite> frames = new List<Sprite>();

    private SpriteRenderer rend;
    private void Awake()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        time = Random.Range(0, timePerFrame);
        currentFrame = Random.Range(0, frames.Count);
        rend.sprite = frames[currentFrame];
    }

    private void Update()
    {
        if(time < timePerFrame)
        {
            time += Time.deltaTime;
        }
        else
        {
            rend.sprite = frames[currentFrame];
            currentFrame++;
            time = 0;
            if(currentFrame >= frames.Count) { currentFrame = 0; }
        }
    }
}
