using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spotter : MonoBehaviour
{
    private Ship ship;

    public float speedWeighting = 0.2f;
    public float heightWeighting = 0.2f;
    public float attackWeighting = 0.6f;

    private Dictionary<Ship, bool> knownEnemyShips = new Dictionary<Ship, bool>();

    public float scanRate = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponentInParent<Ship>();
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider coll) {
        
        var otherShip = coll.transform.root.GetComponent<Ship>();
        if (otherShip != null) {
            float distance = Vector3.Distance(otherShip.getPosition(), ship.getPosition());

            bool detected = isDetected(otherShip, distance);
            if (!knownEnemyShips.ContainsKey(otherShip)) {
                knownEnemyShips.Add(otherShip, detected);
                if (knownEnemyShips.Count == 1) {
                    InvokeRepeating("scanKnownShips", 0f, scanRate);
                }
            }
            if (detected) {
                ship.shipSpotted(otherShip);
            }
        }
    }

    private void scanKnownShips() {

        for (int i = 0; i < knownEnemyShips.Count; i++) {

            var otherShip = knownEnemyShips.Keys.ToList()[i];
            var previousValue = knownEnemyShips[otherShip];
            Debug.DrawRay(ship.getPosition(), otherShip.getPosition() - ship.getPosition(), Color.yellow);
            float distance = Vector3.Distance(otherShip.getPosition(), ship.getPosition());
            bool detected = isDetected(otherShip, distance);
            if (detected && !previousValue) {
                Debug.LogFormat("We can actually see {0}", otherShip);
                knownEnemyShips[otherShip] = true;
                ship.shipSpotted(otherShip);
            } else if (!detected && previousValue) {
                Debug.LogFormat("We just lost sight of {0}", otherShip);
                knownEnemyShips[otherShip] = false;
                ship.ShipVisuallyLost(otherShip);
            }
        }
    }

    void OnTriggerExit(Collider coll) {
        var otherShip = coll.transform.root.GetComponent<Ship>();
        if (otherShip != null) {
            knownEnemyShips.Remove(otherShip);
            if (knownEnemyShips.Count == 0) {
                CancelInvoke("scanKnownShips");
            }
        }   
    }

    private bool isDetected(Ship ship, float distance) {
        float minDistance = ship.minDetectionRadius;
        float maxDistance = ship.maxDetectionRadius;

        if (distance < maxDistance) {

            // if they're too close, no hiding!
            if (distance <= minDistance) {
                return true;
            } else {
                // this will eventually be proportional to their speed, height and whether they are firing cannons
                // torpedoes can be stealthy
                if (ship.isFiring()) {
                    return true;
                }
            }
        }

        return false;
    }
}
