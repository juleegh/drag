using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class DialogLoader
{
    private static int idColumn = 0;
    private static int textColumn = 1;
    private static int nextNodeColumn = 2;

    public static DialogTree LoadDialogs(TextAsset dialogCsv)
    {
        List<string[]> dialogs = FileDataReader.LoadGrid(dialogCsv);

        DialogTree tree = new DialogTree();

        foreach (string[] line in dialogs)
        {
            string identifier = line[idColumn].Trim();
            string nodeText = line[textColumn].Trim();
            string[] nextNodes = line[nextNodeColumn].Split(';');
            DialogNode newNode;

            if (nextNodes.Length > 1)
                newNode = new DialogQuestion();
            else
                newNode = new DialogText();

            newNode.SetText(nodeText);
            tree.LoadNode(identifier, newNode);
        }

        foreach (string[] line in dialogs)
        {

            if (line[nextNodeColumn].Trim() == "")
                continue;

            string identifier = line[idColumn].Trim();
            string[] nextNodes = line[nextNodeColumn].Split(';');

            if (nextNodes.Length > 1)
            {
                DialogQuestion question = tree.GetNode(identifier) as DialogQuestion;

                DialogNode[] answers = new DialogNode[nextNodes.Length];
                for (int i = 0; i < answers.Length; i++)
                {
                    answers[i] = tree.GetNode(nextNodes[i].Trim());
                }

                question.AddAnswers(answers);
            }
            else
            {
                DialogText text = tree.GetNode(identifier) as DialogText;
                text.AssignNextNode(tree.GetNode(line[nextNodeColumn].Trim()));
            }
        }

        return tree;
    }
}
