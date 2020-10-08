using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeSlider : MonoBehaviour
{
    public float _currentValue;
    public float _latestValue;

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
        _latestValue = value;
    }

    public void ApplyNewValue(float value)
    {
        _currentValue = value;
        _latestValue = value;
        slider.value = value;
    }
}
