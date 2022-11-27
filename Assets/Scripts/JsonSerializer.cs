using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Assets.Scripts.DTOs;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public class JsonSerializer : MonoBehaviour
    {
        private StreamWriter streamWriter;
        private int frameCount = 0;
        private bool firstLine = true;
        private SimulationChunkDto simulationChunk;

        private bool simulationOver = false;

        private void Start()
        {
            simulationChunk = new SimulationChunkDto();
            simulationChunk.frames = new List<FrameDto>();

            //streamWriter = new StreamWriter("C:/Users/Aleksander/Desktop/data.json");

            //InvokeRepeating("SendSimulationData", 0, 1f);

        }

        private void Update()
        {

            //if (simulationOver) return;

            var frameDto = GenerateFrameDto();
            if (frameCount % 10 == 0)
            {
                simulationChunk.frames.Add(frameDto);

            }
        }

        private FrameDto GenerateFrameDto()
        {
            var frame = new FrameDto();
            frame.frameNumber = frameCount;
            frame.cars = new List<CarDto>();

            List<Car> cars = FindObjectsOfType<Car>().ToList();


            foreach (var car in cars)
            {
                frame.cars.Add(car.GenerateCarDto());
            }

            frameCount++;
            return frame;
        }

        private void SendSimulationData()
        {
            StartCoroutine("SimluationChunkPostRequest");
            simulationChunk.frames.Clear();
        }

        private IEnumerator SimluationChunkPostRequest()
        {
            //Debug.Log("SENDING");

            var request = new UnityWebRequest("https://ctsbackend.bieda.it/api/simulation", "POST");
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("ApiKey", "1234");
            byte[] data = Encoding.UTF8.GetBytes(JsonUtility.ToJson(simulationChunk));
            request.uploadHandler = new UploadHandlerRaw(data);
            yield return request.SendWebRequest();

            Debug.Log("SENT");
        }

        private IEnumerator ComputePostRequest()
        {
            Debug.Log("SENDING COMPUTE SIGNAL");

            var request = new UnityWebRequest("https://ctscompms.bieda.it/api/process?settings_hash=biuro2137&map_hash=biuro2137", "POST");
            // request.SetRequestHeader("Content-Type", "application/json");
            // request.SetRequestHeader("ApiKey", "1234");
            // byte[] data = Encoding.UTF8.GetBytes(JsonUtility.ToJson(simulationChunk));
            // request.uploadHandler = new UploadHandlerRaw(data);
            yield return request.SendWebRequest();

            Debug.Log("COMPUTED");
        }
        void OnApplicationQuit()
        {
            //streamWriter.Write(JsonUtility.ToJson(simulationChunk));
            //streamWriter.Close();

            //StartCoroutine("ComputePostRequest");
        }
    }
}