using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePopUp : MonoBehaviour
{
    public Text textTimer;

    // Start is called before the first frame update
    public void DisplayTime()
    {
        textTimer.text = GameTimer.timerInstance.GetCurrentTime().text;
    }
}
