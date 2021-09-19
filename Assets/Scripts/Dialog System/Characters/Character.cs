using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, RequiredComponent
{
    [SerializeField] private CharacterInfo info;

    DialogTree dialogTree;
    public DialogTree Dialogs { get { return dialogTree; } }
    public string CharacterName { get { return info.NameIdentifier; } }

    public void ConfigureRequiredComponent()
    {
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.ClubLoaded, LoadCharacter);
    }

    private void LoadCharacter()
    {
        dialogTree = DialogLoader.LoadDialogs(info.DialogsFile);
        dialogTree.LoadFirst();

        //GetComponent<QueenRandomnizer>().MakeRandomIdleQueen();
    }
}
