using System;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        ChangePlayerHealth();
    }

    private void OnEnable()
    {
        gameController.OnPlayerHealthChanged += ChangePlayerHealth;
    }
    
    private void OnDisable()
    {
        gameController.OnPlayerHealthChanged -= ChangePlayerHealth;
    }

    private void ChangePlayerHealth()
    {
        text.text = $"Попытка {Math.Clamp(gameController.Health - gameController.CurrentHealth + 1, 1, gameController.Health)}/{gameController.Health}";
    }
}