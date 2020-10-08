using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private ShieldHandler fatherShield;
       
    private void Start()
    {
        fatherShield = transform.parent.GetComponent<ShieldHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("shot"))
        {
            fatherShield.TakeDamage(10);
        }
    }
}
