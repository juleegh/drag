using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class DialogLoader
{
    private static int idColumn = 0;
    private static int textColumn = 1;
    private static int nextNodeColumn = 2;
    private static int saveStateColumn = 3;
    private static int triggerColumn = 4;
    private static int callbackColumn = 5;

    public static DialogTree LoadDialogs(string characterName, Character belongingCharacter, TextAsset dialogCsv)
    {
        List<string[]> dialogs = FileDataReader.LoadGrid(dialogCsv);

        DialogTree tree = new DialogTree();

        foreach (string[] line in dialogs)
        {
            string identifier = line[idColumn].Trim();
            string nodeText = line[textColumn].Trim();
            string isSaveState = line[saveStateColumn].Trim();
            string trigger = line[triggerColumn].Trim();
            string callback = line[callbackColumn].Trim();
            string[] nextNodes = line[nextNodeColumn].Split(';');
            DialogNode newNode = new DialogNode(identifier, belongingCharacter);


            if (nextNodes.Length > 1)
                newNode = new DialogQuestion(identifier, belongingCharacter);

            if (nodeText.Contains("<"))
            {
                nodeText = nodeText.Replace("<", "");
                nodeText = nodeText.Replace(">", "");
                newNode.SetAction(nodeText);

                if (callback != "")
                {
                    callback = callback.Replace("<", "");
                    callback = callback.Replace(">", "");
                    newNode.SetCallback(callback);
                }
            }
            else
            {
                newNode.SetText(nodeText);
            }

            tree.LoadNode(identifier, newNode);

            if (isSaveState == "y")
                tree.AddSaveState(characterName, identifier);

            if (trigger != "")
            {
                trigger = trigger.Replace("<", "");
                trigger = trigger.Replace(">", "");
                tree.AddTrigger((GameEvent)System.Enum.Parse(typeof(GameEvent), trigger), newNode);
            }
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
                tree.GetNode(identifier).AssignNextNode(tree.GetNode(line[nextNodeColumn].Trim()));
            }
        }

        return tree;
    }
}
