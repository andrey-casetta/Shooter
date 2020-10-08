using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranspShield : MonoBehaviour
{
    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                transform.GetChild(i).transform.GetChild(j).gameObject.SetActive(false);
            }
        }
    }
}
