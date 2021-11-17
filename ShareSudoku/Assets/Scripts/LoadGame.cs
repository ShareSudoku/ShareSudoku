using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoadGame : MonoBehaviour
{

    public Text time;
    public Text difficulty;

    string LeadingZero(int a)
    {
        return a.ToString().PadLeft(2, '0');
    }


    // Start is called before the first frame update
    void Start()
    {
        if (Configuration.CheckDataFile() == false)
        {
            gameObject.GetComponent<Button>().interactable = false;
            time.text = "";
            difficulty.text = "";
        }
        else
        {
            float dTime = Configuration.ReadGameTime();
            dTime += Time.deltaTime;
            TimeSpan ts = TimeSpan.FromSeconds(dTime);

            string hour = LeadingZero(ts.Hours);
            string min = LeadingZero(ts.Minutes);
            string sec = LeadingZero(ts.Seconds);

            time.text = hour + ":" + min + ":" + sec;

            difficulty.text = Configuration.ReadBoardLevel();
        }
    }

    public void LoadGameData()
    {
        GameSettings.gsInstance.SetGameMode(Configuration.ReadBoardLevel());
    }
}
