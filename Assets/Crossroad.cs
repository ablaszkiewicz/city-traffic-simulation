using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using PathCreation;
using UnityEngine;

[Serializable]
public class RoadElementsPathCreatorTuple
{
    public IRoadElement previous;
    public PathCreator pathCreator;
    public IRoadElement next;
}

public class Crossroad : IRoadElement
{
    private List<PathCreator> pathCreators;
    
    private void Awake()
    {
        pathCreators = GetComponentsInChildren<PathCreator>().ToList();
    }

    private void Start()
    {
        InitializePreviousAndNextRoadElements();
        InitializeGetThroughMap();
    }

    public override List<PathCreator> GetPathCreators()
    {
        return pathCreators;
    }
    
    [SerializeField]
    private List<RoadElementsPathCreatorTuple> pathCreatorsMap = new List<RoadElementsPathCreatorTuple>(); 
    public override List<IRoadElement> GetNextRoadElements(IRoadElement previous)
    {
        return pathCreatorsMap.Where(tuple => tuple.previous == previous).Select(tuple => tuple.next).ToList();
    }

    private void InitializePreviousAndNextRoadElements()
    {
        foreach(var pathCreator in pathCreators)
        {
            var points = RoadElementsManager.Instance.GetStartEndPoints(pathCreator);
            var nextRoadElement = RoadElementsManager.Instance.GetRoadElementStartingAtPoint(points.endPoint, this);
            var previousRoadElement = RoadElementsManager.Instance.GetRoadElementEndingAtPoint(points.startPoint, this);
            
            
            if (previousRoadElement != null)
            {
                previousRoadElements.Add(previousRoadElement);
            }
            
            if (nextRoadElement != null)
            {
                nextRoadElements.Add(nextRoadElement);
            }
        }

        nextRoadElements = nextRoadElements.Distinct().ToList();
        previousRoadElements = previousRoadElements.Distinct().ToList();
    }

    private void InitializeGetThroughMap()
    {
        foreach(var pathCreator in pathCreators)
        {
            var points = RoadElementsManager.Instance.GetStartEndPoints(pathCreator);
            var nextRoadElement = RoadElementsManager.Instance.GetRoadElementStartingAtPoint(points.endPoint, this);
            var previousRoadElement = RoadElementsManager.Instance.GetRoadElementEndingAtPoint(points.startPoint, this);
            
            if (previousRoadElement != null && nextRoadElement != null)
            {
                var tuple = new RoadElementsPathCreatorTuple()
                    {previous = previousRoadElement, pathCreator = pathCreator, next = nextRoadElement};
                
                pathCreatorsMap.Add(tuple);
            }
        }
    }

    public override PathCreator GetPathCreatorToGetThrough(IRoadElement previous, IRoadElement next)
    {
        return pathCreatorsMap.FirstOrDefault(tuple => tuple.previous == previous && tuple.next == next).
            pathCreator;
    }

    public override List<RoadElementsPathCreatorTuple> GetMap()
    {
        return pathCreatorsMap;
    }
}
