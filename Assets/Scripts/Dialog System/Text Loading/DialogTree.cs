using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DialogTree
{
    private Dictionary<string, DialogNode> nodes;
    private Dictionary<GameEvent, DialogNode> triggers;
    private Dictionary<string, bool> saveStates;

    private DialogNode currentNode;
    private DialogNode currentSaveState;

    public DialogNode CurrentNode { get { return currentNode; } }

    public DialogTree()
    {
        nodes = new Dictionary<string, DialogNode>();
        triggers = new Dictionary<GameEvent, DialogNode>();
        saveStates = new Dictionary<string, bool>();
    }

    public void LoadNode(string identifier, DialogNode newNode)
    {
        nodes.Add(identifier, newNode);
    }

    public void AddSaveState(string characterName, string identifier)
    {
        bool completed = PlayerPrefs.GetInt(characterName + "-" + identifier, 0) != 0;
        saveStates.Add(identifier, completed);
    }

    public void AddTrigger(GameEvent gameEvent, DialogNode node)
    {
        GameEventsManager.Instance.AddActionToEvent(gameEvent, () => { currentNode = node; });
    }

    public void LoadPreviousSaveState()
    {
        List<KeyValuePair<string, bool>> states = saveStates.ToList();

        for (int i = 0; i < states.Count; i++)
        {
            if (states[i].Value)
            {
                if (i + 1 < states.Count && !states[i + 1].Value)
                {
                    currentNode = nodes[states[i].Key];
                    return;
                }
                else if (i + 1 == states.Count)
                {
                    currentNode = nodes[states[i].Key];
                    return;
                }
            }
            else if (i == 0)
            {
                currentNode = nodes[states[i].Key];
                return;
            }

        }
    }

    public DialogNode GetNext()
    {
        return currentNode.NextNode;
    }

    public DialogNode[] GetAnswers()
    {
        DialogQuestion question = currentNode as DialogQuestion;
        return question.Answers;
    }

    public string[] GetAnswersInText()
    {
        DialogQuestion question = currentNode as DialogQuestion;
        return question.AnswersInText;
    }

    public void AdvanceNode(DialogNode selected)
    {
        if (saveStates.ContainsKey(currentNode.DIdentifier))
        {
            saveStates[currentNode.DIdentifier] = true;
        }
        currentNode = selected;
    }

    public DialogNode GetNode(string identifier)
    {
        if (nodes.ContainsKey(identifier))
            return nodes[identifier];

        return null;
    }
}
