using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.DTOs
{
    [Serializable]
    public class MapDto
    {
        public string hash = "fragment1";
        public List<RoadDto> roads = new List<RoadDto>();
    }
}