using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 1f;

    public float alertRange = 5f;
    public float attackRange = 2.5f;

    public bool playerAround;
    public bool attackPlayer;

    // Update is called once per frame
    void Update()
    {
        playerAround = CheckPlayerAround();
    }

    public void CheckAttack(Transform player)
    {
        attackPlayer = Vector2.Distance(transform.position, player.position) <= attackRange ? true : false;
        if (attackPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    public bool CheckPlayerAround()
    {
        Collider2D[] colliders;
        colliders = Physics2D.OverlapCircleAll(transform.position, alertRange);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == 12)
            {
                if (collider.GetComponent<HealthManager>().CurrentHP > 0)
                {
                    CheckAttack(collider.transform);
                    return true;
                }
            }
        }
        return false;
    }
}
