using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 15;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();

        if (rb == null)
            Debug.LogError("RigidBody missing on Projectile!");

        Destroy(gameObject, lifetime);
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.linearVelocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.currHealth -= damage;
        }
        Destroy(gameObject);
    }
}
