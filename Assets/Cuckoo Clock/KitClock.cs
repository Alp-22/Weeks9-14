using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KitClock : MonoBehaviour
{
    public float timeAnHourTakes = 5;
    public Transform minuteHand;
    public Transform hourHand;

    public float t;
    public int hour = 0;

    public UnityEvent<int> OnTheHour;

    Coroutine clockIsRunning;
    IEnumerator doOneHour;

    void Start()
    {
        clockIsRunning = StartCoroutine(MoveTheClock());
    }

    private IEnumerator MoveTheClock()
    {
        while (true)
        {
            doOneHour = MoveTheClockHandsOneHour();
            yield return StartCoroutine(doOneHour);
        }
    }
    private IEnumerator MoveTheClockHandsOneHour()
    {
        t = 0;
        while (t < timeAnHourTakes)
        {
            t += Time.deltaTime;
            minuteHand.Rotate(0, 0, -(360/timeAnHourTakes)* Time.deltaTime);
            hourHand.Rotate(0, 0, -(30 / timeAnHourTakes) * Time.deltaTime);
            yield return null;
        }
        if (hour == 13)
        {
            hour = 1;
        }
        hour++;
        OnTheHour.Invoke(hour);
    }
    public void StopTheClock()
    {
        if(clockIsRunning != null)
        {
            StopCoroutine(clockIsRunning);
        }
        if(doOneHour != null)
        {
            StopCoroutine(doOneHour);
        }
    }
}
