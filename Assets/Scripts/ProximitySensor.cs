using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
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

        private PathPlanner pathPlanner;

        [SerializeField]
        private string nameOfTheClosestObject;
        
        private void Start()
        {
            pathPlanner = GetComponent<PathPlanner>();
        }

        public float GetDistanceToClosestObjectOnPath()
        {
            Vector3 forward = sensorPosition.TransformDirection(Vector3.forward);
            

            RaycastHit objectHit;
            var hits = Physics.RaycastAll(sensorPosition.position, forward, 50);

            if (hits.Length == 0)
            {
                Debug.DrawRay(sensorPosition.position, forward * 10, Color.blue);
                return 100;
            }

            var viableHits = hits.Where(hit => IsViableBarrier(hit.transform.parent.gameObject));
            if (viableHits.Count() == 0)
            {
                Debug.DrawRay(sensorPosition.position, forward * 10, Color.blue);
                return 100;
            }
            
            var closest = viableHits.OrderBy(hit => Vector3.Distance(hit.point, sensorPosition.position)).First();

            nameOfTheClosestObject = closest.transform.gameObject.name;
            
            Debug.DrawLine(sensorPosition.position, closest.point, Color.red);
            return Vector3.Distance(closest.point, sensorPosition.position);
        }

        private bool IsViableBarrier(GameObject gameObject)
        {
            gameObject.TryGetComponent(out CrossroadBarrier barrier);
            if (barrier != null)
            {
                var pathCreator = barrier.PathCreator;

                // if (pathCreator != pathPlanner.CurrentPathCreator)
                // {
                //     Debug.Log($"My path creator is {pathPlanner.CurrentPathCreator.gameObject.name} \n Barrier's is {pathCreator.gameObject.name}");
                // }
                
                return pathCreator == pathPlanner.CurrentPathCreator;
            }

            return true;
        }
    }
}