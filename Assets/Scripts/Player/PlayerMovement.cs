using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private float speed;
    [SerializeField] private Transform ballStartTransform;
    [SerializeField] private ContactFilter2D collisionsFilter;

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
        // rb.linearVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, 0);

        if (Input.GetKey(KeyCode.Space) && !ballWasLaunched)
        {
            ball.Launch(new Vector2(1, 4));
            ballWasLaunched = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
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
        var directionX = Input.GetAxisRaw("Horizontal");

        if (directionX == 0)
        {
            return;
        }

        Vector2 nextPosition = (Vector2)collider.transform.position + new Vector2(directionX, 0) * speed * Time.fixedDeltaTime;

        List<Collider2D> overlapColliders = new List<Collider2D>();

        if (collider.Overlap(nextPosition, 0f, collisionsFilter, overlapColliders) > 0)
        {
            var overlapCollider = overlapColliders[0];
            var closestPoint = overlapCollider.ClosestPoint(transform.position);
            nextPosition = new Vector2(closestPoint.x + (Vector2.Scale(collider.size, transform.lossyScale) * 0.5f * -directionX).x, nextPosition.y);
        }
        
        rb.MovePosition(nextPosition);
    }
}