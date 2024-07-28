using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Intance { get; set; }

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private float rotationSpeed = 10f;
    [SerializeField]
    private float playerRadius = .7f;
    [SerializeField]
    private float playerHeight = 2f;
    [SerializeField]
    private float interactDistance = 2f;
    [SerializeField]
    private LayerMask counterLayerMask;

    private Vector3 lookingDirection = Vector3.zero;

    [SerializeField]
    private GameInput gameInput;

    private EmptyCounter selectedCounter;

    private bool isWalking;

    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;

    public class OnSelectedCounterChangeEventArgs : EventArgs
    {
        public EmptyCounter selectedCounter;
    }


    private void Awake()
    {
        if (Intance != null)
        {
            Debug.Log("More than one player");
        }
        Intance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        MovePlayer();
        HandleInteractions();
    }

    private void MovePlayer()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lookingDirection = moveDir;

        }

        moveDir = MovementAfterCollisions(moveDir);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);



        isWalking = moveDir != Vector3.zero;
    }

    private Vector3 MovementAfterCollisions(Vector3 moveDir)
    {
        bool movementCollides = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveSpeed * Time.deltaTime);
        Vector3 result = moveDir;

        if (movementCollides)
        {
            // Try to move on X axis
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f);

            bool movementCollidesXAxis = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveSpeed * Time.deltaTime);

            if (!movementCollidesXAxis)
            {
                result = moveDirX;
            }
            else
            {
                // Try to move on Z axis
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z);

                bool movementCollidesZAxis = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveSpeed * Time.deltaTime);

                if (!movementCollidesZAxis)
                {
                    result = moveDirZ;
                }
                else
                {
                    result = Vector3.zero;
                }
            }
        }

        result.Normalize();

        return result;
    }

    private void HandleInteractions()
    {

        if (Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, lookingDirection, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out EmptyCounter emptyCounter))
            {
                if (emptyCounter != selectedCounter)
                {

                    ChangeSelectedCounter(emptyCounter);
                }
            }
            else
            {
                ChangeSelectedCounter(null);
            }
        }
        else if (selectedCounter != null)
        {
            ChangeSelectedCounter(null);
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void ChangeSelectedCounter(EmptyCounter emptyCounter)
    {
        selectedCounter = emptyCounter;
        OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeEventArgs { selectedCounter = emptyCounter });
    }
}
