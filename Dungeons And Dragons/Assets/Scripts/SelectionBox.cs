using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBox : MonoBehaviour {

    private bool subscribed = false;
    private MouseDetection mouseDetection;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        mouseDetection = this.GetComponentInParent<MouseDetection>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        SubscribeToMouseEvents();
    }

    void OnDisable()
    {
        UnsubscribeToMouseEvents();
    }

    void SubscribeToMouseEvents()
    {
        mouseDetection.OnMouseDownEvent += this.MouseDown;
        subscribed = true;
    }

    void UnsubscribeToMouseEvents()
    {
        mouseDetection.OnMouseDownEvent -= this.MouseDown;
        subscribed = false;
    }

    void MouseDown()
    {
        if(spriteRenderer.enabled == true)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }
}
