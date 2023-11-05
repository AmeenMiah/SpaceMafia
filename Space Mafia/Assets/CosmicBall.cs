using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmicBall : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    public Vector3 angleChangingSpeed;
    public float speed = 10;
    public float rotationSpeed = 5;
    public float focusDistance = 5;
    public bool isLookingAtObject = true;
    public float blastRadius;
    public float explosionForce;
    public int damage;
    public LayerMask explosionLayers;
    private Collider[] hitColliders;
    public GameObject impactEffect;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (rb.position.y < 0)
        {
            return;
        }
        Vector3 direction = (Vector3) target.position - rb.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, 0.0F);
        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);

        if (Vector3.Distance(transform.position, target.position) < focusDistance)
        {
            isLookingAtObject = false;
        }
        if (isLookingAtObject)
        {
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        DoExplosion(collision.contacts[0].point);
        if (collision.collider.tag == "Player")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 9)
        {
            GameObject impactGO = Instantiate(impactEffect, collision.transform.position, Quaternion.Euler(collision.transform.rotation.eulerAngles));
            Destroy(impactGO, 2f);
            Destroy(gameObject);
        }
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

                PlayerCollision target = hitcol.transform.GetComponent<PlayerCollision>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
                Destroy(gameObject);
                
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
