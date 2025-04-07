using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float time;
    int seconds, minutes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        minutes = Mathf.FloorToInt(time/60);
        seconds = Mathf.FloorToInt(time) - minutes*60;
        text.text = "Timer: " + minutes + "m " + seconds + "s";
    }
}
