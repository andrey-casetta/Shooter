using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.321f, player.transform.position.z);
    }
}
