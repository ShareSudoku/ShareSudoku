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
}
