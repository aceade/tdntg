using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShip : MonoBehaviour
{

    public List<TestWaypoint> waypoints;

    private Ship ship;

    private int waypointIndex = -1;

    void Start()
    {
        ship = GetComponent<Ship>();   
        Invoke("initialMove", 1f);
    }

    private void initialMove() {
        IncrementIndex(ship.maxSpeed);
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
