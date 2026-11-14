using UnityEngine;

public abstract class Billboard<T> : MonoBehaviour where T : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if (target != null)
            transform.LookAt(target);
    }
}
