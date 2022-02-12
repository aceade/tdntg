using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Ship : MonoBehaviour, IDamage, IPointerClickHandler
{
    public int maxHitpoints;
    public float maxSpeed;
    public float maxReverseSpeed;
    public float acceleration;

    public float maxTurningSpeed;
    
    public float minDetectionRadius;
    public float maxDetectionRadius;

    public enum MoveType {
        accelerating,
        decelerating,
        steady
    }
    private MoveType moveType = MoveType.steady;

    private int currentHitPoints;
    private float currentSpeed;
    private float targetSpeed;

    private float turningSpeed;

    private float bearing;

    private Transform myTransform;

    private Faction command;

    private Material defaultMaterial;
    public Material selectedMaterial;

    private List<Weapon> weapons;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        currentHitPoints = maxHitpoints;  
        currentSpeed = 0f;
        defaultMaterial = GetComponentInChildren<Renderer>().material;
        weapons = new List<Weapon>(GetComponentsInChildren<Weapon>());
    }

    // Update is called once per frame
    void Update()
    {
        if (moveType == MoveType.accelerating && currentSpeed <= targetSpeed) {
            currentSpeed += acceleration;
        } else if (moveType == MoveType.decelerating && currentSpeed >= targetSpeed) {
            currentSpeed -= acceleration;
        }
        myTransform.Translate(myTransform.forward * currentSpeed * Time.deltaTime);

        
        myTransform.Rotate(myTransform.up, turningSpeed * Time.deltaTime);
        if (shouldStraighten()) {
            setTurningSpeed(0f);
        }

    }

    private bool shouldStraighten() {
        return Mathf.Approximately(bearing, Vector3.Angle(myTransform.forward, Vector3.forward));
    }

    public Transform GetTransform() {
        return myTransform;
    }

    public void InflictDamage(DamageType damageType, int attackDamage) {
        // for now, ignore damage type
        currentHitPoints -= attackDamage;
        if (currentHitPoints <= 0) {
            Debug.LogFormat("Oh no, I am dead");
        }
    }

    public int GetHitPoints() {
        return currentHitPoints;
    }

    public float getCurrentSpeed() {
        return Mathf.RoundToInt(currentSpeed * 10) / 10f;
    }

    public float getCurrentRudder() {
        return Mathf.RoundToInt(turningSpeed * 10) / 10f;
    }

    public void setTargetSpeed(float speed) {
        targetSpeed = Mathf.Clamp(speed, maxReverseSpeed, maxSpeed);
        if (targetSpeed < currentSpeed) {
            moveType = MoveType.decelerating;
        } else if (targetSpeed > currentSpeed) {
            moveType = MoveType.accelerating;
        }
    }

    /// <summary>
    /// Point towards the specified compass bearing.
    /// </summary>
    /// <param name="angle"></param>
    public void setTargetBearing(float angle) {
        bearing = angle;
        float speed = angle < 0f ? (maxTurningSpeed / -2f) : (maxTurningSpeed / 2f);
        setTurningSpeed(speed);
    }

    public void setTargetVector(Vector3 displacement) {
        float angle = Vector3.Angle(displacement, myTransform.forward);
        if (Vector3.Dot(displacement, myTransform.right) < 0f) {
            angle *= -1f;
        }
        Debug.LogFormat("Ship at {0} must displace by {1}. This gives an angle of {2}", getPosition(), displacement, angle);
        setTargetBearing(angle);
        setTargetSpeed(targetSpeed);
    }

    public Vector3 getPosition() {
        return myTransform.position;
    }

    public void setTurningSpeed(float speed) {
        turningSpeed = Mathf.Clamp(speed, -maxTurningSpeed, maxTurningSpeed);
    }

    public bool isFiring() {
        for (int i = 0; i < weapons.Count; i++) {
            if (weapons[i].IsCurrentlyFiring()) {
                return true;
            }
        }
        return false;
    }

    public void setCommander(Faction faction) {
        this.command = faction;
    }

    public void shipSpotted(Ship ship) {
        command.ShipSpotted(ship);
        weapons.ForEach(x => x.StartTracking(ship));
    }

    public void ShipVisuallyLost(Ship ship) {
        command.ShipDetectionLost(ship);
        weapons.ForEach(x => x.StopTrackingCurrentTarget());
    }

    public void toggleRendering(bool show) {
        GetComponentInChildren<Renderer>().enabled = show;
    }

    /// <summary>
    /// Implement an on-click method for this, using Unity's EventSystem.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData) {
        Debug.LogFormat("I haz been clicked. EventData: {0}", eventData);
        command.shipSelected(this);
        GetComponentInChildren<Renderer>().material = selectedMaterial;
    }

    public void Deselect() {
        GetComponentInChildren<Renderer>().material = defaultMaterial;
    }


}
