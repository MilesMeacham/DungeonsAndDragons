using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBasedMovement : MonoBehaviour
{
    [Header("Player Settings")]
    public float speed = 30;

    [Header("Technical Settings")]
    float unitsPerSpace = 5;
    float lerpSpeed = 2.0f;
    float deadzone = 0.1f;
    float movementDelay = 0.1f;
    Vector3 currentPosition;
    private int diagonalMoves = 0;
    
    private bool isRunning = false;
    private bool subscribed = false;

    void Start()
    {
        currentPosition = transform.position;
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(!subscribed)
            {
                SubscribeToController();
            }
            else
            {
                UnsubscribeToController();
            }
        }
    }

    void SubscribeToController()
    {
        KeyboardInputController.Instance.OnMoveVerticalEvent += MoveVertical;
        KeyboardInputController.Instance.OnMoveHorizontalEvent += MoveHorizontal;
        KeyboardInputController.Instance.OnMoveDiagonalUpEvent += MoveDiagonalUp;
        KeyboardInputController.Instance.OnMoveDiagonalDownEvent += MoveDiagonalDown;
        subscribed = true;
    }

    void UnsubscribeToController()
    {
        KeyboardInputController.Instance.OnMoveVerticalEvent -= MoveVertical;
        KeyboardInputController.Instance.OnMoveHorizontalEvent -= MoveHorizontal;
        KeyboardInputController.Instance.OnMoveDiagonalUpEvent -= MoveDiagonalUp;
        KeyboardInputController.Instance.OnMoveDiagonalDownEvent -= MoveDiagonalDown;
        subscribed = false;
    }

    void AdjustMovesLeft(bool diagonal)
    {
        if(diagonal)
        {
            diagonalMoves++;
            if (diagonalMoves % 2 == 0)
            {
                speed -= (unitsPerSpace * 2);
            }
            else
            {
                speed -= unitsPerSpace;
            }
        }
        else
        {
            speed -= unitsPerSpace;
        }
    }

    bool CanMove(bool diagonal)
    {
        bool canMove = true;

        if(diagonal && diagonalMoves % 2 != 0 && speed <= unitsPerSpace)
        {
            canMove = false;
        }

        if(speed <= 0)
        {
            canMove = false;
        }

        return canMove;
    }

    void MoveVertical(float direction)
    {
        if (isRunning || !CanMove(false))
        {
            return;
        }
        isRunning = true;

        if (direction > 0)
        {
            currentPosition += Vector3.up;
        }
        if (direction < 0)
        {
            currentPosition += Vector3.down;
        }

        AdjustMovesLeft(false);
        StartCoroutine(Movement(currentPosition));
    }

    void MoveHorizontal(float direction)
    {
        if (isRunning || !CanMove(false))
        {
            return;
        }
        isRunning = true;

        if (direction > 0)
        {
            currentPosition += Vector3.right;
        }
        if (direction < 0)
        {
            currentPosition += Vector3.left;
        }

        AdjustMovesLeft(false);
        StartCoroutine(Movement(currentPosition));         
    }

    void MoveDiagonalUp(float direction)
    {
        if (isRunning || !CanMove(true))
        {
            return;
        }
        isRunning = true;

        if (direction > 0)
        {
            currentPosition += Vector3.right;
        }
        if (direction < 0)
        {
            currentPosition += Vector3.left;
        }

        AdjustMovesLeft(true);
        currentPosition += Vector3.up;
        StartCoroutine(Movement(currentPosition));
    }

    void MoveDiagonalDown(float direction)
    {
        if (isRunning || !CanMove(true))
        {
            return;
        }
        isRunning = true;

        if (direction > 0)
        {
            currentPosition += Vector3.right;
        }
        if (direction < 0)
        {
            currentPosition += Vector3.left;
        }

        AdjustMovesLeft(true);
        currentPosition += Vector3.down;
        StartCoroutine(Movement(currentPosition));
    }

    IEnumerator Movement(Vector3 targetPosition)
    {
        while(transform.position != targetPosition)
        {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * lerpSpeed);

        yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(movementDelay);

        isRunning = false;
    }
}
