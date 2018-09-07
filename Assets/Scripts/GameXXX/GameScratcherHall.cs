using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameScratcherHall : MonoBehaviour
{

    [SerializeField] private Text coinsText;

    [Header("ScratcherItems")]
    [SerializeField] private PokerScratcherItem pokerScratcherItem;

  
    private long resultChips;
    [Header("ResultPanel")]
    [SerializeField] private Transform resultPanelTransform;
    [SerializeField] private Text resultChipsText;



    /// <summary>
    /// 
    /// </summary>
    /// <param name="chip">-1 : resultPanelTransform.gameObject.SetActive(false)</param>
    public void Show(long chip)
    {
        this.gameObject.SetActive(true);

        pokerScratcherItem.Init();

        resultChips = chip;
        if (resultChips > 0)
        {
            resultPanelTransform.gameObject.SetActive(true);
            resultChipsText.text = GameHelper.CoinLongToString(resultChips);
        }
        else
        {
            resultPanelTransform.gameObject.SetActive(false);
        }

    }

    public void ResetGameScratcherHall()
    {
        resultPanelTransform.gameObject.SetActive(false);

        if (resultChips > 0)
        {
            GameHelper.player.ChangeCoins(resultChips);
            resultChips = 0;
        }

    }

    public void OnResultPanelCloseButtonClicked()
    {
        GameHelper.Instance.coinCollectEffect.RunEffect(resultChips,
            GameHelper.Instance.ToCanvasLocalPos(resultChipsText.transform.position),
            new Vector3(-95.0f, 100.0f),
            new Vector3(-140.0f, 190));

        resultChips = 0;

        resultPanelTransform.gameObject.SetActive(false);
    }

    public void OnScratchButtonClicked()
    {
        this.gameObject.SetActive(false);
        CanvasControl.Instance.gameCrap.gameObject.SetActive(false);
        CanvasControl.Instance.gameScratcher.gameObject.SetActive(true);

        CanvasControl.Instance.gameScratcher.Show();
        GameHelper.Instance.purchaseMessage.ResetAllTransforms();
    }

    public void OnScratchAnotherButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        GameHelper.Instance.coinCollectEffect.RunEffect(resultChips,
            GameHelper.Instance.ToCanvasLocalPos(resultChipsText.transform.position),
            new Vector3(-95.0f, 100.0f),
            new Vector3(-140.0f, 190));

        resultChips = 0;
        resultPanelTransform.gameObject.SetActive(false);

        StartCoroutine(DelayPurchase());

        
    }

    IEnumerator DelayPurchase()
    {
        yield return new WaitForSeconds(1.8f);

        GameHelper.Instance.purchaseMessage.ShowWaitImage();
        IAPManager.Instance.BuyProductID("Craps_4_99_scratcher");
    }

    public void OnPurcheseButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        GameHelper.Instance.purchaseMessage.ShowWaitImage();
        IAPManager.Instance.BuyProductID("Craps_4_99_scratcher");
    }

    public void OnBackButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        this.gameObject.SetActive(false);
        CanvasControl.Instance.gameSetting.Hide();
        CanvasControl.Instance.gameHall.gameObject.SetActive(true);

        CanvasControl.Instance.gameHourlyGift.InitHourlyGift();

    }

    public void OnAddCoinButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        CanvasControl.Instance.gameStore.Show();
    }

    public void OnSettinButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        CanvasControl.Instance.gameSetting.Switch();
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

    private void OnDisable()
    {
        if (resultChips > 0)
        {
            GameHelper.player.ChangeCoins(resultChips);
            resultChips = 0;
        }
    }

}
