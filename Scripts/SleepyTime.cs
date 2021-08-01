using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Timers;
using System.Runtime.InteropServices;

public class SleepyTime : MonoBehaviour
{

    [DllImport("Powrprof.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
    public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);
    private static System.Timers.Timer _delayTimer;
    private float currentMouseX;
    private float currentMouseY;
    public float oldMouseX;
    public float oldMouseY;

    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    //public Text timeText;

    Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        MousePostion();

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                SetSuspendState(true, true, true);

            }
        }
    }

    private void MousePostion()
    {
        mousePos = Input.mousePosition;

        currentMouseX = mousePos.x;
        currentMouseY = mousePos.y;

        delay(1000);
        if (currentMouseX == oldMouseX)
        {
            Debug.Log("Same: " + currentMouseX);
            timerIsRunning = true;
            
            
        }
        else
        {
            Debug.Log("Not the Same: " + currentMouseX);
            timerIsRunning = false;
            timeRemaining = 10;
        }

        oldMouseX = currentMouseX;
        oldMouseY = currentMouseY;

    }

    private void delay(int Time_delay)
    {
        int i = 0;
        _delayTimer = new System.Timers.Timer();
        _delayTimer.Interval = Time_delay;
        _delayTimer.AutoReset = false; //so that it only calls the method once
        _delayTimer.Elapsed += (s, args) => i = 1;
        _delayTimer.Start();
        while (i == 0) { };
    }

    private void putScreenToSleep(bool UserInput) 
        {    
            bool bHibernate;
            bHibernate = UserInput;
        }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        Debug.Log(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

}
