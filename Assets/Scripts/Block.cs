using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private int health;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Ball ball))
        {
            health -= 1;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}