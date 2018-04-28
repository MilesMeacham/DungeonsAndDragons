using UnityEngine;
using System.Collections;

public class KeyboardInputController : MonoBehaviour
{
    float deadzone = 0.1f;

    // Singleton Pattern
    private static KeyboardInputController instance;

    public static KeyboardInputController Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType(typeof(KeyboardInputController)) as KeyboardInputController;
                if (!instance)
                    Debug.LogError("No active KeyboardInputController script on any GameObject.");
            }
            return instance;
        }
    }

    public delegate void KeyboardEvent();
    public delegate void KeyboardMoveEvent(float direction);

    public event KeyboardMoveEvent OnMoveVerticalEvent;
    public event KeyboardMoveEvent OnMoveHorizontalEvent;
    public event KeyboardMoveEvent OnMoveDiagonalUpEvent;
    public event KeyboardMoveEvent OnMoveDiagonalDownEvent;

    void FixedUpdate()
    {
        if (OnMoveVerticalEvent != null)
        {
            if(Mathf.Abs(Input.GetAxis("Vertical")) > deadzone)
            {
                OnMoveVerticalEvent(Input.GetAxis("Vertical"));
            }
        }

        if (OnMoveHorizontalEvent != null)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > deadzone)
            {
                OnMoveHorizontalEvent(Input.GetAxis("Horizontal"));
            }
        }

        if (OnMoveHorizontalEvent != null)
        {
            if (Mathf.Abs(Input.GetAxis("DiagonalUp")) > deadzone)
            {
                OnMoveDiagonalUpEvent(Input.GetAxis("DiagonalUp"));
            }
        }

        if (OnMoveHorizontalEvent != null)
        {
            if (Mathf.Abs(Input.GetAxis("DiagonalDown")) > deadzone)
            {
                OnMoveDiagonalDownEvent(Input.GetAxis("DiagonalDown"));
            }
        }
    }
}