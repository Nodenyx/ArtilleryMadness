using UnityEngine;

public class TankStats : MonoBehaviour
{
    [SerializeField]
    private float tankHealth = 100f;

    public void TakeDamage(float damage)
    {
        tankHealth -= damage;
        if (tankHealth <= 0)
            gameObject.SetActive(false);
    }
}
