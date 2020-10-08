using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager instance;

    [SerializeField]
    private Image hpBar;

    [SerializeField]
    private Image shieldBar;
    
    [SerializeField]
    private Image ammoBar;

    [SerializeField]
    private Image stamminaBar;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateHP(int value)
    {
        hpBar.fillAmount = value * 0.01f;
    }

    public void UpdateShield(int value)
    {
        shieldBar.fillAmount = value * 0.01f;
    }

    public void UpdateAmmo(int value)
    {
        ammoBar.fillAmount = value * 0.01f;
    }

    public void UpdateStammina(int value)
    {
        stamminaBar.fillAmount = value * 0.1f;
    }
}
