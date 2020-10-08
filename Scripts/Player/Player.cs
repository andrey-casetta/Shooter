using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig2D;
    private Animator ani;
    private SpriteRenderer spr;
    private AudioSource audioSrc;
    private CharacterBase characterBase;
    private Collider2D collider;
    private TeleportHandler teleportHandler;
    private ChangeShotType changeShotTypeInstance;
    private PlayerStatsManager statsManager;
    private ShootingManager shootingManager;
    private PlayerAimWeapon playerAim;
    private ShieldHandler shieldHandler;
    private HealthManager healthHandler;


    private Vector3 moveDir;
    private bool right = true;
    public bool canMove = true;
    private bool isStunned = false;
    private bool isDead = false;
    float horizontal = 0, vertical = 0;
    private bool hasShieldActive;
    public bool canDash = true;
    private float dashDuration = .30f;
    private bool dashing = false;
    private Animator meleeAN;
    public bool canShoot = true;

    [SerializeField]
    private float velocity = 3f;

    [SerializeField]
    private GameObject meleeAtack;

    [SerializeField]
    private float dashDistance;

    [SerializeField]
    private int dashCooldown;

    [SerializeField]
    private int shieldCooldown;

    [SerializeField]
    private GameObject shieldImgAOE;

    [SerializeField]
    private Transform shieldFather;

    [SerializeField]
    private Animator gun;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        rig2D = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        meleeAN = meleeAtack.GetComponent<Animator>();
        characterBase = GetComponent<CharacterBase>();
        teleportHandler = GetComponent<TeleportHandler>();
        changeShotTypeInstance = ChangeShotType.instance;
        statsManager = GetComponent<PlayerStatsManager>();
        shootingManager = GetComponent<ShootingManager>();
        playerAim = GetComponent<PlayerAimWeapon>();
        shieldHandler = shieldFather.GetComponent<ShieldHandler>();
        healthHandler = GetComponent<HealthManager>();
    }

    private void Update()
    {
        if (!isDead)
        {
            if (Input.GetButtonDown("Fire1") && canShoot)
            {
                GameObject newGO = changeShotTypeInstance.GetCurrentPrefab();
                if (newGO != null)
                {
                    shootingManager.Shoot(newGO);
                    gun.SetTrigger("kick");
                }
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                meleeAtack.SetActive(true);
                meleeAN.Play("meleeAtackAnim");
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                characterBase.StartShields(shieldFather, shieldCooldown);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                characterBase.Dash(moveDir, dashDistance, dashCooldown, dashDuration);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                teleportHandler.StartTeleport();
            }

            if (Input.GetKeyUp(KeyCode.F))
            {
                teleportHandler.CancelTeleport();
                canShoot = true;
            }

            if (Input.GetKey(KeyCode.F))
            {
                teleportHandler.Teleport();
                canShoot = false;
            }
            else
            {
                if (!isStunned)
                {
                    canMove = true;
                }
            }
        }
        ani.SetBool("dead", isDead);
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(horizontal, vertical).normalized;

        collider = Physics2D.OverlapCircle(transform.position, .5f);

        if (collider != null)
        {
            if (collider.gameObject.layer == 8)
                characterBase.StopDash();
        }

        if (canMove)
        {
            Move();
        }

        characterBase.Animation(horizontal, vertical);
    }

    #region Old Shield System
    /*
    public void SelectShieldPos(GameObject shield)
    {
        canShoot = true;
        string id = shield.name.Substring(0, 1);
        SetShieldPos(id, shield);
    }

    private void SetShieldPos(string id, GameObject shield)
    {
        int iId = Convert.ToInt32(id);
        int shieldHP = 150;
        Shield shieldScript;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit)
        {
            if (hit.collider.gameObject.layer == 8)
            {
                audioSrc.Play();
                return;
            }
        }

        for (int i = 0; i < shieldFather.transform.childCount; i++)
        {
            if (shieldFather.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                return;
            }
        }

        if (currentShield != null)
        {
            shieldScript = currentShield.GetComponent<Shield>();
            shieldHP = shieldScript.shieldHP;
        }

        currentShield = shieldFather.transform.GetChild(iId).gameObject;
        shieldScript = currentShield.GetComponent<Shield>();
        shieldScript.shieldHP = shieldHP;

        if (shieldScript.shieldHP > 0)
        {
            currentShield.transform.position = shield.transform.position;
            currentShield.SetActive(true);
            hasShieldActive = true;
            shieldImgAOE.SetActive(false);
        }
        else
        {
            audioSrc.Play();
        }
    }
    */
    #endregion

    private void UpdateShieldCharges(GameObject shield, int value)
    {
        //shield.GetComponent<Shield>().ShieldHP += value;
    }

    public void Stun(float duration)
    {
        isStunned = true;
        canMove = false;
        canShoot = false;
        canDash = false;
        rig2D.velocity = Vector2.zero;
        teleportHandler.canTeleport = false;
        StartCoroutine(StunCoroutine(duration));
    }

    public void Die()
    {
        isDead = true;
        isStunned = true;
        canMove = false;
        canShoot = false;
        canDash = false;
        rig2D.velocity = Vector2.zero;
        teleportHandler.canTeleport = false;
        playerAim.enabled = false;
        characterBase.enabled = false;
        gun.gameObject.SetActive(false);
        rig2D.isKinematic = true;
        ani.enabled = false;
        this.enabled = false;

    }

    private IEnumerator StunCoroutine(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        isStunned = false;
        canMove = true;
        canShoot = true;
        canDash = true;
        teleportHandler.canTeleport = true;
    }

    private void Move()
    {
        rig2D.velocity = new Vector3(horizontal, vertical) * velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //enemy collision damage
        if (collision.gameObject.layer == 9)
        {
            healthHandler.TakeDamage(20);
            DamageCooldown();
        }

        //enemy shot damage
        if (collision.gameObject.layer == 10)
        {
            healthHandler.TakeDamage(collision.gameObject.GetComponent<Bullet>().Damage);
            DamageCooldown();
        }
    }

    private void DamageCooldown()
    {
        CapsuleCollider2D[] colliders = transform.GetComponents<CapsuleCollider2D>();
        foreach (CapsuleCollider2D collider in colliders)
        {
            collider.enabled = false;
        }
        StartCoroutine(DamageCooldownCoroutine());
    }

    private IEnumerator DamageCooldownCoroutine()
    {
        yield return new WaitForSecondsRealtime(.2f);
        CapsuleCollider2D[] colliders = transform.GetComponents<CapsuleCollider2D>();
        foreach (CapsuleCollider2D collider in colliders)
        {
            collider.enabled = true;
        }
    }

}
