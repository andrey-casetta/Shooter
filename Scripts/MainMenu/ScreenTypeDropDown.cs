using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTypeDropDown : MonoBehaviour
{
    [SerializeField]
    private List<string> _windowModeTypes = new List<string>() { "Select Screen Mode", "FullScren", "Windowed" };

    public int currentIndex;
    public int latestValue;

    private Dropdown dropDown;

    private Text _dropDownSelectedOption;

    void Start()
    {
        dropDown = GetComponent<Dropdown>();
        dropDown.AddOptions(_windowModeTypes);
        _dropDownSelectedOption = this.gameObject.transform.GetChild(0).GetComponent<Text>();

        if (Screen.fullScreen)
            currentIndex = 1;
        else
            currentIndex = 2;
    }

    public void SetDropDownWindowOption(int index)
    {
        if (index != 0)
        {
            _dropDownSelectedOption.text = _windowModeTypes[index];
            latestValue = currentIndex;
            currentIndex = index;
        }
    }

    public string GetCurrentOption()
    {
        return _dropDownSelectedOption.text;
    }

    public void ApplyNewValue(int value)
    {
        currentIndex = value;
        latestValue = value;
        _dropDownSelectedOption.text = _windowModeTypes[value];
    }
}
