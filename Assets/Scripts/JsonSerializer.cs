using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.DTOs;
using UnityEngine;

namespace Assets.Scripts
{
    public class JsonSerializer: MonoBehaviour
    {
        private StreamWriter streamWriter;
        private int frameCount = 0;
        private bool firstLine = true;
        private SimulationChunkDto simulationChunk;

        private void Start()
        {
            simulationChunk = new SimulationChunkDto();
            simulationChunk.frames = new List<FrameDto>();
            
            streamWriter = new StreamWriter("C:/Users/Aleksander/Desktop/data.json");
        }
        
        private void Update()
        {
            var frameDto = GenerateFrameDto();
            simulationChunk.frames.Add(frameDto);
        }

        private FrameDto GenerateFrameDto()
        {
            var frame = new FrameDto();
            frame.frame = frameCount;
            frame.cars = new List<CarDto>();
            
            List<Car> cars = FindObjectsOfType<Car>().ToList();
            
            
            foreach (var car in cars)
            {
                frame.cars.Add(car.GenerateCarDto());
            }

            frameCount++;
            return frame;
        }

        void OnApplicationQuit()
        {
            streamWriter.Write(JsonUtility.ToJson(simulationChunk));
            streamWriter.Close();
        }
    }
}