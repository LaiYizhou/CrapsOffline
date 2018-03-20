using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    [Header("AdButtons")]
    [SerializeField] private Button gameHallAdButton;
    [SerializeField] private Button gameCrapsAdButton;

    // Use this for initialization
    void Start ()
    {

        Instance = this;

        gameCrap.gameObject.SetActive(true);
        gameHall.gameObject.SetActive(true);
        gameDailyGift.gameObject.SetActive(true);

    }

    public void UpdateRewardedButton()
    {
        if (IronSourceControl.Instance.IsRewardedVideoReady && !GameHelper.player.IsPaid)
        {
            gameHallAdButton.gameObject.SetActive(true);
            gameCrapsAdButton.gameObject.SetActive(true);
        }
        else
        {
            gameHallAdButton.gameObject.SetActive(false);
            gameCrapsAdButton.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void LoadCrapScenes(int levelId)
    {
        gameCrap.chipsManager.BuildCandiChips(GameHelper.Instance.GetCrapSceneInfo(levelId));
    }

}
