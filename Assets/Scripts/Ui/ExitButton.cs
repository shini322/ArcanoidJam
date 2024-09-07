using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(Quit);
    }
    
    private void OnDisable()
    {
        button.onClick.RemoveListener(Quit);
    }

    private void Quit()
    {
        Application.Quit();
    }
}