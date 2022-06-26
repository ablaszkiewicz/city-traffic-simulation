using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using PathCreation;
using UnityEngine;

public class PathPlanner : MonoBehaviour
{
    [SerializeField]
    private List<PathParent> pathParents;

    private float cumulativeLengthOfAlreadyFinishedPaths = 0;

    private VertexPath currentPath;
    private int currentPathIndex = 0;
    private Engine engine;

    private void Start()
    {
        currentPath = pathParents[0].GetPath();
        
        Debug.Log($"Set current path to {currentPath}");
        engine = GetComponent<Engine>();
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
        currentPathIndex++;
        if (currentPathIndex == pathParents.Count)
        {
            currentPathIndex = 0;
        }

        var nextPathParent = pathParents[currentPathIndex];

        if (nextPathParent.RequiresNextPlannedPath)
        {
            var nextNextPathParent = pathParents[currentPathIndex + 1];
            currentPath = pathParents[currentPathIndex].GetPath(nextNextPathParent);
        }
        else
        {
            currentPath = pathParents[currentPathIndex].GetPath();
        }
        
        engine.ResetDistanceTravelledOnThisPath();
    }
    
}
