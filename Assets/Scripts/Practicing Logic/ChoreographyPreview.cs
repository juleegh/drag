using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChoreographyPreview : MonoBehaviour, RequiredComponent
{
    [SerializeField] private int temposOnScreen;
    [SerializeField] private Transform temposContainer;
    [SerializeField] private GameObject tempoPreviewPrefab;
    [SerializeField] private GameObject UpIndicator;
    [SerializeField] private GameObject DownIndicator;
    private TempoMovesPreview[] tempoPreviews;


    private Song Song { get { return ProgressManager.Instance.CurrentLevel.BattleSong; } }
    private Choreography Choreography { get { return ChoreographyEditor.Instance.Choreography; } }
    public int TemposOnScreen { get { return temposOnScreen; } }

    public void ConfigureRequiredComponent()
    {
        PracticeEventsManager.Instance.AddActionToEvent(PracticeEvents.ChoreographyLoaded, LoadUI);
    }

    private void LoadUI()
    {
        tempoPreviews = new TempoMovesPreview[temposOnScreen];
        for (int i = 0; i < temposOnScreen; i++)
        {
            tempoPreviews[i] = Instantiate(tempoPreviewPrefab).GetComponent<TempoMovesPreview>();
            tempoPreviews[i].transform.SetParent(temposContainer);
        }
        RefreshView(0, 0);
    }

    public void RefreshView(int topTempo, int selectedTempo)
    {
        int previewIndex = 0;

        List<KeyValuePair<int, DanceMove[]>> tempos = Choreography.MovesPerTime.ToList();
        for (int i = topTempo; i < topTempo + temposOnScreen; i++)
        {
            KeyValuePair<int, DanceMove[]> tempo = tempos[i];
            tempoPreviews[previewIndex].SetBuffInfo(Song.SongBuffs[tempo.Key], tempo.Key);
            tempoPreviews[previewIndex].FillDanceMoves(tempo.Value);
            tempoPreviews[previewIndex].ToggleSelected(previewIndex == selectedTempo);
            previewIndex++;
        }
        Debug.LogError(selectedTempo);

        UpIndicator.SetActive(topTempo > 0);
        DownIndicator.SetActive(topTempo + temposOnScreen < Choreography.MovesPerTime.Count - 1);
    }
}
