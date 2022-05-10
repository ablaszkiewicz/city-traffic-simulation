using PathCreation;
using UnityEngine;

namespace Assets.Scripts
{
    public class Engine : MonoBehaviour
    {
        [SerializeField]
        private float distanceTravelled = 0;
        
        private float moveSpeed = 2;
        private VertexPath path;
        private Car owner;
        
        private void Start()
        {
            owner = GetComponent<Car>();
            path = owner.Path;
        }
        
        private void Update()
        {
            distanceTravelled += moveSpeed * Time.deltaTime;
            transform.position = path.GetPointAtDistance(distanceTravelled);
            transform.rotation = path.GetRotationAtDistance(distanceTravelled);
        }

        public void SetMoveSpeed(float newMoveSpeed)
        {
            moveSpeed = newMoveSpeed;
        }
    }
}