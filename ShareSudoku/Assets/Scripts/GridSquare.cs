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

    private bool sqSelected_ = false;
    private int squareIndex_ = -1;

    public bool IsSelected()
    {
        return sqSelected_;
    }

    public void SetSquareIndex(int index)
    {
        squareIndex_ = index;
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
        if (sqSelected_)
            SetNumber(number);
    }

    public void OnSquareSelected(int squareIndex)
    {
        if (squareIndex_ != squareIndex)
        {
            sqSelected_ = false;
        }
    }
}
