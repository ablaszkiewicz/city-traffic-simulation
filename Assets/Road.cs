using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using PathCreation;
using UnityEngine;

public class Road : MonoBehaviour, IRoadElement
{
    private PathCreator pathCreator;
    
    private void Awake()
    {
        pathCreator = GetComponentInChildren<PathCreator>();
    }
    
    private void Start()
    {
        var points = RoadElementsManager.Instance.GetStartEndPoints(pathCreator);
        
        var nextPathCreator = RoadElementsManager.Instance.GetPathCreatorWhichStartsAtPoint(points.endPoint);
        Debug.Log(nextPathCreator.gameObject.name);
        
        var nextRoadElement = RoadElementsManager.Instance.GetRoadElementWhichStartsAtPoint(points.endPoint);
        Debug.Log(nextRoadElement.GetRoadElementType());

    }
    
    public RoadElementType GetRoadElementType() => RoadElementType.ROAD;
    
    public List<PathCreator> GetPathCreators()
    {
        var list = new List<PathCreator>();
        list.Add(pathCreator);
        return list;
    }
}
