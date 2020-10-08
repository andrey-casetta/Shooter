using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongShot : MonoBehaviour
{
    Rigidbody2D rg;
    [SerializeField]
    private GameObject shootPos;

    private Vector3 initialForward;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        shootPos = GameObject.FindGameObjectWithTag("ShotPos");

        if (shootPos != null)
            initialForward = shootPos.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        //if (shootPos != null)
        //    transform.position += initialForward * Time.deltaTime * 15;

        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 dir = mousePos - transform.position;

        //transform.position += dir * Time.deltaTime * 10;
            

    }

}
