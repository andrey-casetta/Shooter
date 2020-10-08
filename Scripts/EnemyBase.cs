using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private int healthMax = 100;
    private Transform canvasHP;
    private GenericHealthBar healthScript;
    private EnemyBehavior enemyBehavior;
    private GameManager gmInstance;
    public int health = 100;

    private void Start()
    {
        canvasHP = transform.GetChild(0);
        healthScript = canvasHP.gameObject.GetComponent<GenericHealthBar>();
        enemyBehavior = GetComponent<EnemyBehavior>();
        gmInstance = GameManager.instance;
    }

    public EnemyBase(int healthMax)
    {
        this.healthMax = healthMax;
        health = healthMax;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetHealthMax()
    {
        return healthMax;
    }

    public float GetHealthNormalized()
    {
        return (float)health / healthMax;
    }

    public void Damage(int amount)
    {
        health -= amount;
        healthScript.ChangeValue(amount);
        GameObject newPopup = ObjectPoolerManager.instance.GetPooledObject(PoolObjectType.PopUpText);
        newPopup.GetComponent<DamagePopUp>().Setup(amount);
        newPopup.transform.position = this.transform.position;
        newPopup.SetActive(true);

        if (health < 0)
        {
            health = 0;
        }

        if (health <= 0)
        {
            Die();
            gmInstance.UpdateEnemieKilled();
        }
    }

    public void Die()
    {
        enemyBehavior.enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        Debug.Log("Dead!!!");
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > healthMax)
        {
            health = healthMax;
        }

    }

    public void SetHealthMax(int healthMax, bool fullHealth)
    {
        this.healthMax = healthMax;
        if (fullHealth) health = healthMax;
    }

    public void Stun(float duration)
    {
        //stun
    }

    private void ElectricDamage()
    {
        WeaponDebuffs.CheckElectricShotArea(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Damage(10);
            ElectricDamage();
        }

        if (collision.gameObject.tag == "shot")
        {
            Damage(collision.GetComponent<Bullet>().Damage);
            //ElectricDamage();
        }
    }

}
