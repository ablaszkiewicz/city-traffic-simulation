using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using PathCreation;
using UnityEngine;

public class Road : MonoBehaviour, IRoadElement
{
    private PathCreator pathCreator;
    private List<IRoadElement> nextRoadElements = new List<IRoadElement>();
    
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

        // if (nextRoadElement is Crossroad)
        // {
        //     Debug.Log($"Next road element is: {((Crossroad)nextRoadElement).gameObject.name}");
        // }
        // else if (nextRoadElement is Road)
        // {
        //     Debug.Log($"Next road element is: {((Road)nextRoadElement).gameObject.name}");
        // }
        
        nextRoadElements.Add(nextRoadElement);
    }
    
    public RoadElementType GetRoadElementType() => RoadElementType.ROAD;
    
    public List<PathCreator> GetPathCreators()
    {
        var list = new List<PathCreator>();
        list.Add(pathCreator);
        return list;
    }

    public List<IRoadElement> GetNextRoadElements()
    {
        return nextRoadElements;
    }
}
