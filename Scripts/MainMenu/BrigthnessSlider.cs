using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrigthnessSlider : MonoBehaviour
{
    public float _currentValue;
    public float _latestValue;
    public float _savedValue;
    private Slider slider;

    private void OnEnable()
    {
        if (slider != null)
            slider.interactable = true;

        slider.value = _savedValue;
    }

    void Start()
    {
        slider = GetComponent<Slider>();
        _latestValue = slider.value;
        _currentValue = _latestValue;
        Debug.Log("x");
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
}
