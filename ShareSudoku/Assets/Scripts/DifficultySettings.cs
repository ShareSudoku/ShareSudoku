using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySettings : MonoBehaviour
{
    public enum GameMode
    {
        NOT_SET,
        Easy,
        Medium,
        Hard,
        VeryHard
    };

    public static DifficultySettings difInstance;
    
    private void Awake()
    {
        isPaused = false;
        if (difInstance == null)
        {
            DontDestroyOnLoad(this);
            difInstance = this;
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
