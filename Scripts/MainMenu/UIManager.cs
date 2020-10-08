using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private MainMenuManager _mainMenu;

    [SerializeField]
    private PauseMenu _pauseMenu;

    [SerializeField]
    private Camera _dummyCamera;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    public void StartGame()
    {
        if (GameManagerTest.Instance.CurrentGameState != GameManagerTest.GameState.PREGAME)
            return;
        else
        {
            GameManagerTest.Instance.StartGame();
        }
    }

    private void Start()
    {
        GameManagerTest.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        _mainMenu.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
    }

    void HandleMainMenuFadeComplete(bool fadeOut)
    {
        OnMainMenuFadeComplete.Invoke(fadeOut);
    }

    void HandleGameStateChanged(GameManagerTest.GameState currentState, GameManagerTest.GameState previousState)
    {
        _pauseMenu.gameObject.SetActive(currentState == GameManagerTest.GameState.PAUSED);
    }

    private void Update()
    {
        if (GameManagerTest.Instance.CurrentGameState != GameManagerTest.GameState.PREGAME)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManagerTest.Instance.StartGame();
        }
    }

    public void SetDummyCameraActive(bool active)
    {
        _dummyCamera.gameObject.SetActive(active);
    }

    public void SetMainMenuActive(bool active)
    {
        _mainMenu.gameObject.SetActive(active);
    }
}
