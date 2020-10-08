using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportHandler : MonoBehaviour
{
    private Player player;
    public bool canTeleport = true;
    private Coroutine slowEffectCT;
    private Rigidbody2D rig2D;

    [SerializeField]
    private int _maxTeleportCharges = 3;

    [SerializeField]
    private int _teleportCharges = 3;

    [SerializeField]
    private GameObject _teleportAOE;

    [SerializeField]
    private float _teleportRadius;

    [SerializeField]
    private float teleportCooldown;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
    }

    public void StartTeleport()
    {
        if (canTeleport && _teleportCharges > 0)
        {
            slowEffectCT = StartCoroutine(ActiveSlowEffect());
            _teleportAOE.SetActive(true);
        }
    }

    public void CancelTeleport()
    {
        if (_teleportAOE.activeInHierarchy)
        {

            if (_teleportCharges >= 0)
            {
                _teleportCharges--;
            }
            StopCoroutine(slowEffectCT);
            StopCoroutine(TeleportCooldown());
            SlowEffectInactive();
            TeleportToMouseDirection();
        }
    }

    public void Teleport()
    {
        if (canTeleport && _teleportCharges > 0)
        {
            if (_teleportAOE.activeInHierarchy)
            {
                _teleportAOE.transform.localScale = new Vector3(_teleportRadius, _teleportRadius, _teleportAOE.transform.localScale.z);

                player.canMove = false;
                rig2D.velocity = Vector2.zero;
                Time.timeScale = .25f;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));

                if (Input.GetMouseButtonDown(0) && Vector2.Distance(mousePos, transform.position) <= _teleportRadius)
                {
                    RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
                    if (hit)
                    {
                        if (hit.collider.gameObject.layer == 8)
                            return;
                    }
                    StopCoroutine(slowEffectCT);
                    transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 5));

                    canTeleport = false;
                    SlowEffectInactive();
                    StartCoroutine(TeleportCooldown());
                    _teleportAOE.SetActive(false);
                    _teleportCharges--;
                }
            }
            else
            {
                player.canMove = true;
                player.canShoot = true;
            }
        }
    }

    private void UpdateTeleportCharges(int teleportValue)
    {
        _teleportCharges += teleportValue;
        _teleportCharges = Mathf.Clamp(_teleportCharges, 0, _maxTeleportCharges);
    }

    private IEnumerator TeleportCooldown()
    {
        yield return new WaitForSecondsRealtime(teleportCooldown);
        canTeleport = true;
    }

    private IEnumerator ActiveSlowEffect()
    {
        yield return new WaitForSecondsRealtime(3f);
        canTeleport = false;
        TeleportToMouseDirection();
        SlowEffectInactive();
    }

    private void SlowEffectInactive()
    {
        _teleportAOE.SetActive(false);
        canTeleport = true;
        Time.timeScale = 1;
    }

    private void TeleportToMouseDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        mousePos.z = 10;
        Vector2 mouse2D = new Vector2(mousePos.x, mousePos.y);
        Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPos = (mouse2D + pos2D) / 2;
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit)
        {
            if (hit.collider.gameObject.layer == 8)
                return;
        }

        if (Vector2.Distance(mousePos, transform.position) <= _teleportRadius)
        {
            transform.position = mousePos + new Vector3(0, 0, 5);
            _teleportCharges--;
            return;
        }
        else
        {
            while (Vector3.Distance(transform.position, newPos) >= _teleportRadius)
            {
                newPos = newPos / 2;
            }
            transform.position = newPos;
            _teleportCharges--;
        }
    }

}
