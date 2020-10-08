using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [HideInInspector]
    public static GameObject taget = null;

    private Animator ani;
    private Coroutine dashCoroutine;
    private bool canDash = true;
    private SpriteRenderer renderer;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    public void Animation(float horizontal, float vertical)
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (taget != null)
        {
            position = taget.transform.position;
        }

        Vector3 distance = (position - transform.position).normalized;

        ani.SetFloat("distanceX", distance.x);
        ani.SetFloat("distanceY", distance.y);

        ani.SetFloat("horizontal", horizontal);
        ani.SetFloat("vertical", vertical);

        renderer.flipX = (distance.x < 0) ? true : false;

        //Vector3 v3Scale = transform.localScale;
        //v3Scale.x = (distance.x < 0) ? 1 : -1;
        //transform.localScale = v3Scale;
    }

    public void Dash(Vector3 moveDir, float dashDistance, int dashCooldown, float dashDuration)
    {
        if (!canDash)
            return;

        Vector2 direction;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (moveDir != Vector3.zero)
        {
            direction = moveDir;
        }
        else
        {
            direction = (Vector2)((worldMousePos - transform.position));
        }

        dashCoroutine = StartCoroutine(DashCoroutine(direction.normalized, dashDistance, dashDuration));

        canDash = false;
        ani.Play("Dash");
        StartCoroutine(DashCooldown(dashCooldown));
    }

    IEnumerator DashCoroutine(Vector3 direction, float dashDistance, float dashDuration)
    {
        // Account for some edge cases  
        bool dashing;

        if (dashDistance <= 0.001f)
            yield break;

        if (dashDuration <= 0.001f)
        {
            this.transform.position += direction * dashDistance;
            yield break;
        }

        // Update our state
        float elapsed = 0f;
        Vector3 start = this.transform.position;
        Vector3 target = this.transform.position + dashDistance * direction;
        dashing = true;
        // There are a few different ways to do this, but I've always preferred
        // Lerp for things that have a fixed duration as the interpolant is clear
        while (elapsed < dashDuration)
        {
            Vector3 iterTarget = Vector3.Lerp(start, target, elapsed / dashDuration);
            this.transform.position = iterTarget;

            yield return null;
            elapsed += Time.deltaTime;
        }
        dashing = false;
        this.transform.position = target;
    }

    private IEnumerator DashCooldown(float dashCooldown)
    {
        yield return new WaitForSecondsRealtime(dashCooldown);
        canDash = true;
    }

    public void StopDash()
    {
        if(dashCoroutine != null)
        StopCoroutine(dashCoroutine);
    }

    public void StartShields(Transform shieldFather, float shieldCooldown)
    {
        ActivateShields(shieldFather);
        StartCoroutine(ShieldCooldown(shieldFather, shieldCooldown));
    }

    private void ActivateShields(Transform shieldFather)
    {
        for (int i = 0; i < shieldFather.transform.childCount; i++)
        {
            shieldFather.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void DeactivateShields(Transform shieldFather)
    {
        for (int i = 0; i < shieldFather.transform.childCount; i++)
        {
            shieldFather.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private IEnumerator ShieldCooldown(Transform shieldFather, float shieldCooldown)
    {
        yield return new WaitForSecondsRealtime(shieldCooldown);
        DeactivateShields(shieldFather);
    }
}
