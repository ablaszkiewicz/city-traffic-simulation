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

        private void Start()
        {
            streamWriter = new StreamWriter(Application.persistentDataPath + "/data.json");
            streamWriter.WriteLine("[");
        }
        
        private void Update()
        {
            var dto = GenerateSimulationDto();
            streamWriter.WriteLine($"{JsonUtility.ToJson(dto)},");
        }

        private SimulationDto GenerateSimulationDto()
        {
            SimulationDto simulationDto = new SimulationDto();
            List<Car> cars = FindObjectsOfType<Car>().ToList();

            foreach (var car in cars)
            {
                simulationDto.carDtos.Add(car.GenerateCarDto());
            }

            return simulationDto;
        }

        void OnApplicationQuit()
        {
            streamWriter.Close();
        }
    }
}