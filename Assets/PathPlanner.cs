using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using PathCreation;
using UnityEngine;

public class PathPlanner : MonoBehaviour
{
    [SerializeField]
    private PathParent firstElement;
    
    [SerializeField]
    private List<PathParent> pathParents;

    private float cumulativeLengthOfAlreadyFinishedPaths = 0;

    [SerializeField]
    private List<VertexPath> localPaths = new List<VertexPath>();
    private VertexPath currentPath;
    private int currentPathIndex = 0;
    private Engine engine;

    private void Start()
    {
        engine = GetComponent<Engine>();
        
        RandomizePath();
        InitializeLocalPaths();
        ChangeToNextPath();
        
    }

    private void RandomizePath()
    {
        pathParents= new List<PathParent>();
        pathParents.Add(firstElement);

        var nextElementOptions = firstElement.GetPossibleExits(firstElement);
        Debug.Log(nextElementOptions.Count);
        //pathParents.Add(nextElementOptions[0]);
    }

    private void InitializeLocalPaths()
    {
        for (int i = 0; i < pathParents.Count; i++)
        {
            var pathParent = pathParents[i];

            if (pathParent.RequiresNextPlannedPath)
            {
                var previousPathParent = pathParents[i - 1];
                var nextPathParent = pathParents[i + 1];
                var path = pathParent.GetPath(previousPathParent, nextPathParent);
                localPaths.Add(path);

            }
            else
            {
                var path = pathParent.GetPath();
                localPaths.Add(path);
            }
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
        if (currentPathIndex == pathParents.Count)
        {
            currentPathIndex = 0;
        }
        
        // var previousPathParent = pathParents[currentPathIndex];
        // currentPathIndex++;
        // if (currentPathIndex == pathParents.Count)
        // {
        //     currentPathIndex = 0;
        // }
        //
        // var nextPathParent = pathParents[currentPathIndex];
        //
        //
        // if (nextPathParent.RequiresNextPlannedPath)
        // {
        //     var nextNextPathParent = pathParents[currentPathIndex + 1];
        //     currentPath = pathParents[currentPathIndex].GetPath(previousPathParent, nextNextPathParent);
        // }
        // else
        // {
        //     currentPath = pathParents[currentPathIndex].GetPath();
        // }
        
        engine.ResetDistanceTravelledOnThisPath();
    }
    
}
