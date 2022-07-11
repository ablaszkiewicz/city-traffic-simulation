using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using PathCreation;
using UnityEditor;
using UnityEngine;

public class RoadGUI : MonoBehaviour
{

    void OnDrawGizmos()
    {
        var pathCreators = GetComponentsInChildren<PathCreator>();

        foreach (var pathCreator in pathCreators)
        {
            for (float i = 0f; i < 1f; i += 0.1f)
            {
                var point = pathCreator.path.GetPointAtTime(i);
                var direction = pathCreator.path.GetDirection(i);
                DrawArrow.ForGizmo(point, direction * 0.01f, Color.blue, 0.4f);
            }
        }
        
    }
}
