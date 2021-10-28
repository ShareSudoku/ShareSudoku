using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{

    public GameObject numberText;
    private int number_ = 0;
    private int correctNumber = 0;

    private bool sqSelected_ = false;
    private int squareIndex_ = -1;
    private bool has_default_value = false;

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
    }
    // Start is called before the first frame update
    void Start()
    {
        sqSelected_ = false;
    }

    // Update is called once per frame
    void Update()
    {

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
    }

    private void OnDisable()
    {
        GameEvents.OnUpdateSqNum -= OnSetNumber;
        GameEvents.UpdateSelectedSquare += OnSquareSelected;
    }

    public void OnSetNumber(int number)
    {
        if (sqSelected_ && has_default_value == false)
        {
            SetNumber(number);
            if (number_ != correctNumber)
            {
                var colors = this.colors;
                colors.normalColor = Color.red;
                this.colors = colors;

                GameEvents.OnWrongNumberMethod();
            }
            else
            {
                has_default_value = true;
                var colors = this.colors;
                colors.normalColor = Color.white;
                this.colors = colors;
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
}
