using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    public static PlayerSingleton singleton { get; private set; }

    private void Awake()
    {
        singleton = this;
    }

}
