using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public delegate void UpdateSqNum(int number);
    public static event UpdateSqNum OnUpdateSqNum;

    public static void UpdateSqNumMethod(int number)
    {
        if (OnUpdateSqNum != null)
            OnUpdateSqNum(number);
    }

    public delegate void SelectedSquare(int squareIndex);
    public static event SelectedSquare UpdateSelectedSquare;

    public static void SquareSelectedMethod(int squareIndex)
    {
        if (UpdateSelectedSquare != null)
            UpdateSelectedSquare(squareIndex);
    }

    public delegate void WrongNumber();
    public static event WrongNumber OnWrongNumber;

    public static void OnWrongNumberMethod()
    {
        if (OnWrongNumber != null)
            OnWrongNumber();
    }

    public delegate void NoteActive(bool active);
    public static event NoteActive OnNoteActive;

    public static void OnNoteActiveMethod(bool active)
    {
        if (OnNoteActive != null)
            OnNoteActive(active);
    }

    public delegate void Eraser();
    public static event Eraser OnEraseNumber;

    public static void OnEraseNumberMethod()
    {
        if (OnEraseNumber != null)
            OnEraseNumber();
    }

    public delegate void BoardCompleted();
    public static event BoardCompleted OnBoardCompleted;

    public static void OnBoardCompletedMethod()
    {
        if (OnBoardCompleted != null)
            OnBoardCompleted();
    }

    public delegate void CheckBoard();
    public static event CheckBoard OnCheckBoard;

    public static void OnCheckBoardMethod()
    {
        if (OnCheckBoard != null)
            OnCheckBoard();
    }

    public delegate void GameOver();  
    public static event GameOver OnGameOver;

    public static void OnGameOverMethod()
    {
        if (OnGameOver != null)
            OnGameOver();
    }
}
