using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponManager : MonoBehaviour
{
    public static ChangeWeaponManager instance;

    [SerializeField]
    private Sprite[] AllWeapons;

    [SerializeField]
    private GameObject weapon;

    [SerializeField]
    private NewWeapon[] inHandWeapons = new NewWeapon[2];

    private SpriteRenderer weaponRenderer;
    private int currentPos = 0;

    public class NewWeapon
    {
        public Sprite sprite;

        public NewWeapon(Sprite sprite)
        {
            this.sprite = sprite;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        weaponRenderer = weapon.GetComponent<SpriteRenderer>();
        inHandWeapons[0] = new NewWeapon(AllWeapons[0]);

        if (AllWeapons.Length > 0)
        {
            weaponRenderer.sprite = inHandWeapons[0].sprite;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && inHandWeapons[0] != null && inHandWeapons[1] != null)
        {
            weaponRenderer.sprite = weaponRenderer.sprite == inHandWeapons[0].sprite ? weaponRenderer.sprite = inHandWeapons[1].sprite : weaponRenderer.sprite = inHandWeapons[0].sprite;
            currentPos = currentPos == 0 ? currentPos = 1 : currentPos = 0;
        }
    }

    public void ChangeWeapon(Sprite sprite)
    {
        if (inHandWeapons[0] == null)
        {
            inHandWeapons[0] = new NewWeapon(sprite);
            currentPos = 0;
        }
        else if (inHandWeapons[1] == null)
        {
            inHandWeapons[1] = new NewWeapon(sprite);
            currentPos = 1;
        }

        inHandWeapons[currentPos].sprite = sprite;
        weaponRenderer.sprite = inHandWeapons[currentPos].sprite;

    }
}
