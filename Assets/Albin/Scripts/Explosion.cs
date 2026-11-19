using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float lifeTime = 0.2f;

    private float timeLeft;

    private float explosionSize = 10f;
    private float explosionForce = 10f;
    private float explosionDamage = 10f;
    private bool damageDone = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeLeft = lifeTime;
        transform.localScale = new Vector3(explosionSize, explosionSize, explosionSize);
    }

    // Update is called once per frame
    void Update()
    {
        if((timeLeft -= Time.deltaTime) <= 0f)
            Destroy(gameObject);

        float scale = explosionSize * (timeLeft / lifeTime);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 collisionPoint = other.ClosestPoint(transform.localPosition);
        Vector3 hitDirection = collisionPoint - transform.localPosition;
        float distance = hitDirection.magnitude;
        hitDirection.Normalize();

        Rigidbody rb = other.attachedRigidbody;

        if(rb != null)
            rb.AddForce((hitDirection + Vector3.up) * explosionForce, ForceMode.Impulse);
        if(other.CompareTag("Player") && !damageDone)
        {
            rb.GetComponent<TankStats>().TakeDamage(explosionDamage);
            damageDone = true;
        }
        else if(other.CompareTag("Destructible"))
        {
            if(distance < explosionForce / 4)
                Destroy(other.gameObject);
        }
        else if(other.CompareTag("Missile"))
        {
            other.GetComponent<Missile>().ExplodeRpc();
        }
    }
}
