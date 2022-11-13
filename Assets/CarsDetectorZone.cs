using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using UnityEngine;

public class CarsDetectorZone : MonoBehaviour
{
    [SerializeField]
    private Material detectedMaterial;

    [SerializeField]
    private Material idleMaterial;

    [SerializeField]
    private PathCreator pathCreator;

    [SerializeField]
    private bool firstCarIsMoving;
    
    private List<Car> carsInside = new List<Car>();

    private MeshRenderer renderer;
    public Action OnFull;
    public Action OnEmpty;

    public bool FirstCarIsMoving => firstCarIsMoving;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    public void Update()
    {
        firstCarIsMoving = carsInside.Count >= 1 && carsInside[0].IsMoving;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<Car>())
        {
            var pathPlanner = other.gameObject.GetComponentInParent<PathPlanner>();

            Debug.Log(pathPlanner.gameObject.name);
            if (pathPlanner.IsCurrentOrNextPathCreator(pathCreator))
            {
                carsInside.Add(other.gameObject.GetComponentInParent<Car>());
            }
        }
        
        UpdateColor();
        FirePossibleEvents();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponentInParent<Car>())
        {
            var car = other.gameObject.GetComponentInParent<Car>();

            if (carsInside.Contains(car))
            {
                carsInside.Remove(other.gameObject.GetComponentInParent<Car>());
            }
        }
        
        UpdateColor();
        FirePossibleEvents();
    }

    public void FirePossibleEvents()
    {
        if (carsInside.Count == 0)
        {
            OnEmpty.Invoke();
        }
        else
        {
            OnFull.Invoke();
        }
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
