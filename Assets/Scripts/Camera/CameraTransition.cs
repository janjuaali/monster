using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem.Examples;
using System;

public class CameraTransition : MonoBehaviour
{
    [SerializeField] protected CameraPoint _cameraPoint;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private bool _setPointAsParent;
    [SerializeField] private float _timeToTransit;

    protected float Delay = 0f;
    private CameraFollowing _following;
    private FocalPoint _focalPoint;
    private Camera _camera;
    private float changeSpeed;
    private bool _inTransition;

    public event Action TransitionCompleted;

    private void Start()
    {
        _camera = Camera.main;

        _focalPoint = FindObjectOfType<FocalPoint>();
        Error.CheckOnNull(_focalPoint, nameof(FocalPoint));

        _following = FindObjectOfType<CameraFollowing>();
        Error.CheckOnNull(_following, nameof(CameraFollowing));
    }

    public bool TryTransit()
    {
        if (ViewState.IsCameraInTransition)
            return false;

        if (_camera.transform.parent != null)
            _camera.transform.parent = null;

        if (_setPointAsParent)
            _camera.transform.SetParent(_cameraPoint.transform);

        _following.enabled = false;

        float distance = Vector3.Distance(Camera.main.transform.position, _cameraPoint.transform.position);
        changeSpeed = distance / _timeToTransit;
        StartCoroutine(TransitAnimation());

        return true;
    }

    private IEnumerator TransitAnimation()
    {
        yield return new WaitForSeconds(Delay);

        ViewState.IsCameraInTransition = true;

        float distance = Vector3.Distance(_camera.transform.position, _cameraPoint.transform.position);

        while (distance >0.4f)
        {
            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _cameraPoint.transform.position, changeSpeed * Time.deltaTime);

            _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, _cameraPoint.transform.rotation, _rotationSpeed * Time.deltaTime);

            distance = Vector3.Distance(_camera.transform.position, _cameraPoint.transform.position);

            yield return null;
        }

        ViewState.IsCameraInTransition = false;

        TransitionCompleted?.Invoke();
    }
}
