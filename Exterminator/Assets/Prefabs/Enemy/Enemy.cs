using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] Animator animator;

    void Start()
    {
        if (healthComponent != null)
        {
            healthComponent.onHealthEmpty += StartDeath;
            healthComponent.onTakeDamage += TakenDamage;
        }
    }

    void TakenDamage(float health, float delta, float maxHealth)
    {
        
    }

    void StartDeath()
    {
        TriggerDeathAnimation();
    }

    void TriggerDeathAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Dead");
        }
    }

    public void OnDeathAnimationFinished()
    {
        Destroy(gameObject);
    }

}
