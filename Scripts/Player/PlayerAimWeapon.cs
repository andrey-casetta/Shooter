using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    [SerializeField]
    private Transform aimTransform;

    [SerializeField]
    private Transform gun;

    private SpriteRenderer rendererGun;

    private void Start()
    {
        rendererGun = gun.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Aim();
    }

    private void Aim()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePos.z = 0f;
        Vector3 aimDirection = (worldMousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 localScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
            rendererGun.sortingOrder = 8;
        }
        else
        {
            localScale.y = 1f;
            rendererGun.sortingOrder = 9;
        }
        aimTransform.localScale = localScale;
    }
}
