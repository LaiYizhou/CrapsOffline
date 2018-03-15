using UnityEngine;
using System.Collections;

public class CanvasControl : MonoBehaviour
{

    public static CanvasControl Instance;

    [Header("Game XXX")]
    public GameCrap gameCrap;
    public GameTutorial gameTutorial;
    public GameHall gameHall;
    public GameSetting gameSetting;
    public GameStore gameStore;
    public GameDailyGift gameDailyGift;

    // Use this for initialization
    void Start ()
    {

        Instance = this;

        gameHall.gameObject.SetActive(true);
        gameDailyGift.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update () {
	
	}

    public void LoadCrapScenes(int levelId)
    {
        gameCrap.chipsManager.BuildCandiChips(GameHelper.Instance.GetCrapSceneInfo(levelId));
    }

}
