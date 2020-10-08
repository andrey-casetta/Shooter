using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject startPanel;

    [SerializeField]
    private GameObject optionsPanel;

    [SerializeField]
    private GameObject creditsPanel;

    [SerializeField]
    private Dropdown _dropDownWindowModes;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    private int _resolutionWidth;
    private int _resolutionHeigth;

    [SerializeField]
    private Animator _mainMenuAnimator;
    [SerializeField]
    private Animation _mainMenuAnimation;
    [SerializeField]
    private AnimationClip _fadeOutClip;
    [SerializeField]
    private AnimationClip _fadeInClip;


    public void OnFadeOutComplete()
    {
        OnMainMenuFadeComplete.Invoke(true);
    }

    public void OnFadeInComplete()
    {
        UIManager.Instance.SetDummyCameraActive(true);
        OnMainMenuFadeComplete.Invoke(false);
    }

    public void FadeIn()
    {
        _mainMenuAnimator.SetTrigger("fadeIn");
    }

    public void FadeOut()
    {
        UIManager.Instance.SetDummyCameraActive(false);
        _mainMenuAnimator.Play("MainMenuFadeOutAnim");
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameManagerTest.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    void HandleGameStateChanged(GameManagerTest.GameState currentState, GameManagerTest.GameState previousState)
    {
        if (previousState == GameManagerTest.GameState.PREGAME && currentState == GameManagerTest.GameState.RUNNING)
        {
            FadeOut();
        }

        if (previousState != GameManagerTest.GameState.PREGAME && currentState == GameManagerTest.GameState.PREGAME)
        {
            FadeIn();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMainMenu();
        }
    }

    public void BackToMainMenu()
    {
        startPanel.SetActive(false);
        //optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void OpenStartMenu()
    {
        startPanel.SetActive(true);
    }

    public void CloseStartMenu()
    {
        startPanel.SetActive(false);
    }

    public void OpenOptionsMenu()
    {
        //optionsPanel.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        //optionsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
