using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int _currentLevel;
    public float _health;
    public float[] _position;

    public PlayerData(PlayerBase playerBase)
    {
        _currentLevel = playerBase.CurrentLevel;
        _health = playerBase.GetHealth();
        
        _position = new float[2];

        _position[0] = playerBase.transform.position.x;
        _position[1] = playerBase.transform.position.y;
    }

}
