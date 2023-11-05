using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Text dialogueText;
    public GameManager GM;
    public bool Skip;
    public bool Skipper;

    Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        //Set sentences to new Queue<string>
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //Set what ever text is refrenced to the name of the character
        nameText.text = dialogue.name;

        //Clear the previous sentences 
        sentences.Clear();

        //Start A foreach loop
        foreach (string sentence in dialogue.sentences)
        {
            //Enqueue the next text from sentences 
            sentences.Enqueue(sentence);
        }
        
        //Call the function called "DisplayNextSentence()
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (Skipper == true)
        {
            Skipper = false;
            Skip = true;
            return;
        }

            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            Skip = false;
            string sentence = sentences.Dequeue();
            Debug.Log(sentence);
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        //while (Skip == false)
        //{
        
            foreach (char letter in sentence.ToCharArray())
            {
                if (Skip == false)
                {
                    Skipper = true;
                    dialogueText.text += letter;
                    //yield return null;
                    yield return new WaitForSeconds(0.05f);
                    Debug.Log(letter);
                    Debug.Log(sentence.ToCharArray());
                }
                else
                {
                    dialogueText.text += letter;
                    Skipper = false;

                }
            }
        Skipper = false;
    }


    void EndDialogue()
    {
        Debug.Log("End of conversation");
        if ((SceneManager.GetActiveScene().buildIndex == 8) || (SceneManager.GetActiveScene().buildIndex == 11))
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            GM.LoadNextLevel();
        }

    }
}
