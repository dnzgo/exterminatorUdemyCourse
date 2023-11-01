using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] string attachSlotTag;
    [SerializeField] float attackRateMult = 1f;
    [SerializeField] AnimatorOverrideController overrideController;

    public abstract void Attack();

    public string GetAttachSlotTag()
    {
        return attachSlotTag;
    }

    public GameObject Owner
    {
        get;
        private set;
    }

    public void Init(GameObject owner)
    {
        Owner = owner;
        UnEquip();
    }

    public void Equip()
    {
        gameObject.SetActive(true);
        Owner.GetComponent<Animator>().runtimeAnimatorController = overrideController;
        Owner.GetComponent<Animator>().SetFloat("attackRateMult", attackRateMult);
    }

    public void UnEquip()
    {
        gameObject.SetActive(false);
    }

    public void DamageGameObject(GameObject objToDamage, float amt)
    {
        HealthComponent healthComponent = objToDamage.GetComponent<HealthComponent>();
        if (healthComponent != null)
        {
            healthComponent.ChangeHealth(-amt, Owner);
        }
    }

}
