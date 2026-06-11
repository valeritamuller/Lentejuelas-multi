using UnityEngine;

public class Ayuda : MonoBehaviour
{
    public float speed = 4f;
    public float runSpeed = 8f;
    public float jumpForce = 6f;
    public float gravity = -20f;

    private CharacterController controller;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

       Transform head = Camera.main.transform;

Vector3 forward = head.forward;
Vector3 right = head.right;

forward.y = 0;
right.y = 0;

forward.Normalize();
right.Normalize();

Vector3 move =
forward * z +
right * x;

        if (controller.isGrounded)
        {
            verticalVelocity = -1f;

            if (Input.GetKeyDown(KeyCode.Space))
                verticalVelocity = jumpForce;
        }

        verticalVelocity += gravity * Time.deltaTime;

        move.y = verticalVelocity;

        controller.Move(move * currentSpeed * Time.deltaTime);
    }
}