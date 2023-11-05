using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    public GunScript G1;
    public MeleeAttack G2;
    public GunScript G3;
    public LaunchGrenade G4;
    public GunScript G5;
    public WeaponSwitching WS;
    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        if (WS.selectedWeapon == 0)
        {
            scoreText.text = (G1.name)+ "  " + (G1.currentAmmo).ToString("0") + "/" + (G1.maxAmmo).ToString("0");
            if (G1.isReloading == true)
            {
                scoreText.text = "Reloading...";
            }
        }
        if (WS.selectedWeapon == 1)
        {
            scoreText.text = (G2.name) + "  " + (G2.SwordTime).ToString("0") + "/" + (G2.WaitForSwing).ToString("0");
        }
        if (WS.selectedWeapon == 2)
        {
            scoreText.text = (G3.name) + "  " + (G3.currentAmmo).ToString("0") + "/" + (G3.maxAmmo).ToString("0");
            if (G3.isReloading == true)
            {
                scoreText.text = "Reloading...";
            }
        }
        if (WS.selectedWeapon == 3)
        {
            scoreText.text = (G4.name) + "  " + (G4.currentAmmo).ToString("0") + "/" + (G4.maxAmmo).ToString("0");
            if (G4.isReloading == true)
            {
                scoreText.text = "Reloading...";
            }
        }
        if (WS.selectedWeapon == 4)
        {
            scoreText.text = (G5.name) + "  " + (G5.currentAmmo).ToString("0") + "/" + (G5.maxAmmo).ToString("0");
            if (G5.isReloading == true)
            {
                scoreText.text = "Reloading...";
            }
        }


    }
}
