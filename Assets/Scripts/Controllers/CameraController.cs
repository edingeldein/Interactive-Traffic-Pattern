using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera MainCamera;
    public Transform CurrentTarget;
    public bool Move;
    public float a = 9f;
    Transform mainCameraTrans;
    Transform eastCameraTrans;
    Transform westCameraTrans;
    float vMax;

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

        MainCamera.transform.position = CurrentTarget.position;
        MainCamera.transform.rotation = CurrentTarget.rotation;

        Move = (MainCamera.transform.position == CurrentTarget.position &&
            MainCamera.transform.rotation == CurrentTarget.rotation);
    }

    public void SetTransform(CameraPosition pos)
    {
        if (pos == CameraPosition.Main)
            CurrentTarget = mainCameraTrans;
        else if (pos == CameraPosition.East)
            CurrentTarget = eastCameraTrans;
        else
            CurrentTarget = westCameraTrans;

        Move = true;
    }

}

public enum CameraPosition
{
    Main,
    East,
    West
}

