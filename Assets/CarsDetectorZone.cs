using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsDetectorZone : MonoBehaviour
{
    [SerializeField]
    private Material detectedMaterial;

    [SerializeField]
    private Material idleMaterial;

    private List<Car> carsInside = new List<Car>();

    private MeshRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<Car>())
        {
            carsInside.Add(other.gameObject.GetComponentInParent<Car>());
        }
        
        UpdateColor();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponentInParent<Car>())
        {
            carsInside.Remove(other.gameObject.GetComponentInParent<Car>());
        }
        
        UpdateColor();
    }

    private void UpdateColor()
    {
        if (carsInside.Count == 0)
        {
            renderer.material = idleMaterial;
        }
        else
        {
            renderer.material = detectedMaterial;
        }
    }
}
