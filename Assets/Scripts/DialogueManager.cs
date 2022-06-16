using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

 // Fundamental dialgoue scripts taken from Brackeys (Obviously): https://www.youtube.com/watch?v=_nRzoTzeyxU
public class DialogueManager : MonoBehaviour
{
    Queue<string> sentences; // Queue is FIFO list

    public TextMeshProUGUI nameText; // Variable type tip taken from: https://answers.unity.com/questions/1747337/cant-assign-a-text-mesh-pro-on-the-inspector.html
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    public float textDelayTime;

    // Start is called before the first frame update
    void Start() {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear(); // Removes any sentences from a previous conversation

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue(); //Take the first element from the sentences queue and store it in a variable
        StopAllCoroutines(); // Make sure there isn't another sentence trying to type out

        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence) { //IEnumerator means it's a coroutine (Can run while other methods are running)
        dialogueText.text = "";

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("DialogueBox_Open")) { // Parameter code taken from: https://answers.unity.com/questions/362629/how-can-i-check-if-an-animation-is-being-played-or.html
            yield return null; // Wait a frame if the open animation isn't playing yet
        }

        foreach (char letter in sentence.ToCharArray()) { // Loop through the sentence in the form of characters
            dialogueText.text += letter;
            yield return new WaitForSeconds(textDelayTime);
        }
    }

    void EndDialogue() {
        animator.SetBool("isOpen", false);
    }
}
