using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerLocomotion : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float _moveSpeed = 10;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        RotateOnMouseRay();
    }

    private Vector3 GetInput()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        input.Normalize();
        return input;
    }

    private void Move()
    {
        var _moveDirection = _moveSpeed * GetInput();
        _rigidbody.velocity = _moveDirection;
    }

    private void RotateOnMouseRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            Vector3 difference = raycastHit.point - transform.position;
            float rotation_z = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, rotation_z - 90, 0f);
        }
    }
}
