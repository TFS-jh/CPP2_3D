using UnityEngine;

public class Boo : MonoBehaviour
{
    public Transform target;

    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float forwardVelocity = 10f;
    [SerializeField] private float angleThreshold = 45f;

    public bool isBeingWatched = false;

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        Vector3 angleToBoo = (transform.position - target.position).normalized;
        float angle = Vector3.Angle(target.forward, angleToBoo);
        isBeingWatched = angle < angleThreshold;

        Vector3 angleToPlayer = target.position - transform.position;
        angleToPlayer.y = 0f;

        if (angleToPlayer != Vector3.zero) 
        {
            transform.forward = Vector3.RotateTowards(transform.forward, angleToPlayer, turnSpeed * Time.deltaTime, 0f);
        }

        transform.position = !isBeingWatched ? Vector3.MoveTowards(transform.position, target.position, forwardVelocity * Time.deltaTime) : transform.position;
    }
}
