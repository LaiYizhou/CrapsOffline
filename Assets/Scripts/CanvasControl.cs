﻿using UnityEngine;
using System.Collections;

public class CanvasControl : MonoBehaviour
{

    public static CanvasControl Instance;

    [Header("GameXXX")]
    public GameTutorial gameTutorial;
    

    [Header("Managers")]
    public ChipsManager chipsManager;
    public HistoryPanelManager historyPanelManager;

    // Use this for initialization
    void Start ()
    {

        Instance = this;


        //Test Code
        //chipsManager.BuildChips(GameHelper.Instance.GetCrapSceneInfo(1));

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadCrapScenes(int levelId)
    {
        chipsManager.BuildChips(GameHelper.Instance.GetCrapSceneInfo(levelId));
    }

}
