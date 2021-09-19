using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DialogSystemController : MonoBehaviour, GlobalComponent
{
    private static DialogSystemController instance;
    public static DialogSystemController Instance { get { return instance; } }

    [SerializeField] private DialogVisuals visuals;

    private Character currentCharacter;
    public bool IsInteracting { get { return isInteracting; } }
    private bool isInteracting;
    private int selectedOption;
    private float nextDialogDelay;

    private List<DialogNode> currentDialogs;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        visuals.ToggleView(false);
    }

    public void StartInteraction(Character character)
    {
        currentCharacter = character;
        visuals.ToggleView(true);
        ConfigCurrent();
        isInteracting = true;
    }

    private void ConfigCurrent()
    {
        nextDialogDelay = 0.15f;
        selectedOption = 0;

        if (currentCharacter.Dialogs.CurrentNode != null)
        {
            currentDialogs = new List<DialogNode> { currentCharacter.Dialogs.CurrentNode.NextNode };
            if (currentCharacter.Dialogs.CurrentNode.IsQuestion)
                currentDialogs = currentCharacter.Dialogs.GetAnswers().ToList();
        }
        else
        {
            isInteracting = false;
            visuals.ToggleView(false);
            return;
        }

        ShowCurrent();
    }

    private void ShowCurrent()
    {
        if (currentCharacter.Dialogs.CurrentNode.IsQuestion)
            visuals.SetContent(currentCharacter.CharacterName, currentCharacter.Dialogs.CurrentNode.Text, currentCharacter.Dialogs.GetAnswersInText());
        else
            visuals.SetContent(currentCharacter.CharacterName, currentCharacter.Dialogs.CurrentNode.Text);

    }

    private void Update()
    {
        if (nextDialogDelay > 0)
        {
            nextDialogDelay -= Time.deltaTime;
            return;
        }

        if (!isInteracting)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow) && selectedOption > 0)
            selectedOption--;

        if (Input.GetKeyDown(KeyCode.DownArrow) && selectedOption < currentDialogs.Count - 1)
            selectedOption++;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceOption();
            ConfigCurrent();
        }
    }

    private void AdvanceOption()
    {
        if (currentCharacter.Dialogs.CurrentNode.IsQuestion)
            currentCharacter.Dialogs.AdvanceNode(currentDialogs[selectedOption].NextNode);
        else
            currentCharacter.Dialogs.AdvanceNode(currentDialogs[selectedOption]);
    }
}
