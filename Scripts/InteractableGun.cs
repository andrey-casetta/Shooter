using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGun : MonoBehaviour
{
    [SerializeField]
    private GameObject panelGrab;

    private bool grab = false;
    private GameObject spriteGO;
    private Sprite sprite;

    private void Start()
    {
        //spriteGO = transform.GetChild(0).gameObject;
        sprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        if (grab)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ChangeWeaponManager.instance.ChangeWeapon(sprite);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ShowTalkTop(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ShowTalkTop(false);

            ControlTalk.control.Close();
            CharacterBase.taget = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!grab)
            {
                ShowTalkTop(true);
            }
        }
    }

    private void ShowTalkTop(bool show)
    {
        this.grab = show;
        panelGrab.SetActive(show);
    }
}
