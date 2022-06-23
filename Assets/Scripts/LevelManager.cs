using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] levels;

    GameObject currentlevel;
    GameObject levelInstance;

    public int counter = -1;
    bool destoryConvo = false;

    public DialogueTrigger[] trigArray;

    void Awake() {
        currentlevel = levels[counter]; // Set the current level to the first (will make this more modular as I flesh out the level system and menus)
        LoadLevel();
    }

    void LoadLevel() {
        levelInstance = Instantiate(currentlevel, transform.position, Quaternion.identity); // Instatiate the object at the correct position (transform.position is just the origin)
        levelInstance.transform.parent = gameObject.transform.parent; // Assign the instances parent (Not entirely necessary but it helps)
        
        if (destoryConvo) { // Check if the level has been loaded from a loss, and destroy the conversation if it has
            DestroyConvo();
        }
    }

    void Update() {
        if (GameObject.Find("WinPoint").GetComponent<WinPoint>().win == true) { // Sort of a roundabout / not super clean way of telling whether or not the level has been won
            WonLevel();
        }
    }

    public void LostLevel() { // This method is public so that the spikes can access it
        Destroy(levelInstance); // Destroy the instance of the current level
        
        destoryConvo = true;
        LoadLevel(); // Reload the same level
    }

    public void DestroyConvo() { // Make this a public function so it can be called when the player wants to skip dialogue
        Destroy(GameObject.FindGameObjectWithTag("StartConvo")); // If the level has been restarted from a loss, delete the conversation button so it doesn't pop up again
        destoryConvo = false;
    }

    void WonLevel() {
        Destroy(levelInstance); // Destroy the instance of the current level

        if (counter == levels.Length - 1 || counter == -1) { counter = 0; } else { counter++; } // Increment the counter, or reset it to 0 if the length of the levels array has been reached

        currentlevel = levels[counter];
        // trigArray[counter].TriggerDialogue();
        LoadLevel(); // Instantiate the next level
    }
}
