using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogText : DialogNode
{
    protected DialogAction associatedAction;
    public DialogAction AssociatedAction { get { return associatedAction; } }
}
