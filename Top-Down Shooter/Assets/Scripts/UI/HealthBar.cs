using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarFilling;
    [SerializeField] private UnitHealth _unitHealth;
    private Camera _camera;

    private void Awake()
    {
        _unitHealth.HealthChanged += OnHealthChanged;
        _camera = Camera.main;
    }

    private void OnDestroy()
    {
        _unitHealth.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float value)
    {
        _healthBarFilling.fillAmount = value;
    }

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
        transform.Rotate(0, 180, 0);
    }
}
