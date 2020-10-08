using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject shotPos;

    [SerializeField]
    private GameObject flare;

    private ObjectPoolerPrefabID flarePos;

    private PlayerStatsManager statsManager;
    private int currentAmmo = 100;

    public int CurrentAmmo
    {
        get { return currentAmmo; }
        set
        {
            if (currentAmmo == value) return;
            currentAmmo = value;
            if (OnVariableChange != null)
                OnVariableChange(currentAmmo);
        }
    }

    public delegate void OnVariableChangeDelegate(int newVal);
    public event OnVariableChangeDelegate OnVariableChange;

    private void Start()
    {
        this.OnVariableChange += VariableChangeHandler;
        statsManager = GetComponent<PlayerStatsManager>();
        flarePos = flare.GetComponent<ObjectPoolerPrefabID>();
    }

    private void VariableChangeHandler(int newVal)
    {
        statsManager.UpdateAmmo(newVal);
        GameObject flareGunAnim = ObjectPoolerManager.instance.GetPooledObject(flare.GetComponent<PoolerTypes>().type);
        flareGunAnim.transform.position = shotPos.transform.position;
        flareGunAnim.transform.rotation = shotPos.transform.rotation;

        flareGunAnim.SetActive(true);
    }

    public void Shoot(GameObject newGo)
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newGo.transform.position = shotPos.transform.position;
        Vector3 shootDir = (worldMousePos - shotPos.transform.position).normalized;
        Vector3 dir = worldMousePos - newGo.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Rigidbody2D newGoRg = newGo.GetComponent<Rigidbody2D>();
        newGoRg.transform.eulerAngles = new Vector3(0, 0, angle);
        newGo.SetActive(true);
        newGo.GetComponent<Bullet>().Setup(shootDir);
        CurrentAmmo--;
    }
}
