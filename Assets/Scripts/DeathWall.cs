using UnityEngine;

public class DeathWall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Ball ball))
        {
            ball.gameObject.SetActive(false);
        }
    }
}