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
        GameSettings.gsInstance.SetGameMode(GameSettings.GameMode.Easy);
        SceneManager.LoadScene(name);
    }

    public void LoadMediumGame(string name)
    {
        GameSettings.gsInstance.SetGameMode(GameSettings.GameMode.Medium);
        SceneManager.LoadScene(name);

    }
    public void LoadHardGame(string name)
    {
        GameSettings.gsInstance.SetGameMode(GameSettings.GameMode.Hard);
        SceneManager.LoadScene(name);

    }
    public void LoadVeryHardGame(string name)
    {
        GameSettings.gsInstance.SetGameMode(GameSettings.GameMode.VeryHard);
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

    public void SetPause(bool paused)
    {
        GameSettings.gsInstance.SetPaused(paused);
    }

    public void LoadPrevGame(bool load)
    {
        GameSettings.gsInstance.SetLoadedGame(load);
    }

    public void WinExitBT()
    {
        GameSettings.gsInstance.SetExitOnWin(true);
    }
}
