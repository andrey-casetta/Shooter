using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerPrefabID : MonoBehaviour
{
    [SerializeField]
    private int iD;

    [SerializeField]
    private bool disable = true;
    
    [SerializeField]
    private float timeToDisable = 2f;

    public int ID { get => iD; }

    private void OnEnable()
    {
        if(disable)
        StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSecondsRealtime(timeToDisable);
        gameObject.SetActive(false);
    }
}
