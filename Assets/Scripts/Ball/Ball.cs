using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private ContactFilter2D filter;

    public float Speed => speed;
    
    public event Action OnDie; 
    
    private Rigidbody2D rb;

    private void Awake()
    {
        InitRb();
    }

    private void Update()
    {
        rb.linearVelocity = rb.linearVelocity.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out PlayerMovement player))
        {
            float relativePosition = Mathf.Clamp(transform.position.x - player.transform.position.x, -1, 1);
            ChangeVelocity(new Vector2(relativePosition, 1));
            return;
        }
        Debug.Log("OnCollisionEnter2D ");
        Vector2 effectiveNormal = Vector2.zero; 
        foreach (ContactPoint2D contact in collision.contacts)
        {
            effectiveNormal += contact.normal;
            Debug.Log("contact " + contact.normal);
            // Debug.DrawRay(contact.point, contact.normal, Color.red);
        }

        effectiveNormal /= collision.contacts.Length;
        var velocity = Vector2.Reflect(rb.linearVelocity.normalized, effectiveNormal) * rb.linearVelocity.magnitude;
        Debug.DrawRay(transform.position, rb.linearVelocity.normalized, Color.blue);
        Debug.DrawRay(transform.position, velocity.normalized, Color.green);
        Debug.DrawRay(transform.position, effectiveNormal, Color.magenta);
        float angle = Vector2.SignedAngle(velocity, Vector2.right);
        bool isAngleMore90 = angle > 90;

        if (isAngleMore90)
        {
            angle = 180 - angle;
        }

        var minAngle = 15f;

        if (Mathf.Abs(angle) < minAngle)
        {
            var correctedAngle = Quaternion.Euler(0, 0, (isAngleMore90 ? 180 - minAngle : minAngle) * Mathf.Sign(angle));
            var correctedDir = correctedAngle * Vector2.left;
            velocity = velocity.magnitude * correctedDir;
        }
        
        Debug.DrawRay(transform.position, velocity.normalized, Color.yellow);
        Debug.Log("angle " + angle);
        
        ChangeVelocity(velocity);

        // ContactPoint2D contact = collision.GetContact(0);
        //
        // Vector2 direction = ((Vector2)transform.position - contact.point).normalized;
        // ChangeVelocity(Vector2.Reflect(direction, contact.normal));
    }

    // private void OnTriggerEnter2D(Collider2D collider)
    // {
    //     var direction = rb.linearVelocity.normalized;
    //     ContactPoint2D[] points = new ContactPoint2D[];
    //     collider.GetContacts(points)
    //     var hit = Physics2D.Raycast(collider.ClosestPoint(), direction);
    //     
    //     if (hit)
    //     {
    //         var velocity = Vector2.Reflect(direction, Vector2.right);
    //         ChangeVelocity(velocity);
    //     }
    // }

    public void ChangeVelocity(Vector2 velocity)
    {
        rb.linearVelocity = velocity;
    }

    public void Catch(Transform parent)
    {
        if (!rb)
        {
            InitRb();
        }

        rb.simulated = false;
        rb.transform.parent = parent;
        ChangeVelocity(Vector2.zero);
    }

    public void Launch(Vector2 direction)
    {
        if (!rb)
        {
            InitRb();
        }

        rb.simulated = true;
        rb.transform.parent = null;
        ChangeVelocity(direction.normalized);
    }

    public void Die()
    {
        OnDie?.Invoke();
    }

    private void InitRb()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}