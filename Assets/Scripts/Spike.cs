using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    GameObject levelManager;

    void Awake() {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
    }

    void OnTriggerEnter(Collider other) {
        levelManager.GetComponent<LevelManager>().LostLevel();
        Destroy(other.gameObject);
    }
}
