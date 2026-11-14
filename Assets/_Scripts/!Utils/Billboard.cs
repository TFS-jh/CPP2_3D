using UnityEngine;

public abstract class Billboard<T> : MonoBehaviour where T : MonoBehaviour
{
    public Transform target;

    protected virtual void LateUpdate()
    {
        if (target != null)
            transform.LookAt(target);
    }
}
