using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogAction
{
    protected Action action;

    public void ExecuteAction()
    {
        if (action != null)
            action();
    }
}
