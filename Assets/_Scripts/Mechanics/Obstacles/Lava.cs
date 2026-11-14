using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] private int tickDamage = 10;
    [SerializeField] private float tickInterval = 0.25f;
    private float nextDamageTime;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            GameManager.Instance.currHealth -= tickDamage;
            nextDamageTime = Time.time + tickInterval;
        }
    }
}
