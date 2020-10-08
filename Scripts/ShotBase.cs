using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBase : MonoBehaviour
{
    [SerializeField]
    private int damage;

    public int Damage { get => damage; set => damage = value; }
}
