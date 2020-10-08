using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionDropDown : MonoBehaviour
{
    [SerializeField]
    private List<string> _windowResolutions = new List<string>() { "Select Resolution, 1024x576", "1152x648", "1280x720", "1366x768", "1600x900", "1920x1080" };

    public int width;
    public int heigth;
    public int _currentWidth;
    public int _currentHeigth;

    private Dropdown dropDown;

    private Text _dropDownSelectedOption;

    void Start()
    {
        dropDown = GetComponent<Dropdown>();
        dropDown.AddOptions(_windowResolutions);
        _currentWidth = Screen.width;
        _currentHeigth = Screen.height;
        _dropDownSelectedOption = this.gameObject.transform.GetChild(0).GetComponent<Text>();
    }

    public void SetDropDownResolution(int index)
    {
        string[] sRes;

        if (index != 0)
        {
            _dropDownSelectedOption.text = _windowResolutions[index];
            sRes = _dropDownSelectedOption.text.Split('x');
            string sWidth = sRes[0];
            string sHeigth = sRes[1];
            width = Convert.ToInt32(sWidth);
            heigth = Convert.ToInt32(sHeigth);
        }
    }
}
