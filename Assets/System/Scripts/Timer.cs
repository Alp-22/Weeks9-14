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

        //Starts counting time
        time += Time.deltaTime;
        //Sets float to int and divides by 60 for minutes
        minutes = Mathf.FloorToInt(time/60);
        //Minuses seconds by minutes so that it resets back to 0 everytime a minute is added
        seconds = Mathf.FloorToInt(time) - minutes*60;
        //Set text in UI
        text.text = "Timer: " + minutes + "m " + seconds + "s";
    }
}
