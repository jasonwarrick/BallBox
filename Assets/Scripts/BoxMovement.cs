using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

// All Rewired code taken from the Rewired documentation

// [RequireComponent(typeof(CharacterController))]
public class BoxMovement : MonoBehaviour {

    // The Rewired player id of this character
    [SerializeField] int playerId = 0;
    Player player; // The Rewired Player
    Rigidbody playerRB;
    Vector3 moveVector;
    bool bump = false;

    [SerializeField] Vector3 rotationSpeed = new Vector3(0, 0, 50);
    
    [SerializeField] float maxBump = 2f;
    [SerializeField] float minBump = 0f;
    [SerializeField] float bumpSpeed = 0.2f;
    Vector3 bumpVector = new Vector3(0, 0, 0);

    float rotateVal;

    void Awake() {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
        playerRB = GetComponent<Rigidbody>();
    }

    void Update () {
        GetInput();
    }

    void FixedUpdate() {
        ProcessMovement(); // Placed in fixed update to improve physics performance
        ProcessRotation();
    }

    private void ProcessMovement() {
        if (bump) {
            bumpVector = new Vector3(0, Mathf.Lerp(playerRB.position.y, maxBump, bumpSpeed), 0);
            playerRB.position = transform.TransformDirection(bumpVector);
        } else {
            bumpVector = new Vector3(0, Mathf.Lerp(playerRB.position.y, minBump, bumpSpeed), 0);
            playerRB.position = transform.TransformDirection(bumpVector);
        }
    }

    private void GetInput() {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.
        moveVector.x = player.GetAxis("MoveHoriz"); // get input by name or action id
        moveVector.y = player.GetAxis("MoveVert");
        bump = player.GetButton("Bump");
        rotateVal = player.GetAxis("Rotate");
    }

    private void ProcessRotation() {
        if(rotateVal != 0.0f) {
            Quaternion deltaRotation = Quaternion.Euler(rotationSpeed * rotateVal * Time.fixedDeltaTime);
            Quaternion multipliedRotation = playerRB.rotation * deltaRotation;
            playerRB.MoveRotation(multipliedRotation);
            // if (transform.eulerAngles.z <= 20f && transform.eulerAngles.z >= -20f) {
            //     playerRB.MoveRotation(multipliedRotation);  
            // } else if (transform.eulerAngles.z > 20f) {
            //     playerRB.MoveRotation(Quaternion.Euler(0, 0, 20f)); 
            // }
        }
    }
}
