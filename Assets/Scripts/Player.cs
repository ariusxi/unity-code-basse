using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    public float speed;
    public float gravity;
    public float rotationSpeed;

    private float rotation;
    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();    
    }

    void Move() 
    {
        if (controller.isGrounded) {
            if (Input.GetKey(KeyCode.W)) {
                if (Input.GetKey(KeyCode.LeftShift)) {
                    moveDirection = Vector3.forward * (speed * 1.5f);
                    animator.SetInteger("transition", 2);
                } else {
                    moveDirection = Vector3.forward * speed;
                    animator.SetInteger("transition", 1);
                }
            }
            if (Input.GetKeyUp(KeyCode.W)) {
                moveDirection = Vector3.zero;
                animator.SetInteger("transition", 0);
            }
        }

        // Rotacionando o personagem para as teclas A e D
        rotation += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rotation, 0);

        moveDirection.y -= gravity * Time.deltaTime;
        moveDirection = transform.TransformDirection(moveDirection);

        controller.Move(moveDirection * Time.deltaTime);
    }
}
