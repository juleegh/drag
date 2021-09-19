using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DialogTree
{
    private Dictionary<string, DialogNode> nodes;
    private DialogNode currentNode;

    public DialogNode CurrentNode { get { return currentNode; } }

    public void LoadNode(string identifier, DialogNode newNode)
    {
        if (nodes == null)
            nodes = new Dictionary<string, DialogNode>();

        nodes.Add(identifier, newNode);
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
        currentNode = selected;
    }

    public DialogNode GetNode(string identifier)
    {
        if (nodes.ContainsKey(identifier))
            return nodes[identifier];

        return null;
    }

    public void LoadFirst()
    {
        currentNode = nodes.ToList()[0].Value;
    }
}
