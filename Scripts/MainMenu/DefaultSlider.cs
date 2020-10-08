using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultSlider : MonoBehaviour
{
    public float _currentValue;
    public float _latestValue;
    public float _savedValue;
    private Slider slider;

    private void OnEnable()
    {
        if (slider != null)
            slider.interactable = true;

    }

    void Start()
    {
        slider = GetComponent<Slider>();
        _latestValue = slider.value;
        _currentValue = _latestValue;
    }

    public void SetNewValue(float value)
    {
        _currentValue = value;
    }

    public void ApplyNewValue(float value)
    {
        _currentValue = value;
        _latestValue = value;
        _savedValue = value;
        slider.value = value;
    }

    public void GetCurrentValue()
    {
        SystemData data = SaveSystem.LoadSystemData();
        if (data != null)
            _currentValue = data._brightness;
        else
            _currentValue = .5f;
    }
}
