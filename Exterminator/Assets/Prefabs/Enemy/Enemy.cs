using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] Animator animator;
    [SerializeField] PerceptionComponent perceptionComponent;

    GameObject target;

    void Start()
    {
        if (healthComponent != null)
        {
            healthComponent.onHealthEmpty += StartDeath;
            healthComponent.onTakeDamage += TakenDamage;
        }
        perceptionComponent.onPerceptionTargetChanged += TargetChanged;
    }

    private void TargetChanged(GameObject target, bool sensed)
    {
        if (sensed)
        {
            this.target = target;
        }
        else
        {
            this.target = null;
        }
    }

    void TakenDamage(float health, float delta, float maxHealth, GameObject instigator)
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

    private void OnDrawGizmos()
    {
        if (target != null)
        {
            Vector3 drawTargetPos = target.transform.position + Vector3.up;
            Gizmos.DrawSphere(drawTargetPos, 0.7f);

            Gizmos.DrawLine(transform.position + Vector3.up, drawTargetPos);
        }
    }

}
