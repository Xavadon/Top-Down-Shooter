using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class ProjectileTrailBehaviour : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trailRenderer;

    private void OnDisable()
    {
        _trailRenderer.emitting = false;
    }

    private void OnEnable()
    {
        Invoke(nameof(EnableEmitting), 0.02f);
    }

    private void EnableEmitting()
    {
        _trailRenderer.emitting = true;
    }
}
