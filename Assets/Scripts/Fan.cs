using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] float fanSpeed;

    void OnTriggerEnter(Collider other) {
        Debug.Log("Fan");
        other.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * fanSpeed * Time.deltaTime);
    }
}
