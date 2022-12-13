using System;
using System.Collections.Generic;

namespace Assets.Scripts.DTOs
{
    [Serializable]
    public class SimulationChunkDto
    {
        public string settingsHash = "asd";
        public string mapHash = "asd";
        public string runId = "1";
        public List<FrameDto> frames = new List<FrameDto>();
    }

    [Serializable]
    public class FrameDto
    {
        public int frameNumber;
        public List<CarDto> cars = new List<CarDto>();
    }
}