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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out Ball ball))
        {
            float relativePosition = (ball.transform.position.x - transform.position.x) / collider.bounds.size.x;
            ball.ChangeVelocity(new Vector2(relativePosition, 1).normalized * other.rigidbody.linearVelocity.magnitude);
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
}