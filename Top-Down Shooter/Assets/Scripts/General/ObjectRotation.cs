using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        transform.RotateAround(transform.position, new Vector3(0, 1, 0), _rotationSpeed * Time.deltaTime);
    }
}
