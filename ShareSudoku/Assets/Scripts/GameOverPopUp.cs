using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPopUp : MonoBehaviour
{
    public Text textTimer;

    // Start is called before the first frame update
    void Start()
    {
        textTimer.text = GameTimer.timerInstance.GetCurrentTime().text;
    }

}
