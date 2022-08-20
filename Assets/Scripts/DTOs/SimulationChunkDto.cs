using System;
using System.Collections.Generic;

namespace Assets.Scripts.DTOs
{
    [Serializable]
    public class SimulationChunkDto
    {
        public string settingsHash = "dupa";
        public string mapHash = "dupa";
        public List<FrameDto> frames = new List<FrameDto>();
    } 
    
    [Serializable]
    public class FrameDto
    {
        public int frame;
        public List<CarDto> cars = new List<CarDto>();
    }
}