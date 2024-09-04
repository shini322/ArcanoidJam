using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private float speed;
    [SerializeField] private Transform ballStartTransform;

    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private bool ballWasLaunched = false;
    private Ball ball;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        ball = Instantiate(ballPrefab, transform, true);
        ball.transform.position = ballStartTransform.position;
        ball.Catch(transform);
    }

    private void Update()
    {
        rb.linearVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, 0);

        if (Input.GetKey(KeyCode.Space) && !ballWasLaunched)
        {
            ball.Launch(new Vector2(1, 4));
            ballWasLaunched = true;
        }
        
        // todo Для теста
        if (Input.GetKey(KeyCode.Q))
        {
            ResetBall();
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
}