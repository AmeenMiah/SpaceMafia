using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public bool original = true;
    public int KillTime = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y >= 0)
        {
            original = false;
            StartCoroutine(Dead());
        }

    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(KillTime);
        Destroy(gameObject);
    }
}
