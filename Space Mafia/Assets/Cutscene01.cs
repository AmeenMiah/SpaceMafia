using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cutscene01 : MonoBehaviour
{
    public Text CT;
    public float num = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Cutscene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Increase()
    {
        num++;
    }
    IEnumerator Cutscene()
    {

        CT.text = "Hello";
        yield return new WaitForSeconds(1f);
        CT.text = "You are the Bounty Hunter";
        yield return new WaitForSeconds(2f);
        CT.text = "I have called you from another world";
        yield return new WaitForSeconds(2f);
        CT.text = "Your goal is to defend humannity from the Space Mafia";
        yield return new WaitForSeconds(3f);
        CT.text = "Now go and complete your mission";
        yield return new WaitForSeconds(2f);
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Level01");
        

    }
}
