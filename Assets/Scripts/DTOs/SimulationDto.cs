using System;
using System.Collections.Generic;

namespace Assets.Scripts.DTOs
{
    [Serializable]
    public class SimulationDto
    {
        public string settingsHash = "dupa";
        public string mapHash = "dupa";
        public List<CarDto> carDtos = new List<CarDto>();
        public string timestamp;
    }
}