using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGame : MonoBehaviour
{

    public GameObject winPopUp;

    // Start is called before the first frame update
    void Start()
    {
        winPopUp.SetActive(false);
    }

    private void OnBoardCompleted()
    {
        winPopUp.SetActive(true);
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
