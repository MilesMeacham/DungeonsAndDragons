using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetection : MonoBehaviour {

    public delegate void MouseEvent();
    public delegate void KeyboardMoveEvent(float direction);

    public event MouseEvent OnMouseDownEvent;
    public event MouseEvent OnMouseOverEvent;

    void OnMouseDown()
    {
        if (OnMouseDownEvent != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnMouseDownEvent();
            }
        }
    }
}
