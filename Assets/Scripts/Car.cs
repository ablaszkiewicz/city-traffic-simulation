using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.DTOs;
using PathCreation;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Car : MonoBehaviour
{
    [SerializeField]
    private int id;

    private StateMachine stateMachine;
    private Engine engine;

    public bool IsMoving
    {
        get => engine.VelocityMultiplier > 0;
    }
    
    private void Start()
    {
        id = Random.Range(0, 100000);
        stateMachine = GetComponent<StateMachine>();
        engine = GetComponent<Engine>();
    }

    public CarDto GenerateCarDto()
    {
        CarDto dto = new CarDto();
        dto.id = id;
        dto.position.x = transform.position.x;
        dto.position.y = transform.position.z;
        dto.state = stateMachine.CurrentState.ToString();

        return dto;
    }
}
