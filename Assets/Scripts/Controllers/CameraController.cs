using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera MainCamera;
    public int NumSteps = 100;
    public Transform CurrentTarget;
    public bool Move;
    Transform mainCameraTrans;
    Transform eastCameraTrans;
    Transform westCameraTrans;
    Vector3 step;
    int stepNum;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        mainCameraTrans = transform.Find("MainCameraPosition");
        eastCameraTrans = transform.Find("EastCameraPosition");
        westCameraTrans = transform.Find("WestCameraPosition");
        Move = false;
    }

    void Update()
    {
        if (!Move) return;

        MainCamera.transform.position = MainCamera.transform.position + step;
        MainCamera.transform.rotation = CurrentTarget.rotation;

        Move = !(stepNum >= NumSteps);
        stepNum++;
    }

    public void SetTransform(CameraPosition pos)
    {
        if (pos == CameraPosition.Main)
        {

            CurrentTarget = mainCameraTrans;
        }
        else if (pos == CameraPosition.East)
        {
            CurrentTarget = eastCameraTrans;
        }
        else
        {
            CurrentTarget = westCameraTrans;
        }

        Vector3 cameraPos = MainCamera.transform.position;
        Vector3 targetPos = CurrentTarget.position;
        Vector3 displacement = targetPos - cameraPos;
        step = displacement / NumSteps;
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

