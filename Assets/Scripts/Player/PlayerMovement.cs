using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float sprintMultiplier = 1f;
    public float rotationSpeed = 5f;
    public float gravityValue = -9.81f;
    public float playerMass = 1f;

    private CharacterController Controller = null;
    private float originalSpeed = 5f;
    private Vector3 playerVelocity;
    private Vector2 movedir = new Vector2();
    private Vector2 rotdir = new Vector2();
    private Transform camTransform;

    public UnityEvent PlayerMoving;
    public UnityEvent PlayerStopped;

    public void Awake()
    {
        originalSpeed = speed;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Controller = GetComponent<CharacterController>();
        camTransform = Camera.main.transform;
    }

    public void Update()
    {
        if (playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        ConsumeMovement();
        playerVelocity.y += (gravityValue * playerMass) * Time.deltaTime;
        Controller.Move(playerVelocity * Time.deltaTime);
    }

    public void GetDirection(InputAction.CallbackContext context)
    {
        movedir = context.ReadValue<Vector2>();
    }

    public void GetRotation(InputAction.CallbackContext context)
    {
        rotdir = context.ReadValue<Vector2>();
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame())
        {
            speed = originalSpeed * sprintMultiplier;
        }

        if (context.action.WasReleasedThisFrame())
        {
            speed = originalSpeed;
        }
    }

    public void MovementCheck(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame())
        {
            PlayerMoving?.Invoke();
        }

        if (context.action.WasReleasedThisFrame())
        {
            PlayerStopped?.Invoke();
        }
    }

    private void ConsumeMovement()
    {
        // do operation to figure out movement delta( pos - mouseDelta ) + speed or something like that
        Vector3 movementDelta = (camTransform.forward.normalized * movedir.y) + (camTransform.right.normalized * movedir.x);
        movementDelta.y = 0f;
        Controller.Move((movementDelta * Time.deltaTime) * speed);
    }

    private void ConsumeRotation()
    {
        Vector3 rotationDelta = camTransform.up * rotdir * rotationSpeed;
        Vector3 rotation = new Vector3(0, rotationDelta.x, 0);
        this.gameObject.transform.Rotate(rotation);
    }
}
