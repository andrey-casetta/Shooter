using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private Button ResumeButton;

    [SerializeField]
    private Button RestartButton;

    [SerializeField]
    private Button QuitButton;

    private void Start()
    {
        ResumeButton.onClick.AddListener(HandleResumeClick);
        RestartButton.onClick.AddListener(HandleRestartClick);
        QuitButton.onClick.AddListener(HandleQuitClick);

    }

    void HandleResumeClick()
    {
        GameManagerTest.Instance.TogglePause();
    }

    void HandleRestartClick()
    {
        GameManagerTest.Instance.RestartGame();
    }

    void HandleQuitClick()
    {
        GameManagerTest.Instance.QuitGame();
    }
}
