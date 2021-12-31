using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    void damage(DamageType damageType, int damageAmount);

    int getHitPoints();
}
