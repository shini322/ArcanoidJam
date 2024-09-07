using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-25)]
public class GameController : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private int health;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject loseCanvas;
    [field:SerializeField] public List<Transform> GoslingPoints { get; private set; }
    [field:SerializeField] public Transform GoslingCenter { get; private set; }

    public event Action OnPlayerHealthChanged;

    public int CurrentHealth { get; private set; }
    public int Health => health;

    private void Awake()
    {
        winCanvas.gameObject.SetActive(false);
        loseCanvas.gameObject.SetActive(false);
        CurrentHealth = health;
    }

    private void OnEnable()
    {
        ball.OnDie += BallDie;
        LevelManager.Instance.OnAllLevelsCompleted += Win;
    }
    
    private void OnDisable()
    {
        ball.OnDie -= BallDie;
        LevelManager.Instance.OnAllLevelsCompleted -= Win;
    }

    private void BallDie()
    {
        CurrentHealth -= 1;
        OnPlayerHealthChanged?.Invoke();
        
        if (CurrentHealth <= 0)
        {
            Lose();
        }
    }

    private void Lose()
    {
        DisableObjects();
        loseCanvas.gameObject.SetActive(true);
        Debug.Log("Lose");
    }

    private void Win()
    {
        DisableObjects();
        winCanvas.gameObject.SetActive(true);
        Debug.Log("Win");
    }

    private void DisableObjects()
    {
        player.gameObject.SetActive(false);
        ball.gameObject.SetActive(false);
    }
    
}