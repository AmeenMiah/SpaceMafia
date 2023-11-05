using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int Buff = 0;
    public GunScript Gun;
    public MeleeAttack Sword;
    public GrenadeExplosion RPGRocket;
    public LaunchGrenade RPG;
    public Text BuffText;
    public int Cost = 10;
    public float BuffNum = 5;
    public int HPBuffNum = 5;
    public Text CostText;
    public bool BoughtOnce = false;
    public int CostIncrease = 5;
    public ThirdPersonMovement ThirdPerMov;
    public PlayerCollision PlayerCol;
    public AudioManager Audio;
    public GunScript Gun2;
    public GunScript Gun3;
    public GameObject GoldenBuffParticle;
    // Start is called before the first frame update
    void Start()
    {
        BuffText.text = "0";
        Audio = FindObjectOfType<AudioManager>();
        CostText.text = "Cost: " + Cost;
    }

    public void DamageBuffFunction()
    {
        BoughtOnce = false;
        if (Buff == 0 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            Gun.damage += BuffNum;
            Cost += CostIncrease;
            BoughtOnce = true;
        }

        if (Buff == 1 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            Gun.damage += BuffNum;
            Cost += CostIncrease;
            BoughtOnce = true;
        }

        if (Buff == 2 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            Gun.damage += BuffNum;
            Cost += CostIncrease * 2;
            BoughtOnce = true;
        }

        if (Buff == 3 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            Gun.damage += BuffNum;
            Cost += CostIncrease * 3;
            BoughtOnce = true;
        }

        if (Buff == 4 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            Gun.damage += BuffNum;
            Cost = 0;
            BoughtOnce = true;
        }

        switch (Buff)
        {
            case 1:
                BuffText.text = "I";
                break;
            case 2:
                BuffText.text = "II";
                break;
            case 3:
                BuffText.text = "III";
                break;
            case 4:
                BuffText.text = "IV";
                break;
            case 5:
                BuffText.text = "V";
                break;
            default:
                BuffText.text = "0";
                break;
        }

        if (BoughtOnce)
        {
            Audio.Play("Success");
        }
        else
        {
            Audio.Play("Failure");
        }
        CostText.text = "Cost: " + Cost;

    }

    public void ReloadBuffFunction()
    {
        BoughtOnce = false;
        if (Buff == 0 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            Gun.reloadTime -= BuffNum;
            Cost += CostIncrease;
            BoughtOnce = true;
        }

        if (Buff == 1 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            Gun.reloadTime -= BuffNum;
            Cost += CostIncrease;
            BoughtOnce = true;
        }

        if (Buff == 2 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            Gun.reloadTime -= BuffNum;
            Cost += CostIncrease * 2;
            BoughtOnce = true;
        }

        if (Buff == 3 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            Gun.reloadTime -= BuffNum;
            Cost += CostIncrease * 3;
            BoughtOnce = true;
        }

        if (Buff == 4 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            Gun.reloadTime -= BuffNum;
            Cost = 0;
            BoughtOnce = true;
        }

        switch (Buff)
        {
            case 1:
                BuffText.text = "I";
                break;
            case 2:
                BuffText.text = "II";
                break;
            case 3:
                BuffText.text = "III";
                break;
            case 4:
                BuffText.text = "IV";
                break;
            case 5:
                BuffText.text = "V";
                break;
            default:
                BuffText.text = "0";
                break;
        }


        if (BoughtOnce)
        {
            Audio.Play("Success");
        }
        else
        {
            Audio.Play("Failure");
        }
        CostText.text = "Cost: " + Cost;


    }

    public void MeleeBuffFunction()
    {
        BoughtOnce = false;
        if (Buff == 0 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            Sword.damage += BuffNum;
            Cost += CostIncrease;
            BoughtOnce = true;
        }

        if (Buff == 1 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            Sword.damage += BuffNum;
            Cost += CostIncrease;
            BoughtOnce = true;
        }

        if (Buff == 2 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            Sword.damage += BuffNum;
            Cost += CostIncrease * 2;
            BoughtOnce = true;
        }

        if (Buff == 3 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            Sword.damage += BuffNum;
            Cost += CostIncrease * 3;
            BoughtOnce = true;
        }

        if (Buff == 4 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            Sword.damage += BuffNum;
            Cost = 0;
            BoughtOnce = true;
        }

        switch (Buff)
        {
            case 1:
                BuffText.text = "I";
                break;
            case 2:
                BuffText.text = "II";
                break;
            case 3:
                BuffText.text = "III";
                break;
            case 4:
                BuffText.text = "IV";
                break;
            case 5:
                BuffText.text = "V";
                break;
            default:
                BuffText.text = "0";
                break;
        }


        if (BoughtOnce)
        {
            Audio.Play("Success");
        }
        else
        {
            Audio.Play("Failure");
        }

        CostText.text = "Cost: " + Cost;
    }

    public void RPGDamageBuffFunction()
    {
        BoughtOnce = false;
        if (Buff == 0 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            RPGRocket.damage += BuffNum;
            Cost += CostIncrease;
            BoughtOnce = true;
        }

        if (Buff == 1 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            RPGRocket.damage += BuffNum;
            Cost += CostIncrease;
            BoughtOnce = true;
        }

        if (Buff == 2 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            RPGRocket.damage += BuffNum;
            Cost += CostIncrease * 2;
            BoughtOnce = true;
        }

        if (Buff == 3 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            RPGRocket.damage += BuffNum;
            Cost += CostIncrease * 3;
            BoughtOnce = true;
        }

        if (Buff == 4 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            RPGRocket.damage += BuffNum;
            Cost = 0;
            BoughtOnce = true;
        }

        switch (Buff)
        {
            case 1:
                BuffText.text = "I";
                break;
            case 2:
                BuffText.text = "II";
                break;
            case 3:
                BuffText.text = "III";
                break;
            case 4:
                BuffText.text = "IV";
                break;
            case 5:
                BuffText.text = "V";
                break;
            default:
                BuffText.text = "0";
                break;
        }

        if (BoughtOnce)
        {
            Audio.Play("Success");
        }
        else
        {
            Audio.Play("Failure");
        }

        CostText.text = "Cost: " + Cost;
    }

    public void RPGReloadBuffFunction()
    {
        BoughtOnce = false;
        if (Buff == 0 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            RPG.reloadTime -= BuffNum;
            Cost += CostIncrease;
            BoughtOnce = true;
        }

        if (Buff == 1 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            RPG.reloadTime -= BuffNum;
            Cost += CostIncrease;
            BoughtOnce = true;
        }

        if (Buff == 2 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            RPG.reloadTime -= BuffNum;
            Cost += CostIncrease * 2;
            BoughtOnce = true;
        }

        if (Buff == 3 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            RPG.reloadTime -= BuffNum;
            Cost += CostIncrease * 3;
            BoughtOnce = true;
        }

        if (Buff == 4 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            RPG.reloadTime -= BuffNum;
            Cost = 0;
            BoughtOnce = true;
        }

        switch (Buff)
        {
            case 1:
                BuffText.text = "I";
                break;
            case 2:
                BuffText.text = "II";
                break;
            case 3:
                BuffText.text = "III";
                break;
            case 4:
                BuffText.text = "IV";
                break;
            case 5:
                BuffText.text = "V";
                break;
            default:
                BuffText.text = "0";
                break;
        }


        if (BoughtOnce)
        {
            Audio.Play("Success");
        }
        else
        {
            Audio.Play("Failure");
        }

        CostText.text = "Cost: " + Cost;

    }

    public void HealthIncrease()
    {
        BoughtOnce = false;
        if (Buff == 0 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            PlayerCol.maxHealth += HPBuffNum;
            PlayerCol.healthBar.SetMaxHealth(PlayerCol.maxHealth);
            PlayerCol.playerCurrentHealth = PlayerCol.maxHealth;
            Cost += CostIncrease;
            BoughtOnce = true;
        }

        if (Buff == 1 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            PlayerCol.maxHealth += HPBuffNum;
            PlayerCol.healthBar.SetMaxHealth(PlayerCol.maxHealth);
            PlayerCol.playerCurrentHealth = PlayerCol.maxHealth;
            Cost += CostIncrease;
            BoughtOnce = true;
        }

        if (Buff == 2 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            PlayerCol.maxHealth += HPBuffNum;
            PlayerCol.healthBar.SetMaxHealth(PlayerCol.maxHealth);
            PlayerCol.playerCurrentHealth = PlayerCol.maxHealth;
            Cost += CostIncrease * 2;
            BoughtOnce = true;
        }

        if (Buff == 3 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            PlayerCol.maxHealth += HPBuffNum;
            PlayerCol.healthBar.SetMaxHealth(PlayerCol.maxHealth);
            PlayerCol.playerCurrentHealth = PlayerCol.maxHealth;
            Cost += CostIncrease * 3;
            BoughtOnce = true;
        }

        if (Buff == 4 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            PlayerCol.maxHealth += HPBuffNum;
            PlayerCol.healthBar.SetMaxHealth(PlayerCol.maxHealth);
            PlayerCol.playerCurrentHealth = PlayerCol.maxHealth;
            Cost = 0;
            BoughtOnce = true;
        }

        switch (Buff)
        {
            case 1:
                BuffText.text = "I";
                break;
            case 2:
                BuffText.text = "II";
                break;
            case 3:
                BuffText.text = "III";
                break;
            case 4:
                BuffText.text = "IV";
                break;
            case 5:
                BuffText.text = "V";
                break;
            default:
                BuffText.text = "0";
                break;
        }

        if (BoughtOnce)
        {
            Audio.Play("Success");
        }
        else
        {
            Audio.Play("Failure");
        }

        CostText.text = "Cost: " + Cost;
    }

    public void DodgeFunction()
    {
        BoughtOnce = false;
        if (Buff == 0 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            ThirdPerMov.DodgeSprintConsumation -= HPBuffNum;
            Cost += CostIncrease;
            BoughtOnce = true;
        }

        if (Buff == 1 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            ThirdPerMov.DodgeSprintConsumation -= HPBuffNum;
            Cost += CostIncrease;
            BoughtOnce = true;
        }

        if (Buff == 2 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            ThirdPerMov.DodgeSprintConsumation -= HPBuffNum;
            Cost += CostIncrease * 2;
            BoughtOnce = true;
        }

        if (Buff == 3 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            ThirdPerMov.DodgeSprintConsumation -= HPBuffNum;
            Cost += CostIncrease * 3;
            BoughtOnce = true;
        }

        if (Buff == 4 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            ThirdPerMov.DodgeSprintConsumation -= HPBuffNum;
            Cost = 0;
            BoughtOnce = true;
        }

        switch (Buff)
        {
            case 1:
                BuffText.text = "I";
                break;
            case 2:
                BuffText.text = "II";
                break;
            case 3:
                BuffText.text = "III";
                break;
            case 4:
                BuffText.text = "IV";
                break;
            case 5:
                BuffText.text = "V";
                break;
            default:
                BuffText.text = "0";
                break;
        }

        if (BoughtOnce)
        {
            Audio.Play("Success");
        }
        else
        {
            Audio.Play("Failure");
        }

        CostText.text = "Cost: " + Cost;

    }

    public void SprintIncrease()
    {
        BoughtOnce = false;
        if (Buff == 0 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            ThirdPerMov.maxSprintTime += HPBuffNum;
            ThirdPerMov.sprintBar.SetMaxSprint(ThirdPerMov.maxSprintTime);
            Cost += CostIncrease;
            BoughtOnce = true;
        }

        if (Buff == 1 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            ThirdPerMov.maxSprintTime += HPBuffNum;
            ThirdPerMov.sprintBar.SetMaxSprint(ThirdPerMov.maxSprintTime);
            Cost += CostIncrease;
            BoughtOnce = true;
        }

        if (Buff == 2 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            ThirdPerMov.maxSprintTime += HPBuffNum;
            ThirdPerMov.sprintBar.SetMaxSprint(ThirdPerMov.maxSprintTime);
            Cost += CostIncrease * 2;
            BoughtOnce = true;
        }

        if (Buff == 3 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            ThirdPerMov.maxSprintTime += HPBuffNum;
            ThirdPerMov.sprintBar.SetMaxSprint(ThirdPerMov.maxSprintTime);
            Cost += CostIncrease * 3;
            BoughtOnce = true;
        }

        if (Buff == 4 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 1;
            WaveSpawner.Score -= Cost;
            ThirdPerMov.maxSprintTime += HPBuffNum;
            ThirdPerMov.sprintBar.SetMaxSprint(ThirdPerMov.maxSprintTime);
            Cost = 0;
            BoughtOnce = true;
        }

        switch (Buff)
        {
            case 1:
                BuffText.text = "I";
                break;
            case 2:
                BuffText.text = "II";
                break;
            case 3:
                BuffText.text = "III";
                break;
            case 4:
                BuffText.text = "IV";
                break;
            case 5:
                BuffText.text = "V";
                break;
            default:
                BuffText.text = "0";
                break;
        }

        if (BoughtOnce)
        {
            Audio.Play("Success");
        }
        else
        {
            Audio.Play("Failure");
        }

        CostText.text = "Cost: " + Cost;
    }

    public void GoldenBuff()
    {

        BoughtOnce = false;
        if (Buff == 0 && WaveSpawner.Score >= Cost && BoughtOnce == false)
        {
            Buff += 5;
            WaveSpawner.Score -= Cost;
            Gun.damage += 8;
            Gun2.damage += 2;
            Gun3.damage += 50;
            Sword.damage += 25;
            RPGRocket.damage += 30;
            PlayerCol.maxHealth += HPBuffNum;
            PlayerCol.healthBar.SetMaxHealth(PlayerCol.maxHealth);
            PlayerCol.playerCurrentHealth = PlayerCol.maxHealth;
            BuffText.text = "V";
            Cost = 0;
            GoldenBuffParticle.SetActive(true);
            BoughtOnce = true;
        }

        if (BoughtOnce)
        {
            Audio.Play("Success");
        }
        else
        {
            Audio.Play("Failure");
        }

        CostText.text = "Cost: " + Cost;
    }
}
