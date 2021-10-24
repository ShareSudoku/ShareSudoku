using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadEasyGame(string name)
    {
        DifficultySettings.difInstance.SetGameMode(DifficultySettings.GameMode.Easy);
        SceneManager.LoadScene(name);
    }

    public void LoadMediumGame(string name)
    {
        DifficultySettings.difInstance.SetGameMode(DifficultySettings.GameMode.Medium);
        SceneManager.LoadScene(name);

    }
    public void LoadHardGame(string name)
    {
        DifficultySettings.difInstance.SetGameMode(DifficultySettings.GameMode.Hard);
        SceneManager.LoadScene(name);

    }
    public void LoadVeryHardGame(string name)
    {
        DifficultySettings.difInstance.SetGameMode(DifficultySettings.GameMode.VeryHard);
        SceneManager.LoadScene(name);
    }

    public void ActivateObject(GameObject obj)
    {
        obj.SetActive(true);
    }


    public void DeActivateObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
