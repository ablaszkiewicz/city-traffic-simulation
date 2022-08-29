using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using PathCreation;
using Unity.VisualScripting;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject carPrefab;
    private void Start()
    {
        InvokeRepeating("Spawn", 0.1f, 1f);
        //Invoke("Spawn", 1f);
    }

    private void Spawn()
    {
        var closest = FindClosestRoad();
        var car = Instantiate(carPrefab.gameObject, closest.transform.position, Quaternion.identity);
        
        car.GetComponent<PathPlanner>().Initialize(new List<IRoadElement> {closest});
    }

    private IRoadElement FindClosestRoad()
    {
        var pathCreators = FindObjectsOfType<PathCreator>().ToList();
        var sorted = pathCreators.OrderBy(pc => Vector3.Distance(transform.position, pc.path.GetPoint(0))).ToList();
        var closest = sorted.FirstOrDefault();
        
        return closest.GetComponentInParent<IRoadElement>();
    }
}
