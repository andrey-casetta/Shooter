using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableBuffOrDebuff))]
public class Plants : MonoBehaviour
{
    [SerializeField]
    private float plantStunTime;

    [SerializeField]
    private float AOE;

    private CircleCollider2D collider;

    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        collider.radius = AOE;
    }

    private IEnumerator PlantStunCoroutine()
    {
        yield return new WaitForSecondsRealtime(plantStunTime);
    }

    public void HealEffect()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        float distance = Vector2.Distance(transform.position, player.transform.position);
    }

    public void StunEffect()
    {

    }
}
