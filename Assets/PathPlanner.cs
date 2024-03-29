using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using PathCreation;
using UnityEngine;

public class PathPlanner : MonoBehaviour
{
    [SerializeField]
    private bool destroyOnPathFinish;

    [SerializeField]
    private List<IRoadElement> defaultStartingRoadElements;

    [SerializeField]
    private List<IRoadElement> randomRoadElements = new List<IRoadElement>();

    [SerializeField]
    private List<IRoadElement> roadElements;

    private float cumulativeLengthOfAlreadyFinishedPaths = 0;

    private List<VertexPath> localPaths = new List<VertexPath>();

    [SerializeField]
    private List<PathCreator> localPathCreators = new List<PathCreator>();
    private VertexPath currentPath;
    private PathCreator pathCreator;
    private PathCreator currentPathCreator;

    [SerializeField]
    private int currentPathIndex = 0;
    private Engine engine;
    private bool isReady = false;
    [SerializeField]
    private bool destroyOnNextChange = false;

    private System.Random random = new System.Random();

    public bool IsReady
    {
        get => isReady;
    }

    // public VertexPath CurrentVertexPath
    // {
    //     get => currentPath;
    // }

    public PathCreator CurrentPathCreator
    {
        get => currentPathCreator;
    }

    private void Awake()
    {
        engine = GetComponent<Engine>();
    }

    private void Start()
    {
        // Invoke("InitializeWithDefaultRoadElements", 1);
    }

    public void InitializeWithReadyPath(List<IRoadElement> path)
    {
        this.randomRoadElements = path;
        try
        {
            InitializeLocalPaths();
        }
        catch (Exception e) { }
        ChangeToNextPath();
        isReady = true;
    }

    private void InitializeWithDefaultRoadElements()
    {
        RandomizePath(defaultStartingRoadElements);
        InitializeLocalPaths();
        ChangeToNextPath();
        isReady = true;
    }

    public void Initialize(List<IRoadElement> startingRoadElements)
    {
        RandomizePath(startingRoadElements);
        InitializeLocalPaths();
        ChangeToNextPath();
        isReady = true;
    }

    private void RandomizePath(List<IRoadElement> startingRoadElements)
    {
        var startingRoadElement = startingRoadElements[random.Next(startingRoadElements.Count)];

        randomRoadElements.Add(startingRoadElement);
        var currentElement = startingRoadElement;
        IRoadElement previous = null;

        while (true && randomRoadElements.Count < 100)
        {
            var possibleNextRoadElements = currentElement.GetNextRoadElements(previous);

            if (possibleNextRoadElements.Count == 0)
            {
                return;
            }

            int randomIndex = random.Next(possibleNextRoadElements.Count);
            previous = currentElement;
            currentElement = possibleNextRoadElements[randomIndex];
            randomRoadElements.Add(currentElement);
        }
    }

    private void InitializeLocalPaths()
    {
        roadElements = randomRoadElements;

        for (int i = 0; i < roadElements.Count; i++)
        {
            var previousRoadElement = i == 0 ? null : roadElements[i - 1];
            var currentRoadElement = roadElements[i];
            var nextRoadElement = i >= roadElements.Count - 1 ? null : roadElements[i + 1];


            var pathCreator = currentRoadElement.GetPathCreatorToGetThrough(previousRoadElement, nextRoadElement);
            localPaths.Add(pathCreator.path);
            localPathCreators.Add(pathCreator);
        }
    }

    public Vector3 GetPointAtDistance(float distance)
    {
        if (distance > currentPath.length)
        {
            if (currentPathIndex == localPaths.Count && destroyOnPathFinish)
            {
                Destroy(gameObject);
            }

            ChangeToNextPath();
            return currentPath.GetPointAtDistance(0);
        }
        return currentPath.GetPointAtDistance(distance);
    }

    public Quaternion GetRotationAtDistance(float distance)
    {
        return currentPath.GetRotationAtDistance(distance);
    }

    private void ChangeToNextPath()
    {
        if (destroyOnNextChange)
        {
            Debug.Log("Destroying");
            Destroy(gameObject);
            return;
        }

        currentPath = localPaths[currentPathIndex];
        currentPathCreator = localPathCreators[currentPathIndex];
        currentPathIndex++;
        if (currentPathIndex == roadElements.Count)
        {
            destroyOnNextChange = true;
        }

        engine.ResetDistanceTravelledOnThisPath();
    }

    public bool IsCurrentOrNextPathCreator(PathCreator pathCreator)
    {
        if (currentPathCreator == pathCreator)
        {
            return true;
        }

        if (localPathCreators[currentPathIndex] == pathCreator)
        {
            return true;
        }

        return false;
    }
}
