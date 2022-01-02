using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotter : MonoBehaviour
{
    private Ship ship;

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
        }
    }
}
