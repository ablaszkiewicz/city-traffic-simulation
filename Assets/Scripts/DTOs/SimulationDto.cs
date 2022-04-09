using System;
using System.Collections.Generic;

namespace Assets.Scripts.DTOs
{
    [Serializable]
    public class SimulationDto
    {
        public List<CarDto> carDtos = new List<CarDto>();
    }
}