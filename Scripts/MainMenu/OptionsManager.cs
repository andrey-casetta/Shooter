using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    private Resolution currentResolution;

    [SerializeField]
    private GameObject confirmPanel;

    [SerializeField]
    private GameObject resolutionDropDown;

    [SerializeField]
    private ScreenTypeDropDown windowStyleDropDown;

    [SerializeField]
    private DefaultSlider brightnessSliderScript, masterVolumeSliderScript, musicVolumeSliderScript, effectsVolumeSliderScript;

    [SerializeField]
    private GameObject languageDropdown;

    private Slider brightnessSlider, masterVolumeSlider, musicVolumeSlider, effectsVolumeSlider;

    private ResolutionDropDown resDropScript;
    private MainMenuManager menuManager;

    private void Start()
    {
        currentResolution = Screen.currentResolution;
        menuManager = MainMenuManager.instance;
        resDropScript = resolutionDropDown.GetComponent<ResolutionDropDown>();
        brightnessSlider = brightnessSliderScript.GetComponent<Slider>();
        masterVolumeSlider = masterVolumeSliderScript.GetComponent<Slider>();
        musicVolumeSlider = musicVolumeSliderScript.GetComponent<Slider>();
        effectsVolumeSlider = effectsVolumeSliderScript.GetComponent<Slider>();

    }

    public void ApplyChanges()
    {
        SetResolution(resDropScript.width, resDropScript.heigth);
        ChangeMusicVolume(musicVolumeSliderScript._latestValue);
        ChangeEffectsVolume(effectsVolumeSliderScript._latestValue);
        ChangeMasterVolume(masterVolumeSliderScript._latestValue);
        ChangeBrigthness(brightnessSliderScript._latestValue);
        windowStyleDropDown.ApplyNewValue(windowStyleDropDown.currentIndex);

    }

    public void GetValues()
    {
        musicVolumeSliderScript.GetCurrentValue();
        effectsVolumeSliderScript.GetCurrentValue();
        masterVolumeSliderScript.GetCurrentValue();
        brightnessSliderScript.GetCurrentValue();
    }

    public void RevertChanges()
    {
        SetResolution(resDropScript._currentWidth, resDropScript._currentHeigth);
        ChangeMusicVolume(musicVolumeSliderScript._currentValue);
        ChangeEffectsVolume(effectsVolumeSliderScript._currentValue);
        ChangeMasterVolume(masterVolumeSliderScript._currentValue);
        ChangeBrigthness(brightnessSliderScript._currentValue);
        windowStyleDropDown.ApplyNewValue(windowStyleDropDown.latestValue);

    }

    public void EnableCountdown()
    {
        //confirmPanel.SetActive(true);
        //InteractObjects();
    }


    private void InteractObjects()
    {
        masterVolumeSlider.interactable = !masterVolumeSlider.interactable;
        musicVolumeSlider.interactable = !musicVolumeSlider.interactable;
        effectsVolumeSlider.interactable = !effectsVolumeSlider.interactable;
        brightnessSlider.interactable = !brightnessSlider.interactable;
    }

    public void GetWindowMode()
    {
        string s = windowStyleDropDown.GetComponent<ScreenTypeDropDown>().GetCurrentOption();

        if (s.Equals("Windowed"))
            ScreenStyleWindowed();
        else
            ScreenStyleFullScreen();
    }

    public void ScreenStyleFullScreen()
    {
        Screen.SetResolution(currentResolution.width, currentResolution.height, FullScreenMode.FullScreenWindow);
    }

    public void ScreenStyleWindowed()
    {
        Screen.SetResolution(currentResolution.width, currentResolution.height, FullScreenMode.Windowed);
    }

    public void SetResolution(int width, int heigth)
    {
        Screen.SetResolution(width, heigth, true);
    }

    public void ChangeResolution()
    {
        SetResolution(resDropScript.width, resDropScript.heigth);
    }

    public void ChangeBrigthness(float value)
    {
        brightnessSliderScript.ApplyNewValue(value);
    }

    public void ChangeMasterVolume(float value)
    {
        masterVolumeSliderScript.ApplyNewValue(value);
    }

    public void ChangeEffectsVolume(float value)
    {
        effectsVolumeSliderScript.ApplyNewValue(value);
    }

    public void ChangeMusicVolume(float value)
    {
        musicVolumeSliderScript.ApplyNewValue(value);
    }

}
