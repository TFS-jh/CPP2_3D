using System;
using System.Collections;
using UnityEngine;

public class PickupObj : MonoBehaviour
{
    public enum PickupType
    {
        Star,
        OneUp,
        Poison
    }

    [SerializeField] private PickupType type;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.Instance == null) throw new MissingComponentException("CharacterController component is missing from this GameObject!");

            PlayerController player = collision.GetComponent<PlayerController>();
            switch (type)
            {
                case PickupType.Star:
                    Debug.Log("Star collected!");
                    GameManager.Instance.starCount++;
                    break;
                case PickupType.OneUp:
                    Debug.Log("1UP collected!");
                    GameManager.Instance.livesCount++;
                    break;
                case PickupType.Poison:
                    Debug.Log(" You will die in 5 seconds...");
                    GameManager.Instance.TimeToDie();
                    break;
            }
            Destroy(gameObject);
        }
    }
}
