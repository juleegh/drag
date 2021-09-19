using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogQuestion : DialogNode
{
    private DialogNode[] answers;
    private string[] answersInText;
    public DialogNode[] Answers { get { return answers; } }
    public string[] AnswersInText { get { return answersInText; } }

    public override bool IsQuestion { get { return true; } }

    public void AddAnswers(DialogNode[] answersP)
    {
        answers = answersP;

        answersInText = new string[answersP.Length];
        for (int i = 0; i < answersInText.Length; i++)
        {
            answersInText[i] = answers[i].Text;
        }
    }
}
