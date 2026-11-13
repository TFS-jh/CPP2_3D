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

    private void OnCharacterControllerHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Player"))
        {
            PlayerController player = hit.collider.GetComponent<PlayerController>();
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
                    StartCoroutine(PoisonTimer());
                    break;
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator PoisonTimer()
    {
        yield return new WaitForSeconds(5f);
        GameManager.Instance.QuitGame();
    }
}
