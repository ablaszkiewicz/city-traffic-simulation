using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using UnityEngine;

public class LightsTimer : MonoBehaviour
{
    [SerializeField]
    private List<LightsTiming> lightsTiming;
    
    private List<TrafficLight> childrenTrafficLights;
    private int currentIndex = 0;
    private void Awake()
    {
        childrenTrafficLights = GetComponentsInChildren<TrafficLight>().ToList();
    }

    private void Start()
    {
        ProcessNextLightsTiming();
    }

    private void ProcessNextLightsTiming()
    {
        var currentLightsTiming = lightsTiming[currentIndex];
        currentIndex++;
        currentIndex %= lightsTiming.Count;

        ProcessLightsTiming(currentLightsTiming);
        Invoke("ProcessNextLightsTiming", currentLightsTiming.duration);
    }

    private void ProcessLightsTiming(LightsTiming lightsTiming)
    {
        TurnOffAllLights();
        
        lightsTiming.trafficLightsToTurnOn.ForEach(delegate(TrafficLight trafficLight)
        {
            trafficLight.TurnOn();
        });
    }

    private void TurnOffAllLights()
    {
        childrenTrafficLights.ForEach(delegate(TrafficLight trafficLight)
        {
            trafficLight.TurnOff();
        });
    }
}

[Serializable]
public class LightsTiming
{
    public List<TrafficLight> trafficLightsToTurnOn;
    public float duration;
}