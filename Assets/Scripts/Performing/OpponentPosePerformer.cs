using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpponentPosePerformer : CharacterPose, RequiredComponent
{
    private static OpponentPosePerformer instance;
    public static OpponentPosePerformer Instance { get { return instance; } }

    public void ConfigureRequiredComponent()
    {
        instance = this;
    }

    public void SetTempo(float tempo)
    {
        bodyAnimator.speed = 1 / tempo;
    }
}
