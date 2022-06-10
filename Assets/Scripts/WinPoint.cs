using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    [SerializeField] SpawnPoint spawnPoint;
    
    void OnTriggerEnter(Collider other) {
        Debug.Log("Win");
        // spawnPoint.SpawnBall();
    }
}
