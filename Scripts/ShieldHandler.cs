using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHandler : MonoBehaviour
{
    public int MAX_SHIELD_HP = 80;
    public int shieldHp = 80;
    private float secondsToHealShield = 5f;
    private Coroutine recoverCoroutine;
    private Coroutine recoveryShieldProcessCoroutine;
    
    [SerializeField]
    private float recoveryRate = 1f;
    
    [SerializeField]
    private int recoveryAmount = 10;

    private PlayerStatsManager statsManager;

    public int ShieldHp
    {
        get { return shieldHp; }
        set
        {
            if (shieldHp == value) return;
            shieldHp = value;
            if (OnVariableChange != null)
                OnVariableChange(shieldHp);
        }
    }

    public delegate void OnVariableChangeDelegate(int newVal);
    public event OnVariableChangeDelegate OnVariableChange;

    private void Start()
    {
        this.OnVariableChange += VariableChangeHandler;
        statsManager = transform.parent.GetComponent<PlayerStatsManager>();
    }

    private void VariableChangeHandler(int newVal)
    {
        statsManager.UpdateShield(newVal);
    }

    public void TakeDamage(int value)
    {
        StopAllCoroutines();
        ShieldHp -= value;
        recoverCoroutine = StartCoroutine(RecoverCoroutine());
    }

    private IEnumerator RecoverCoroutine()
    {
        yield return new WaitForSecondsRealtime(secondsToHealShield);
        recoveryShieldProcessCoroutine = StartCoroutine(RecoveryShieldProcess());
    }

    private IEnumerator RecoveryShieldProcess()
    {
        yield return new WaitForSecondsRealtime(recoveryRate);
        ShieldHp = Mathf.Clamp(ShieldHp + recoveryAmount, ShieldHp, MAX_SHIELD_HP);
        StartCoroutine(RecoveryShieldProcess());
    }

}
