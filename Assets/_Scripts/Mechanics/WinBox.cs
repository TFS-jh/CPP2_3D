using UnityEngine;

public class WinBox : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
            GameManager.Instance.QuitGame();
    }
}
