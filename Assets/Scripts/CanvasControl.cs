using UnityEngine;
using System.Collections;

public class CanvasControl : MonoBehaviour
{

    public static CanvasControl Instance;

    [Header("GameXXX")]
    public GameHall gameHall;
    public GameTutorial gameTutorial;

    [Header("Managers")]
    public GameCrap gameCrap;
    public ChipsManager chipsManager;
    public CrapsTableAreaManager crapsTableAreaManager;
    public HistoryPanelManager historyPanelManager;

    // Use this for initialization
    void Start ()
    {

        Instance = this;


        //Test Code
        //chipsManager.BuildCandiChips(GameHelper.Instance.GetCrapSceneInfo(1));
        //chipsManager.BuildCandiChips()

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadCrapScenes(int levelId)
    {
        chipsManager.BuildCandiChips(GameHelper.Instance.GetCrapSceneInfo(levelId));
    }

}
