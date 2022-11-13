using System;
using PathCreation;
using UnityEditor.UIElements;
using UnityEngine;

namespace Assets.Scripts
{
    public class Engine : MonoBehaviour
    {
        [SerializeField]
        private float distanceTravelledOnThisPath = 0;
        
        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private AnimationCurve breakingCurve;
        
        private PathPlanner pathPlanner;
        private bool canMove;
        
        [SerializeField]//TODO
        private float velocityMultiplier;

        public float VelocityMultiplier
        {
            get => velocityMultiplier;
        }

        private void Start()
        {
            pathPlanner = GetComponent<PathPlanner>();
        }
        
        private void Update()
        {
            if (!canMove || !pathPlanner.IsReady) return;
            Move();

            
            velocityMultiplier += Time.deltaTime;
            velocityMultiplier = Math.Min(velocityMultiplier, 1.0f);
        }

        private void Move()
        {
            distanceTravelledOnThisPath += moveSpeed * Time.deltaTime * velocityMultiplier;
            transform.position = pathPlanner.GetPointAtDistance(distanceTravelledOnThisPath);
            transform.rotation = pathPlanner.GetRotationAtDistance(distanceTravelledOnThisPath);
        }

        public void SetCanMove(bool canMove)
        {
            this.canMove = canMove;
            if (!canMove)
            {
                velocityMultiplier = 0;
            }
        }

        public void ResetDistanceTravelledOnThisPath()
        {
            distanceTravelledOnThisPath = 0;
        }
    }
}