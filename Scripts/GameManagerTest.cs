using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerTest : Singleton<GameManagerTest>
{
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED
    }

    public GameObject[] SystemPrefabs;
    public Events.EventGameState OnGameStateChanged;

    private List<GameObject> _instancedSystemPrefabs;
    private List<AsyncOperation> _listOperations;

    private GameState _currentGameState = GameState.PREGAME;

    private string _currentLevelName = string.Empty;

    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        private set { _currentGameState = value; }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _listOperations = new List<AsyncOperation>();
        _instancedSystemPrefabs = new List<GameObject>();
        InstantiateSystemPrefabs();
        UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);

        Screen.SetResolution(1024, 768, FullScreenMode.FullScreenWindow);
    }

    private void Update()
    {
        if (CurrentGameState == GameState.PREGAME)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void UpdateState(GameState state)
    {
        GameState previousGameState = _currentGameState;
        _currentGameState = state;

        switch (_currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1f;

                break;
            case GameState.RUNNING:
                Time.timeScale = 1f;

                break;
            case GameState.PAUSED:
                Time.timeScale = 0f;
                break;

            default:
                break;

        }
        OnGameStateChanged.Invoke(_currentGameState, previousGameState);

    }

    void OnLoadLevelAsync(AsyncOperation ao)
    {
        if (_listOperations.Contains(ao))
        {
            _listOperations.Remove(ao);

            if (_listOperations.Count == 0)
            {
                UpdateState(GameState.RUNNING);
            }
        }
        Debug.Log("Load completed");
    }

    void OnUnloadLevelAsync(AsyncOperation ao)
    {
        Debug.Log("Unload completed");
    }

    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;

        for (int i = 0; i < SystemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            _instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        if (ao == null)
        {
            Debug.LogError("[GameManager] unable to load level " + levelName);
            return;
        }

        ao.completed += OnLoadLevelAsync;
        _listOperations.Add(ao);
        _currentLevelName = levelName;
    }

    public void UnLoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);

        if (ao == null)
        {
            Debug.LogError("[GameManager] unable to unload level " + levelName);
            return;
        }

        ao.completed += OnUnloadLevelAsync;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for (int i = 0; i < _instancedSystemPrefabs.Count; i++)
        {
            Destroy(_instancedSystemPrefabs[i]);
        }

        _instancedSystemPrefabs.Clear();
    }

    public void StartGame()
    {
        LoadLevel("SampleScene");
    }

    public void TogglePause()
    {
        UpdateState(_currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    }

    public void RestartGame()
    {
        UpdateState(GameState.PREGAME);
    }

    public void QuitGame()
    {
        //autosave
        //features before quitting
        Application.Quit();
    }

    void HandleMainMenuFadeComplete(bool fadeOut)
    {
        if (!fadeOut)
        {
            UnLoadLevel(_currentLevelName);
        }
    }

}
