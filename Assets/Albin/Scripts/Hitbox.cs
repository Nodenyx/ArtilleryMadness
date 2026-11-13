using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField, Tooltip("The object to be affected by being hit")]
    private GameObject tank;

    public GameObject GetTank()
    {
        return tank;
    }
}
