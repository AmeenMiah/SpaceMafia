using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public GunScript gunScript1;
    public GunScript gunScript2;
    public GunScript gunScript3;
    public LaunchGrenade RPG;
    public Animator PlayerAnim;
    public bool SwordOn = false;
    public bool PistolOn = false;
    public bool AKOn = false;
    public bool RPGOn = false;
    public bool LGOn = false;
    public int AKScene = 3;
    public int RPGScene = 6;
    public int LaserGunScene = 6;

    // Start is called before the first frame update
    void Start()
    {
        selectWeapon();        
        PlayerAnim.Play("HoldthePistol");
        PistolOn = false;

    }

    // Update is called once per frame
    void Update()
    {

        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && gunScript1.isReloading == false && gunScript2.isReloading == false && gunScript3.isReloading == false && RPG.isReloading == false)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && gunScript1.isReloading == false && gunScript2.isReloading == false && gunScript3.isReloading == false && RPG.isReloading == false)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && gunScript1.isReloading == false && gunScript2.isReloading == false && gunScript3.isReloading == false && RPG.isReloading == false)
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 && gunScript1.isReloading == false && gunScript2.isReloading == false && gunScript3.isReloading == false && RPG.isReloading == false)
        {
            selectedWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3 && gunScript1.isReloading == false && gunScript2.isReloading == false && gunScript3.isReloading == false && RPG.isReloading == false && SceneManager.GetActiveScene().buildIndex == AKScene)
        {
            selectedWeapon = 2;
        }



        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4 && gunScript1.isReloading == false && gunScript2.isReloading == false && gunScript3.isReloading == false && RPG.isReloading == false && SceneManager.GetActiveScene().buildIndex == RPGScene)
        {
            selectedWeapon = 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5 && gunScript1.isReloading == false && gunScript2.isReloading == false && gunScript3.isReloading == false && RPG.isReloading == false && SceneManager.GetActiveScene().buildIndex == LaserGunScene)
        {
            selectedWeapon = 4;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            PistolOn = true;
            SwordOn = true;
            AKOn = true;
            RPGOn = true;
            LGOn = true;
            selectWeapon();
        }

        if (selectedWeapon == 1 && SwordOn == true)
        {
            PlayerAnim.Play("Sword");
            SwordOn = false;

        }

        if (selectedWeapon == 2 && AKOn == true)
        {
            PlayerAnim.Play("Hold the AK");
            AKOn = false;
        }

        if (selectedWeapon == 3 && RPGOn == true)
        {
            PlayerAnim.Play("Hold the RPG");
            RPGOn = false;
        }

        if (selectedWeapon == 4 && LGOn == true)
        {
            PlayerAnim.Play("HoldtheLaserGun");
            LGOn = false;
        }

        if (selectedWeapon == 0 && PistolOn == true)
        {
            PlayerAnim.Play("HoldthePistol");
            PistolOn = false;

        }

        
        

    }

    void selectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
