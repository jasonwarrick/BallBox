using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // All fan code adapted from: https://www.youtube.com/watch?v=iCMhrKOuBZg
    
    public bool inFan = false;
    GameObject fan;
    
    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
       if(inFan) {
           rb.AddForce(fan.GetComponent<Fan>().direction * fan.GetComponent<Fan>().fanSpeed);
       }
    }

    void OnTriggerEnter(Collider coll) {
       if(coll.gameObject.tag == "Fan") {
           fan = coll.gameObject;
           inFan = true;
       }
    }

    void OnTriggerExit(Collider coll) {
       if(coll.gameObject.tag == "Fan") {
           inFan = false;
       }
    }
}
