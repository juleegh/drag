using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeInputManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PracticeEventsManager.Instance.Notify(PracticeEvents.NavigatedNextTempo);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PracticeEventsManager.Instance.Notify(PracticeEvents.NavigatedPreviousTempo);
        }
    }
}
