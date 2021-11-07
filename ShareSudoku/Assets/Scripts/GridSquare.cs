using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{

    public GameObject numberText;

    public List<GameObject> notesNumber;

    private bool noteActive;

    private int number_ = 0;
    private int correctNumber = 0;

    private bool sqSelected_ = false;
    private int squareIndex_ = -1;
    private bool has_default_value = false;
    private bool has_wrong_value = false;

    public bool HasWrongValue()
    {
        return has_wrong_value;
    }

    public void SetSquareHasDefaultValue(bool defaultSquareVal)
    {
        has_default_value = defaultSquareVal;
    }

    public bool GetIfSquareHasDefaultValue()
    {
        return has_default_value;
    }

    public bool IsSelected()
    {
        return sqSelected_;
    }

    public void SetSquareIndex(int index)
    {
        squareIndex_ = index;
    }

    public void SetCorrectNum(int number)
    {
        correctNumber = number;
        has_wrong_value = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        noteActive = false;
        sqSelected_ = false;

        SetNoteNumberValue(0);
    }

    public List<string> GetSquareNotes()
    {
        List<string> notes = new List<string>();

        foreach (var number in notesNumber)
        {
            notes.Add(number.GetComponent<Text>().text);
        }
        return notes;
    }

    private void SetClearEmptyNotes()
    {
        foreach(var number in notesNumber)
        {
            if(number.GetComponent<Text>().text == "0")
            {
                number.GetComponent<Text>().text = " ";
            }
        }
    }

    private void SetNoteNumberValue(int value)
    {
        foreach(var number in notesNumber)
        {
            if(value <= 0)
            {
                number.GetComponent<Text>().text = " ";
            }
            else
            {
                number.GetComponent<Text>().text = value.ToString();
            }
        }
    }

    private void SetNoteSingleValue(int value, bool forceUpdate = false)
    {
        if (noteActive == false && forceUpdate == false)
        {
            return;
        }
        else if (value <= 0)
            notesNumber[value - 1].GetComponent<Text>().text = " ";
        else {
            if (notesNumber[value - 1].GetComponent<Text>().text == " " || forceUpdate)
                notesNumber[value - 1].GetComponent<Text>().text = value.ToString();
            else
                notesNumber[value - 1].GetComponent<Text>().text = " ";
        } 
    }

    public void SetGridNotes(List<int> notes)
    {
        foreach(var note in notes)
        {
            SetNoteSingleValue(note, true);
        }
    }

    public void OnNoteActive(bool active)
    {
        noteActive = active;
    }

    public void DisplayText()
    {
        if (number_ <= 0)
            numberText.GetComponent<Text>().text = " ";
        else
            numberText.GetComponent<Text>().text = number_.ToString();

    }

    public void SetNumber(int number)
    {
        number_ = number;
        DisplayText();        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        sqSelected_ = true;
        GameEvents.SquareSelectedMethod(squareIndex_);
    }

    public void OnSubmit(BaseEventData eventData)
    {

    }

    private void OnEnable()
    {
        GameEvents.OnUpdateSqNum += OnSetNumber;
        GameEvents.UpdateSelectedSquare += OnSquareSelected;
        GameEvents.OnNoteActive += OnNoteActive;
        GameEvents.OnEraseNumber += OnEraseNumber;
    }

    private void OnDisable()
    {
        GameEvents.OnUpdateSqNum -= OnSetNumber;
        GameEvents.UpdateSelectedSquare -= OnSquareSelected;
        GameEvents.OnNoteActive -= OnNoteActive;
        GameEvents.OnEraseNumber -= OnEraseNumber;
    }

    public void OnSetNumber(int number)
    {
        if (sqSelected_ && has_default_value == false)
        {
            if (noteActive == true && has_wrong_value == false)
            {
                SetNoteSingleValue(number);
            }
            else if (noteActive == false)
            {
                SetNoteNumberValue(0);
                SetNumber(number);
                if (number_ != correctNumber)
                {
                    has_wrong_value = true;
                    var colors = this.colors;
                    colors.normalColor = Color.red;
                    this.colors = colors;

                    GameEvents.OnWrongNumberMethod();
                }
                else
                {
                    has_wrong_value = false;
                    has_default_value = true;
                    var colors = this.colors;
                    colors.normalColor = Color.white;
                    this.colors = colors;
                }
            }
        }
    }
    public void OnSquareSelected(int squareIndex)
    {
        if (squareIndex_ != squareIndex)
        {
            sqSelected_ = false;
        }
    }

    public void SetSquareColour(Color col)
    {
        var colors = this.colors;
        colors.normalColor = col;
        this.colors = colors;
    }

    public void OnEraseNumber()
    {
        if (sqSelected_ && !has_default_value)
        {
            number_ = 0;
            has_wrong_value = false;
            SetSquareColour(Color.white);
            SetNoteNumberValue(0);
            DisplayText();
        }
    }
}
