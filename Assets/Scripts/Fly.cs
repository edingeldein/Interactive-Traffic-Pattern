using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class Fly : MonoBehaviour
{
    public float speed = 5f;
    public float maxRotation = 6f;
    //public float minDist = 0.05f;
    public Transform[] waypoints;

    private bool flying = false;
    private int destPoint = 0;
    private Vector3 destination;
    private Vector3 currentHeading;
    private float currentAngularVelocity;
    private float remainingDistance;

    public void BeginFlight()
    {
        if (waypoints.Length == 0)
            return;
        flying = true;
        GoToNextPoint();
    }

    void GoToNextPoint()
    {
        // Returns if no points have been set up
        if (waypoints.Length == 0 || !flying)
            return;

        // Set the agent to go to the currently selected destination.
        destination = waypoints[destPoint].position;

        // Choose the next point in the array as the destination, cycling to start if necessary.
        destPoint = (destPoint + 1) % waypoints.Length;
    }

    void Move()
    {
        // destination = Vector3 of the target waypoint transform
        // Calculating the directional vector (heading) to the next waypoint from current aircraft.
        var heading = destination - transform.position;
        var normalizedHeading = heading / heading.magnitude;

        // currentHeading = normalized instantaneous velocity vector of current aircraft.
        // If current heading isn't the same as the directional heading, turn towards the directional heading
        if (currentHeading != normalizedHeading)
            currentHeading = currentHeading.TurnTowards(normalizedHeading, maxRotation * Time.deltaTime);

        // Rotate the model to face current velocity heading
        transform.rotation = Quaternion.LookRotation(currentHeading);
        // Clamp the magnitude of velocity vector to max speed
        var velocity = Vector3.ClampMagnitude(currentHeading * speed * Time.deltaTime, speed);
        // Move the aircraft in worldspace
        transform.Translate(velocity, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Aircraft trigger: {other.name}");
        GoToNextPoint();
    }

    void Update()
    {
        if (flying)
            Move();
    }
}
