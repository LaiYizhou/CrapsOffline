using UnityEngine;
using System.Collections;

public class CanvasControl : MonoBehaviour
{

    public static CanvasControl Instance;

    [Header("Game XXX")]
    public GameHall gameHall;
    public GameTutorial gameTutorial;
    public GameCrap gameCrap;
    public GameDailyGift gameDailyGift;

    // Use this for initialization
    void Start ()
    {

        Instance = this;

        gameHall.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update () {
	
	}

    public void LoadCrapScenes(int levelId)
    {
        gameCrap.chipsManager.BuildCandiChips(GameHelper.Instance.GetCrapSceneInfo(levelId));
    }

}
