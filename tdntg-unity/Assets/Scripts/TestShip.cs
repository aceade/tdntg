using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShip : MonoBehaviour
{

    public List<TestWaypoint> waypoints;

    private Ship ship;

    private int waypointIndex = -1;

    [Range(-180, 180)]
    public float fallbackBearing;

    void Start()
    {
        ship = GetComponent<Ship>();   
        Invoke("initialMove", 1f);
    }

    private void initialMove() {
        if (waypoints.Count > 0) {
            IncrementIndex(ship.maxSpeed);
        } else {
            ship.setTargetBearing(fallbackBearing);
            ship.setTargetSpeed(1f);
        }
    }

    public void IncrementIndex(float targetSpeed) {
        waypointIndex++;
        if (waypointIndex >= waypoints.Count) {
            waypointIndex = 0;
        }

        Vector3 displacement = waypoints[waypointIndex].getPosition() - ship.getPosition();
        displacement.y = 0;
        ship.setTargetVector(displacement);
        ship.setTargetSpeed(targetSpeed);
    }

}
