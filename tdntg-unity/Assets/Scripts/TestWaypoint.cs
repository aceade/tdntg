using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWaypoint : MonoBehaviour
{
    public TestShip testShip;

    public float targetSpeed = 2f;

    // Update is called once per frame
    void OnTriggerEnter(Collider other) {
        if (!other.isTrigger && other.transform.root == testShip.transform) {
            Debug.LogFormat("{0} entered me", other);
            testShip.IncrementIndex(targetSpeed);
        }
    }

    public Vector3 getPosition() {
        return transform.position;
    }
}
