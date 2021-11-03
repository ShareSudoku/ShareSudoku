using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class NoteButton : Selectable, IPointerClickHandler
{

    public Sprite onImage;
    public Sprite offImage;

    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        active = !active;
        if (active)
            GetComponent<Image>().sprite = onImage;
        else 
            GetComponent<Image>().sprite = offImage;

        GameEvents.OnNoteActiveMethod(active);
    }
}
