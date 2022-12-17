using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Assets.Scripts.DTOs;
using UnityEngine;
using UnityEngine.Networking;
using PathCreation;

namespace Assets.Scripts
{
    public class JsonSerializer : MonoBehaviour
    {
        private StreamWriter streamWriter;
        private int frameCount = 0;
        private bool firstLine = true;
        private SimulationChunkDto simulationChunk;
        private List<PathCreator> roadsPathCreators;

        private bool simulationOver = false;
        private int rateLimitterCounter = 0;
        private int rateLimitter = 40;
        private MapDto mapDto;

        [SerializeField]
        private string settingsHash;


        [SerializeField]
        private string mapHash;

        [SerializeField]
        private SettingsScriptableObject settingsScriptableObject;


        public SettingsScriptableObject SettingsScriptableObject => settingsScriptableObject;

        private void Start()
        {
            simulationChunk = new SimulationChunkDto();
            simulationChunk.settingsHash = settingsHash;
            simulationChunk.mapHash = mapHash;
            simulationChunk.frames = new List<FrameDto>();

            //streamWriter = new StreamWriter("C:/Users/Aleksander/Desktop/data.json");

            InvokeRepeating("SendSimulationData", 0, 1f);
            roadsPathCreators = FindObjectsOfType<PathCreator>().ToList();
            mapDto = SerializeRoads();
            StartCoroutine("SendMapDtoPostRequest");
        }

        private void Update()
        {

            //if (simulationOver) return;

            rateLimitterCounter++;
            if (rateLimitterCounter % rateLimitter == 0)
            {
                var frameDto = GenerateFrameDto();
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

        private IEnumerator SendMapDtoPostRequest()
        {
            //Debug.Log("SENDING");

            var request = new UnityWebRequest("https://ctsbackend.bieda.it/api/maps", "POST");
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("ApiKey", "1234");
            byte[] data = Encoding.UTF8.GetBytes(JsonUtility.ToJson(mapDto));
            request.uploadHandler = new UploadHandlerRaw(data);
            yield return request.SendWebRequest();

            Debug.Log("SENT MAP");
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

            var request = new UnityWebRequest("https://ctscompms.bieda.it/api/process?settings_hash=phantomTraffic1&map_hash=phantomTraffic1", "POST");
            yield return request.SendWebRequest();

            Debug.Log("COMPUTED");
        }
        void OnApplicationQuit()
        {
            //streamWriter.Write(JsonUtility.ToJson(simulationChunk));
            //streamWriter.Close();

            //StartCoroutine("ComputePostRequest");
        }

        private MapDto SerializeRoads()
        {
            var map = new MapDto();

            roadsPathCreators.ForEach(pathCreator =>
            {
                var numberOfPoints = pathCreator.bezierPath.NumPoints;
                var points = new List<Vector2>();

                for (int i = 0; i < numberOfPoints; i++)
                {
                    var point = pathCreator.bezierPath.GetPoint(i);
                    var transformed = pathCreator.transform.TransformPoint(point);
                    points.Add(new Vector2(transformed.x, transformed.z));
                }

                var road = new RoadDto();
                road.points = points;
                map.roads.Add(road);
            });


            map.hash = mapHash;
            return map;
        }
    }
}