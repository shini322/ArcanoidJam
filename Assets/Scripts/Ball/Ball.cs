using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ChangeVelocity(Vector2 velocity)
    {
        rb.linearVelocity = velocity.normalized * speed;
    }

    public void Catch(Transform parent)
    {
        rb.simulated = false;
        rb.transform.parent = parent;
        ChangeVelocity(Vector2.zero);
    }

    public void Launch(Vector2 direction)
    {
        rb.simulated = true;
        rb.transform.parent = null;
        ChangeVelocity(direction.normalized);
    }
}