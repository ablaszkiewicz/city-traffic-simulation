using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.DTOs
{
    [Serializable]
    public class CarDto
    {
        public int id;
        public string state;

        public Vector2 velocity = new Vector2(0, 0);
        public Vector2 acceleration = new Vector2(0, 0);
        public Vector2 position = new Vector2(0, 0);
        public float distanceToPrecedingCar = 0;
        public int precedingCarId = 0;
        public List<string> tags = new List<string>();
    }
}