using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnTraffic : MonoBehaviour //, IPointerClickHandler
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

    public void SpawnAircraft(string location)
    {
        Vector3 startPos = new Vector3(0f,0f,0f);
        int startIndex = 0;
        foreach(var trans in leftPattern)
        {
            if (trans.name.Equals(location))
            {
                startPos = trans.position + new Vector3(0f, 0.5f, 2f);
                break;
            }
            startIndex++;
        }

        if (startPos == null)
            return;
        var startRot = Quaternion.LookRotation(leftPattern[(startIndex + 1) % leftPattern.Length].position - startPos, Vector3.up);
        var aircraft = Instantiate(aircraftPrefab, startPos, startRot);
        aircraft.GetComponent<Fly>().DestPoint = startIndex;
        aircraft.GetComponent<Fly>().Destination = leftPattern[startIndex];

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

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    SpawnAircraft();
    //}
}

internal enum PatternDirection
{
    Left,
    Right
}

