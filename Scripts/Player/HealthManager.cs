using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private PlayerStatsManager statsManager;
    private PlayerBase playerBase;
    private int MAX_HEALTH = 100;

    public int currentHP = 100;

    public int CurrentHP
    {
        get { return currentHP; }
        set
        {
            if (currentHP == value) return;
            currentHP = value;
            if (OnVariableChange != null)
                OnVariableChange(currentHP);
        }
    }

    public delegate void OnVariableChangeDelegate(int newVal);
    public event OnVariableChangeDelegate OnVariableChange;

    public float GetHealth()
    {
        return currentHP;
    }

    public void SetHealth(float value)
    {
        int h = Convert.ToInt32(value);
        CurrentHP = h;
    }

    public void TakeDamage(float value)
    {
        if (CurrentHP > 0)
        {
            int h = Convert.ToInt32(value);

            GameObject newPopup = ObjectPoolerManager.instance.GetPooledObject(PoolObjectType.PopUpText);
            newPopup.GetComponent<DamagePopUp>().Setup(h);
            newPopup.transform.position = this.transform.position;
            newPopup.SetActive(true);
            CurrentHP -= h;
        }

        if (CurrentHP <= 0)
        {
            GetComponent<Animator>().SetTrigger("die");
        }
    }

    private void Start()
    {
        this.OnVariableChange += VariableChangeHandler;
        statsManager = GetComponent<PlayerStatsManager>();
        playerBase = GetComponent<PlayerBase>();
    }

    private void VariableChangeHandler(int newVal)
    {
        statsManager.UpdateHP(newVal);
    }


}
