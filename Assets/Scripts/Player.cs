using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private float rotationSpeed = 10f;
    [SerializeField]
    private float playerRadius = .7f;
    [SerializeField]
    private float playerHeight = 2f;

    [SerializeField]
    private GameInput gameInput;

    private bool isWalking;


    // Update is called once per frame
    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

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

    public bool IsWalking()
    {
        return isWalking;
    }
}
