using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    private int _currentLevel;

    private GameManager gmInstance;
    private Rigidbody2D _rg2D;
    private Player playerScript;
    private HealthManager healthManager;

    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }

    void Start()
    {
        _rg2D = GetComponent<Rigidbody2D>();
        gmInstance = GameManager.instance;
        playerScript = GetComponent<Player>();
        healthManager = GetComponent<HealthManager>();
    }

    public int GetHealth()
    {
        return healthManager.CurrentHP;
    }

    public void SetHealth(float value)
    {
        int h = Convert.ToInt32(value);
        healthManager.CurrentHP = h;
    }

    public void Stun(float duration)
    {
        if (playerScript != null)
        {
            playerScript.Stun(duration);
        }
    }

    public void Die()
    {
        playerScript.Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            gmInstance.StopSpree();
            healthManager.CurrentHP--;

            if (healthManager.CurrentHP <= 0)
            {
                Die();
            }
        }
    }


}
