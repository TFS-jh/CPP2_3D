using UnityEngine;
using System;
using UnityEditor;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static bool IsInitialized => _instance != null;
    protected static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                try
                {
                    _instance = FindAnyObjectByType<T>();
                    if (_instance == null) 
                        throw new InvalidOperationException($"No instance of Singleton<{typeof(T).Name}> found in this scene.");
                }
                catch (Exception e)
                {
                    Debug.Log(e.StackTrace);
                }
                finally
                {
                    GameManager.Instance.QuitGame();
                }
                
            }
            return _instance;
        }
    }

    // ONLY USE TRY-CATCH-FINALLY FOR FUNCTIONS THAT CAN RESET TO DEFAULT AND CONTINUE
    // ONLY USE TRY-CATCH-FINALLY FOR FUNCTIONS THAT CAN RESET TO DEFAULT AND CONTINUE
    // ONLY USE TRY-CATCH-FINALLY FOR FUNCTIONS THAT CAN RESET TO DEFAULT AND CONTINUE

    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning($"Multiple instances of a Singleton<{typeof(T).Name}> detected. Destroying duplicates...");
            Destroy(gameObject);
            return;
        }

        _instance = this as T;

        if (_instance == null)
        {
            Destroy(gameObject);
            throw new InvalidCastException($"Failed to cast {typeof(T).Name} to Singleton<{typeof(T).Name}>.");
        }

        DontDestroyOnLoad(gameObject);
    }
}
