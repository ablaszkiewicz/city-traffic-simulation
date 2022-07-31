using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [SerializeField]
    private PathCreator attachedRoad;

    [SerializeField]
    private GameObject body;
    
    
    public void TurnOn()
    {
        body.SetActive(true);
    }

    public void TurnOff()
    {
        
        body.SetActive(false);
    }
}
