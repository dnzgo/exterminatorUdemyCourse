using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SenseComponent : MonoBehaviour
{
    static List<PerceptionStimuli> registeredStimulis = new List<PerceptionStimuli>();
    List<PerceptionStimuli> percievableStimulis = new List<PerceptionStimuli>();

    static public void RegisterStimuli(PerceptionStimuli stimuli)
    {
        if (registeredStimulis.Contains(stimuli)) { return; }

        registeredStimulis.Add(stimuli);
    }

    static public void UnRegisterStimuli(PerceptionStimuli stimuli)
    {
        registeredStimulis.Remove(stimuli);
    }

    protected abstract bool IsStimuliSensable(PerceptionStimuli stimuli);

    void Update()
    {
        foreach (var stimuli in registeredStimulis)
        {
            if (IsStimuliSensable(stimuli))
            {
                if (!percievableStimulis.Contains(stimuli))
                {
                    percievableStimulis.Add(stimuli);
                    Debug.Log($"I just sensed {stimuli.gameObject}");
                }
            }
            else
            {
                if (percievableStimulis.Contains(stimuli))
                {
                    percievableStimulis.Remove(stimuli);
                    Debug.Log($"I lost track of {stimuli.gameObject}");
                }
            }
        }
    }

    protected virtual void DrawDebug()
    {

    }

    private void OnDrawGizmos()
    {
        DrawDebug();
    }

}
