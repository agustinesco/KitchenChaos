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
    private GameInput gameInput;

    private bool isWalking;


    // Update is called once per frame
    private void Update()
    {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);

        isWalking = inputVector != Vector2.zero;
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
