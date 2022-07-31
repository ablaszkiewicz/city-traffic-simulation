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
        private int frame = 0;
        private bool firstLine = true;

        private void Start()
        {
            streamWriter = new StreamWriter("C:/Users/Aleksander/Desktop/data.json");
            streamWriter.WriteLine("[");
        }
        
        private void Update()
        {
            var dto = GenerateSimulationDto();

            if (firstLine)
            {
                streamWriter.Write($"{JsonUtility.ToJson(dto)}");
                firstLine = false;
            }
            else
            {
                streamWriter.WriteLine(",");
                streamWriter.Write($"{JsonUtility.ToJson(dto)}");
            }
        }

        private SimulationDto GenerateSimulationDto()
        {
            SimulationDto simulationDto = new SimulationDto();
            List<Car> cars = FindObjectsOfType<Car>().ToList();

            foreach (var car in cars)
            {
                simulationDto.carDtos.Add(car.GenerateCarDto());
            }

            simulationDto.frame = frame;
            frame++;
            return simulationDto;
        }

        void OnApplicationQuit()
        {
            streamWriter.Write("]");
            streamWriter.Close();
        }
    }
}