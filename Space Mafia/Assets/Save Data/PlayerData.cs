using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public bool Level1Completed;
    public bool Level2Completed;
    public bool Level3Completed;
    public bool Level4Completed;
    public bool Level5Completed;
    public float[] position;
    
    public PlayerData (GameManager player)
    {
        Level1Completed = player.Level1Completed;
        Level2Completed = player.Level2Completed;
        Level3Completed = player.Level3Completed;
        Level4Completed = player.Level4Completed;
        Level5Completed = player.Level5Completed;
        //level = player.level;
        //health = player.health;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
