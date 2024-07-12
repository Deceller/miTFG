using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class CharacterController : MonoBehaviour
{
    private InputHandler _input;
    private Animator _animatorController;

    [SerializeField]
    private bool RotateTowardMouse;

    [SerializeField]
    private float MovementSpeed;
    [SerializeField]
    private float RotationSpeed;

    [SerializeField]
    private Camera Camera;


    public float fallThreshold = -1f;
    private GameManager gameManager;

    private void Awake()
    {
        _input = GetComponent<InputHandler>();
        _animatorController = GetComponent<Animator>();
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y < fallThreshold)
        {
            if (gameManager != null)
            {
                gameManager.OnPlayerFell();
            }
            Destroy(gameObject);
        }

        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
        var movementVector = MoveTowardTarget(targetVector);

        

        if(_input.InputVector.x == 0 & _input.InputVector.y == 0)
        {
            _animatorController.SetBool("Moving", false);
        }
        else
        {
            _animatorController.SetBool("Moving", true);

        }



        if (!RotateTowardMouse)
        {
            RotateTowardMovementVector(movementVector);
        }
        if (RotateTowardMouse)
        {
            RotateFromMouseVector();
        }

    }

    private void RotateFromMouseVector()
    {
        Ray ray = Camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = MovementSpeed * Time.deltaTime;

        if (_animatorController.GetBool("AttackingP") == true)
        {
            speed = 0;
        }

        else
        {
            speed = MovementSpeed * Time.deltaTime;
        }

        targetVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }

    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if (movementDirection.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotationSpeed);
    }
}