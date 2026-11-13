using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public int starCount = 0;
    public int livesCount = 5;

    private Coroutine death;

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
