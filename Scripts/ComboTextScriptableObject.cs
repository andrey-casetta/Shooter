using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combo", menuName = "Combo")]
public class ComboTextScriptableObject : ScriptableObject
{
    public string _message;
    public int _id;
}
