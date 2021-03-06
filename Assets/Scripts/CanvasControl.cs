﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{

    public static CanvasControl Instance;

    [Header("Game XXX")]
    public GameCrap gameCrap;
    public GameTutorial gameTutorial;
    public GameScratcher gameScratcher;
    public GameScratcherHall gameScratcherHall;
    public GameHall gameHall;
    public GameSetting gameSetting;
    public GameStore gameStore;
    public GameDailyGift gameDailyGift;
    public GameAchievement gameAchievement;
    public GameHourlyGift gameHourlyGift;
    public GamePromotion gamePromotion;
    public GameRewardedVideo gameRewardedVideo;

    [Header("AdButtons")]
    [SerializeField] private Button gameHallAdButton;
    [SerializeField] private Button gameCrapsAdButton;

    [Header("Test")]
    [SerializeField] private GameTestHelper gameTestHelper;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button playButton;

    // Use this for initialization
    IEnumerator Start ()
    {

        Instance = this;

        gameCrap.gameObject.SetActive(true);

        gameScratcherHall.gameObject.SetActive(false);
        gameScratcher.gameObject.SetActive(false);

        gameHall.gameObject.SetActive(true);
        gameDailyGift.gameObject.SetActive(true);
        gameHourlyGift.gameObject.SetActive(true);

        gameCrap.UpdateGameAchievementsEffect();
        gameAchievement.DrawLine();

#if TEST
        gameTestHelper.gameObject.SetActive(true);
#else
        gameTestHelper.gameObject.SetActive(false);
#endif

        yield return new WaitForEndOfFrame();

        IronSourceControl.Instance.LoadInterstitial();

    }

    public void UpdateInterstitial()
    {
        //if (IronSourceControl.Instance.IsInterstitialReady && !GameHelper.player.IsPaid)
        //{
        //    playButton.gameObject.SetActive(true);
        //    loadButton.gameObject.SetActive(false);
        //}
        //else
        //{
        //    playButton.gameObject.SetActive(false);
        //    loadButton.gameObject.SetActive(true);
        //}
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

    public void LoadCrapScenes(int levelId)
    {
        gameCrap.chipsManager.BuildCandiChips(GameHelper.Instance.GetCrapSceneInfo(levelId));
    }

}
