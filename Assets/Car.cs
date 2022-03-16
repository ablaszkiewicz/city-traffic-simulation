using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using UnityEngine;
using UnityEngine.UIElements;

public class Car : MonoBehaviour
{
    [SerializeField]
    private PathCreator pathCreator;
    [SerializeField]
    private float moveSpeed;
    
    private VertexPath path;
    
    [SerializeField]
    private float distanceTravelled = 0;
    
    [SerializeField]
    private float distanceToObstacle = 0;

    private void Start()
    {
        path = pathCreator.path;
        pathCreator.pathUpdated += OnPathChanged;
    }
    
    private void Update()
    {
        if (distanceToObstacle < 3)
        {
            return;
        }
        distanceTravelled += moveSpeed * Time.deltaTime;
        transform.position = path.GetPointAtDistance(distanceTravelled);
        
    }
    
    private void OnPathChanged()
    {
        path = pathCreator.path;
        distanceTravelled = path.GetClosestDistanceAlongPath(transform.position);
    }
    
    
    private void OnDrawGizmos()
    {
        if (path == null)
        {
            return;
        }
        Gizmos.color = Color.red;

        List<Vector3> points = new List<Vector3>();
        List<float> distances = new List<float>();
        
        for (float i = 3; i < 20; i++)
        {
            Vector3 point = path.GetPointAtDistance(distanceTravelled + i / 2);
            Gizmos.DrawSphere(point, 0.1f);
            points.Add(point);
        }

        
        
        foreach (var point in points)
        {
            var colliders = Physics.OverlapSphere(point, 0.1f);
            foreach (var collider in colliders)
            {
                Debug.Log(collider.gameObject.name);
                distances.Add(Vector3.Distance(transform.position, collider.gameObject.transform.position));
            }
        }

        var ordered = points.SelectMany(point => Physics.OverlapSphere(point, 0.1f)).ToList().OrderBy(collider =>
            Vector3.Distance(transform.position, collider.gameObject.transform.position)).ToList();

        if (ordered.Count > 0)
        {
            distanceToObstacle = Vector3.Distance(transform.position, ordered[0].gameObject.transform.position);
        }
        else
        {
            distanceToObstacle = 10;
        }
        
        
    }
}
