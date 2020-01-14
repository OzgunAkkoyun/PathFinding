using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoadData
{
    public Vector3 coordinate;
    public bool onlyRoad;
    public bool isIfState;
    public bool isBarrier;
    

    public RoadData(Vector3 coordinate,bool onlyRoad, bool isIfState, bool isBarrier)
    {
        this.coordinate = coordinate;
        this.onlyRoad = onlyRoad;
        this.isIfState = isIfState;
        this.isBarrier = isBarrier;
    }
}