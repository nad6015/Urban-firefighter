using UnityEngine;

public class HealthBarTester : MonoBehaviour
{
    public HealthBarController healthBarController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D key pressed: Taking damage");
            healthBarController.TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("H key pressed: Healing");
            healthBarController.Heal(10);
        }
    }
}