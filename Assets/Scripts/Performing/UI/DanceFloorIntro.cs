using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DanceFloorIntro : MonoBehaviour, RequiredComponent
{
    [SerializeField] private Image[] movesPreviews;
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject prompt;
    bool receivingInput;
    bool waitingToStart;
    bool finished;
    private Dictionary<MoveType, bool> previewed;
    private Vector3 indicatorSize;

    public void ConfigureRequiredComponent()
    {
        receivingInput = false;
        previewed = new Dictionary<MoveType, bool>();
        previewed[MoveType.AType] = false;
        previewed[MoveType.BType] = false;
        previewed[MoveType.XType] = false;
        previewed[MoveType.YType] = false;
        prompt.SetActive(false);
        container.SetActive(false);
        indicatorSize = movesPreviews[0].transform.localScale;
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.EnteredTheDanceFloor, StartPreview);
        finished = false;
    }

    private void StartPreview()
    {
        if (finished)
            return;

        UpdatePreviews();
        container.SetActive(true);
        receivingInput = true;
    }

    private void Update()
    {
        if (!receivingInput && !waitingToStart)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && waitingToStart)
        {
            finished = true;
            waitingToStart = false;
            receivingInput = false;
            container.SetActive(false);
            prompt.SetActive(false);
            PerformingEventsManager.Instance.Notify(PerformingEvent.PlayerReadyToPerform);
            return;
        }

        if (Input.GetKeyDown(MovesInputManager.Instance.A))
        {
            PlayerSelectedMove(MoveType.AType);
        }
        else if (Input.GetKeyDown(MovesInputManager.Instance.B))
        {
            PlayerSelectedMove(MoveType.BType);
        }
        else if (Input.GetKeyDown(MovesInputManager.Instance.X))
        {
            PlayerSelectedMove(MoveType.XType);
        }
        else if (Input.GetKeyDown(MovesInputManager.Instance.Y))
        {
            PlayerSelectedMove(MoveType.YType);
        }
    }

    private void PlayerSelectedMove(MoveType moveType)
    {
        movesPreviews[PerformanceConversions.ConvertIndexFromMoveType(moveType)].transform.localScale = indicatorSize * 1.1f;
        movesPreviews[PerformanceConversions.ConvertIndexFromMoveType(moveType)].transform.DOScale(indicatorSize, PerformSystem.Instance.Tempo).SetEase(Ease.Linear);

        previewed[moveType] = true;
        UpdatePreviews();
        if (waitingToStart)
            prompt.SetActive(true);
    }

    private void UpdatePreviews()
    {
        waitingToStart = true;
        foreach (KeyValuePair<MoveType, bool> preview in previewed)
        {
            float alpha = preview.Value ? 1f : 0.3f;
            if (!preview.Value)
                waitingToStart = false;
            Color color = movesPreviews[PerformanceConversions.ConvertIndexFromMoveType(preview.Key)].color;
            color.a = alpha;
            movesPreviews[PerformanceConversions.ConvertIndexFromMoveType(preview.Key)].color = color;
        }
    }
}