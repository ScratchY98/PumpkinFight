using UnityEngine;

public class TurnProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 640f;
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + speed * Time.deltaTime));
    }
}