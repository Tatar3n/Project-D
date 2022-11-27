using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 moveDelta;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    public float speed;
    public float jumpForce;


    private void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        moveDelta = Vector3.zero;

        float x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(x * speed, rb.velocity.y);

        moveDelta = new Vector3(x,0,0);

        if(moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1,1,1);   

    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            rb.velocity = Vector2.up * jumpForce; 
        }
    }

}
