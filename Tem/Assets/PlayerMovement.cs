using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private Vector3 moveDelta;

    private void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() {
        moveDelta = Vector3.zero;

        float x = Input.GetAxisRaw("Horizontal");

        Debug.Log(x);

        moveDelta = new Vector3(x,0,0);

        if(moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1,1,1);

        transform.Translate(moveDelta * Time.deltaTime);
    }

}
