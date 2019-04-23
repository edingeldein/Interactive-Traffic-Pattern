using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Transform Properties
    Transform mainCameraTrans;
    Transform eastCameraTrans;
    Transform westCameraTrans;
    Vector3 step;

    // Movement properties
    public int NumSteps = 100;
    public Transform CurrentTarget;
    public bool Move;
    int stepNum;
    float speed;

    // Camera poperties
    public Camera MainCamera;
    public float SizeTarget;
    float sizeStep;
    bool increase;

    // Start is called before the first frame update
    void Start()
    {
        mainCameraTrans = transform.Find("MainCameraPosition");
        eastCameraTrans = transform.Find("EastCameraPosition");
        westCameraTrans = transform.Find("WestCameraPosition");
        Move = false;
        SizeTarget = 80f;
    }

    void Update()
    {
        if (!Move) return;

        MainCamera.transform.position = MainCamera.transform.position + step;
        MainCamera.transform.rotation = CurrentTarget.rotation;
        MainCamera.orthographicSize += (increase) ? sizeStep : sizeStep * -1;

        Move = !(stepNum >= NumSteps);
        stepNum++;
    }

    public void SetTransform(CameraPosition pos)
    {
        if (pos == CameraPosition.Main)
        {
            CurrentTarget = mainCameraTrans;
            SizeTarget = 80f;
        }
        else if (pos == CameraPosition.East)
        {
            CurrentTarget = eastCameraTrans;
            SizeTarget = 50f;
        }
        else
        {
            CurrentTarget = westCameraTrans;
            SizeTarget = 50f;
        }

        Vector3 cameraPos = MainCamera.transform.position;
        Vector3 targetPos = CurrentTarget.position;
        Vector3 displacement = targetPos - cameraPos;
        step = displacement / NumSteps;

        sizeStep = (MainCamera.orthographicSize - SizeTarget) / NumSteps;
        if (sizeStep < 0)
        {
            increase = true;
            sizeStep *= -1;
        }
        else
            increase = false;

        stepNum = 0;

        Move = true;
    }

}

public enum CameraPosition
{
    Main,
    East,
    West
}

