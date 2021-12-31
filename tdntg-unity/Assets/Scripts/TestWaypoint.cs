using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWaypoint : MonoBehaviour
{
    public TestShip testShip;

    public float targetSpeed = 2f;

    // Update is called once per frame
    void OnTriggerEnter(Collider other) {
        Debug.LogFormat("{0} entered me", other);
        if (other.transform.root == testShip.transform) {
            testShip.IncrementIndex(targetSpeed);
        }
    }

    public Vector3 getPosition() {
        return transform.position;
    }
}
