using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEditor;
using System.Linq;

[System.Serializable]
public class RoadPositioning : MonoBehaviour
{
    [SerializeField] private float Height;
    [SerializeField] private float Offset;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private AnimationCurve _speedCurve;

    private PathCreator _pathCreator;

    public void Place()
    {
        transform.position = GetPosition(transform.position, Height, Offset);
        transform.rotation = GetRotation(transform.position, Height, Offset);
    }

    public void PlaceWithoutRotation()
    {
        transform.position = GetPosition(transform.position, Height, Offset);
    }

    public void PlaceByCurve()
    {
        transform.position = GetPositionByCurve(transform.position, Offset, _animationCurve);
    }

    public Vector3 GetPosition(Vector3 position, float height, float offset)
    {
        _pathCreator = FindNearestPath(transform);
        Vector3 point = _pathCreator.path.GetClosestPointOnPath(position);
        position = new Vector3(point.x + offset, point.y + height, point.z);

        return position;
    }

    public Vector3 GetPositionByCurve(Vector3 position, float offset, AnimationCurve animationCurve)
    {
        _pathCreator = FindNearestPath(transform);
        Vector3 point = _pathCreator.path.GetClosestPointOnPath(position);
        float time = _pathCreator.path.GetClosestTimeOnPath(point);

        position = new Vector3(point.x + offset, point.y + animationCurve.Evaluate(time), point.z + _speedCurve.Evaluate(time));

        return position;
    }

    public Quaternion GetRotation(Vector3 position, float height, float offset)
    {
        Vector3 point = GetPosition(position, height, offset);

        float time = _pathCreator.path.GetClosestTimeOnPath(point);
        Quaternion rotation = _pathCreator.path.GetRotation(time);

        rotation = new Quaternion(0, rotation.y, 0, rotation.w);

        return rotation;
    }

    public static PathCreator FindNearestPath(Transform transform)
    {
        var allPaths = FindObjectsOfType<PathCreator>();

        float minDistance = float.MaxValue;
        PathCreator nearestPath = null;

        foreach (var pathCreator in allPaths)
        {
            if (pathCreator.transform.IsChildOf(transform))
                continue;

            var nearestPoint = pathCreator.path.GetClosestPointOnPath(transform.position);
            var distance = Vector3.Distance(transform.position, nearestPoint);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPath = pathCreator;
            }
        }

        return nearestPath;
    }
}
