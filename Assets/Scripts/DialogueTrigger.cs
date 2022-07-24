using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using TMPro;

 // Fundamental dialgoue scripts taken from Brackeys (Obviously): https://www.youtube.com/watch?v=_nRzoTzeyxU
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dialogue;

    //Rewired Stuff
    [SerializeField] int playerId = 0;
    Player player; // The Rewired Player

    public int dialogueCounter = 0; // Makes sure the array isn't iterated over
    public bool dialogueFinished = false;
    public bool lost = false;
    bool dialogueFlag = false;

    void Awake() {
        player = ReInput.players.GetPlayer(playerId);
    }

    void Update() {
        if (player.GetButton("Continue") && !dialogueFlag) {
            TriggerDialogue();
            dialogueFlag = true;
        }
    }

    public void TriggerDialogue() {
        HideButton(); // Hide the button so it isn't visible during dialogue
        if (!dialogueFinished && !lost) {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue[dialogueCounter]);
        } // If the dialogue should still be running, start it with the current speaker's lines

        if (dialogueCounter < dialogue.Length - 1) { // If there are more speakers, incremement forward in the array
            dialogueCounter++;
        } else {
            dialogueFinished = true; // When the array is incremented through, mark the dialogue trigger as finished
        }
    }

    public void HideButton() {
        // GameObject startConvo = GameObject.FindGameObjectWithTag("StartConvo");
        gameObject.GetComponent<Image>().enabled = false;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
    }
}
