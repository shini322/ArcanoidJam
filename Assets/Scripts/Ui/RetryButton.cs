using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(Retry);
    }
    
    private void OnDisable()
    {
        button.onClick.RemoveListener(Retry);
    }

    private void Retry()
    {
        SceneManager.LoadScene("GameScene");
    }
}