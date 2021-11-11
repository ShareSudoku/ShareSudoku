using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{

    public List<GameObject> errorXs;
    public GameObject gameOverPopUp;

    private int lives = 0;
    private int errorNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        lives = errorXs.Count;
        errorNum = 0;
    }

    private void WrongNumber()
    {
        if(errorNum < errorXs.Count)
        {
            errorXs[errorNum].SetActive(true);
            errorNum++;
            lives--;
        }

        CheckGameOver();
    }

    private void CheckGameOver()
    {
        if (lives <= 0)
        {
            GameEvents.OnGameOverMethod();
            gameOverPopUp.SetActive(true);
        }
    }

    private void OnEnable()
    {
        GameEvents.OnWrongNumber += WrongNumber;   
    }

    private void OnDisable()
    {
        GameEvents.OnWrongNumber -= WrongNumber;
    }
}
