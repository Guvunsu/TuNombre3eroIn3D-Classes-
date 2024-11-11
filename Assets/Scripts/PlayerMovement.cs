using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] float speed;
    float gravity;
    [SerializeField] float jumpHeight;

    Vector3 velocity;
    bool isGrounded;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float rotationSpeed;

    private Transform currentPlatform;
    private Vector3 lastPlatformPosition;

    private void Start() {
        controller = GetComponent<CharacterController>();
        gravity = -9.81f;
        jumpHeight = 3f;
    }

    void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float m_MoveX = Input.GetAxis("Horizontal");
        float m_MoveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(m_MoveX, 0, m_MoveZ);

        if (move.magnitude > 0.1f) {
            controller.Move(move * speed * Time.deltaTime);
            Quaternion m_targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, m_targetRotation, Time.deltaTime * rotationSpeed);
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Movimiento de la plataforma
        if (currentPlatform != null) {
            Vector3 platformMovement = currentPlatform.position - lastPlatformPosition;
            //Vector3 rotationPos = Rotacion de la plataforma;
            //Player.rotation = transform.rotation + rotationPos;
            controller.Move(platformMovement); // Mueve el personaje con la plataforma
            lastPlatformPosition = currentPlatform.position;
        }
    }

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //}

    //private void OnCollisionEnter(Collision collision) {
    //    if (collision.gameObject.CompareTag("Platform")) {
    //        currentPlatform = collision.gameObject.transform;
    //        lastPlatformPosition = currentPlatform.position;
    //    }
    //}

    //private void OnCollisionExit(Collision collision) {
    //    if (collision.gameObject.CompareTag("Platform")) {
    //        currentPlatform = null;
    //    }
    //}
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Platform")) {
            currentPlatform = other.transform;
            lastPlatformPosition = currentPlatform.position;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Platform")) {
            currentPlatform = null;
        }
    }
}