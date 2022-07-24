using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class single<T> : MonoBehaviour where T:single<T>
{
    private static T instance;

    public static T Instance { get => instance;}

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T)this;
        }
    }
}
