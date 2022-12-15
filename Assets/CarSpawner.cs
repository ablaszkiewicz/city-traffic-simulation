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
    private int spawnLimit;

    [SerializeField, Range(0.0f, 30.0f)]
    private float minSpawnInterval;

    [SerializeField, Range(0.0f, 30.0f)]
    private float maxSpawnInterval;
    [SerializeField]
    private bool randomizeCarsPath;
    [SerializeField]
    private bool useForcedPath;

    [SerializeField]
    private GameObject carPrefab;

    [SerializeField]
    private List<string> carTags;

    [SerializeField]
    private List<Road> finishRoads;

    private PathFinder pathFinder;
    private int spawnedInstances;

    [SerializeField]
    private List<IRoadElement> forcedPath;
    [SerializeField]
    private List<IRoadElement> computedPath;
    private void Start()
    {
        pathFinder = FindObjectOfType<PathFinder>();
        StartCoroutine("RepeatingCoroutine");
    }

    private IEnumerator RepeatingCoroutine()
    {
        float normalizedTime = 0;
        var spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / spawnInterval;
            yield return null;
        }

        if (useForcedPath)
        {
            SpawnForced();
        }
        else if (randomizeCarsPath)
        {
            SpawnRandom();
        }
        else
        {
            Spawn();
        }

        spawnedInstances++;

        if (spawnedInstances < spawnLimit)
        {
            StartCoroutine("RepeatingCoroutine");
        }
    }
    private void SpawnForced()
    {
        var closest = FindClosestRoad();
        var car = Instantiate(carPrefab.gameObject, closest.GetPathCreators()[0].path.GetPoint(0), Quaternion.identity);

        car.GetComponent<PathPlanner>().InitializeWithReadyPath(forcedPath);
        car.GetComponent<Car>().Initialize(carTags);
    }
    private void SpawnRandom()
    {
        var closest = FindClosestRoad();
        var car = Instantiate(carPrefab.gameObject, closest.GetPathCreators()[0].path.GetPoint(0), Quaternion.identity);

        car.GetComponent<PathPlanner>().Initialize(new List<IRoadElement>() { closest });
        car.GetComponent<Car>().Initialize(carTags);
    }

    private void Spawn()
    {
        var closest = FindClosestRoad();
        var car = Instantiate(carPrefab.gameObject, closest.GetPathCreators()[0].path.GetPoint(0), Quaternion.identity);

        var finishRoad = finishRoads[Random.Range(0, finishRoads.Count)];
        Debug.Log(closest.gameObject.name);
        Debug.Log(finishRoad.gameObject.name);
        var path = pathFinder.GetPath(closest, finishRoad);

        computedPath = path;

        car.GetComponent<PathPlanner>().InitializeWithReadyPath(path);
        car.GetComponent<Car>().Initialize(carTags);
    }

    private IRoadElement FindClosestRoad()
    {
        var pathCreators = FindObjectsOfType<PathCreator>().ToList();
        var sorted = pathCreators.OrderBy(pc => Vector3.Distance(transform.position, pc.path.GetPoint(0))).ToList();
        var closest = sorted.FirstOrDefault();

        return closest.GetComponentInParent<IRoadElement>();
    }

    private void DebugLoopPhantomTraffic()
    {
        var a = forcedPath[1];
        var b = forcedPath[2];
        var c = forcedPath[3];
        var d = forcedPath[4];


        for (int i = 0; i < 500; i++)
        {
            forcedPath.Add(a);
            forcedPath.Add(b);
            forcedPath.Add(c);
            forcedPath.Add(d);
        }
    }
}
