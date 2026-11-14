using UnityEngine;

public class Boo : MonoBehaviour
{
    public Transform target;

    [Header("Movement")]
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float forwardVelocity = 10f;
    [SerializeField] private float angleThreshold = 45f;

    [Header("Projectile")]
    [SerializeField] private float forwardProjVel = 10f;
    [SerializeField] private float upwardProjVel = 5f;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Projectile projectilePrefab;

    private bool _isBeingWatched;
    public bool isBeingWatched 
    {
        get => _isBeingWatched;
        set
        {
            if (_isBeingWatched == value) return;

            _isBeingWatched = value;

            if (!_isBeingWatched)
            {
                Fire();
            }
        }
    }

    void Start()
    {
        if (float.IsNaN(forwardProjVel) || float.IsInfinity(forwardProjVel))
        {
            forwardProjVel = 10f;
            Debug.LogWarning("Shoot: forwardProjVel value is invalid! Resetting to default (10f)...");
        }

        if (float.IsNaN(upwardProjVel) || float.IsInfinity(upwardProjVel))
        {
            upwardProjVel = 5f;
            Debug.LogWarning("Shoot: upwardProjVel value is invalid! Resetting to default (5f)...");
        }

        if (spawnPoint == null  || projectilePrefab == null)
        {
            Debug.LogError("Shoot: spawn point or projectile prefabs are not assigned - in order to use the shoot component, it hss to be assigned.");
        }
    }

    // Update is called once per frame
    public void Fire()
    {
        if (spawnPoint == null || projectilePrefab == null)
        {
            Debug.LogError("Fire will not work due to Shoot script missing a spawn point or projectile.");
            return;
        }

        Vector3 launchVel = (transform.forward * forwardProjVel) + (Vector3.up * upwardProjVel);

        Projectile curProjectile;

        curProjectile = Instantiate(projectilePrefab, spawnPoint.position, transform.rotation);
        curProjectile.SetVelocity(launchVel);

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        Vector3 angleToBoo = (transform.position - target.position).normalized;
        float angle = Vector3.Angle(target.forward, angleToBoo);
        isBeingWatched = angle < angleThreshold;

        Vector3 angleToPlayer = target.position - transform.position;
        angleToPlayer.y = 0f;

        float distance = Vector3.Distance(transform.position, target.position);

        if (angleToPlayer != Vector3.zero) 
        {
            transform.forward = Vector3.RotateTowards(transform.forward, angleToPlayer, turnSpeed * Time.deltaTime, 0f);
        }
        
        if (distance <= 85)
            transform.position = !isBeingWatched ? Vector3.MoveTowards(transform.position, target.position, forwardVelocity * Time.deltaTime) : transform.position;
    }
}
