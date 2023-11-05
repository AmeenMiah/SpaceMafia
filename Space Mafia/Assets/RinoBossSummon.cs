using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinoBossSummon : MonoBehaviour
{
	public GameObject RinoBoss;
	public GameObject RinoHealth;
	public GameObject Door;
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 11)
		{
			Door.gameObject.SetActive(true);
			Debug.Log("Hi");
			RinoHealth.SetActive(true);
			RinoBoss.SetActive(true);
		}
	}
}
