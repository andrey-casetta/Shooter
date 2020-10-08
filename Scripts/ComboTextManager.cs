using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboTextManager : MonoBehaviour
{
    private int _currentSpree = 0;
    private SampleSceneUIManager _uimInstance;

    [SerializeField]
    private ComboTextScriptableObject[] Combos;

    private void Start()
    {
        _uimInstance = SampleSceneUIManager.instance;
    }

    public void StopSpree()
    {
        _currentSpree = 0;
    }

    public void UpdateSpree()
    {
        _currentSpree++;

        foreach (ComboTextScriptableObject combo in Combos)
        {
            if (_currentSpree == combo._id)
            {
                _uimInstance.UpdateComboSpree(combo._message);
            }
        }
    }

}
