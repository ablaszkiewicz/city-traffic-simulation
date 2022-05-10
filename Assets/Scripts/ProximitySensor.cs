using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class ProximitySensor : MonoBehaviour
    {
        [SerializeField]
        private Transform sensorPosition;

        [SerializeField]
        private int numberOfDetectors;

        [SerializeField]
        private float detectorsSpread;

        [SerializeField]
        private bool drawGizmos;


        private Car owner;


        private void Start()
        {
            InitializeOwner();
        }

        private void InitializeOwner()
        {
            owner = this.transform.GetComponent<Car>();
        }

        // private List<Vector3> GetDetectorsPositions()
        // {
        //     List<Vector3> detectors = new List<Vector3>();
        //     var path = owner.Path;
        //     
        //
        //     for (int i = 0; i < numberOfDetectors; i++)
        //     {
        //         var distance = (float)i / numberOfDetectors * detectorsSpread;
        //         var currentDistance = path.GetClosestDistanceAlongPath(sensorPosition.position);
        //         detectors.Add(path.GetPointAtDistance(currentDistance + distance));
        //     }
        //
        //     return detectors;
        // }

        public float GetDistanceToClosestObjectOnPath()
        {
            Vector3 forward = sensorPosition.TransformDirection(Vector3.forward);
            

            RaycastHit objectHit;
            if (Physics.Raycast(sensorPosition.position, forward, out objectHit, 50))
            {
                var distance = Vector3.Distance(sensorPosition.position, objectHit.point);
                Debug.DrawRay(sensorPosition.position,  forward * objectHit.distance, Color.red);
                return distance;
            }

            
            Debug.DrawRay(sensorPosition.position, forward * 10, Color.green);
            return 100;


        }

        // public float GetDistanceToClosestObjectOnPath()
            // {
            //     var points = GetDetectorsPositions();
            //     var distanceToObstacle = 0.0f;
            //     
            //     foreach (var point in points)
            //     {
            //         var colliders = Physics.OverlapSphere(point, 0.1f);
            //     }
            //     
            //     var ordered = points.SelectMany(point => Physics.OverlapSphere(point, 0.1f)).ToList().OrderBy(collider =>
            //         Vector3.Distance(sensorPosition.position, collider.gameObject.transform.position)).ToList();
            //     
            //     if (ordered.Count > 0)
            //     {
            //         distanceToObstacle = Vector3.Distance(sensorPosition.position, ordered[0].gameObject.transform.position);
            //     }
            //     else
            //     {
            //         distanceToObstacle = Single.PositiveInfinity;
            //     }
            //
            //     Debug.Log(distanceToObstacle);
            //     return distanceToObstacle;
            // }

            // private void OnDrawGizmos()
            // {
            //     if (!drawGizmos) return;
            //     if (!owner) return;
            //
            //     var points = GetDetectorsPositions();
            //     points.ForEach(point => Gizmos.DrawSphere(point, 0.1f));
            // }
        }
}