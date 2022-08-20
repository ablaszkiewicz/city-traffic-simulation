using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using PathCreation;
using UnityEngine;

public class PathPlanner : MonoBehaviour
{
    [SerializeField]
    private List<IRoadElement> startingRoadElements;
    
    [SerializeField]
    private List<IRoadElement> randomRoadElements = new List<IRoadElement>();
    
    [SerializeField]
    private List<IRoadElement> roadElements;

    private float cumulativeLengthOfAlreadyFinishedPaths = 0;

    [SerializeField]
    private List<VertexPath> localPaths = new List<VertexPath>();
    private VertexPath currentPath;
    private int currentPathIndex = 0;
    private Engine engine;
    private bool isReady = false;

    private System.Random random = new System.Random();

    public bool IsReady
    {
        get => isReady;
    }

    private void Start()
    {
        engine = GetComponent<Engine>();
        
        Invoke("Initialize", 1);
    }

    private void Initialize()
    {
        RandomizePath();
        InitializeLocalPaths();
        ChangeToNextPath();
        isReady = true;
    }

    private void RandomizePath()
    {
        var startingRoadElement = startingRoadElements[random.Next(startingRoadElements.Count)];
        
        randomRoadElements.Add(startingRoadElement);
        var currentElement = startingRoadElement;
        IRoadElement previous = null;
        
        while(true)
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
        }
    }
    
    public Vector3 GetPointAtDistance(float distance)
    {


        if (distance > currentPath.length)
        {
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
        currentPath = localPaths[currentPathIndex];
        currentPathIndex++;
        if (currentPathIndex == roadElements.Count)
        {
            currentPathIndex = 0;
        }

        engine.ResetDistanceTravelledOnThisPath();
    }
    
}
