using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSense : SenseComponent
{
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] float hitMemory = 1.5f;

    Dictionary<PerceptionStimuli, Coroutine> hitRecord = new Dictionary<PerceptionStimuli, Coroutine>();

    protected override bool IsStimuliSensable(PerceptionStimuli stimuli)
    {
        return hitRecord.ContainsKey(stimuli);
    }

    private void Start()
    {
        healthComponent.onTakeDamage += TookDamage;
    }

    private void TookDamage(float health, float delta, float maxHealth, GameObject instigator)
    {
        PerceptionStimuli stimuli = instigator.GetComponent<PerceptionStimuli>();
        if (stimuli != null)
        {
            Coroutine newForgettingCoroutine = StartCoroutine(ForgetStimuli(stimuli));
            if (hitRecord.TryGetValue(stimuli, out Coroutine onGoingCoroutine))
            {
                StopCoroutine(onGoingCoroutine);
                hitRecord[stimuli] = newForgettingCoroutine;
            }
            else
            {
                hitRecord.Add(stimuli, newForgettingCoroutine);
            }
        }
    }

    IEnumerator ForgetStimuli (PerceptionStimuli stimuli)
    {
        yield return new WaitForSeconds(hitMemory);
        hitRecord.Remove(stimuli);
    }

}
