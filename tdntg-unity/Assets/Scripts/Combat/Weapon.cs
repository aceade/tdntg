using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float turningSpeed;

    public float fireRate;
    private bool isFiring = false;

    private Transform myTransform, muzzle;

    public AmmoPool ammoPool;

    public Projectile.Type acceptableAmmoType;

    public float muzzleVelocity;

    private IDamage currentTarget;

    // torpedoes will have no dispersion
    public float dispersion = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        muzzle = myTransform.GetChild(0);
        if (ammoPool.GetAmmoType() != acceptableAmmoType) {
            Debug.LogErrorFormat("This weapon is supposed to fire [{0}], not [{1}]", acceptableAmmoType, ammoPool.GetAmmoType());
        }
    }

    public void StartTracking(IDamage target) {
        currentTarget = target;
        InvokeRepeating("TrackCurrentTarget", 0f, 0.1f);
        
    }

    private void TrackCurrentTarget() {
        Vector3 targetDir = currentTarget.GetTransform().position = transform.position;
        myTransform.forward = Vector3.RotateTowards(myTransform.forward, targetDir - myTransform.forward, turningSpeed, 0f);
        if (Vector3.Angle(myTransform.forward, targetDir) < dispersion) {
            Launch(myTransform.forward);
        }
    }

    public void StopTrackingCurrentTarget() {
        currentTarget = null;
        CancelInvoke("TrackCurrentTarget");
    }

    public void Launch(Vector3 direction) {
        if (isFiring) {
            return;
        }
        isFiring = true;
        Projectile projectile = ammoPool.GetProjectile();
        projectile.Launch(muzzle.position, direction, muzzleVelocity);
        Invoke("CompleteFireCycle", fireRate);
    }

    private void CompleteFireCycle() {
        isFiring = false;
    }

    public bool IsCurrentlyFiring() {
        return isFiring;
    }
}
