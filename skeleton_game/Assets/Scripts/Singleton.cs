using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    // Variables
    public static T instance;

    // Constructors
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    GameObject anObject = new GameObject();
                    anObject.name = typeof(T).Name;
                    instance = anObject.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    // Methods
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
