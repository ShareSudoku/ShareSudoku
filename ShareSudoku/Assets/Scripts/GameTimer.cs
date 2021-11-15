using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameTimer : MonoBehaviour
{

    private int hour = 0;
    private int min = 0;
    private int sec = 0;

    private Text textTimer;
    private float dTime;
    private bool timerStop = false;

    public static GameTimer timerInstance;

    private void Awake()
    {
        if (timerInstance)
            Destroy(this);

        timerInstance = this;

        textTimer = GetComponent<Text>();
        dTime = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        timerStop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(DifficultySettings.difInstance.GetPaused() == false && timerStop == false)
        {
            dTime += Time.deltaTime;
            TimeSpan tSpan = TimeSpan.FromSeconds(dTime);

            string hours = LeadingZero(tSpan.Hours);
            string minutes = LeadingZero(tSpan.Minutes);
            string seconds = LeadingZero(tSpan.Seconds);

            textTimer.text = hour + ":" + minutes + ":" +seconds;
        }
    }

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }

    public void OnGameOver()
    {
        timerStop = true;
    }

    private void OnEnable()
    {
        GameEvents.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= OnGameOver;
    }

    public static string GetTime()
    {
        return timerInstance.dTime.ToString();
    } 

    public Text GetCurrentTime()
    {
        return textTimer;
    }
}
