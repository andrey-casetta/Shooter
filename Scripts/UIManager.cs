using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerTest : MonoBehaviour
{
    public static UIManagerTest instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField]
    private Text _currentWeapon;

    [SerializeField]
    private Text _currentSpree;

    [SerializeField]
    private Text _enemieKilledText;

    public void UpdateComboSpree(string text)
    {
        _currentSpree.text = text;
    }

    public void UpdateEnemiesKilled(int text)
    {
        _enemieKilledText.text = text.ToString();
    }

    public void UpdateCurrentWeapon(int pos)
    {
        ChangeShotType.instance.SetCurrenWeapon(pos);
    }
}
