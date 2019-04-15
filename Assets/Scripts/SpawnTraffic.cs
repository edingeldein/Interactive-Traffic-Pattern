using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnTraffic : MonoBehaviour, IPointerClickHandler
{

    public GameObject aircraftPrefab;
    private Transform[] leftPattern;
    private Transform[] rightPattern;

    // Start is called before the first frame update
    void Start()
    {
        leftPattern = GetPatternTransforms(PatternDirection.Left);
        rightPattern = GetPatternTransforms(PatternDirection.Right);
    }

    void SpawnAircraft()
    {
        var startPos = leftPattern[0].position + new Vector3(0f, 0f, 4f);
        var startRot = Quaternion.LookRotation(leftPattern[1].position - startPos, Vector3.up);
        var aircraft = Instantiate(aircraftPrefab, startPos, startRot);

        var aircraftFly = aircraft.GetComponent<Fly>();
        aircraftFly.waypoints = leftPattern;
        aircraftFly.BeginFlight();
    }

    private Transform[] GetPatternTransforms(PatternDirection dir)
    {
        string patternName = (dir == PatternDirection.Left) ? "LeftPattern" : "RightPattern";
        Transform tpTransform = transform.Find(patternName).transform;

        int numChildren = tpTransform.childCount;
        Transform[] patternWaypoints = new Transform[numChildren];
        for(int child = 0; child < numChildren; child++)
        {
            patternWaypoints[child] = tpTransform.GetChild(child);
        }

        return patternWaypoints;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SpawnAircraft();
    }
}

internal enum PatternDirection
{
    Left,
    Right
}

