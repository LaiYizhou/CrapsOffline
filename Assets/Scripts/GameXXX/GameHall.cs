﻿using System;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameHall : MonoBehaviour
{

    [SerializeField] private Text coinsText;
    private int lastGameHallId;
    public int LastGameHallId
    {
        get
        {
            return PlayerPrefs.HasKey("LastGameHallId") ? PlayerPrefs.GetInt("LastGameHallId") : -1;
        }

        set
        {
            lastGameHallId = value;
            PlayerPrefs.SetInt("LastGameHallId", lastGameHallId);
        }
    }

    public void UpdatePlayerCoin()
    {

        long currentCoins = 0;
        long targetCoins = 0;

        if (GameHelper.Instance == null || GameHelper.player == null)
            targetCoins = GameHelper.StartCoins;
        else
            targetCoins = GameHelper.player.Coins;

        try
        {
            currentCoins = GameHelper.CoinStringToLong(coinsText.text);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            currentCoins = targetCoins;
        }

        if (currentCoins != targetCoins)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(DOTween.To(() => currentCoins,
                x =>
                {
                    currentCoins = x;
                    coinsText.text = GameHelper.CoinLongToString(currentCoins);
                },
                targetCoins, 1.0f));

            sequence.AppendCallback(() =>
            {
                coinsText.text = GameHelper.CoinLongToString(currentCoins);
            });
        }
        else
        {
            coinsText.text = GameHelper.CoinLongToString(currentCoins);
        }


    }

    public void OnDailyGiftsButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        CanvasControl.Instance.gameDailyGift.Show();
    }

    public void OnAddCoinButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        CanvasControl.Instance.gameStore.Show();
    }

    public void OnSettingButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        CanvasControl.Instance.gameSetting.Switch();
    }

    public void OnCrapPlayNowButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.GameSceneClick);

        if (LastGameHallId == -1)
        {
            int index = Random.Range(1, 7);
            while (GameHelper.player.Coins < GameHelper.Instance.GetCrapSceneInfo(index).JoinMinCoins)
            {
                index = Random.Range(1, 7);
            }

            LoadCrapScene(index);
        }
        else
        {
            LoadCrapScene(LastGameHallId);
        }

        
    }

    

    public void OnAdButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        IronSourceControl.Instance.ShowRewardedVideoButtonClicked();
    }

    public void OnGameAchievementButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        CanvasControl.Instance.gameAchievement.Show();
    }

    public void OnScratcherButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        CanvasControl.Instance.gameCrap.gameObject.SetActive(false);

        CanvasControl.Instance.gameScratcherHall.gameObject.SetActive(true);
        CanvasControl.Instance.gameScratcherHall.Show(-1);

        this.gameObject.SetActive(false);
    }

    public void LoadCrapScene(int levelId)
    {

        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.GameSceneClick);

        if (levelId > 0 && levelId <= 6)
        {
            //if (GameHelper.player.Coins >= GameHelper.Instance.GetCrapSceneInfo(levelId).JoinMinCoins)
            {

                CanvasControl.Instance.gameCrap.gameObject.SetActive(true);
                CanvasControl.Instance.gameScratcherHall.gameObject.SetActive(false);

                CanvasControl.Instance.gameCrap.Init(levelId);
                CanvasControl.Instance.gameCrap.UpdateGameAchievementsEffect();

                CanvasControl.Instance.gameAchievement.Reset();

                LastGameHallId = levelId;
                this.gameObject.SetActive(false);
            }
   
        }
        
    }
}
