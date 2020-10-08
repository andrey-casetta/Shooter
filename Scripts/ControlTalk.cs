using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlTalk : MonoBehaviour
{
    [SerializeField]
    private GameObject pnTalk;

    [SerializeField]
    private Text txtTalk;

    [SerializeField]
    private Text txtName;

    public static ControlTalk control;

    private bool isShowText = true;

    void Start()
    {
        control = this;        
    }

    public void Open()
    {
        pnTalk.SetActive(true);
    }

    public bool IsShowText()
    {
        return isShowText;
    }

    public void Text(string text, string name)
    {
        if (!pnTalk.activeSelf)
        {
            Open();
        }

        StartCoroutine(TextAnimation(text));
        txtName.text = name;
    }

    private IEnumerator TextAnimation(string text)
    {
        txtTalk.text = "";
        isShowText = false;
        for (int i = 0; i < text.Length; i++)
        {
            yield return null;
            txtTalk.text += text[i];
        }
        isShowText = true;
    }

    public void Close()
    {
        pnTalk.SetActive(false);
    }

    public bool ActiveSelf()
    {
        return pnTalk.activeSelf;
    }

}
