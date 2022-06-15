using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] levels;

    GameObject currentlevel;
    GameObject levelInstance;

    int counter = 0;

    void Awake() {
        currentlevel = levels[0]; // Set the current level to the first (will make this more modular as I flesh out the level system and menus)
        LoadLevel();
    }

    void LoadLevel() {
        levelInstance = Instantiate(currentlevel, transform.position, Quaternion.identity); // Instatiate the object at the correct position (transform.position is just the origin)
        levelInstance.transform.parent = gameObject.transform.parent; // Assign the instances parent (Not entirely necessary but it helps)
    }

    void Update() {
        if (GameObject.Find("WinPoint").GetComponent<WinPoint>().win == true)
        { // Sort of a roundabout / not super clean way of telling whether or not the level has been won
            WonLevel();
        }

        // if (GameObject.Find("Spike").GetComponent<Spike>().lost == true) { // Similarly roundabout / not super clean way of telling whether or not the level has been lost
        //     LostLevel();
        // }
    }

    // private void LostLevel()
    // {
    //     Destroy(levelInstance); // Destroy the instance of the current level

    //     LoadLevel(); // Reload the same level
    // }

    private void WonLevel()
    {
        Destroy(levelInstance); // Destroy the instance of the current level

        if (counter == levels.Length - 1) { counter = 0; } else { counter++; } // Increment the counter, or reset it to 0 if the length of the levels array has been reached

        currentlevel = levels[counter];
        LoadLevel(); // Instantiate the next level
    }
}
