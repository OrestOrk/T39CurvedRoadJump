using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    private Vector3 _offset = new Vector3(0, 10, -5);
    private float _smoothSpeed = 5f;
    private float _rotationSpeed = 5f;

    private ActorController _actor;

    private void Start()
    {
        _actor = ServiceLocator.GetService<ActorController>();

        if (_cameraTransform != null)
        {
            SetDefaultCameraRotation();
        }
    }

    private void LateUpdate()
    {
        if (_actor == null)
        {
            Debug.LogWarning("Гравець не призначений для камери!");
            return;
        }

        FollowPlayer();
    }

    private void FollowPlayer()
    {
        // Отримуємо напрямок, куди дивиться гравець
        Vector3 backwardOffset = -_actor.transform.forward * 5f + Vector3.up * 10f;

        // Розраховуємо бажану позицію камери
        Vector3 desiredPosition = _actor.transform.position + backwardOffset;

        // Плавно переміщуємо камеру
        Vector3 smoothedPosition = Vector3.Lerp(_cameraTransform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        smoothedPosition.y = 10f;
        _cameraTransform.position = smoothedPosition;

        // Камера дивиться на гравця
        Vector3 dirToPlayer = _actor.transform.position - _cameraTransform.position;
        Quaternion targetRotation = Quaternion.LookRotation(dirToPlayer);
        _cameraTransform.rotation = Quaternion.Lerp(_cameraTransform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
    }


    private void SetDefaultCameraRotation()
    {
        Quaternion defaultRotation = Quaternion.Euler(50f, 0f, 0f);
        _cameraTransform.rotation = defaultRotation;
    }
    
}