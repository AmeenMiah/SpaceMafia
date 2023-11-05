using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{

    public float blastRadius;
    public float explosionForce;
    public float damage;
    public LayerMask explosionLayers;
    private Collider[] hitColliders;
    public GameObject impactEffect;

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.contacts[0].point.ToString());
        DoExplosion(collision.contacts[0].point);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void DoExplosion(Vector3 explosionPoint)
    {
        hitColliders = Physics.OverlapSphere(explosionPoint, blastRadius, explosionLayers);

        foreach (Collider hitcol in hitColliders)
        {
            if (hitcol.GetComponent<Rigidbody>() != null)
            {
                //Debug.Log(hitcol.gameObject.name);
                hitcol.GetComponent<Rigidbody>().isKinematic = false;
                hitcol.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, explosionPoint, blastRadius, 1, ForceMode.Impulse);
                GameObject impactGO = Instantiate(impactEffect, hitcol.transform.position, Quaternion.Euler(hitcol.transform.rotation.eulerAngles));
                Destroy(impactGO, 2f);

                BasicEnemy target = hitcol.transform.GetComponent<BasicEnemy>();
                if (target != null)
                {
                    target.TakeDamage(damage * 3);
                }

                Destrucible target2 = hitcol.transform.GetComponent<Destrucible>();

                if (target2 != null)
                {
                    target2.TakeDamage(damage);
                }

                RinoBoss target3 = hitcol.transform.GetComponent<RinoBoss>();

                if (target3 != null)
                {
                    target3.TakeDamage(damage);
                }

                GhostBoss target4 = hitcol.transform.GetComponent<GhostBoss>();

                if (target4 != null)
                {
                    target4.TakeDamage(damage);
                }

                Clock target5 = hitcol.transform.GetComponent<Clock>();

                if (target5 != null)
                {
                    target5.TakeDamage(damage);
                }

                Godfather target6 = hitcol.transform.GetComponent<Godfather>();

                if (target6 != null)
                {
                    target6.TakeDamage(damage);
                }


                ChaosReaper target7 = hitcol.transform.GetComponent<ChaosReaper>();

                if (target7 != null)
                {
                    target7.TakeDamage(damage);
                }
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
