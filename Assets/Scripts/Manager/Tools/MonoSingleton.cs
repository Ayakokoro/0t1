using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public bool global = true;
    static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
            }
            return instance;
        }

    }

    void Awake()
    {
        if (global)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        OnStart();
    }

    protected virtual void OnStart()
    {

    }
}
