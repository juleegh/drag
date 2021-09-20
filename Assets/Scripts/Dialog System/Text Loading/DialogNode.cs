using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogNode
{
    protected DialogNode nextNode;
    public DialogNode NextNode { get { return nextNode; } }

    protected string text;
    public string Text { get { return text; } }

    public virtual bool IsQuestion { get { return false; } }

    protected DialogAction associatedAction;
    public DialogAction AssociatedAction { get { return associatedAction; } }

    public void SetAction(string actionType)
    {
        associatedAction = new DialogAction();
        associatedAction.SetAction(actionType);
    }

    public void SetText(string newText)
    {
        text = newText;
    }

    public void AssignNextNode(DialogNode nextNodeP)
    {
        nextNode = nextNodeP;
    }
}
