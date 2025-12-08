//THIS CODE WAS CLEANED-UP WITH AI 



using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;              // Reference to the player's Rigidbody
    public float speed = 500f;        // Forward/backward movement speed
    public float rotSpeed = 10f;      // Rotation speed
    public float jumpForce = 2f;      // Impulse force applied when jumping
    public bool isGrounded = true;    // Whether the player is on the ground
    public Vector3 startPos = Vector3.zero; // Initial position of the player
    public Vector3 startRot = Vector3.zero; // Initial rotation of the player
    public Vector3 startJump;         // Direction vector for jump force

    void Start()
    {
        rb = GetComponent<Rigidbody>();                 // Get Rigidbody component
        startPos = transform.position;                 // Store starting position
        startRot = transform.localEulerAngles;         // Store starting rotation
        startJump = new Vector3(0f, 2f, 0f);          // Set upward jump vector
    }

    void FixedUpdate()
    {
        float translation, rotation;

        // Get input for forward/backward movement and scale by speed and deltaTime
        translation = Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime;

        // Get input for horizontal rotation and scale by rotation speed and deltaTime
        rotation = Input.GetAxis("Horizontal") * rotSpeed * Time.fixedDeltaTime;

        // Create a quaternion representing the rotation to apply
        Quaternion turn = Quaternion.Euler(0f, rotation, 0f);

        // Apply forward/backward movement relative to player's orientation
        rb.AddRelativeForce(Vector3.forward * translation);

        // Apply rotation to the Rigidbody
        rb.MoveRotation(rb.rotation * turn);
        
        // Handle jumping when Space is pressed and player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(startJump * jumpForce, ForceMode.Impulse); // Apply upward impulse
        }
    }
}

