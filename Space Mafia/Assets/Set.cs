using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : MonoBehaviour
{
    public GameObject player;
    public GameObject sword;
    public float Up = 0.3f;
    public Vector3 AVector;
    public float XValue;
    public float ZValue;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SetValues());

    }

    IEnumerator SetValues()
    {
        yield return new WaitForSeconds(1f);
        AVector = new Vector3(player.transform.position.x + XValue, player.transform.position.y + Up, player.transform.position.z + ZValue);
        sword.transform.position = AVector;
    }


}
