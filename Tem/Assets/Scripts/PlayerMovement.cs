using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private RaycastHit2D _checkGroundRay1;
    private RaycastHit2D _checkGroundRay2;
    private RaycastHit2D _checkBorderHeadRay;
    private RaycastHit2D _checkBorderBodyRay;
    private Vector3 _leftFlip = new Vector3(0, 180, 0);
    private Vector2 _horizontalVelocity;
    private float _horizontalSpeed;
    private float _verticalSpeed;
    private float _signPreviosFrame;
    private float _signCurrentFrame;
    private bool _isGround1;
    private bool _isGround2;
    private bool _isBorder;
    private int rot = 1;


    public Transform CheckBorderHeadRayTransform;
    public Transform CheckBorderBodyRayTransform;
    public LayerMask BorderLayerMask;
    public LayerMask GroundLayerMask;
    public float MoveSpeed;
    public float JumpForce;
    public float RayDistance;

    public Animator anim;

    // для драки
    public float knockbackDuration = 0.5f; // продолжительность отбрасывания
    private float knockbackTimer = 0f; // таймер отбрасывания
    private bool isKnockback = false; // флаг отбрасывания
    public float attackNPCDuration = 2f;
    private float attackNPCTimer = 0f; 
    private bool isAttacked = false;
    private bool isBlocking;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isKnockback && !isBlocking)
            Move();
    }

    private void Update()
    {
        _horizontalSpeed = Input.GetAxis("Horizontal");
        _verticalSpeed = Input.GetAxis("Vertical");

        if (isKnockback)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0f)
            {
                isKnockback = false;
            }
        }

        isBlocking = GetComponent<MeleeAttackController>().isBlocking;
        if (GameObject.Find("AA") != null)
            if (isAttacked)
            {
                GameObject.Find("AA").GetComponent<Collider2D>().enabled = false;
                this.gameObject.layer = 11;
                attackNPCTimer -= Time.deltaTime;
                if (attackNPCTimer <= 0f)
                {
                    isAttacked = false;
                }
            }
            else
            {
                GameObject.Find("AA").GetComponent<Collider2D>().enabled = true;
                this.gameObject.layer = 9;
            }
        else
            this.gameObject.layer = 9;

       
        StateUpdate();
        Jump();
        Flip();
    }

    private void StateUpdate()
    {
        _checkGroundRay1 = Physics2D.Raycast
            (
                transform.position + new Vector3(-0.10f,0,0),
                -Vector2.up,
                RayDistance,
                GroundLayerMask
            );
        _isGround1 = _checkGroundRay1;

        _checkGroundRay2 = Physics2D.Raycast
            (
                transform.position + new Vector3(0.10f, 0, 0),
                -Vector2.up,
                RayDistance,
                GroundLayerMask
            );
        _isGround2 = _checkGroundRay2;

        _checkBorderHeadRay = Physics2D.Raycast
            (
                CheckBorderHeadRayTransform.position,
                CheckBorderHeadRayTransform.right,
                RayDistance,
                BorderLayerMask
            );
        _checkBorderBodyRay = Physics2D.Raycast
            (
                CheckBorderBodyRayTransform.position,
                CheckBorderBodyRayTransform.right,
                RayDistance,
                BorderLayerMask
            );
        _isBorder = _checkBorderHeadRay || _checkBorderBodyRay;

        Debug.DrawRay
            (
                transform.position + new Vector3(-0.10f, 0, 0),
                -Vector2.up * RayDistance,
                Color.red
            );
        Debug.DrawRay
            (
                transform.position + new Vector3(0.10f, 0, 0),
                -Vector2.up * RayDistance,
                Color.red
            );
        Debug.DrawRay
           (
               CheckBorderHeadRayTransform.position,
                CheckBorderHeadRayTransform.right * RayDistance,
               Color.red
           );
        Debug.DrawRay
           (
               CheckBorderBodyRayTransform.position,
                CheckBorderBodyRayTransform.right * RayDistance,
               Color.red
           );
    }

    private void Move()
    {
        if (_horizontalSpeed == -1f || _horizontalSpeed == 1f)
            anim.SetBool("stop", false);
        else
            anim.SetBool("stop", true);

        if (!_isBorder)
        {
            _horizontalVelocity.Set(_horizontalSpeed * MoveSpeed, _rigidbody.velocity.y);
            _rigidbody.velocity = _horizontalVelocity;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && (_isGround1 || _isGround2))
            _rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    private void Flip()
    {
        _signCurrentFrame = _horizontalSpeed == 0 ? _signPreviosFrame : Mathf.Sign(_horizontalSpeed);

        if (_signCurrentFrame != _signPreviosFrame)
        {
            transform.rotation = Quaternion.Euler(_horizontalSpeed < 0 ? _leftFlip : Vector3.zero);
            rot = -rot;
        }
        _signPreviosFrame = _signCurrentFrame;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !isKnockback && !isAttacked && !isBlocking)
        {
            Vector2 knockbackDirection = new Vector2(1.2f * rot, 2);
            _rigidbody.velocity = new Vector2(0, 0);
            Debug.Log(knockbackDirection);
            GetComponent<Rigidbody2D>().AddForce(knockbackDirection, ForceMode2D.Impulse);
            isKnockback = true;
            isAttacked = true;
            knockbackTimer = knockbackDuration;
            attackNPCTimer = attackNPCDuration;

            anim.Play("DamagedHeroAnim");

            Timer playerTimer = GameObject.Find("TimerCanvas").GetComponentInChildren<Timer>();
            playerTimer._timeLeft -= 5f; // убираем 5 секунд с таймера игрока

        }
        else if (other.CompareTag("Enemy") && isBlocking)
		{
            Vector2 knockbackDirection = new Vector2(1.2f * rot, 2);
            _rigidbody.velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().AddForce(knockbackDirection, ForceMode2D.Impulse);
            isKnockback = true;
            knockbackTimer = knockbackDuration;
            Timer playerTimer = GameObject.Find("TimerCanvas").GetComponentInChildren<Timer>();
            playerTimer._timeLeft -= 2f; // убираем 2 секунд с таймера игрока
        }
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
  
    }

}
