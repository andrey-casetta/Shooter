using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SystemData
{
    //declare variables to save
    public int _brightness;
    public float _health;
    public float[] _position;

    public SystemData(GameManager gameManager)
    {
        //get variables from manager
        //_currentLevel = playerBase.CurrentLevel;
        //_health = playerBase.GetHealth();

        //_position = new float[2];

        //_position[0] = playerBase.transform.position.x;
        //_position[1] = playerBase.transform.position.y;
    }

}
