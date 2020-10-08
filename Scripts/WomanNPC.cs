using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanNPC : MonoBehaviour
{
    private float velocityDefault = 1.2f;
    private float velocityCurrent;

    private Rigidbody2D rig2D;
    private Animator ani;
    private SpriteRenderer spr;

    private bool inverted = false;
    private int direction = 0;

    [SerializeField]
    private string name;



    private bool right = true;

    private void Awake()
    {
        string name = gameObject.name;

        if (name.IndexOf("DirHor") > 0) direction = 1;
        else if (name.IndexOf("DirVer") > 0) direction = 2;
        else { direction = 0; velocityDefault = 0; }

        inverted = (name.IndexOf("Invert") > 0);

        velocityCurrent = velocityDefault;
    }

    private void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();

        if (inverted)
        {
            velocityCurrent = velocityCurrent * -1;
            velocityDefault = velocityCurrent;            
        }
    }

    private void Update()
    {
        Vector2 vDirection = new Vector2(0, 0);

        if (direction == 1)
        {
            vDirection = new Vector2(velocityCurrent, 0);
        }
        else if (direction == 2)
        {
            vDirection = new Vector2(0, velocityCurrent);
        }                                                

        rig2D.velocity = vDirection;

        ani.SetFloat("velocity", Mathf.Abs(velocityCurrent));

        if (velocityCurrent < 0 && right)
        {
            spr.flipX = true;
            right = false;
        }
        else if (velocityCurrent > 0 && !right)
        {
            spr.flipX = false;
            right = true;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "point")
        {
            StartCoroutine(ChangeVelocity());
        }
        if (collision.gameObject.tag == "Player")
        {
            StopCoroutine(ChangeVelocity());
            velocityCurrent = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            velocityCurrent = 0;
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            velocityCurrent = velocityDefault;
        }
    }

    private IEnumerator ChangeVelocity()
    {
        velocityDefault = velocityDefault * -1;
        velocityCurrent = 0;
        
        yield return new WaitForSeconds(Random.Range(2f, 6f));
        velocityCurrent = velocityDefault;        
    }

}
