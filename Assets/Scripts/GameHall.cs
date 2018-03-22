using System;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameHall : MonoBehaviour
{

    [SerializeField] private Text coinsText;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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

        int index = Random.Range(1, 7);
        while (GameHelper.player.Coins < GameHelper.Instance.GetCrapSceneInfo(index).JoinMinCoins)
        {
            index = Random.Range(1, 7);
        }

        LoadCrapScene(index);
    }

    

    public void OnAdButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        IronSourceControl.Instance.ShowRewardedVideoButtonClicked();
    }

    public void LoadCrapScene(int levelId)
    {

        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.GameSceneClick);

        if (levelId > 0 && levelId <= 6)
        {
            //if (GameHelper.player.Coins >= GameHelper.Instance.GetCrapSceneInfo(levelId).JoinMinCoins)
            {
                CanvasControl.Instance.gameCrap.Init(levelId);
                this.gameObject.SetActive(false);
            }
            //else
            //{
            //    GameTestHelper.Instance.Tip("Not enough Coins ! ! !");
            //}
   
        }
        
    }
}
