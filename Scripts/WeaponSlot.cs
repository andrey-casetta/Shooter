using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour,
       IPointerClickHandler
     , IDragHandler
     , IPointerEnterHandler
     , IPointerExitHandler
{
    Image img;
    Color target;
    Color color;

    public int iWeaponSlot;

    /*
     * colors
     * pink FF6DD6
     * yellow FFCB52
     * blue 29C7FF
     * green 08DF00
     */

    [SerializeField]
    private Color selectedColor = Color.white;

    string hexColorPink = "#FF6DD6";
    string hexColorYellow = "#FFCB52";
    string hexColorBlue = "#29C7FF";
    string hexColorGreen = "#08DF00";


    void Awake()
    {
        img = GetComponent<Image>();
        if (ColorUtility.TryParseHtmlString(hexColorBlue, out color))
            target = color;
    }

    void Update()
    {
        if (img != null)
            img.color = Vector4.MoveTowards(img.color, target, Time.deltaTime * 10);
    }

    //on click
    public void OnPointerClick(PointerEventData eventData)
    {
        if (ColorUtility.TryParseHtmlString(hexColorPink, out color))
            target = color;
    }
    
    //on drag
    public void OnDrag(PointerEventData eventData)
    {
        if (ColorUtility.TryParseHtmlString(hexColorYellow, out color))
            target = color;
    }

    //on hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ColorUtility.TryParseHtmlString(hexColorGreen, out color))
            target = color;
    }

    //on exit
    public void OnPointerExit(PointerEventData eventData)
    {
        if (ColorUtility.TryParseHtmlString(hexColorBlue, out color))
            target = color;
    }

    private void OnEnable()
    {
        ResetColor();
    }

    public void ResetColor()
    {
        if (ColorUtility.TryParseHtmlString(hexColorBlue, out color))
            target = color;
    }
}
