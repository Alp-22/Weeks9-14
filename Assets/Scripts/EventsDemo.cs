using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventsDemo : MonoBehaviour
{
    public RectTransform banana;
    public UnityEvent onTimerHasFinished;
    public float timerLength = 2;
    public float t;
    
    Vector3 size = new Vector3(1.2f, 1.2f, 1.2f);
    Vector3 sizeNormal = new Vector3(1, 1, 1);

    void Update()
    {
        t += Time.deltaTime;
        if (t>timerLength)
        {
            t = 0;
            onTimerHasFinished.Invoke();
        }
    }


    public void IJustPushedTheButton()
    {
        Debug.Log("I just pushed the button!");
    }

    public void IAlsoPushedTheButton()
    {
        Debug.Log("Me too!");
    }

    public void MouseIsNowInside()
    {
        Debug.Log("Mouse has entered the sprite!");
        banana.localScale = size;
    }
    public void MouseIsNowOutside()
    {
        Debug.Log("Mouse has left the sprite!");
        banana.localScale = sizeNormal;
    }
}
