using System;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-35)]
public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> levelsPrefabs;

    public event Action<int> OnLevelChanged;
    public event Action OnAllLevelsCompleted;

    public int LevelsCount => levels.Count;
    
    private List<Level> levels = new ();
    private int currentLevelIndex = 0;
    private Level currentLevel;
    private int currentLevelBlocks;
    
    private void Awake()
    {
        levels = new();
        currentLevelIndex = 0;
        currentLevel = null;
        currentLevelBlocks = 0;
        InitInstance();
        InitLevels();
        InitNextLevel();
    }
    
    public static LevelManager Instance;

    private void InitLevels()
    {
        foreach (Level levelPrefab in levelsPrefabs)
        {
            var level = Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);
            level.gameObject.SetActive(false);
            levels.Add(level);
        }
    }

    private void InitNextLevel()
    {
        if (currentLevelIndex >= levels.Count)
        {
            OnAllLevelsCompleted?.Invoke();
            return;
        }

        if (currentLevel != null)
        {
            currentLevel.gameObject.SetActive(false);
        }

        currentLevel = levels[currentLevelIndex];
        currentLevel.gameObject.SetActive(true);
        currentLevelBlocks = currentLevel.BlocksCount;
        currentLevelIndex += 1;
        OnLevelChanged?.Invoke(currentLevelIndex);
    }

    public void BlockDestroy()
    {
        currentLevelBlocks -= 1;

        if (currentLevelBlocks <= 0)
        {
            InitNextLevel();
        }
    }

    private void InitInstance()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}