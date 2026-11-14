using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor.Localization;
#endif

public class GameManager : Singleton<GameManager>
{
    public int starCount = 0;
    public int livesCount = 5;

    [SerializeField] public int maxHealth = 150;
    [HideInInspector] public int currHealth = 150;

    private Coroutine death;

    private void Update()
    {
        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
            Debug.Log($"Max Health cannot exceed limit of {maxHealth}!");
        }

        if (currHealth <= 0)
        {
            QuitGame();
        }

        Debug.Log($"currHealth: {currHealth} | maxHealth: {maxHealth}");
    }

    public IEnumerator PoisonTimer()
    {
        yield return new WaitForSeconds(5f);
        QuitGame();
    }

    public void TimeToDie()
    {
        if (death == null) death = StartCoroutine(PoisonTimer());
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
