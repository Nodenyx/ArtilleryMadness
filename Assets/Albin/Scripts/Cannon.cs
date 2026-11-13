using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject projectile;
    public GameObject projectileSpawn;

    public float firePower;
    public int cooldown = 20;

    public int currentCooldown = 0;
    
}
