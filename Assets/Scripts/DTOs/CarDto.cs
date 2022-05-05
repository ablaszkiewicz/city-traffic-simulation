using System;
using UnityEngine;

namespace Assets.Scripts.DTOs
{
    [Serializable]
    public class CarDto
    {
        public int ID;
        public string state;

        public Vector2 velocity = new Vector2(0,0);
        public Vector2 acceleration = new Vector2(0,0);
        public Vector2 position = new Vector2(0,0);
        public float distanceToPreceedingCar = 0;
        public int preceedingCarId = 0;
    }
}