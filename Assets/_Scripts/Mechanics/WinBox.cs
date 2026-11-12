using UnityEngine;

public class WinBox : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Player entered!");

        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.QuitGame();
        }
    }
}
