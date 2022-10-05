using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))] 
public class ItemHeal : MonoBehaviour
{
    [SerializeField] private float _healValue;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IHealable healable))
        {
            healable.ApplyHeal(_healValue);
            gameObject.SetActive(false);
        }
    }
}
