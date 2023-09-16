using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Options_Item : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private Image image;
    private Text text;
    private bool isSelect;

    private Color blackColor = new Color(0, 0, 0, 0.6f);
    private Color whiteColor = new Color(1, 1, 1, 0.6f);
    public bool IsSelect { 
        get => isSelect;
        set
        {
            isSelect = value;
            if (isSelect)
            {
                image.color = blackColor;
                text.color = Color.white;
            }
            else
            {
                image.color = whiteColor;
                text.color = Color.black;

            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsSelect = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsSelect = false;
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {


    }

    public void Init(string txt)
    {
        image = GetComponent<Image>();
        text = transform.Find("Text").GetComponent<Text>();
        text.text = txt;

        IsSelect = false;

    }


}
