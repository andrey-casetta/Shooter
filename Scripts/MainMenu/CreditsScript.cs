using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour
{
    [SerializeField]
    private GameObject creditsAnim;

    private Animator anim;

    void Start()
    {
        anim = creditsAnim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isActiveAndEnabled && Input.anyKey)
            anim.speed = 2;
        else
            anim.speed = 1;
    }
}
