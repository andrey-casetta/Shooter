using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talks : MonoBehaviour
{
    [System.Serializable]
    public class Talk
    {
        [TextArea]
        public string[] talks;
    }
    
    [SerializeField]
    public Talk[] ArrayTalk;
}
