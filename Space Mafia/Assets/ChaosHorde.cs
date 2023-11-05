using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosHorde : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject player;
    public float WhenToSpawn = 5;
    public float spawnenemytimer = 0;

    void Update()
    {
        spawnenemytimer += Time.deltaTime;
        Debug.Log(spawnenemytimer);
        if (spawnenemytimer >= WhenToSpawn)
        {
            SpawnEnemy();
        }
        
    }

    void SpawnEnemy()
    {
        Vector3 Hi = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z+10);
        Instantiate(Enemy, Hi, player.transform.rotation);
        spawnenemytimer = 0;
    }
}
