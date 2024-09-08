using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private float speed;
    [SerializeField] private Transform ballStartTransform;

    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private bool ballWasLaunched = false;
    private bool isLeftSide;
    private bool isRightSide;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        ball.transform.position = ballStartTransform.position;
        ball.Catch(transform);
    }

    private void OnEnable()
    {
        ball.OnDie += ResetBall;
        LevelManager.Instance.OnLevelChanged += LevelChanged;
    }

    private void OnDisable()
    {
        ball.OnDie -= ResetBall;
        LevelManager.Instance.OnLevelChanged -= LevelChanged;
    }

    private void Update()
    {
        rb.linearVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, 0);

        if (Input.GetKey(KeyCode.Space) && !ballWasLaunched)
        {
            ball.Launch(new Vector2(1, 4));
            ballWasLaunched = true;
        }
    }

    public void ResetBall()
    {
        ballWasLaunched = false;
        ball.gameObject.SetActive(true);
        ball.transform.position = ballStartTransform.position;
        ball.Catch(transform);
    }

    private void LevelChanged(int _)
    {
        ResetBall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Ball ball))
        {
            return;
        }

        var colliderPositionX = collision.gameObject.transform.position.x;

        if (colliderPositionX < transform.position.x)
        {
            isLeftSide = true;
            isRightSide = false;
        }
        else
        {
            isLeftSide = false;
            isRightSide = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isLeftSide = false;
        isRightSide = false;
    }

    private void Move()
    {
        var velocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        
        if (isLeftSide)
        {
            velocity.x = Mathf.Clamp(velocity.x, 0, 1);
        }
        else if(isRightSide)
        {
            velocity.x = Mathf.Clamp(velocity.x, -1, 0);
        }

        rb.linearVelocity = velocity * speed;
    }
}