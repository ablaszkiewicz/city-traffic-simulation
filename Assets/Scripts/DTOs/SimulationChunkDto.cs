using System;
using System.Collections.Generic;

namespace Assets.Scripts.DTOs
{
    [Serializable]
    public class SimulationChunkDto
    {
        public string settingsHash = "rozpierdol";
        public string mapHash = "rozpierdol";
        public List<FrameDto> frames = new List<FrameDto>();
    } 
    
    [Serializable]
    public class FrameDto
    {
        public int frameNumber;
        public List<CarDto> cars = new List<CarDto>();
    }
}