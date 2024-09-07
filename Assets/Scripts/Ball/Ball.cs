using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float speed;

    public event Action OnDie; 
    
    private Rigidbody2D rb;

    private void Awake()
    {
        InitRb();
    }

    public void ChangeVelocity(Vector2 velocity)
    {
        rb.linearVelocity = velocity.normalized * speed;
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