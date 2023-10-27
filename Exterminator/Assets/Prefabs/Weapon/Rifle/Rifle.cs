using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField] AimComponent aimComponent;
    [SerializeField] float damage = 5f;

    public override void Attack()
    {
        GameObject target = aimComponent.GetAimTarget();
        DamageGameObject(target, damage);
    }

}
