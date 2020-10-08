using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShotType : MonoBehaviour
{
    public static ChangeShotType instance;

    [SerializeField]
    GameObject[] ShotPrefabs;

    private int currentPos = 0;
    private GameObject currentPrefab;
    private ObjectPoolerManager poolerManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        currentPrefab = ShotPrefabs[currentPos];
        poolerManager = ObjectPoolerManager.instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentPos--;
            currentPos = Mathf.Clamp(currentPos, 0, ShotPrefabs.Length - 1);
            SetCurrenWeapon(currentPos);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentPos++;
            currentPos = Mathf.Clamp(currentPos, 0, ShotPrefabs.Length - 1);
            SetCurrenWeapon(currentPos);
        }
    }

    public void SetCurrenWeapon(int pos)
    {
        currentPrefab = ShotPrefabs[pos];
    }

    public GameObject GetCurrentPrefab()
    {
        if (currentPrefab.GetComponent<PoolerTypes>() != null)
        {
            return poolerManager.GetPooledObject(currentPrefab.GetComponent<PoolerTypes>().type);
        }
        else return null;
    }

}
