using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class Choreography
{
    private BossLevel bossLevel;
    private Dictionary<int, DanceMove[]> movesPerTime;
    public Dictionary<int, DanceMove[]> MovesPerTime { get { return movesPerTime; } }

    public void AddMoveToTempo(int tempo, int position, DanceMove danceMove)
    {
        if (movesPerTime == null)
            movesPerTime = new Dictionary<int, DanceMove[]>();

        if (!movesPerTime.ContainsKey(tempo))
        {
            movesPerTime.Add(tempo, new DanceMove[PerformanceConversions.MoveTypesQuantity]);
        }

        movesPerTime[tempo][position] = danceMove;
    }

    public void SaveChoreo()
    {
        string savePath = Path.Combine(Application.persistentDataPath, "choreo_" + bossLevel.ToString());

        BinaryWriter bWriter = new BinaryWriter(File.Open(savePath, FileMode.Create));
        GameDataWriter writer = new GameDataWriter(bWriter);
        SaveMoves(writer);
        bWriter.Close();
    }

    public void LoadChoreo(BossLevel boss)
    {
        bossLevel = boss;

        try
        {
            string savePath = Path.Combine(Application.persistentDataPath, "choreo_" + bossLevel.ToString());
            BinaryReader bReader = new BinaryReader(File.Open(savePath, FileMode.Open));
            GameDataReader reader = new GameDataReader(bReader);
            LoadMoves(reader);
            bReader.Close();
        }
        catch (Exception)
        {
            LoadEmpty();
        }
    }

    private void SaveMoves(GameDataWriter writer)
    {
        foreach (KeyValuePair<int, MoveBuff> tempo in ProgressManager.Instance.CurrentLevel.BattleSong.SongBuffs)
        {
            writer.Write(tempo.Key);

            for (int i = 0; i < PerformanceConversions.MoveTypesQuantity; i++)
            {
                if (!movesPerTime.ContainsKey(tempo.Key) || movesPerTime[tempo.Key][i] == null)
                    writer.Write("Empty");
                else
                {
                    writer.Write(movesPerTime[tempo.Key][i].Identifier);
                }
            }
        }
    }

    private void LoadMoves(GameDataReader reader)
    {
        movesPerTime = new Dictionary<int, DanceMove[]>();
        int movesQuantity = ProgressManager.Instance.GameBosses[bossLevel].BattleSong.MovesQuantity;

        if (movesQuantity == 0)
        {
            ProgressManager.Instance.GameBosses[bossLevel].BattleSong.LoadPlayableTempos();
            movesQuantity = ProgressManager.Instance.GameBosses[bossLevel].BattleSong.MovesQuantity;
        }

        for (int i = 0; i < movesQuantity; i++)
        {
            int tempo = reader.ReadInt();

            for (int slot = 0; slot < PerformanceConversions.MoveTypesQuantity; slot++)
            {
                string move = reader.ReadString();
                if (move != null && !move.Equals("Empty") && !move.Equals("") && DanceMovesManager.Instance.DanceMovesList.ContainsKey(move))
                {
                    AddMoveToTempo(tempo, slot, DanceMovesManager.Instance.DanceMovesList[move]);
                }
                else
                    AddMoveToTempo(tempo, slot, null);
            }
        }
    }

    private void LoadEmpty()
    {
        movesPerTime = new Dictionary<int, DanceMove[]>();
        foreach (KeyValuePair<int, MoveBuff> moves in ProgressManager.Instance.CurrentLevel.BattleSong.SongBuffs)
        {
            for (int slot = 0; slot < PerformanceConversions.MoveTypesQuantity; slot++)
            {
                AddMoveToTempo(moves.Key, slot, null);
            }
        }
    }

    public int GetTotalStamina()
    {
        int total = 0;
        foreach (KeyValuePair<int, DanceMove[]> tempo in movesPerTime)
        {
            int inTempo = 0;
            for (int i = 0; i < PerformanceConversions.MoveTypesQuantity; i++)
            {
                if (tempo.Value[i] != null)
                    inTempo += tempo.Value[i].StaminaRequired;
            }
            total += inTempo;
        }
        return total;
    }

    public DanceMove GetMoveInTempoByType(int tempo, MoveType moveType)
    {
        if (!movesPerTime.ContainsKey(tempo))
            return null;

        return movesPerTime[tempo][PerformanceConversions.ConvertIndexFromMoveType(moveType)];
    }
}
