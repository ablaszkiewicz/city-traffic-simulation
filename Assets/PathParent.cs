using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using UnityEngine;

[Serializable]
public class PathsPair
{
    public PathParent nextPlannedPathParent;
    public PathCreator pathToGetThere;
}

public class PathParent : MonoBehaviour
{
    [SerializeField]
    private List<PathsPair> crossroadNextPathMap;

    [SerializeField]
    private bool requiresNextPlannedPath;

    public bool RequiresNextPlannedPath
    {
        get => requiresNextPlannedPath;
    }

    private VertexPath path;
    
    private void Awake()
    {
        if (!requiresNextPlannedPath)
        {
            var pathCreator = GetComponent<PathCreator>();
            path = pathCreator.path;
        }
    }
    
    public VertexPath GetPath(PathParent nextPart = null)
    {
        if (nextPart == null)
        {
            return path;
        }

        return crossroadNextPathMap.Where((pair => pair.nextPlannedPathParent == nextPart)).First()
            .pathToGetThere.path;
    }
}
