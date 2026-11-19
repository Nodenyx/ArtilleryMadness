using Unity.Netcode;
using UnityEngine;

public class Missile : MonoBehaviour
{

    [SerializeField]
    private float lifeTime = 3f;
    [SerializeField]
    private float explosivePower = 1f;

    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private Rigidbody rb;

    private float timeLeft;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeLeft = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.LookRotation(rb.linearVelocity);

        if ((timeLeft -= Time.deltaTime) <= 0f) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ExplodeRpc();
    }
    [Rpc(SendTo.Server)]
    public void ExplodeRpc()
    {
        //Debug.Log("Missile hit: " + collision.gameObject.name);
        GameObject explode = Instantiate(explosion, transform.position, Quaternion.identity);
        explode.GetComponent<NetworkObject>().Spawn();
        Destroy(gameObject);
    }
}