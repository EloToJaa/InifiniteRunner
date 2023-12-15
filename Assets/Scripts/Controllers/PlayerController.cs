using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float liftingForce;
    public bool jumped;
    public bool doubleJumped;
    public LayerMask whatIsGround;
    private Rigidbody2D rb;
    private float timestamp;
    private BoxCollider2D boxCollider2D;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!GameManager.instance.inGame) return;

        if (IsGrounded() && Time.time >= timestamp)
        {
            if (jumped || doubleJumped)
            {
                jumped = false;
                doubleJumped = false;
            }
            timestamp = Time.time + 1f;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!jumped)
            {
                rb.velocity = (new Vector2(0f, jumpForce));
                jumped = true;
                SoundManager.instance.PlayOnceJump();
            }
            else if (!doubleJumped)
            {
                rb.velocity = (new Vector2(0f, jumpForce));
                doubleJumped = true;
                SoundManager.instance.PlayOnceJump();
            }
        }
        if (Input.GetMouseButton(0) && rb.velocity.y <= 0)
        {
            rb.AddForce(new Vector2(0f, liftingForce * Time.deltaTime));
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            origin: boxCollider2D.bounds.center,
            size: boxCollider2D.bounds.size,
            angle: 0f,
            direction: Vector2.down,
            distance: 0.1f,
            layerMask: whatIsGround);

        return hit.collider != null;
    }

    private void PlayerDeath()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        GameManager.instance.GameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle") && !GameManager.instance.immortality.isActive)
        {
            PlayerDeath();
        }
        else if(collision.CompareTag("Coin"))
        {
            GameManager.instance.CoinCollected();

            Destroy(collision.gameObject);
        }
        else if(collision.CompareTag("Immortality"))
        {
            GameManager.instance.ImmortalityCollected();

            Destroy(collision.gameObject);
        }
        else if(collision.CompareTag("Magnet"))
        {
            GameManager.instance.MagnetCollected();

            Destroy(collision.gameObject);  
        }
    }
}