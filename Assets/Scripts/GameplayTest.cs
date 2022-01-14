using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayTest : MonoBehaviour, RequiredComponent
{
    private static GameplayTest instance;
    public static GameplayTest Instance { get { return instance; } }

    private List<string> moves;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        Debug.LogError("--------");

        moves = new List<string>();
        moves.Add("Hit 1 HP");
        moves.Add("Hit 2 HP");
        moves.Add("Eliminate next action");
        moves.Add("Eliminate buff");
        moves.Add("Reduce all damage");
        moves.Add("Reduce half damage");
        moves.Add("Eliminate effect");
        moves.Add("Protect current buff");
        moves.Add("Recover 1 life point");
        moves.Add("Recover 2 life points");
        moves.Add("Erase status ailment");
        moves.Add("Duplicate next action effect");
        moves.Add("Make 1500 points");
        moves.Add("Duplicate current score");
        moves.Add("Make 1000 points");
        moves.Add("Make 1200 points");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            DrawCard();
        }
    }

    void DrawCard()
    {
        if (moves.Count == 0)
            return;

        int index = Random.Range(0, moves.Count);
        string current = moves[index];
        moves.RemoveAt(index);
        Debug.LogError(current);
    }
}
