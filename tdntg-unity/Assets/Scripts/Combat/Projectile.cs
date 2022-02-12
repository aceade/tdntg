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
        GetComponent<Collider>().enabled = false;
        myBody.Sleep();
    }

    public void Launch(Vector3 position, Vector3 direction, float speed) {
        myTransform.position = position;
        myTransform.forward = direction;
        myBody.WakeUp();
        myBody.AddForce(direction * speed, ForceMode.VelocityChange);
        Invoke("EnableCollisions", 0.1f);
    }

    private void EnableCollisions() {
        GetComponent<Collider>().enabled = true;
    }

    void OnCollisionEnter(Collision other) {

        if (other.collider.isTrigger) {
            return;
        }
        IDamage damageScript = other.collider.transform.root.GetComponent<IDamage>();
        if (damageScript != null) {
            Debug.LogFormat("Projectile hit {0}", damageScript);
            damageScript.InflictDamage(this.damageType, damagePoints);
            myBody.Sleep();
            GetComponent<Collider>().enabled = false;
        }
    }
}