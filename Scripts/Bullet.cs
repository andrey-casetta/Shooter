using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage = 10;
    private Vector3 shootDir;
    float moveSpeed = 20f;
    private Rigidbody2D rg;

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        if (rg == null)
            rg = GetComponent<Rigidbody2D>();

        rg.AddForce(transform.right * moveSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            PoolerTypes type = GetComponent<PoolerTypes>();
            ObjectPoolerManager.instance.CoolObject(this.gameObject, type.type);
        }
    }
}
