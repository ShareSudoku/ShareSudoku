using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{

    public List<GameObject> errorXs;
    public GameObject gameOverPopUp;

    private int lives = 0;
    private int errorNum = 0;

    public static PlayerLives lInstance;

    private void Awake()
    {
        if (lInstance)
            Destroy(this);
        
        lInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        lives = errorXs.Count;
        errorNum = 0;

        if (GameSettings.gsInstance.GetLoadPrevGame())
        {
            errorNum = Configuration.ReadErrorNum();
            lives = errorXs.Count - errorNum;

            for(int err = 0; err < errorNum; err++)
            {
                errorXs[err].SetActive(true);
            }
        }
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

    public int GetErrorNums()
    {
        return errorNum;
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
