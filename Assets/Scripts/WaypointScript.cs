using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointScript : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"COLLISION {collision.collider.name}");
    }
}
