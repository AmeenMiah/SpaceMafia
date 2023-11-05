using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagolueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager DM;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(StartDia());

    }

    IEnumerator StartDia()
    {
        yield return new WaitForSeconds(0.5f);
        DM.StartDialogue(dialogue);
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
