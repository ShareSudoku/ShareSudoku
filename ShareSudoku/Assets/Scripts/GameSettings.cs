using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public enum GameMode
    {
        NOT_SET,
        Easy,
        Medium,
        Hard,
        VeryHard
    };

    private bool loadPreviousGame = false;
    private bool exitOnWin = false;
    public static GameSettings gsInstance;
    
    public void SetExitOnWin(bool set)
    {
        exitOnWin = set;
        loadPreviousGame = false;
    }

    public bool GetExitOnWin()
    {
        return exitOnWin;
    }

    public void SetLoadedGame(bool loadGame)
    {
        loadPreviousGame = loadGame;
    }

    public bool GetLoadPrevGame()
    {
        return loadPreviousGame;
    }

    private void Awake()
    {
        isPaused = false;
        if (gsInstance == null)
        {
            DontDestroyOnLoad(this);
            gsInstance = this;
        }
        else
            Destroy(this);
    }

    private GameMode _GameMode;
    private bool isPaused = false;

    public void SetPaused(bool paused) { isPaused = paused; }

    public bool GetPaused() { return isPaused; }

    private void Start()
    {
        _GameMode = GameMode.NOT_SET;
        loadPreviousGame = false;
    }

    public void SetGameMode(GameMode mode)
    {
        _GameMode = mode;
    }

    public void SetGameMode(string mode)
    {
        if (mode == "Easy") SetGameMode(GameMode.Easy);
        else if (mode == "Medium") SetGameMode(GameMode.Medium);
        else if (mode == "Hard") SetGameMode(GameMode.Hard);
        else if (mode == "Very Hard") SetGameMode(GameMode.VeryHard);
        else SetGameMode(GameMode.NOT_SET);
    }

    public string GetGameMode()
    {
        switch (_GameMode)
        {
            case GameMode.Easy: return "Easy";
            case GameMode.Medium: return "Medium";
            case GameMode.Hard: return "Hard";
            case GameMode.VeryHard: return "Very Hard";
        }

        Debug.LogError("Error: Game level is not set");
        return " ";
    }
}
