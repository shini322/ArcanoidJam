using TMPro;
using UnityEngine;

public class LevelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBlocksCount;
    
    private TextMeshProUGUI textLevel;
    private int levelsCount;

    private void Awake()
    {
        levelsCount = LevelManager.Instance.LevelsCount;
        textLevel = GetComponent<TextMeshProUGUI>();
        ChangeLevel(1);
    }

    private void OnEnable()
    {
        LevelManager.Instance.OnLevelChanged += ChangeLevel;
        LevelManager.Instance.OnBlockDestroy += ChangeBlocksCount;
    }
    
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelChanged -= ChangeLevel;
    }

    private void ChangeLevel(int levelNumber)
    {
        textLevel.text = $"Уровень {levelNumber}/{levelsCount}";
        ChangeBlocksCount();
    }

    private void ChangeBlocksCount()
    {
        textBlocksCount.text = $"Блоки {LevelManager.Instance.CurrentLevelBlocks}";
    }
}