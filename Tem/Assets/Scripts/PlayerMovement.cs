using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 moveDelta;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;

    public float speed;
    public float jumpForce;

    private bool isGround = false;

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
            
    void OnCollisionEnter2D(Collision2D col) {

        if (col.gameObject.tag == ("Wall"))
            isGround = true;
        else if (col.gameObject.tag == ("Ground")) 
            isGround = true;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space) && isGround) {
            rb.velocity = Vector2.up * jumpForce; 
            isGround = false;
        }
    }

}
