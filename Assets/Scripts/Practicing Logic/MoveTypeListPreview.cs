using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoveTypeListPreview : MonoBehaviour, RequiredComponent
{
    [SerializeField] private int movesOnScreen;
    [SerializeField] private GameObject previewContainer;
    [SerializeField] private Transform movesContainer;
    [SerializeField] private GameObject moveTypePreviewPrefab;
    [SerializeField] private GameObject UpIndicator;
    [SerializeField] private GameObject DownIndicator;
    private MoveTypePreview[] movesTypesPreviews;

    public int MovesOnScreen { get { return movesOnScreen; } }

    public void ConfigureRequiredComponent()
    {
        PracticeEventsManager.Instance.AddActionToEvent(PracticeEvents.ChoreographyLoaded, LoadUI);
    }

    private void LoadUI()
    {
        movesTypesPreviews = new MoveTypePreview[movesOnScreen];
        for (int i = 0; i < movesOnScreen; i++)
        {
            movesTypesPreviews[i] = Instantiate(moveTypePreviewPrefab).GetComponent<MoveTypePreview>();
            movesTypesPreviews[i].transform.SetParent(movesContainer);
        }
        RefreshView(0, 0);
        previewContainer.SetActive(false);
    }

    public void RefreshView(int topMove, int selectedMove)
    {
        int previewIndex = 0;

        List<KeyValuePair<string, DanceMove>> movesAvailable = DanceMovesManager.Instance.DanceMovesList.ToList();
        for (int i = topMove; i < topMove + movesOnScreen; i++)
        {
            if (i >= movesAvailable.Count)
            {
                movesTypesPreviews[previewIndex].SetEmpty();
                previewIndex++;
                continue;
            }

            KeyValuePair<string, DanceMove> danceMove = movesAvailable[i];
            movesTypesPreviews[previewIndex].UpdateMoveName(danceMove.Key);
            movesTypesPreviews[previewIndex].ToggleSelected(previewIndex == selectedMove);
            previewIndex++;
        }

        UpIndicator.SetActive(topMove > 0);
        DownIndicator.SetActive(topMove + movesOnScreen < movesAvailable.Count - 1);
    }

    public void ShowList(bool visible)
    {
        previewContainer.SetActive(visible);
    }
}
