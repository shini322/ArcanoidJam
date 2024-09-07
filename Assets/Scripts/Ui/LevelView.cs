using TMPro;
using UnityEngine;

public class LevelView : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int levelsCount;

    private void Awake()
    {
        levelsCount = LevelManager.Instance.LevelsCount;
        text = GetComponent<TextMeshProUGUI>();
        ChangeLevel(1);
    }

    private void OnEnable()
    {
        LevelManager.Instance.OnLevelChanged += ChangeLevel;
    }
    
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelChanged -= ChangeLevel;
    }

    private void ChangeLevel(int levelNumber)
    {
        text.text = $"Уровень {levelNumber}/{levelsCount}";
    }
}