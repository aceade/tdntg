using UnityEngine;

public class Spotter : MonoBehaviour
{
    private Ship ship;

    public float speedWeighting = 0.2f;
    public float heightWeighting = 0.2f;
    public float attackWeighting = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponentInParent<Ship>();
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider coll) {
        
        var otherShip = coll.GetComponent<Ship>();
        if (otherShip != null) {
            float distance = Vector3.Distance(otherShip.getPosition(), ship.getPosition());
            Debug.LogFormat("{0} entered detection for {1} at a distance of {2}", coll, ship, distance);

            
            bool detected = isDetected(otherShip, distance);
            if (detected) {
                ship.shipSpotted(otherShip);
            }
        }
    }

    private bool isDetected(Ship ship, float distance) {
        float minDistance = ship.minDetectionRadius;
        float maxDistance = ship.maxDetectionRadius;

        // if they're too close, no hiding!
        if (distance < maxDistance) {

            if (distance <= minDistance) {
                return true;
            } else {
                // this will be proportional to their speed, height and whether they are firing cannons
                // torpedos can be stealthy
            }
        }

        return false;
    }
}
