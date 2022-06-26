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

            
            Debug.DrawRay(sensorPosition.position, forward * 10, Color.blue);
            return 100;


        }
    }
}