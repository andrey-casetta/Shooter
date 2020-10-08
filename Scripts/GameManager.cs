using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int enemiesKilled = 0;
    private ComboTextManager comboManager;
    private SampleSceneUIManager uiManger;
    private Coroutine spreeCountdownCoroutine;
    private PlayerBase playerBase;
    private GameObject player;
    public int deathCounter;

    [SerializeField]
    private int _secondsForResetSpree;

    public CameraFollow cameraFollow;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        comboManager = GetComponent<ComboTextManager>();
        uiManger = SampleSceneUIManager.instance;
        player = GameObject.FindWithTag("Player");
        playerBase = player.GetComponent<PlayerBase>();
        cameraFollow.Setup(() => player.transform.position);
    }

    public void StopSpree()
    {
        comboManager.StopSpree();
        enemiesKilled = 0;
        uiManger.UpdateEnemiesKilled(enemiesKilled);
    }

    public void UpdateEnemieKilled()
    {
        if (spreeCountdownCoroutine != null)
            StopCoroutine(spreeCountdownCoroutine);
        
        enemiesKilled++;
        uiManger.UpdateEnemiesKilled(enemiesKilled);
        comboManager.UpdateSpree();
        spreeCountdownCoroutine = StartCoroutine(SpreeCountDown());
    }

    private IEnumerator SpreeCountDown()
    {
        yield return new WaitForSecondsRealtime(_secondsForResetSpree);
        StopSpree();
    }

    public void SavePlayer(PlayerBase playerBase)
    {
        SaveSystem.SavePlayer(this.playerBase);
    }

    public void LoadPlayer()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
        playerBase.CurrentLevel = playerData._currentLevel;
        playerBase.SetHealth(playerData._health);
        float posX = playerData._position[0];
        float posY = playerData._position[1];

        Vector2 pos = new Vector2(posX, posY);

        playerBase.transform.position = pos;

    }

    public void SaveSystemData(GameManager gameManager)
    {
        SaveSystem.SaveSystemData(gameManager);
    }

    public void LoadSystemData()
    {
        //SystemData systemData = SaveSystem.LoadSystemData();
        //playerBase.CurrentLevel = systemData._currentLevel;
        //playerBase.SetHealth(systemData._health);
        //float posX = systemData._position[0];
        //float posY = systemData._position[1];

        //Vector2 pos = new Vector2(posX, posY);

        //playerBase.transform.position = pos;

    }
}
