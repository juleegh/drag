using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Choreography
{
    private BossLevel bossLevel;

    private Dictionary<int, DanceMove[]> movesPerTime;

    public void AddMoveToTempo(int tempo, int position, DanceMove danceMove)
    {
        if (movesPerTime == null)
            movesPerTime = new Dictionary<int, DanceMove[]>();

        if (!movesPerTime.ContainsKey(tempo))
        {
            movesPerTime.Add(tempo, new DanceMove[4]);
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

        string savePath = Path.Combine(Application.persistentDataPath, "choreo_" + bossLevel.ToString());
        BinaryReader bReader = new BinaryReader(File.Open(savePath, FileMode.Open));
        GameDataReader reader = new GameDataReader(bReader);

        bReader.Close();
    }

    private void SaveMoves(GameDataWriter writer)
    {
        foreach (KeyValuePair<int, DanceMove[]> tempo in movesPerTime)
        {
            writer.Write(tempo.Key);
            for (int i = 0; i < 4; i++)
            {
                if (tempo.Value[i] == null)
                    writer.Write("Empty");
                else
                    writer.Write(tempo.Value[i].Identifier);
            }
        }
    }

    public void LoadMoves(GameDataReader reader)
    {
        int movesQuantity = ProgressManager.Instance.GameBosses[bossLevel].BattleSong.MovesQuantity;
        for (int i = 0; i < movesQuantity; i++)
        {
            int tempo = reader.ReadInt();
            for (int slot = 0; slot < 4; slot++)
            {
                string move = reader.ReadString();
                if (!move.Equals("Empty"))
                {
                    AddMoveToTempo(tempo, slot, DanceMovesManager.Instance.DanceMovesList[move]);
                }
            }
        }
    }
}
