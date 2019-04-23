using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnTraffic : MonoBehaviour
{

    public GameObject aircraftPrefab;
    private Transform[] patternWaypoints;

    // Start is called before the first frame update
    void Start()
    {
        patternWaypoints = GetPatternTransforms();
    }

    public void SpawnAircraft(string location)
    {
        Vector3 startPos = new Vector3(0f,0f,0f);
        int startIndex = 0;
        foreach(var trans in patternWaypoints)
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
        var startRot = Quaternion.LookRotation(patternWaypoints[(startIndex + 1) % patternWaypoints.Length].position - startPos, Vector3.up);
        var aircraft = Instantiate(aircraftPrefab, startPos, startRot);
        aircraft.GetComponent<Fly>().DestPoint = startIndex;
        aircraft.GetComponent<Fly>().Destination = patternWaypoints[startIndex];

        var aircraftFly = aircraft.GetComponent<Fly>();
        aircraftFly.waypoints = patternWaypoints;
        aircraftFly.BeginFlight();
    }

    private Transform[] GetPatternTransforms()
    {
        Transform tpTransform = transform.Find("Pattern").transform;

        int numChildren = tpTransform.childCount;
        Transform[] patternWaypoints = new Transform[numChildren];
        for(int child = 0; child < numChildren; child++)
        {
            patternWaypoints[child] = tpTransform.GetChild(child);
        }

        return patternWaypoints;
    }
}

