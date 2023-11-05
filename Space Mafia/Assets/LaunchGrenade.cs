using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LaunchGrenade : MonoBehaviour
{
    public float maxAmmo = 1;
    public float currentAmmo;
    public bool isReloading = false;
    public float reloadTime = 10f;
    public float fireRate = 1f;
    private float nextTimeToFire = 0f;

    public Cinemachine.CinemachineFreeLook c_VirtualCamera;
    public float Zoomer;
    public bool Zoomed = false;
    public Array enemyArray;
    public bool CanZoom = false;
    public bool playerInSightRange;
    public GameObject player;
    public GameObject pivot;
    public CinemachineTargetGroup cinemachineTarget;

    public AudioManager Audio;
    public GameObject grenadePrefab;
    public float propulsionForce;
    public LayerMask whatIsEnemy;
    public float sightRange = 50;
    public ClosestEnemy nearestEnemy;
    public bool RPGNotOn = false;

    private Transform myTransform;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        SetIntialRefrence();

    }

    private void OnEnable()
    {
        if (RPGNotOn)
            return;

        Zoomed = false;
        c_VirtualCamera.m_Priority = 5;
        //c_VirtualCamera.m_LookAt = pivot.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Clock.TimeStopped)
            return;

        if (RPGNotOn)
            return;

        playerInSightRange = Physics.CheckSphere(player.transform.position, sightRange, whatIsEnemy);
        if (Input.GetButtonDown("Fire3") && playerInSightRange)
        {
            nearestEnemy = ClosestEnemy.FindClosestEnemy(transform.position);

        }

        if (Input.GetButton("Fire3") && playerInSightRange && nearestEnemy != null)
        {
            Zoomer = 0.3f;
            Zoomed = true;
            cinemachineTarget.m_Targets[1].target = nearestEnemy.transform;
            //c_VirtualCamera.m_LookAt = GameObject.Find("Godfather").transform;
            c_VirtualCamera.m_Priority = 20;
            //c_VirtualCamera.m_LookAt = nearestEnemy.transform;
            player.gameObject.transform.LookAt(nearestEnemy.transform.position);

        }
        else
        {
            Zoomer -= Time.deltaTime;

        }

        if (Zoomer <= 0)
        {
            Zoomed = false;
            c_VirtualCamera.m_Priority = 5;
            //c_VirtualCamera.m_LookAt = pivot.transform;
        }

        if (isReloading)
            return;

        
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Audio.Play("RPGShot");
            SpawnGrenade();
        }

        if (currentAmmo <= 0 && Input.GetButton("Reload") && currentAmmo != maxAmmo)
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

    void SpawnGrenade()
    {
        currentAmmo--;
        GameObject grenade = Instantiate(grenadePrefab, myTransform.transform.TransformPoint(0, 0, 0), myTransform.rotation);
        grenade.GetComponent<Rigidbody>().AddForce(myTransform.forward * propulsionForce, ForceMode.Impulse);
        Destroy(grenade, 1);

    }

    void SetIntialRefrence()
    {
        myTransform = transform;
    }
}
