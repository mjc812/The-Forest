using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private StatusPage statusPage;

    private Vector3 direction;

    private float positiveYSpeed = 3f;
    private float sprintPositiveYSpeed = 7f;
    private float negativeYSpeed = 3f;
    private float XSpeed = 2f;
    private float gravity = 20f;
    private float jumpForce = 6f;
    private float verticalForce = 0;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        statusPage = GameObject.FindWithTag("StatusPage").GetComponent<StatusPage>();
    }

    void Update()
    {
        if (!statusPage.isActive()) {
            Move();
        }
    }

    void Move()
    {
        float x = XForce();
        float y = YForce();
        float z = ZForce();

        direction = new Vector3(x, y, z);
        direction = transform.TransformDirection(direction);
        direction = direction * Time.deltaTime;
        characterController.Move(direction);
    }

    float XForce()
    {
        return Input.GetAxis("Horizontal") * XSpeed;
    }

    float YForce()
    {
        if (!characterController.isGrounded)
        {
            verticalForce = verticalForce - (gravity * Time.deltaTime);
        }
        else if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalForce = jumpForce;
        }
        return verticalForce;
    }
    float ZForce()
    {
        if (Input.GetAxis("Vertical") != 0) {
            float vertical = Input.GetAxis("Vertical");
            float zForce = 0f;
            if (Input.GetAxis("Vertical") < 0) {
                zForce = Input.GetAxis("Vertical") * negativeYSpeed;
            } else if (Input.GetKey(KeyCode.LeftShift)) {
                zForce = Input.GetAxis("Vertical") * sprintPositiveYSpeed;
            } else {
                zForce = Input.GetAxis("Vertical") * positiveYSpeed;
            }
            return zForce;
        } else {
            return 0f;
        }
    }

    public bool isPlayerSprinting() {
        return (Input.GetKey(KeyCode.LeftShift) && (Input.GetAxis("Vertical") > 0));
    }

    public bool isPlayerMoving() {
        return ((Input.GetAxis("Vertical") != 0) || (Input.GetAxis("Horizontal")  != 0));
    }
}
