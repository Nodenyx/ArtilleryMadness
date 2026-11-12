using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject cannonSocket;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private TankStats tankStats;
    [SerializeField]
    private Cannon cannon;

    [SerializeField]
    private float moveSpeed = 20f;
    private float maxAngle = 94f;
    private float pitch = 0;
    [SerializeField]
    private float maxSpeed = 70f;

    InputAction raiseAction;
    // Store the action references.
    InputAction moveAction;
    InputAction fireAction;

    private void Start()
    {
        // Find the references to the "Move" and "Jump" actions
        moveAction = InputSystem.actions.FindAction("Move");
        raiseAction = InputSystem.actions.FindAction("Look");
        fireAction = InputSystem.actions.FindAction("Attack");
    }

    void FixedUpdate()
    {
        // Read the "Move" action value, which is a 2D vector
        // and the "Jump" action state, which is a boolean value

        if(Vector3.Dot(transform.up, new Vector3(0f, 1f, 0f)) < 0.1f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 3f, transform.localPosition.z);
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        Vector2 lookValue = raiseAction.ReadValue<Vector2>();
        pitch = Mathf.Clamp(pitch += lookValue.y, 0, maxAngle);

        cannonSocket.transform.localRotation = Quaternion.Euler(-pitch,
            0f, 0f);

        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        if((rb.linearVelocity.magnitude * 10f) < maxSpeed)
        {
            if (moveValue.y > 0) //Forward
                rb.AddForce(transform.forward * moveSpeed, ForceMode.Force);
            if (moveValue.y < 0) //Backwards
                rb.AddForce(-transform.forward * moveSpeed, ForceMode.Force);
        }
        if (moveValue.x > 0) //Turn right
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + 1f, transform.localRotation.eulerAngles.z);
        if (moveValue.x < 0) //Turn left
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y - 1f, transform.localRotation.eulerAngles.z);

        bool holding = false;
        if (fireAction.IsPressed())
        {
            if (!holding) //One click guard. 
            {
                holding = true;
                GameObject projectile;
                projectile = Instantiate(cannon.projectile, cannon.projectileSpawn.transform.position, Quaternion.identity);
                if(projectile.TryGetComponent<Rigidbody>(out var rb))
                {
                    rb.AddForce(cannonSocket.transform.forward * cannon.firePower, ForceMode.Impulse);
                }
                Debug.Log("FIRE");
            }
        }
        else
            holding = false;
    }
}
