using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10f;
    public float sprintModifier = 1.5f;
    public float gravity = -9;

    private Vector3 moveDirection = Vector3.zero;
    private float verticalSpeed = 0f;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Gravity();
        controller.Move(moveDirection);
    }

    private void Gravity()
    {
        verticalSpeed += gravity * Time.deltaTime;
        moveDirection.y = verticalSpeed;
    }

    public void Run()
    {
        moveDirection *= sprintModifier;
        moveDirection.y = verticalSpeed;
    }

    public void Move(Vector2 direction)
    {
        moveDirection = new Vector3(direction.x, verticalSpeed, direction.y);
        moveDirection *= speed * Time.deltaTime;
    }
}
