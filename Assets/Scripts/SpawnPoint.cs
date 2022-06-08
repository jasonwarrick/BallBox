using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject ball; // Grab reference to the ball prefab
    [SerializeField] GameObject parentGameObject;

    void Awake() {
        SpawnBall();
    }

    public void SpawnBall() {
        GameObject ballInstance = Instantiate(ball, transform.position, Quaternion.identity); // Create an instance of the ball prefab; Quaternion.identity essentially means "no rotation"
        ballInstance.transform.parent = parentGameObject.transform; // Set the ball position to the spawn point position
    }
}
