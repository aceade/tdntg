using UnityEngine;

public class Projectile : MonoBehaviour {
    
    public enum Type {
        TORPEDO, SHELL
    }

    public Type weaponType;

    public DamageType damageType;

    private Transform myTransform;
    private Rigidbody myBody;

    public int damagePoints;

    void Start() {
        myBody = GetComponent<Rigidbody>();
        myTransform = transform;
    }

    public void Launch(Vector3 position, Vector3 direction, float speed) {
        myTransform.position = position;
        myTransform.forward = direction;
        myBody.AddForce(direction * speed, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other) {
        IDamage damageScript = other.collider.transform.root.GetComponent<IDamage>();
        if (damageScript != null) {
            damageScript.InflictDamage(this.damageType, damagePoints);
        }
    }
}