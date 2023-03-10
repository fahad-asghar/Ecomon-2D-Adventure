using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public static GameObject obj;

    void Start()
    {
        if (obj != null)
        {
            Destroy(obj);
        }
        obj = gameObject;
        DontDestroyOnLoad(obj);
    }
}
