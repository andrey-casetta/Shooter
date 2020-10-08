using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanNPCTalk : MonoBehaviour
{
    [SerializeField]
    private Talks talks;
    private int indexCurrentTalk = 0;
    private int indexArrayTalks = 0;

    [SerializeField]
    private GameObject pnPress;
    private bool talk = false;

    // Update is called once per frame
    void Update()
    {
        if (talk)
        {
            if (Input.GetKeyDown(KeyCode.E) && ControlTalk.control.IsShowText())
            {
                CharacterBase.taget = gameObject;

                if (indexCurrentTalk >= talks.ArrayTalk[indexArrayTalks].talks.Length)
                {
                    indexCurrentTalk = 0;
                    ControlTalk.control.Close();
                    CharacterBase.taget = null;
                    indexArrayTalks++;
                    if (indexArrayTalks >= talks.ArrayTalk.Length)
                    {
                        indexArrayTalks = 0;
                    }
                }
                else if (indexCurrentTalk < talks.ArrayTalk[indexArrayTalks].talks.Length)
                {
                    ControlTalk.control.Text(talks.ArrayTalk[indexArrayTalks].talks[indexCurrentTalk], name);
                    indexCurrentTalk++;
                }

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

            indexCurrentTalk = 0;
            ControlTalk.control.Close();
            CharacterBase.taget = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!talk)
            {
                ShowTalkTop(true);
            }
        }
    }

    private void ShowTalkTop(bool show)
    {
        this.talk = show;
        pnPress.SetActive(show);
    }
}
