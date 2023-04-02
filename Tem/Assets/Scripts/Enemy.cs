using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float jumpDistance = 1f;
    public float chaseDistance = 10f;
    public int damage = 10;
    public LayerMask playerLayer;
    public Transform Point;
    public Transform JumpPoint;

    private bool facingRight = false;
    private float checkTimer = 0f;
    public float checkInterval = 0.5f;

    private Rigidbody2D rb;
    private Transform player;

    // для прыжка
    private RaycastHit2D _checkBorderHeadRay;
    private RaycastHit2D _checkBorderBodyRay;
    private RaycastHit2D _checkGroundRay;
    public Transform CheckBorderHeadRayTransform;
    public Transform CheckBorderBodyRayTransform;
    public float RayDistance;
    public LayerMask BorderLayerMask;
    public LayerMask GroundLayerMask;
    private bool _isGround;
    private bool _isBorder;

    private int rot = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void StateUpdate()
    {
        _checkGroundRay = Physics2D.Raycast
            (
                transform.position,
                -Vector2.up,
                RayDistance,
                GroundLayerMask
            );
        _isGround = _checkGroundRay;

        _checkBorderHeadRay = Physics2D.Raycast
            (
                CheckBorderHeadRayTransform.position,
                -CheckBorderHeadRayTransform.right,
                RayDistance,
                BorderLayerMask
            );
        _checkBorderBodyRay = Physics2D.Raycast
            (
                CheckBorderBodyRayTransform.position,
                -CheckBorderBodyRayTransform.right,
                RayDistance,
                BorderLayerMask
            );
        _isBorder = _checkBorderHeadRay || _checkBorderBodyRay;

        Debug.DrawRay
            (
                transform.position,
                -Vector2.up * RayDistance,
                Color.red
            );
        Debug.DrawRay
           (
               CheckBorderHeadRayTransform.position,
                -CheckBorderHeadRayTransform.right * RayDistance,
               Color.red
           );
        Debug.DrawRay
           (
               CheckBorderBodyRayTransform.position,
                -CheckBorderBodyRayTransform.right * RayDistance,
               Color.red
           );
    }

    void FixedUpdate()
    {
        // движение NPC в направлении игрока
            if (!GameObject.Find("DemoHero").GetComponent<MeleeAttackController>().isAttacking)
                if (IsPlayerInRange())
                {
                    Vector2 direction = (player.position - transform.position).normalized;
                    rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(0f, rb.velocity.y);
                }
    }

    void Update()
    {
        StateUpdate();

        checkTimer += Time.deltaTime;

        if (checkTimer >= checkInterval)
        {
            checkTimer = 0f;



            if (Vector2.Distance(transform.position, player.position) < chaseDistance)
            {
                Vector2 direction = player.position - transform.position;
                

                if (_isBorder)
                    Jump();


                if (direction.x > 0 && !facingRight)
                {
                    Flip();
                }
                else if (direction.x < 0 && facingRight)
                {
                    Flip();
                }
            }
        }
    }

    void Jump()
    {
        if (_isGround)
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
        rot = -rot;
    }

    bool IsPlayerInRange()
    {
        // проверяем, видит ли NPC игрока
        Vector2 direction;
        if (!_isBorder)
            direction = (player.position - transform.position).normalized;
        else
            direction = new Vector3(0,0,0);
        RaycastHit2D hit = Physics2D.Raycast(Point.position, direction, chaseDistance, playerLayer);
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    public void OnTriggerWithAttackingHero()
    {
        Vector2 knockbackDirection = new Vector2(2 * rot, 2);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Debug.Log(knockbackDirection);
        GetComponent<Rigidbody2D>().AddForce(knockbackDirection, ForceMode2D.Impulse);
       
        Debug.Log("Вызвали реакцию противника");
    }
    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        // если NPC сталкивается с игроком, наносим ему урон
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
    */
}