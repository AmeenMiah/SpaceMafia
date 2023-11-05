using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float fireRate = 15f;
    private float nextTimeToFire = 0f;
    public GameObject player;
    public GameObject pivot;
    public GameObject Gun;
    public bool Automatic = false;
    public bool isReloading = false;
    public bool StrongGun = false;

    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 5;
    //public Camera Cam;
    public Camera Cam;
    public Cinemachine.CinemachineFreeLook c_VirtualCamera;
    public float Zoomer;
    public bool Zoomed = false;
    public Array enemyArray;
    public bool CanZoom = false;

    public LayerMask whatIsEnemy, whatGunCanHit;
    public float sightRange = 50;
    public bool playerInSightRange;
    public ClosestEnemy nearestEnemy;
    public CinemachineTargetGroup cinemachineTarget;

    public AudioManager AM;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        Zoomed = false;
        Zoomer = 0;
        c_VirtualCamera.m_Priority = 5;
        //c_VirtualCamera.m_LookAt = pivot.transform;
    }


    //private void OnEnable()
    //{
    // isReloading = false;
    //}
    void Update()
    {
        if (Clock.TimeStopped)
            return;

        if (Input.GetButtonDown("Fire3") && playerInSightRange)
        {
            nearestEnemy = ClosestEnemy.FindClosestEnemy(transform.position);
            
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

        if (isReloading)
            return;

        playerInSightRange = Physics.CheckSphere(player.transform.position, sightRange, whatIsEnemy);
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && Automatic == true && StrongGun == false && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && StrongGun == false && currentAmmo > 0)
        {
            AM.Play("GunShot");
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && StrongGun == true && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            currentAmmo--;
            muzzleFlash.Play();
            Invoke("Shoot",5);
        }

        if (Input.GetButton("Reload") && currentAmmo != maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }



    }

    

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading");

        yield return new WaitForSeconds(reloadTime);

        isReloading = false;
        currentAmmo = maxAmmo;
    }
    void Shoot()
    {
        if (StrongGun == false)
        {
            muzzleFlash.Play();
            currentAmmo--;
        }
    


        if (StrongGun == true)
            AM.Play("Laser");

        if (Automatic == true)
            Invoke("GunShot", 1.5f);

        RaycastHit hit;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range, whatGunCanHit) && Zoomed)
        {
            Debug.Log(hit.transform.name);
            if (Zoomed == false)
            {
                player.transform.rotation = Cam.transform.rotation;
            }
  

            BasicEnemy target = hit.transform.GetComponent<BasicEnemy>();
            if (target != null)
            {
                target.TakeDamage(damage * 3);
            }
            //if (hit.rigidbody !=null)
            //{
                //hit.rigidbody.AddForce(-hit.normal * impactForce);
            //}

            Destrucible target2 = hit.transform.GetComponent<Destrucible>();

            if (target2 !=null)
            {
                target2.TakeDamage(damage);
            }

            RinoBoss target3 = hit.transform.GetComponent<RinoBoss>();

            if (target3 !=null)
            {
                target3.TakeDamage(damage);
            }

            GhostBoss target4 = hit.transform.GetComponent<GhostBoss>();

            if (target4 != null)
            {
                target4.TakeDamage(damage);
            }

            Clock target5 = hit.transform.GetComponent<Clock>();

            if (target5 != null)
            {
                target5.TakeDamage(damage);
            }

            Godfather target6 = hit.transform.GetComponent<Godfather>();

            if (target6 != null)
            {
                target6.TakeDamage(damage);
            }

            headshot target7 = hit.transform.GetComponent<headshot>();

            if (target7 != null)
            {
                target7.TakeDamage(damage);
            }

            ChaosReaper target8 = hit.transform.GetComponent<ChaosReaper>();

            if (target8 != null)
            {
                target8.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }

        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range, whatGunCanHit) && Zoomed == false)
        {
            Debug.Log(hit.transform.name);
            player.transform.rotation = Cam.transform.rotation;

            BasicEnemy target = hit.transform.GetComponent<BasicEnemy>();
            if (target != null)
            {
                target.TakeDamage(damage * 3.5f);
            }
            //if (hit.rigidbody != null)
            //{
                //hit.rigidbody.AddForce(-hit.normal * impactForce);
            //}

            Destrucible target2 = hit.transform.GetComponent<Destrucible>();

            if (target2 != null)
            {
                target2.TakeDamage(damage * 1.25f);
            }

            RinoBoss target3 = hit.transform.GetComponent<RinoBoss>();

            if (target3 != null)
            {
                target3.TakeDamage(damage * 1.25f);
            }

            GhostBoss target4 = hit.transform.GetComponent<GhostBoss>();

            if (target4 != null)
            {
                target4.TakeDamage(damage * 1.25f);
            }

            Clock target5 = hit.transform.GetComponent<Clock>();

            if (target5 != null)
            {
                target5.TakeDamage(damage * 1.25f);
            }

            Godfather target6 = hit.transform.GetComponent<Godfather>();

            if (target6 != null)
            {
                target6.TakeDamage(damage * 1.25f);
            }

            headshot target7 = hit.transform.GetComponent<headshot>();

            if (target7 != null)
            {
                target7.TakeDamage(damage * 1.25f);
            }

            ChaosReaper target8 = hit.transform.GetComponent<ChaosReaper>();

            if (target8 != null)
            {
                target8.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }

    void GunShot()
    {
        AM.Play("GunShot");
    }
}
