using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Canvas MainCanvas;
    public Canvas EastCanvas;
    public Canvas WestCanvas;
    public CameraController CameraController;

    // Start is called before the first frame update
    void Start()
    {
        var eastBtn = GameObject.Find("btnEast").GetComponent<Button>();
        var westBtn = GameObject.Find("btnWest").GetComponent<Button>();
        var mainBtn = GameObject.Find("btnMain").GetComponent<Button>();
        CameraController = GameObject.Find("CameraPositions").GetComponent<CameraController>();

        eastBtn.onClick.AddListener(() => CameraController.SetTransform(CameraPosition.East));
        westBtn.onClick.AddListener(() => CameraController.SetTransform(CameraPosition.West));
        mainBtn.onClick.AddListener(() => CameraController.SetTransform(CameraPosition.Main));
    }

}
