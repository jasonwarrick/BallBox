using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    [SerializeField] SpawnPoint spawnPoint;

    public bool win = false;
    
    void OnTriggerEnter(Collider other) {
        win = true;
    }
}
