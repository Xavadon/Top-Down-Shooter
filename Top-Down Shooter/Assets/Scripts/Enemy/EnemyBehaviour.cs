using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AnimatorHandler))]
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyShooting _enemyShooting;
    [SerializeField] private float _shootingDistance = 3;
    [SerializeField] private float _rotationSpeed = 20;
    [SerializeField] private Transform _target;

    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _target = PlayerSingleton.singleton.transform;
    }

    private void Update()
    {
        EnemyLogic();
        RotateOnTarget();
    }

    private void EnemyLogic()
    {
        if (!SeePlayer() || Vector3.Distance(_target.position, transform.position) > _shootingDistance)
            _navMeshAgent.SetDestination(_target.position);
        else
        {
            _navMeshAgent.SetDestination(transform.position);
            _enemyShooting?.Shoot();
        }
    }

    private void RotateOnTarget()
    {
        var rotation = Quaternion.LookRotation(_target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
    }

    private bool SeePlayer()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask)
            && hit.transform.gameObject.TryGetComponent<Player>(out Player player))
            return true;
        else
            return false;
    }
}
