using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

// All Rewired code taken from the Rewired documentation

// [RequireComponent(typeof(CharacterController))]
public class BoxMovement : MonoBehaviour {

    // The Rewired player id of this character
    [SerializeField] int playerId = 0;
    
    // The movement speed of this character
    [SerializeField] float moveSpeed = 3.0f;
    [SerializeField] float rotationSpeed = 3.0f;

    Player player; // The Rewired Player
    Rigidbody playerRB;
    // CharacterController cc;
    Vector3 moveVector;
    float rotateVal;


    void Awake() {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
        

        // Get the character controller
        // cc = GetComponent<CharacterController>();
    }

    void Update () {
        GetInput();
        ProcessInput();
    }

    void FixedUpdate() {
        if(moveVector.x != 0.0f || moveVector.y != 0.0f) {
            transform.Translate(moveVector * moveSpeed * Time.deltaTime);
            // cc.Move(moveVector * moveSpeed * Time.deltaTime);
        }
    }

    private void GetInput() {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.
        moveVector.x = player.GetAxis("MoveHoriz"); // get input by name or action id
        moveVector.y = player.GetAxis("MoveVert");

        rotateVal = player.GetAxis("Rotate");
    }

    private void ProcessInput() {
        // Process movement
        // if(moveVector.x != 0.0f || moveVector.y != 0.0f) {
        //     transform.Translate(moveVector * moveSpeed * Time.deltaTime);
        //     // cc.Move(moveVector * moveSpeed * Time.deltaTime);
        // }

        if(rotateVal != 0.0f) {
            transform.Rotate(Vector3.forward * rotationSpeed * rotateVal * Time.deltaTime);
        }
    }
}
