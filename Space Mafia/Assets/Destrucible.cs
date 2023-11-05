using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrucible : MonoBehaviour
{
    public float health;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    //void OnCollisionEnter(Collision collisionInfo)
    //{
        //if (collisionInfo.collider.tag == "Melee")
        //{
            //health -= 25;
        //}
        //if (health <= 0)
        //{
            //Die();
        //}
    //}
}
