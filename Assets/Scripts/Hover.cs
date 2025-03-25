using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    private Rigidbody2D rb;

    public float hoverHeight = 0.5f;  
    public float hoverSpeed = 2.5f;  
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        HoverMovement();
    }

    void HoverMovement()
    {
        float hoverVelocityY = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        Vector2 currentVelocity = rb.velocity;
        rb.velocity = new Vector2(currentVelocity.x, hoverVelocityY);
    }
}
