using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using PathCreation;
using Unity.VisualScripting;
using UnityEngine;

public class PathCreatorStartEndPoints
{
    public PathCreator pathCreator;
    public Vector3 startPoint;
    public Vector3 endPoint;
}

public class RoadElementsManager : MonoBehaviour
{
    private static RoadElementsManager _instance;
    public static RoadElementsManager Instance { get { return _instance; } }
    
    private static List<Road> roads;
    private static List<Crossroad> crossroads;

    private void Awake()
    {
        roads = FindObjectsOfType<Road>().ToList();
        crossroads = FindObjectsOfType<Crossroad>().ToList();
        
        InitializeSingleton();
    }
    
    private void InitializeSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public IRoadElement GetRoadElementWhichStartsAtPoint(Vector3 point)
    {
        return GetRoadElementFromPathCreator(GetPathCreatorWhichStartsAtPoint(point));
    }

    public PathCreator GetPathCreatorWhichStartsAtPoint(Vector3 point)
    {
        var crossroadsPathCreators = crossroads
            .Select(crossroad => crossroad.GetComponentsInChildren<PathCreator>()).SelectMany(x=>x).ToList();
        
        var startEndPoints = crossroadsPathCreators.Select(GetStartEndPoints).ToList();
        var closestStartEndPoint = startEndPoints.OrderBy(tuple => Vector3.Distance(tuple.startPoint, point)).FirstOrDefault();

        return closestStartEndPoint.pathCreator;
    }
    
    public  PathCreatorStartEndPoints GetStartEndPoints(PathCreator pathCreator)
    {
        var startPosition = pathCreator.transform.TransformPoint(pathCreator.bezierPath.GetPoint(0));
        var endPosition =
            pathCreator.transform.TransformPoint(
                pathCreator.bezierPath.GetPoint(pathCreator.bezierPath.NumPoints - 1));

        return new PathCreatorStartEndPoints() {pathCreator = pathCreator, startPoint = startPosition, endPoint = endPosition};
    }

    public IRoadElement GetRoadElementFromPathCreator(PathCreator pathCreator)
    {
        return pathCreator.GetComponentInParent<IRoadElement>();
    }
}
