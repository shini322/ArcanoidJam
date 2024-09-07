using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private int health;

    public int Health => health;

    public event Action OnHealthChanged;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Ball ball))
        {
            health -= 1;
            OnHealthChanged?.Invoke();

            if (health <= 0)
            {
                Destroy(gameObject);
                LevelManager.Instance.BlockDestroy();
            }
        }
    }
}