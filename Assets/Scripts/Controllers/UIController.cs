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
    Dictionary<string, Button> spawnBtnDict;

    SpawnTraffic currentPattern;

    // Start is called before the first frame update
    void Start()
    {
        eastBtn = GameObject.Find("btnEast").GetComponent<Button>();
        westBtn = GameObject.Find("btnWest").GetComponent<Button>();
        mainBtn = GameObject.Find("btnMain").GetComponent<Button>();

        Canvas spawnCanvas = GameObject.Find("SpawnCanvas").GetComponent<Canvas>();
        spawnCanvas.enabled = false;

        spawnBtnDict = new Dictionary<string, Button>();
        spawnBtnDict.Add("Start", GameObject.Find("btnStart").GetComponent<Button>());
        spawnBtnDict.Add("Crosswind", GameObject.Find("btnCrosswind").GetComponent<Button>());
        spawnBtnDict.Add("Downwind", GameObject.Find("btnDownwind").GetComponent<Button>());
        spawnBtnDict.Add("Base", GameObject.Find("btnBase").GetComponent<Button>());
        spawnBtnDict.Add("Final", GameObject.Find("btnFinal").GetComponent<Button>());

        CameraController = GameObject.Find("CameraPositions").GetComponent<CameraController>();
        DisableButton(mainBtn);

        eastBtn.onClick.AddListener(() => 
        {
            CameraController.SetTransform(CameraPosition.East);
            DisableButton(eastBtn);
            spawnCanvas.enabled = true;
            currentPattern = GameObject.Find("17L/35R").GetComponent<SpawnTraffic>();
        });
        westBtn.onClick.AddListener(() =>
        {
            CameraController.SetTransform(CameraPosition.West);
            DisableButton(westBtn);
            spawnCanvas.enabled = true;
            currentPattern = GameObject.Find("17R/35L").GetComponent<SpawnTraffic>();
        });
        mainBtn.onClick.AddListener(() =>
        {
            CameraController.SetTransform(CameraPosition.Main);
            DisableButton(mainBtn);
            spawnCanvas.enabled = false;
            currentPattern = null;
        });

        foreach(var key in spawnBtnDict.Keys)
        {
            var spawnBtn = spawnBtnDict[key];
            spawnBtn.onClick.AddListener(() => currentPattern.SpawnAircraft(key));
        }
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
