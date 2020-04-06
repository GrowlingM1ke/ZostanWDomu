using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class information : MonoBehaviour
{
    public static information Instance;

    void Awake()
    {
        if (Instance)
            DestroyImmediate(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }


    public int finishedSluch = 0;
    public int finishedWzrok = 0;
    public int finishedRuch = 0;
    public int finishedDotyk = 0;
}
