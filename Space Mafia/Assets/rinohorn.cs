using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rinohorn : MonoBehaviour
{
	// Start is called before the first frame update
	public PlayerCollision PC;
	public ThirdPersonMovement movement;
	public HealthBar healthBar;
	void Start()
    {
        
    }

	// Update is called once per frame
	void OnCollisionEnter(Collision collisionInfo)
	{
		if (collisionInfo.collider.tag == "Player" && movement.isDodging == false)
		{
			PC.playerCurrentHealth -= 10;
			healthBar.SetHealth(PC.playerCurrentHealth);
		}

	}
}
