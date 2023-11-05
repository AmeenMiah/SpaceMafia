using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerCollision : MonoBehaviour
{
	public ThirdPersonMovement movement;
	public int playerCurrentHealth;
	public GameManager GM;
	public int maxHealth = 5;
	public HealthBar healthBar;
	public Text GameOvertext;
	public float Timeheal;
	public bool TakenDamage = false;
	public float TakenDamageCount = 0;
	public AudioManager Audio;
	public Text Objective;
	public Cinemachine.CinemachineFreeLook c_VirtualCamera;

	private void Start()
    {
		GM = FindObjectOfType<GameManager>();
		Audio = FindObjectOfType<AudioManager>();
		playerCurrentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
		c_VirtualCamera.m_XAxis.m_MaxSpeed = PlayerPrefs.GetFloat("HorSen");
		//Cursor.lockState = CursorLockMode.Locked;

	}
    void OnCollisionEnter (Collision collisionInfo)
	{
		Debug.Log(collisionInfo.collider.tag);
		Debug.Log(collisionInfo.gameObject.tag);
		if (collisionInfo.gameObject.layer == 14 && movement.isDodging == false)
		{
			TakenDamage = true;
			playerCurrentHealth -= 1;
			healthBar.SetHealth(playerCurrentHealth);
		}
		if (collisionInfo.gameObject.layer == 12 && movement.isDodging == false)
		{
			TakenDamage = true;
			playerCurrentHealth -= 5;
			healthBar.SetHealth(playerCurrentHealth);
		}
		if (collisionInfo.gameObject.layer == 17)
        {
			Destroy(collisionInfo.gameObject);
			Audio.Play("PickUpSound");
			Objective.text = "You got a collectible!";
			Invoke("TurnTextOff", 3f);
			if (SceneManager.GetActiveScene().buildIndex == 2)
            {
				GM.CollectibleLevel1 = true;
            }
			if (SceneManager.GetActiveScene().buildIndex == 4)
			{
				GM.CollectibleLevel2 = true;
			}
			if (SceneManager.GetActiveScene().buildIndex == 6)
			{
				GM.CollectibleLevel3 = true;
			}
			if (SceneManager.GetActiveScene().buildIndex == 7)
			{
				GM.CollectibleLevel4 = true;
			}
		}
		if (collisionInfo.collider.tag == "Obstacle" && movement.isDodging == false && Clock.TimeStopped)
        {
			TakenDamage = true;
			playerCurrentHealth -= 3;
			healthBar.SetHealth(playerCurrentHealth);
        }
		if (collisionInfo.collider.tag == "Obstacle" && movement.isDodging == false)
		{
			TakenDamage = true;
			playerCurrentHealth -= 3;
			healthBar.SetHealth(playerCurrentHealth);
		}
		if (collisionInfo.collider.tag == "RinoHorn" && movement.isDodging == false)
		{
			TakenDamage = true;
			playerCurrentHealth -= 10;
			healthBar.SetHealth(playerCurrentHealth);
		}
		if (collisionInfo.collider.tag == "TimeHands" && movement.isDodging == false && Clock.TimeStopped)
		{
			TakenDamage = true;
			playerCurrentHealth -= 2;
			healthBar.SetHealth(playerCurrentHealth);
		}
		//if (Clock.TimeStopped)
		//{
		//Timeheal += Time.deltaTime;
		//if (Timeheal > 0.1)
		//{
		//Timeheal = 0;
		//playerCurrentHealth += 1;
		//healthBar.SetHealth(playerCurrentHealth);
		//}

		//}

	}

	private void TurnTextOff()
    {
		Objective.text = "";
	}
    private void Update()
    {
		if (playerCurrentHealth <= 0)
		{
			if (SceneManager.GetActiveScene().buildIndex != 10)
			{
				movement.enabled = false;
				GameOvertext.gameObject.SetActive(true);
				FindObjectOfType<GameManager>().GameOver();
			}
			else
			{
				FindObjectOfType<GameManager>().ChaosHordeGameOver();
			}
		}

		if (TakenDamage)
        {
			TakenDamageCount += Time.deltaTime;
        }
		
		if (TakenDamageCount > 2)
        {
			TakenDamage = false;
			TakenDamageCount = 0;
        }
		if (transform.position.y < 0)
        {
			playerCurrentHealth -= 1;
        }
	}

	public void TakeDamage(int Damage)
    {
		if (movement.isDodging == false)
        {
			playerCurrentHealth -= Damage;
			TakenDamage = true;
			healthBar.SetHealth(playerCurrentHealth);
		}
	}

}
