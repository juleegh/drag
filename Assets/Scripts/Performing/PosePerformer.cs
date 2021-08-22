using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PosePerformer : CharacterPose, GlobalComponent
{
    private static PosePerformer instance;
    public static PosePerformer Instance { get { return instance; } }

    public void ConfigureRequiredComponent()
    {
        if (instance == null)
            instance = this;
    }

    public void SetTempo(float tempo)
    {
        bodyAnimator.speed = 1 / tempo;
    }
}
