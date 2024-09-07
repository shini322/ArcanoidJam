using System;
using UnityEngine;

public class GoslingBlock : MonoBehaviour
{
    [SerializeField] private Gosling goslingPrefab;

    private GameController gameController;
    
    private void Awake()
    {
        gameController = FindAnyObjectByType<GameController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Ball ball))
        {
            Instantiate(goslingPrefab).StartMove(gameController.GoslingPoints, gameController.GoslingCenter);
            Destroy(gameObject);
        }
    }
}