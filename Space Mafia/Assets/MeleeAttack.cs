using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MeleeAttack : MonoBehaviour
{
    public Animator PA;
    public Animator Sword;
    public float damage = 25;
    public float WaitForSwing = 1f;
    public bool SwordSwinging = false;
    public float SwordTime = 0;
    public GameObject impactEffect;

    public Cinemachine.CinemachineFreeLook c_VirtualCamera;
    public CinemachineTargetGroup cinemachineTarget;
    public float Zoomer;
    public bool Zoomed = false;
    public bool CanZoom = false;

    public ClosestSwordEnemy nearestEnemy;
    public LayerMask whatIsEnemy;
    public float sightRange = 50;
    public bool playerInSightRange;
    public GameObject player;
    public GameObject pivot;
    public AudioManager Audio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Zoomed = false;
        Zoomer = 0;
        c_VirtualCamera.m_Priority = 5;
        //c_VirtualCamera.m_LookAt = pivot.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Clock.TimeStopped)
            return;

        playerInSightRange = Physics.CheckSphere(player.transform.position, sightRange, whatIsEnemy);

        if (Input.GetButtonDown("Fire3") && playerInSightRange)
        {
            nearestEnemy = ClosestSwordEnemy.FindClosestEnemy(transform.position);

        }



        if (Input.GetButton("Fire3") && playerInSightRange && nearestEnemy != null)
        {
            Zoomer = 0.3f;
            Zoomed = true;
            //c_VirtualCamera.m_LookAt = GameObject.Find("Godfather").transform;
            c_VirtualCamera.m_Priority = 20;
            cinemachineTarget.m_Targets[1].target = nearestEnemy.transform;
            //c_VirtualCamera.m_LookAt = nearestEnemy.transform;
            player.gameObject.transform.LookAt(nearestEnemy.transform.position);

        }
        else
        {
            Zoomer -= Time.deltaTime;

        }

        if (Zoomer <= 0 && Zoomed == true)
        {
            player.gameObject.transform.LookAt(pivot.transform.position);
            Zoomed = false;
            c_VirtualCamera.m_Priority = 5;
            //c_VirtualCamera.m_LookAt = pivot.transform;
        }

        if (Input.GetButtonDown("Fire1") && SwordSwinging == false)
        {
            Sword.Play("SwordSwing");
            SwordSwinging = true;
            Audio.Play("SwordSwing");
            //StartCoroutine(TurnSwingOff());
        }
        
        if(SwordSwinging == true && WaitForSwing > SwordTime)
        {
            SwordTime += Time.deltaTime;
        }

        if(SwordTime >= WaitForSwing)
        {
            SwordTime = 0;
            SwordSwinging = false;
        }
    }

    IEnumerator TurnSwingOff()
    {
        yield return new WaitForSeconds(WaitForSwing);
        SwordSwinging = false;
    }


    void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log("Sword Hit");
        if (((collisionInfo.collider.tag == "Enemy") || (collisionInfo.collider.tag == "Obstacle") || (collisionInfo.collider.tag == "Ghosty")) && SwordSwinging == true)
        {
            BasicEnemy target = collisionInfo.gameObject.GetComponent<BasicEnemy>();
            if (target != null)
            {
                target.TakeDamage(damage * 5);
            }

            Destrucible target2 = collisionInfo.gameObject.GetComponent<Destrucible>();

            if (target2 != null)
            {
                target2.TakeDamage(damage);
            }

            RinoBoss target3 = collisionInfo.transform.GetComponent<RinoBoss>();

            if (target3 != null)
            {
                target3.TakeDamage(damage);
            }

            GhostBoss target4 = collisionInfo.transform.GetComponent<GhostBoss>();

            if (target4 != null)
            {
                target4.TakeDamage(damage);
            }


            Clock target5 = collisionInfo.transform.GetComponent<Clock>();

            if (target5 != null)
            {
                target5.TakeDamage(damage);
            }

            Godfather target6 = collisionInfo.transform.GetComponent<Godfather>();

            if (target6 != null)
            {
                target6.TakeDamage(damage);
            }


            ChaosReaper target7 = collisionInfo.transform.GetComponent<ChaosReaper>();

            if (target7 != null)
            {
                target7.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, collisionInfo.gameObject.transform.position , Quaternion.LookRotation(collisionInfo.gameObject.transform.rotation.eulerAngles));
            Destroy(impactGO, 2f);
        }

    }

    

}
