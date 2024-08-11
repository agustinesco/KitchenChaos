using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    public static Player Instance { get; set; }

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

    [SerializeField]
    private Transform playerHoldPoint;

    private KitchenObject currentKitchenObject;

    private BaseCounter selectedCounter;

    private bool isWalking;

    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;

    public class OnSelectedCounterChangeEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    public event EventHandler OnPickUp;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("More than one player");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
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

            if (moveDir.x != 0 && !movementCollidesXAxis)
            {
                result = moveDirX;
            }
            else
            {
                // Try to move on Z axis
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z);

                bool movementCollidesZAxis = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveSpeed * Time.deltaTime);

                if (moveDir.z != 0 && !movementCollidesZAxis)
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
            if (raycastHit.transform.TryGetComponent(out BaseCounter BaseCounter))
            {
                if (BaseCounter != selectedCounter)
                {

                    ChangeSelectedCounter(BaseCounter);
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

    private void ChangeSelectedCounter(BaseCounter BaseCounter)
    {
        selectedCounter = BaseCounter;
        OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeEventArgs { selectedCounter = BaseCounter });
    }

    public Transform GetTopPoint()
    {
        return playerHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.currentKitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnPickUp?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return currentKitchenObject;
    }

    public bool HasKitchenObject()
    {
        return currentKitchenObject != null;
    }
}
