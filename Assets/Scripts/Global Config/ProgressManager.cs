using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour, GlobalComponent
{
    private static ProgressManager instance;
    public static ProgressManager Instance { get { return instance; } }

    private float currentHour;
    public float CurrentHour { get { return currentHour; } }

    private int experiencePoints;
    public int ExperiencePoints { get { return experiencePoints; } }

    private int stamina;
    public int Stamina { get { return stamina; } }

    [SerializeField] private List<LevelProperties> gameLevels;
    private Dictionary<BossLevel, LevelProperties> gameBosses;
    private BossLevel currentLevel;

    public LevelProperties CurrentLevel { get { return gameBosses[currentLevel]; } }
    public BossLevel BossLevel { get { return currentLevel; } }
    public Dictionary<BossLevel, LevelProperties> GameBosses { get { return gameBosses; } }

    public void ConfigureRequiredComponent()
    {
        if (instance == null)
        {
            instance = this;
            LoadGameLevels();
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        experiencePoints = PlayerPrefs.GetInt("experience", 0);
        stamina = 100;
    }

    void Start()
    {
        GameEventsManager.Instance.Notify(GameEvent.ExperienceGained);
    }

    public void GainXP(int newXP)
    {
        experiencePoints += newXP;
        PlayerPrefs.SetInt("experience", experiencePoints);
        GameEventsManager.Instance.Notify(GameEvent.ExperienceGained);
    }

    private void LoadGameLevels()
    {
        gameBosses = new Dictionary<BossLevel, LevelProperties>();
        foreach (LevelProperties level in gameLevels)
        {
            gameBosses.Add(level.BossLevel, level);
        }
        int playerLevel = PlayerPrefs.GetInt("progress_level", 0);
        currentLevel = (BossLevel)playerLevel;
    }
}
