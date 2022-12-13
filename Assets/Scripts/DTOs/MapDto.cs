using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.DTOs
{
    [Serializable]
    public class MapDto
    {
        public string hash = "roundabout";
        public List<RoadDto> roads = new List<RoadDto>();
    }
}