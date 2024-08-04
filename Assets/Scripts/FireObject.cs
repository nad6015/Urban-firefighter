using UnityEngine;

public class FireObject : MonoBehaviour
{
    public float health = 100f;

    public void Extinguish(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
