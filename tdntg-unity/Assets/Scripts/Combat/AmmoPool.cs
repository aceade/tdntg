using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : MonoBehaviour
{
    private List<Projectile> projectiles;

    private int index = 0;

    // Start is called before the first frame update
    void Awake()
    {
        projectiles = new List<Projectile>(GetComponentsInChildren<Projectile>());
        projectiles.ForEach(x => x.SetPool(this));
    }

    public Projectile.Type GetAmmoType() {
        return projectiles[0].weaponType;
    }

    public Projectile GetProjectile() {
        Projectile currentProjectile = projectiles[index];
        index++;
        if (index >= projectiles.Count) {
            index = 0;
        }
        return currentProjectile;
    }    
}
