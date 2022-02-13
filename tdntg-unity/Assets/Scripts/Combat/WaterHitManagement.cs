using UnityEngine;

public class WaterHitManagement : MonoBehaviour, IDamage
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float GetCurrentSpeed()
    {
        return 0;
    }

    public int GetHitPoints()
    {
        return -1;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void InflictDamage(DamageType damageType, int damageAmount)
    {
        // splashes
        Debug.LogFormat("Splash");
    }


   
}
