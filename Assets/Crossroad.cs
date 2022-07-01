using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using PathCreation;
using UnityEngine;

public class Crossroad : MonoBehaviour, IRoadElement
{
    private List<PathCreator> pathCreators;
    
    private void Awake()
    {
        pathCreators = GetComponentsInChildren<PathCreator>().ToList();
    }

    public List<PathCreator> GetPathCreators()
    {
        return pathCreators;
    }

    public RoadElementType GetRoadElementType() => RoadElementType.CROSSROAD;
}
