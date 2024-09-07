using TMPro;
using UnityEngine;

public class BlockHealth : MonoBehaviour
{
    [SerializeField] private Block block;

    private TextMeshProUGUI text;
    
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        ChangeHealth();
    }

    private void OnEnable()
    {
        block.OnHealthChanged += ChangeHealth;
    }
    
    private void OnDisable()
    {
        block.OnHealthChanged -= ChangeHealth;
    }

    private void ChangeHealth()
    {
        text.text = block.Health.ToString();
    }
}