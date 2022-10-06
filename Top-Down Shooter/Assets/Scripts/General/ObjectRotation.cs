using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private Vector3 _axis = new Vector3(0, 1, 0);

    private void Update()
    {
        transform.RotateAround(transform.position, _axis, _rotationSpeed * Time.deltaTime);
    }
}
