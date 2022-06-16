using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] levels;

    GameObject currentlevel;
    GameObject levelInstance;

    [SerializeField] int counter = 0;
    public List<GameObject> spikeList;
    // object[] objArray;
    // bool hasSpike = false;

    void Awake() {
        currentlevel = levels[counter]; // Set the current level to the first (will make this more modular as I flesh out the level system and menus)
        LoadLevel();
    }

    void LoadLevel()
    {
        levelInstance = Instantiate(currentlevel, transform.position, Quaternion.identity); // Instatiate the object at the correct position (transform.position is just the origin)
        levelInstance.transform.parent = gameObject.transform.parent; // Assign the instances parent (Not entirely necessary but it helps)
        // objArray = GameObject.FindObjectsOfType(typeof (GameObject)); // Code from: https://answers.unity.com/questions/7615/how-do-i-iterate-over-all-scene-objects-from-an-ed.html
        // hasSpike = false;
        // FindSpikes();
    }

    // void FindSpikes(){
    //     foreach (object obj in objArray) {
    //         GameObject gameObj = (GameObject) obj;
    //             if (gameObj.CompareTag("Spike")) {
    //                 spikeList.Add(gameObj);
    //                 hasSpike = true;
    //             }
    //     }
        
    // }

    void Update() {
        if (GameObject.Find("WinPoint").GetComponent<WinPoint>().win == true) { // Sort of a roundabout / not super clean way of telling whether or not the level has been won
            WonLevel();
        }

        // if (hasSpike) {
        //     CheckSpikeCollisions();
        // }
    }

    // void CheckSpikeCollisions() {
    //     foreach (GameObject spike in spikeList) {
    //         if (spike.GetComponent<Spike>().lost == true) {
                
    //             LostLevel();
    //             break;
    //         }
    //     }
    // }

    // void EmptyList() {
    //     spikeList.Clear();
    //     hasSpike = false;
    // }

    public void LostLevel() { // This method is public so that the spikes can access it
        // EmptyList(); // Clear the spike list first, that way it isn't trying to access the spikes after they've been destroyed
        Destroy(levelInstance); // Destroy the instance of the current level

        LoadLevel(); // Reload the same level
    }

    void WonLevel() {
        Destroy(levelInstance); // Destroy the instance of the current level

        if (counter == levels.Length - 1) { counter = 0; } else { counter++; } // Increment the counter, or reset it to 0 if the length of the levels array has been reached

        currentlevel = levels[counter];
        LoadLevel(); // Instantiate the next level
    }
}
