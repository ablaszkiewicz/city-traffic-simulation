using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using PathCreation;
using Unity.VisualScripting;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField, Range(0.0f, 5.0f)]
    private float minSpawnInterval;

    [SerializeField, Range(0.0f, 5.0f)]
    private float maxSpawnInterval;

    [SerializeField]
    private GameObject carPrefab;
    private void Start()
    {
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

        Spawn();
        StartCoroutine("RepeatingCoroutine");
    }

    private void Spawn()
    {
        var closest = FindClosestRoad();
        var car = Instantiate(carPrefab.gameObject, closest.GetPathCreators()[0].path.GetPoint(0), Quaternion.identity);

        car.GetComponent<PathPlanner>().Initialize(new List<IRoadElement> { closest });
    }

    private IRoadElement FindClosestRoad()
    {
        var pathCreators = FindObjectsOfType<PathCreator>().ToList();
        var sorted = pathCreators.OrderBy(pc => Vector3.Distance(transform.position, pc.path.GetPoint(0))).ToList();
        var closest = sorted.FirstOrDefault();

        return closest.GetComponentInParent<IRoadElement>();
    }
}
