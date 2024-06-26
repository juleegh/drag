using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Properties")]
public class LevelProperties : ScriptableObject
{
    [SerializeField] private BossLevel bossLevel;
    [SerializeField] private Song battleSong;
    [SerializeField] private OpponentChoreography choreography;
    [SerializeField] private LevelOutfitRules outfitRules;
    [SerializeField] private string bossName;
    [SerializeField] private string clubName;

    public BossLevel BossLevel { get { return bossLevel; } }
    public Song BattleSong { get { return battleSong; } }
    public string BossName { get { return bossName; } }
    public string ClubName { get { return clubName; } }
    public OpponentChoreography BossChoreo { get { return choreography; } }
    public LevelOutfitRules OutfitRules { get { return outfitRules; } }
}
