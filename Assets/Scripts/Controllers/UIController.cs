using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Canvas MainCanvas;
    public CameraController CameraController;

    Button eastBtn;
    Button westBtn;
    Button mainBtn;

    Canvas spawnCanvas;
    Dictionary<string, Button> toggleDict;
    Toggle startTog;
    Toggle crosswindTog;
    Toggle downwindTog;
    Toggle baseTog;
    Toggle finalTog;

    // Start is called before the first frame update
    void Start()
    {
        eastBtn = GameObject.Find("btnEast").GetComponent<Button>();
        westBtn = GameObject.Find("btnWest").GetComponent<Button>();
        mainBtn = GameObject.Find("btnMain").GetComponent<Button>();

        Canvas spawnCanvas = GameObject.Find("SpawnCanvas").GetComponent<Canvas>();
        spawnCanvas.enabled = false;

        toggleDict = new Dictionary<string, Button>();
        toggleDict.Add("Start", GameObject.Find("btnStart").GetComponent<Button>());
        toggleDict.Add("Crosswind", GameObject.Find("btnCrosswind").GetComponent<Button>());
        toggleDict.Add("Downwind", GameObject.Find("btnDownwind").GetComponent<Button>());
        toggleDict.Add("Base", GameObject.Find("btnBase").GetComponent<Button>());
        toggleDict.Add("Final", GameObject.Find("btnFinal").GetComponent<Button>());

        CameraController = GameObject.Find("CameraPositions").GetComponent<CameraController>();
        DisableButton(mainBtn);

        eastBtn.onClick.AddListener(() => 
        {
            CameraController.SetTransform(CameraPosition.East);
            DisableButton(eastBtn);
            spawnCanvas.enabled = true;
        });
        westBtn.onClick.AddListener(() =>
        {
            CameraController.SetTransform(CameraPosition.West);
            DisableButton(westBtn);
            spawnCanvas.enabled = true;
        });
        mainBtn.onClick.AddListener(() =>
        {
            CameraController.SetTransform(CameraPosition.Main);
            DisableButton(mainBtn);
            spawnCanvas.enabled = false;
        });

    }

    void DisableButton(Button button)
    {
        mainBtn.interactable = westBtn.interactable = eastBtn.interactable = true;
        if (button == mainBtn)
            mainBtn.interactable = false;
        else if (button == eastBtn)
            eastBtn.interactable = false;
        else if (button == westBtn)
            westBtn.interactable = false;
    }

}
