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

        private void Start()
        {
            pathPlanner = GetComponent<PathPlanner>();
        }
        
        private void Update()
        {
            if (!canMove || !pathPlanner.IsReady) return;
            
            distanceTravelledOnThisPath += moveSpeed * Time.deltaTime;
            transform.position = pathPlanner.GetPointAtDistance(distanceTravelledOnThisPath);
            transform.rotation = pathPlanner.GetRotationAtDistance(distanceTravelledOnThisPath);
        }

        public void SetCanMove(bool canMove)
        {
            this.canMove = canMove;
        }

        public void ResetDistanceTravelledOnThisPath()
        {
            distanceTravelledOnThisPath = 0;
        }
    }
}