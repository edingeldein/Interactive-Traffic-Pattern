using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class Fly : MonoBehaviour
{ 
    // Navigation variables
    public Transform[] waypoints;
    private bool flying = false;
    public int DestPoint = 0;
    public Transform Destination;

    // Aircraft state variables
    public float speed = 5f;
    public float maxRotationDPS = 6f;
    public float standardBankAngle = 30f;
    private Vector3 currentHeading;
    private Vector3 up;
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
        Destination = waypoints[DestPoint];

        // Choose the next point in the array as the destination, cycling to start if necessary.
        DestPoint = (DestPoint + 1) % waypoints.Length;
    }

    void Move()
    {
        // destination = Vector3 of the target waypoint transform
        // Calculating the directional vector (heading) to the next waypoint from current aircraft.
        var heading = Destination.position - transform.position;
        var normalizedHeading = heading / heading.magnitude;

        // currentHeading = normalized instantaneous velocity vector of current aircraft.
        // If current heading isn't the same as the directional heading, turn towards the directional heading
        if (currentHeading != normalizedHeading)
        {
            currentAngularVelocity = maxRotationDPS * Time.deltaTime;
            Vector3 diff = normalizedHeading - currentHeading;

            if (diff.magnitude < 0.01f)
            {
                currentHeading = normalizedHeading.normalized;
            }
            else
            {
                Vector3 diffVeloc = diff * currentAngularVelocity;
                Vector3 newHeading = currentHeading + diffVeloc;
                currentHeading = newHeading.normalized;
            }            
        }

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
