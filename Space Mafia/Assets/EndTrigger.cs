using UnityEngine;

public class EndTrigger : MonoBehaviour
{	
	public GameManager GM;
	public GameObject Door;
    //OnTriggerEnter

    private void Start()
    {
		Door.gameObject.SetActive(false);
    }
    void OnCollisionEnter(Collision collisionInfo)
	{
		if (collisionInfo.collider.tag == "Player")
		{
			Door.gameObject.SetActive(true);
			GM.Level1Complete();
		}
	}
    private void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Player")
		{
			Door.gameObject.SetActive(true);
			GM.Level1Complete();
		}
	}
}
