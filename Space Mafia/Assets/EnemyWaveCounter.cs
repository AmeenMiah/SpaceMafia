using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveCounter : MonoBehaviour
{
    public static int AmountofEnemies = 0;
    public Vector3 StartPosition;
    public int Vectorx = -700;
    public int Vectory = 10;
    public int Vectorz = -270;
    public LayerMask Layer;
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = new Vector3(Vectorx, Vectory, Vectorz);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(AmountofEnemies);
        Invoke("NewWave", 1f);
    }

    void NewWave()
    {

         Collider[] AmountOfEnemies2 = Physics.OverlapSphere(StartPosition, 1000, Layer);
         Debug.Log(AmountOfEnemies2);
         AmountofEnemies = AmountOfEnemies2.Length;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(StartPosition, 1000);
        Gizmos.color = Color.red;
    }
}
