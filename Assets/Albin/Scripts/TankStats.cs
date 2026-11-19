using UnityEngine;

public class TankStats : MonoBehaviour
{
    [SerializeField]
    private float tankHealth = 100f;
    [SerializeField]
    private TankMovement tankMovement;
    public void TakeDamage(float damage)
    {
        tankHealth -= damage;
        if (tankHealth <= 0)
        {
            tankHealth = 100;
            tankMovement.Respawn();
        }
    }
}