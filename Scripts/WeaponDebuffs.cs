using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDebuffs : MonoBehaviour
{
    public static void CheckElectricShotArea(GameObject enemy)
    {
        Collider2D[] colliders;
        colliders = Physics2D.OverlapCircleAll(enemy.transform.position, 2f);
        int affectedEnemies = 0;

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == 9 && affectedEnemies < 2)
            {
                collider.GetComponent<EnemyBase>().Damage(ShotStats.ELECTRIC_SHOT_HIT_DAMAGE / 2);
                affectedEnemies++;
            }
        }

    }

    private void ApplyElectricShotDebuff()
    {

    }
}
