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

    private Ship myShip;

    // torpedoes will have no dispersion
    public float dispersion = 1f;

    public float maxDistance;
    
    // Start is called before the first frame update
    protected void Start()
    {
        myTransform = transform;
        muzzle = myTransform.GetChild(0);
        if (ammoPool.GetAmmoType() != acceptableAmmoType) {
            Debug.LogErrorFormat("This weapon is supposed to fire [{0}], not [{1}]", acceptableAmmoType, ammoPool.GetAmmoType());
        }
    }

    public void SetShip(Ship ship) {
        myShip = ship;
    }

    public void StartTracking(IDamage target) {
        currentTarget = target;
        InvokeRepeating("TrackCurrentTarget", 0.1f, 0.05f);
    }

    private void TrackCurrentTarget() {
        
        if (currentTarget.GetHitPoints() < 0) {
            myShip.TargetDestroyed(currentTarget);
            // quit for now...but perhaps a twitchy commander could order us to make sure
            return;
        }

        Vector3 targetDir = calculateFiringDir();
        myTransform.forward = Vector3.RotateTowards(myTransform.forward, targetDir, turningSpeed, 0f);
        if (Vector3.Angle(myTransform.forward, targetDir) < dispersion && targetDir.magnitude < maxDistance) {
            Launch(myTransform.forward);
        }
    }

    protected virtual Vector3 calculateFiringDir() {
        Vector3 displacement = currentTarget.GetTransform().position - transform.position;
        float speed = currentTarget.GetCurrentSpeed();
        Vector3 targetDir = currentTarget.GetTransform().forward * speed;

        Vector3 firingDir = displacement + targetDir;
        Debug.DrawLine(muzzle.position, firingDir, Color.red);
        Debug.DrawLine(muzzle.position, muzzle.forward * 10, Color.yellow);
        if (acceptableAmmoType == Projectile.Type.TORPEDO) {
            // torpedos can't fly
            firingDir.y = 0;
        }
        return firingDir;
    }

    public void StopTrackingCurrentTarget() {
        currentTarget = null;
        CancelInvoke("TrackCurrentTarget");
    }

    public void Launch(Vector3 direction) {
        if (isFiring) {
            return;
        }
        Debug.LogFormat("Launching {0} from {1} at {2}", acceptableAmmoType, muzzle.position, direction);
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
