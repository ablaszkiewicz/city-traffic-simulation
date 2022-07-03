using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using PathCreation;
using UnityEngine;

public class Crossroad : MonoBehaviour, IRoadElement
{
    private List<PathCreator> pathCreators;
    private List<IRoadElement> nextRoadElements = new List<IRoadElement>();
    
    private void Awake()
    {
        pathCreators = GetComponentsInChildren<PathCreator>().ToList();
    }

    private void Start()
    {
        InitializeNextRoadElements();
    }

    public List<PathCreator> GetPathCreators()
    {
        return pathCreators;
    }

    public RoadElementType GetRoadElementType() => RoadElementType.CROSSROAD;

    public List<IRoadElement> GetNextRoadElements()
    {
        throw new System.NotImplementedException();
    }

    private void InitializeNextRoadElements()
    {
        foreach(var pathCreator in pathCreators)
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

            if (nextRoadElement != null)
            {
                nextRoadElements.Add(nextRoadElement);
            }
            else
            {
                Debug.Log($"Didn't find any matching next road elements for {pathCreator.gameObject.name}");
            }
            
        }
    }
}
