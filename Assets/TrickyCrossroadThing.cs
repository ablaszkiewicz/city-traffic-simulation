using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickyCrossroadThing : MonoBehaviour
{
    private CarsDetectorZone carsDetectorZone;
    private CrossroadBarrier barrier;

    
    private void Start()
    {
        carsDetectorZone = GetComponentInChildren<CarsDetectorZone>();
        barrier = GetComponentInChildren<CrossroadBarrier>();

        carsDetectorZone.OnFull += barrier.Lock;
        carsDetectorZone.OnEmpty += barrier.Unlock;
    }

    private void Update()
    {
        if (!carsDetectorZone.FirstCarIsMoving)
        {
            barrier.Unlock();
        }
    }
    
    private void PossibleLock()
    {

    }

    private void PossibleUnlock()
    {
        
    }
}
