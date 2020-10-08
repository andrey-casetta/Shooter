using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{
    LightAmmo,
    MediumAmmo,
    HeavyAmmo,
    GunFlare,
    PopUpText
}

public class PoolerTypes : MonoBehaviour
{
    public PoolObjectType type;

    [SerializeField]
    private bool disable = true;

    [SerializeField]
    private float timeToDisable = 2f;

    ObjectPoolerManager poolerManager;

    private void Start()
    {
        poolerManager = ObjectPoolerManager.instance;
    }

    private void OnEnable()
    {
        if (disable)
            StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSecondsRealtime(timeToDisable);
        poolerManager.CoolObject(this.gameObject, type);
    }
}
