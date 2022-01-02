using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Prevent ships from leaving the map by forcing them to turn back
/// </summary>
public class MapBoundary : MonoBehaviour
{
    private Vector3 mapCentre;
    
    private HashSet<Ship> outsideShips = new HashSet<Ship>();

    void Start() {
        mapCentre = transform.position;
    }

    void OnTriggerExit(Collider other) {
        Ship ship = other.GetComponent<Ship>();
        if (ship != null) {
            outsideShips.Add(ship);
            Debug.LogFormat("{0} should return towards the map!", ship);
            Vector3 displacement = mapCentre - ship.getPosition();
            displacement.y = 0;
            ship.setTargetVector(displacement);
            ship.setTargetSpeed(0.2f);
        }
    }

    void OnTriggerEnter(Collider coll) {
        Ship ship = coll.GetComponent<Ship>();
        if (ship != null && outsideShips.Contains(ship)) {
            Debug.LogFormat("{0} has entered the map boundary", coll);
            ship.setTargetSpeed(2f);
            outsideShips.Remove(ship);
        }
    }
}
