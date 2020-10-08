using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private float fireTickDamage = 1;

    [SerializeField]
    private float defaultFireDamageDuration;

    [SerializeField]
    private int fireDamage;

    private float currentFireDamageDuration;

    [SerializeField]
    private float defaultBleedDamageDuration;

    [SerializeField]
    private float percentageBleedDamage;

    [SerializeField]
    private float bleedTickDamage = 2.5f;

    private float currentBleedDamageDuration;

    private float stunDuration = 2f;
    /*
    Armadura: Reduz o dano recebido.
    Redução: 5%
    Efeito: 2% de chance de Bloquear o golpe (100%).

    Dano Crítico: Chance de causar porcentagem de Dano extra.
    Chance: 5%
    Dano extra: 50%

    Fire: Causa dano por Segundo.
    Chance: Granadas/Cenário/Armas
    Dano: 2
    Término: 8 segundos.

    Miss: Chance passiva de desviar de um ataque.
    Chance: 2%

    Sangramento: Perda de % vida constante.
    Dano: 2% da vida Total.
    Intervalo de Dano: 2,5 seg.
    Término: 10 segundos.  

    Stun: Chance de atordoar os inimigos.
    Chance: Armas/Granadas/Cenário

    Vampirismo: Chance passiva de recuperar vida ao causar dano.
    Chance: 5%
    Heal: 10% do dano causado.

    Veneno: Dano por acumulativo e por segundo.
    Chance: Armas/Cenário
    Intervalo: 1,8 seg.
    Término: 8 segundos desde o último acúmulo.
     */

    private bool hasArmor = true;
    private int critChance = 0;
    private int missChance = 0;
    private int bleeding = 0;
    private int lifeSteal = 0;

    private bool isBleeding = false;
    private bool isPoisoned = false;
    private bool isLifeStealing = false;
    private bool isMissing = false;
    private bool isOnFire = false;
    private bool isStunned = false;
    private bool isCriticalDamage = false;

    private HealthManager playerHealth;
    private PlayerBase playerBase;
    private EnemyBase enemy;

    public bool HasArmor { get => hasArmor; set => hasArmor = value; }

    private void Start()
    {
        if (this.gameObject.CompareTag("Player"))
        {
            playerHealth = GetComponent<HealthManager>();
            playerBase = GetComponent<PlayerBase>();
        }
        else
        {
            enemy = GetComponent<EnemyBase>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ApplyBleed();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            ApplyFireDamage();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ApplyStun(stunDuration);
        }

    }

    public void ApplyFireDamage()
    {
        currentFireDamageDuration = defaultFireDamageDuration;
        StartCoroutine(FireDamage());
    }

    private IEnumerator FireDamage()
    {
        yield return new WaitForSecondsRealtime(fireTickDamage);

        if (playerHealth != null)
            playerHealth.TakeDamage(fireDamage);
        else
            enemy.Damage(fireDamage);

        currentFireDamageDuration--;

        if (currentFireDamageDuration > 0)
        {
            StartCoroutine(FireDamage());
        }
    }

    public void ApplyBleed()
    {
        currentBleedDamageDuration = defaultBleedDamageDuration;
        StartCoroutine(BleedDamage());
    }

    private IEnumerator BleedDamage()
    {
        yield return new WaitForSecondsRealtime(bleedTickDamage);

        if (playerHealth != null)
        {
            float damage = playerHealth.GetHealth() * (percentageBleedDamage / 100);
            playerHealth.TakeDamage(damage);
        }
        else
        {
            float damage = enemy.GetHealth() * (percentageBleedDamage / 100);
            enemy.Damage(Convert.ToInt32(damage));
        }

        currentBleedDamageDuration--;

        if (currentBleedDamageDuration > 0)
        {
            StartCoroutine(BleedDamage());
        }
    }

    public void ApplyStun(float duration)
    {
        if (playerBase != null)
        {
            playerBase.Stun(duration);
        }
        else
        {
            enemy.Stun(duration);
        }
    }
}
