using System;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-35)]
public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> levelsPrefabs;

    public event Action<int> OnLevelChanged;
    public event Action OnBlockDestroy;
    public event Action OnAllLevelsCompleted;

    public int LevelsCount => levels.Count;
    public int CurrentLevelBlocks { get; private set; }
    public Level CurrentLevel { get; private set; }
    
    private List<Level> levels = new ();
    private int currentLevelIndex = 0;
    
    private void Awake()
    {
        levels = new();
        currentLevelIndex = 0;
        CurrentLevel = null;
        CurrentLevelBlocks = 0;
        InitInstance();
        InitLevels();
        InitNextLevel();
    }
    
    public static LevelManager Instance;

    private void InitLevels()
    {
        foreach (Level levelPrefab in levelsPrefabs)
        {
            var level = Instantiate(levelPrefab, Vector3.zero, Quaternion.identity, transform);
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

        if (CurrentLevel != null)
        {
            CurrentLevel.gameObject.SetActive(false);
        }

        CurrentLevel = levels[currentLevelIndex];
        CurrentLevel.gameObject.SetActive(true);
        CurrentLevelBlocks = CurrentLevel.BlocksCount;
        currentLevelIndex += 1;
        OnLevelChanged?.Invoke(currentLevelIndex);
    }

    public void BlockDestroy()
    {
        CurrentLevelBlocks -= 1;
        OnBlockDestroy?.Invoke();

        if (CurrentLevelBlocks <= 0)
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