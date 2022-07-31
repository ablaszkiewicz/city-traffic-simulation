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
    public int ID => id;
    
    private StateMachine stateMachine;

    private void Start()
    {
        id = Random.Range(0, 100000);
        stateMachine = GetComponent<StateMachine>();
    }

    public CarDto GenerateCarDto()
    {
        CarDto dto = new CarDto();
        dto.ID = ID;
        dto.position.x = transform.position.x;
        dto.position.y = transform.position.z;
        dto.state = stateMachine.CurrentState.ToString();

        return dto;
    }
}
