using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericHealthBar : MonoBehaviour
{
    [SerializeField]
    private Image hpBar;

    public void ChangeValue(int value)
    {
        hpBar.fillAmount -= value * .01f;

        if (hpBar.fillAmount > .7f)
        {
            hpBar.color = Color.green;
        }
        else
        {
            if (hpBar.fillAmount <= .7f && hpBar.fillAmount >= .4f)
            {
                hpBar.color = Color.yellow;
            }
            else
            {
                hpBar.color = Color.red;
            }
        }
    }
}
