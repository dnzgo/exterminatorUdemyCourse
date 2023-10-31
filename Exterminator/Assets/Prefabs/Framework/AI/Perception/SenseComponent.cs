using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SenseComponent : MonoBehaviour
{
    [SerializeField] float forgettingTime = 3f;

    static List<PerceptionStimuli> registeredStimulis = new List<PerceptionStimuli>();
    List<PerceptionStimuli> percievableStimulis = new List<PerceptionStimuli>();

    Dictionary<PerceptionStimuli, Coroutine> forgettingRoutines = new Dictionary<PerceptionStimuli, Coroutine>();

    public delegate void OnPerceptionUpdated(PerceptionStimuli stimuli, bool successfulySensed);

    public event OnPerceptionUpdated onPerceptionUpdated;

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
                    if (forgettingRoutines.TryGetValue(stimuli, out Coroutine routine))
                    {
                        StopCoroutine(routine);
                        forgettingRoutines.Remove(stimuli);
                    }
                    else
                    {
                        onPerceptionUpdated?.Invoke(stimuli, true);
                        //Debug.Log($"I just sensed {stimuli.gameObject}");
                    }
                }
            }
            else
            {
                if (percievableStimulis.Contains(stimuli))
                {
                    percievableStimulis.Remove(stimuli);
                    forgettingRoutines.Add(stimuli, StartCoroutine(ForgetStimuli(stimuli)));
                }
            }
        }
    }

    IEnumerator ForgetStimuli(PerceptionStimuli stimuli)
    {
        yield return new WaitForSeconds(forgettingTime);
        forgettingRoutines.Remove(stimuli);
        onPerceptionUpdated?.Invoke(stimuli, false);
        //Debug.Log($"I lost track of {stimuli.gameObject}");
    }

    protected virtual void DrawDebug()
    {

    }

    private void OnDrawGizmos()
    {
        DrawDebug();
    }

}
