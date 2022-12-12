using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.DTOs
{
    [Serializable]
    public class MapDto
    {
        public string hash = "crossroad";
        public List<RoadDto> roads = new List<RoadDto>();
    }
}