using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public bool lost = false;

    void OnTriggerEnter(Collider other) {
        lost = true;
        Destroy(other.gameObject);
    }
}
