using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class CrossroadBarrier : MonoBehaviour
{
    [SerializeField]
    private GameObject barrier;
    
    [SerializeField]
    private PathCreator pathCreator;

    public PathCreator PathCreator
    {
        get => pathCreator;
    }

    public void Lock()
    {
        barrier.SetActive(true);
    }

    public void Unlock()
    {
        barrier.SetActive(false);
    }
}
