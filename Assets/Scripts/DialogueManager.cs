using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using TMPro;

 // Fundamental dialgoue scripts taken from Brackeys (Obviously): https://www.youtube.com/watch?v=_nRzoTzeyxU
public class DialogueManager : MonoBehaviour
{
    //Rewired Stuff
    [SerializeField] int playerId = 0;
    Player player; // The Rewired Player

    Queue<string> sentences; // Queue is FIFO list

    public TextMeshProUGUI nameText; // Variable type tip taken from: https://answers.unity.com/questions/1747337/cant-assign-a-text-mesh-pro-on-the-inspector.html
    public TextMeshProUGUI dialogueText;
    GameObject levelManager;
    public Button skipButton;

    public Animator animator;

    public float textDelayTime;

    bool coroutineRunning = false;
    string sentence;
    bool pressedCont = false;
    bool contDebouncer = false;
    bool pressedSkip = false;
    bool dialogueEnded = false;
    bool movementFrozen = false;

    void Awake() {
        player = ReInput.players.GetPlayer(playerId);
    }

    // Start is called before the first frame update
    void Start() {
        sentences = new Queue<string>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
    }

    public void StartDialogue(Dialogue dialogue) {
        dialogueEnded = false;
        animator.SetBool("isOpen", true);

        GameObject.Find("LevelManager").GetComponent<LevelManager>().HideSkip(); // Hide the skip dialogue button once dialogue has been started

        nameText.text = dialogue.name;

        sentences.Clear(); // Removes any sentences from a previous conversation

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0 && !coroutineRunning) { // End the dialogue when there are no more sentences to display, and only if the coroutine is done
            EndDialogue();
            return;
        }
        
        StopAllCoroutines(); // Make sure there isn't another sentence trying to type out

        if (!coroutineRunning) { // If the coroutine isn't running, display the next text as normal
            sentence = sentences.Dequeue(); //Take the first element from the sentences queue and store it in a variable
            StartCoroutine(TypeSentence(sentence));
        } else { // If it isn't, skip the rest of the coroutine and immediately display the full sentence
            dialogueText.text = sentence;
            coroutineRunning = false;
        }
    }

    IEnumerator TypeSentence(string sentence) { //IEnumerator means it's a coroutine (Can run while other methods are running)
        coroutineRunning = true; // Keeps track of when the coroutine is running in order to dynnamically change the behavior of the continue button

        dialogueText.text = "";

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("DialogueBox_Open")) { // Parameter code taken from: https://answers.unity.com/questions/362629/how-can-i-check-if-an-animation-is-being-played-or.html
            yield return null; // Wait a frame if the open animation isn't playing yet
        }

        foreach (char letter in sentence.ToCharArray()) { // Loop through the sentence in the form of characters
            dialogueText.text += letter;
            yield return new WaitForSeconds(textDelayTime);
        }

        coroutineRunning = false;
    }

    public void DestroyConvoInstant() {
        Destroy(GameObject.FindGameObjectWithTag("StartConvo"));
    }

    public IEnumerator DestroyConvo() { // Create a coroutine to destroy the conversation once the animation is done playing (to avoid errors)
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("DialogueBox_Open")) { // Parameter code taken from: https://answers.unity.com/questions/362629/how-can-i-check-if-an-animation-is-being-played-or.html
            yield return null; // Wait a frame if the open animation isn't playing yet
        }

        Destroy(GameObject.FindGameObjectWithTag("StartConvo"));
        yield return null;
    }

    public void EndDialogue() {
        if (FindObjectOfType<DialogueTrigger>().dialogueFinished) { // Close the dialogue box if the trigger is exhausted
            animator.SetBool("isOpen", false);
            DestroyConvo();
            levelManager.GetComponent<LevelManager>().UnfreezeMovement();
            dialogueEnded = true;
        } else {
            FindObjectOfType<DialogueTrigger>().TriggerDialogue(); // If it isn't, trigger the next speakers dialogue
        }
    }

    void Get_Input() {
        pressedCont = player.GetButton("Continue");
        pressedSkip = player.GetButton("Skip");

        if (pressedCont && !contDebouncer && !dialogueEnded) { // The debouncer ensures that the method will only be called once per button press
            DisplayNextSentence();
            contDebouncer = true;
        }

        if (!pressedCont && contDebouncer) {
            contDebouncer = false;
        }

        if (pressedSkip && !animator.GetCurrentAnimatorStateInfo(0).IsName("DialogueBox_Open")) { // Make sure that the skip function is only called before the dialogue is shown
            dialogueEnded = true;
            skipButton.onClick.Invoke();
            levelManager.GetComponent<LevelManager>().FreezeMovement(); // Freeze movement so the level doesn't restart on skip
            movementFrozen = true;
        }

        if (!pressedSkip && movementFrozen) { // Unfreeze level movement AFTER skipping
            movementFrozen = false;
            levelManager.GetComponent<LevelManager>().UnfreezeMovement();
        }
    }

    void Update() {
        Get_Input();
    }
}
