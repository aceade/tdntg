using UnityEngine;

public class Cannon : Weapon
{

    public float maxElevation = 30f;

    public float ignoreElevationRange = 40f;

    protected override Vector3 calculateFiringDir()
    {
        // assume that we are on flat ground - the distance between the waterline and the cannons is negligible
        // ignore air resistance too (because we won't use drag on the projectiles)
        // d = v^2 sin(2*theta)/g
        // Note to self: multiply Mathf.Asin by Mathf.Rad2Deg to get the angle in degrees!

        Vector3 baseDir =  base.calculateFiringDir();
        var d = baseDir.magnitude;

        // at point-blank range, ignore trajectories
        if (d > ignoreElevationRange) {
            var tmp = d * Physics.gravity.magnitude / Mathf.Pow(muzzleVelocity, 2);
            var angle = (Mathf.Rad2Deg * Mathf.Asin(tmp))/ 2;
            baseDir.y = angle;
        }
        
        return baseDir;
    }
}
