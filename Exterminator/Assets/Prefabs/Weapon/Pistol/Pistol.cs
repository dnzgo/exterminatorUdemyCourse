using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] AimComponent aimComponent;
    [SerializeField] float damage = 8f;

    public override void Attack()
    {
        GameObject target = aimComponent.GetAimTarget();
        DamageGameObject(target, damage);
    }

}
