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
    [SerializeField] private LayerMask _layerMask;

    private NavMeshAgent _navMeshAgent;

    private void OnEnable()
    {
        _target = PlayerSingleton.singleton.transform;
    }

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
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

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, _layerMask)
            && hit.transform.gameObject.TryGetComponent<Player>(out Player player))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            return true;
        }
        else
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            return false;
        }
    }
}
