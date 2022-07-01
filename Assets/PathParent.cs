using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PathCreation;
using UnityEngine;

[Serializable]
public class ParentsPathCreatorTuple
{
    public PathParent previousPathParent;
    public PathCreator pathCreatorToGetThere;
    public PathParent nextPathParent;
}

[Serializable]
public class PathCreatorPointsTuple
{
    public PathCreator pathCreator;
    public Vector3 startPoint;
    public Vector3 endPoint;
}

[Serializable]
public class PathCreatorPointPair
{
    public PathCreator pathCreator;
    public Vector3 point;
}

public class PathParent : MonoBehaviour
{
    [SerializeField]
    private List<ParentsPathCreatorTuple> pathMap;

    [SerializeField]
    private bool requiresNextPlannedPath;

    private PathCreator pathCreator;
    
    private List<PathCreator> outsidePathCreators;
    private List<PathCreator> childrenPathCreators = new List<PathCreator>();

    [SerializeField]
    private List<PathCreatorPointsTuple> localPathsTuples = new List<PathCreatorPointsTuple>();
    
    [SerializeField]
    private List<PathCreatorPointPair> outsidePathPairs = new List<PathCreatorPointPair>();
    
    public bool RequiresNextPlannedPath
    {
        get => requiresNextPlannedPath;
    }

    private VertexPath path;
    
    private void Awake()
    {
        path = GetComponentInChildren<PathCreator>().path;
        // if (!requiresNextPlannedPath)
        // {
        //     pathCreator = GetComponentInChildren<PathCreator>();
        //     path = pathCreator.path;
        //     return;
        // }
        
        // InitializePathCreators();
        // DetermineConnectionPoints();
        // FindMatchingPaths();
    }

    public VertexPath GetPath(PathParent previousPart = null,  PathParent nextPart = null)
    {
        if (previousPart == null || nextPart == null)
        {
            return path;
        }

        return pathMap.Where(pair => pair.previousPathParent == previousPart && pair.nextPathParent == nextPart).First()
            .pathCreatorToGetThere.path;
    }

    private void SolveForSingleRoad()
    {
        outsidePathCreators = FindObjectsOfType<PathCreator>().Where(p => !childrenPathCreators.Contains(p)).ToList();
        var tuple = ConvertPathCreatorToTuple(pathCreator);

        var endPoint = tuple.endPoint;
    }
    
    private void InitializePathCreators()
    {
        childrenPathCreators = GetComponentsInChildren<PathCreator>().ToList();
        var allPathCreators = FindObjectsOfType<PathCreator>();

        outsidePathCreators = allPathCreators.Where(p => !childrenPathCreators.Contains(p)).ToList();
        
        foreach (var outsidePathCreator in outsidePathCreators)
        {
            var tuple = ConvertPathCreatorToTuple(outsidePathCreator);
            
            outsidePathPairs.Add(new PathCreatorPointPair() {pathCreator = outsidePathCreator, point = tuple.startPoint});
            outsidePathPairs.Add(new PathCreatorPointPair() {pathCreator = outsidePathCreator, point = tuple.endPoint});
            
        }
    }

    private void DetermineConnectionPoints()
    {
        foreach (var childrenPathCreator in childrenPathCreators)
        {
            localPathsTuples.Add(ConvertPathCreatorToTuple(childrenPathCreator));
        }
    }

    private void FindMatchingPaths()
    {
        var outsidePathObjects = outsidePathCreators.Select(p => ConvertPathCreatorToTuple(p)).ToList();
        
        foreach (var localPath in localPathsTuples)
        {
            var matchingPathPairToStartPoint = outsidePathPairs.OrderBy(pair => Vector3.Distance(localPath.startPoint, pair.point))
                .FirstOrDefault();
            
            var matchingPathPairToEndPoint = outsidePathPairs.OrderBy(pair => Vector3.Distance(localPath.endPoint, pair.point))
                .FirstOrDefault();
            
            //Debug.Log($"Matched {localPath.pathCreator.gameObject.name} to {matchingPathPairToStartPoint.pathCreator.gameObject.name} and {matchingPathPairToEndPoint.pathCreator.gameObject.name} ");


            var previousPathParent = GetPathParentFromPathCreator(matchingPathPairToStartPoint.pathCreator);
            var nextPathParent = GetPathParentFromPathCreator(matchingPathPairToEndPoint.pathCreator);
            
            pathMap.Add(new ParentsPathCreatorTuple(){previousPathParent = previousPathParent, pathCreatorToGetThere = localPath.pathCreator, nextPathParent = nextPathParent});
        }
        
    }

    private PathCreatorPointsTuple ConvertPathCreatorToTuple(PathCreator pathCreator)
    {
        var startPosition = pathCreator.transform.TransformPoint(pathCreator.bezierPath.GetPoint(0));
        var endPosition =
            pathCreator.transform.TransformPoint(
                pathCreator.bezierPath.GetPoint(pathCreator.bezierPath.NumPoints - 1));

        return new PathCreatorPointsTuple() {pathCreator = pathCreator, startPoint = startPosition, endPoint = endPosition};
    }

    private PathParent GetPathParentFromPathCreator(PathCreator pathCreator)
    {
        return pathCreator.gameObject.GetComponentInParent<PathParent>();
    }

    public List<PathParent> GetPossibleExits(PathParent source)
    {
        return pathMap.Where(tuple => tuple.previousPathParent == source).Select(tuple => tuple.nextPathParent).ToList();
    }
}
