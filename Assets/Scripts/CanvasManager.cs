using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject[] convos;
    GameObject convoInstance;

    public void startConvo(int index) {
        convoInstance = Instantiate(convos[index], transform.position, Quaternion.identity);
        convoInstance.transform.SetParent(gameObject.transform);
    }

    public void cleanConvo() {
        Destroy(convoInstance);
    }
}
