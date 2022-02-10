using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvements : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    private float horizontal;
    private float vertical;
    private Vector2 moveDirection;
    private float moveLimiter = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
            {
                // limit movement speed diagonally, so you move at 70% speed
                horizontal *= moveLimiter;
                vertical *= moveLimiter;
            }
        moveDirection = new Vector2(horizontal, vertical);
    }

    private void FixedUpdate() {
        
        rb.velocity = moveSpeed * moveDirection;
    }
}
