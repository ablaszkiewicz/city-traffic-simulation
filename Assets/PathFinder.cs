using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System.Linq;


public class PathFinder : MonoBehaviour
{
    private void Start()
    {
        //Invoke("GetPath", 1.0f);
    }

    public List<IRoadElement> GetPath(IRoadElement start, IRoadElement end)
    {
        var path = GetPathRecursive(null, start, end, new List<IRoadElement>());

        if (path == null)
        {
            throw new System.Exception("Path not found");
        }
        path.Insert(0, start);
        return path;
    }

    public List<IRoadElement> GetPathRecursive(IRoadElement previous, IRoadElement current, IRoadElement finish, List<IRoadElement> path)
    {
        if (current == finish)
        {
            return path;
        }

        var nextRoadElements = current.GetNextRoadElements(previous);

        foreach (var nextRoadElement in nextRoadElements)
        {
            if (path.Contains(nextRoadElement))
            {
                continue;
            }

            var newPath = new List<IRoadElement>(path);
            newPath.Add(nextRoadElement);

            var result = GetPathRecursive(current, nextRoadElement, finish, newPath);

            if (result != null)
            {
                return result;
            }
        }

        return null;
    }
}
