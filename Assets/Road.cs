using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using PathCreation;
using UnityEngine;

public class Road : IRoadElement
{
    private PathCreator pathCreator;
    
    private void Awake()
    {
        pathCreator = GetComponentInChildren<PathCreator>();
    }
    
    private void Start()
    {
        InitializeNextRoadElement();
    }

    private void InitializeNextRoadElement()
    {
        var points = RoadElementsManager.Instance.GetStartEndPoints(pathCreator);
        var nextRoadElement = RoadElementsManager.Instance.GetRoadElementStartingAtPoint(points.endPoint, this);

        if (nextRoadElement != null)
        {
            nextRoadElements.Add(nextRoadElement);
        }
    }
    
    
    public override List<PathCreator> GetPathCreators()
    {
        var list = new List<PathCreator>();
        list.Add(pathCreator);
        return list;
    }

    public override PathCreator GetPathCreatorToGetThrough(IRoadElement previous, IRoadElement next)
    {
        return pathCreator;
    }

    public override List<IRoadElement> GetNextRoadElements(IRoadElement previous)
    {
        return nextRoadElements;
    }

    public override List<RoadElementsPathCreatorTuple> GetMap()
    {
        throw new System.NotImplementedException();
    }
}
