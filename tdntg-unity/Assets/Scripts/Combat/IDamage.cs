using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    void InflictDamage(DamageType damageType, int damageAmount);

    int GetHitPoints();

    Transform GetTransform();
}
