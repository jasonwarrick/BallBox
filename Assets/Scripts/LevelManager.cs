using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

public class LevelManager : MonoBehaviour
{
    //Rewired Stuff
    [SerializeField] int playerId = 0;
    Player player; // The Rewired Player

    [SerializeField] GameObject[] levels;
    DialogueTrigger dialogueTrigger;
    // Button 

    GameObject currentlevel;
    GameObject levelInstance;

    GameObject canvasManager;

    public int counter = -1;
    bool destoryConvo = false;
    bool restart = false;
    public bool freezeRestart = false;

    public DialogueTrigger[] trigArray;
    public GameObject skipDialogue;

    void Awake() {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);

        currentlevel = levels[counter]; // Set the current level to the first (will make this more modular as I flesh out the level system and menus)
        canvasManager = GameObject.FindGameObjectWithTag("CanvasManager");
        LoadLevel();
    }

    public void HideSkip() {
        skipDialogue.SetActive(false);
    }

    public void ShowSkip() {
        skipDialogue.SetActive(true);
    }

    public void FreezeMovement() {
        levelInstance.GetComponent<BoxMovement>().enabled = false;
        freezeRestart = true;
    }

    public void UnfreezeMovement() {
        levelInstance.GetComponent<BoxMovement>().enabled = true;
        freezeRestart = false;
    }

    void LoadLevel() {
        Debug.Log(counter);
        levelInstance = Instantiate(currentlevel, transform.position, Quaternion.identity); // Instatiate the object at the correct position (transform.position is just the origin)
        
        dialogueTrigger = levelInstance.GetComponentInChildren<DialogueTrigger>();
        canvasManager.GetComponent<CanvasManager>().cleanConvo(); // Remove any existing conversations
        
        if (!destoryConvo) { // Only instance the conversation if the level hasn't been started from a loss
            FreezeMovement();
            canvasManager.GetComponent<CanvasManager>().startConvo(counter);
            ShowSkip();
        }
        
        levelInstance.transform.position = new Vector3(0, 0, 0);
        levelInstance.transform.parent = gameObject.transform.parent; // Assign the instances parent (Not entirely necessary but it helps)
    }

    void Get_Input() {
        restart = player.GetButton("Restart");

        if (restart && !freezeRestart) {
            HideSkip();
            LostLevel();
        }
    }

    void Update() {
        if (GameObject.Find("WinPoint").GetComponent<WinPoint>().win == true) { // Sort of a roundabout / not super clean way of telling whether or not the level has been won
            WonLevel();
        }

        Get_Input();
    }

    public void LostLevel() { // This method is public so that the spikes can access it
        Destroy(levelInstance); // Destroy the instance of the current level
        destoryConvo = true;
        LoadLevel(); // Reload the same level
    }

    public void DestroyConvo() { // Make this a public function so it can be called when the player wants to skip dialogue
        dialogueTrigger.HideButton();
        destoryConvo = false;
    }

    void WonLevel() {
        Destroy(levelInstance); // Destroy the instance of the current level

        if (counter == levels.Length - 1 || counter == -1) { counter = 0; } else { counter++; } // Increment the counter, or reset it to 0 if the length of the levels array has been reached

        currentlevel = levels[counter];
        destoryConvo = false;
        LoadLevel(); // Instantiate the next level
    }
}
