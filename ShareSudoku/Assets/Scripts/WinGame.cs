using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGame : MonoBehaviour
{

    public GameObject winPopUp;
    public Text textTimer;

    // Start is called before the first frame update
    void Start()
    {
        winPopUp.SetActive(false);
        textTimer.text = GameTimer.timerInstance.GetCurrentTime().text;
    }

    private void OnBoardCompleted()
    {
        winPopUp.SetActive(true);
        textTimer.text = GameTimer.timerInstance.GetCurrentTime().text;
    }

    private void OnEnable()
    {
        GameEvents.OnBoardCompleted += OnBoardCompleted;
    }
    
    private void OnDisable()
    { 
        GameEvents.OnBoardCompleted -= OnBoardCompleted;
    }
}
