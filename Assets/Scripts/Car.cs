using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using PathCreation;
using UnityEngine;
using UnityEngine.UIElements;

public class Car : MonoBehaviour
{
    [SerializeField]
    private PathCreator pathCreator;
    
    public VertexPath Path => pathCreator.path;

    private void Start()
    {
        
    }
    
}
