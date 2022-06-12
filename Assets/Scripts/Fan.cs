using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    // All fan code adapted from: https://www.youtube.com/watch?v=iCMhrKOuBZg

    public float fanSpeed;
    public Vector3 direction;
    
    GameObject ball;

    private void Update() {
        direction = transform.TransformDirection(Vector3.up); // Gets the local up vector
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Fan");
        ball = other.gameObject;
        ball.GetComponent<Ball>().inFan = true;
    }

    void OnTriggerExit(Collider other) {
        ball.GetComponent<Ball>().inFan = false;
    }
}
