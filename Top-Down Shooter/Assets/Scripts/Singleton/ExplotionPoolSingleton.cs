using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionPoolSingleton : MonoBehaviour
{
    public static ExplotionPoolSingleton singleton { get; private set; }

    private void Awake()
    {
        singleton = this;
    }
}
