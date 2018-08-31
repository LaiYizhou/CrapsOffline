using System;
using UnityEngine;
using System.Collections;
using System.Text;
using DG.Tweening;
using UnityEngine.UI;

public class GameTestHelper : MonoBehaviour
{

    public static GameTestHelper Instance;

    [SerializeField] private InputField diceInputField;
    [SerializeField] private HisDice showeDice;
    [SerializeField] private Transform Logbg;
    [SerializeField] private Text logText;
    [SerializeField] private Transform showPanelTransform;
    [SerializeField] private Text showText;

    public InputField achievementInputField;

    // Use this for initialization
    IEnumerator Start ()
	{
        yield return new WaitForEndOfFrame();

	    Instance = this;
	    //GameTestHelper.Instance.Log(GameHelper.player.Coins.ToString());

    }

    public void CrapsButton()
    {
        DiceState diceState = GameHelper.Instance.RandomDice();

        while (!diceState.IsCraps())
        {
            diceState = GameHelper.Instance.RandomDice();
        }

        CanvasControl.Instance.gameCrap.diceManager.ThrowTwoDices(diceState);

        //showeDice.Init(diceState);

        //CanvasControl.Instance.gameCrap.CurrentDiceState = diceState;
        //CanvasControl.Instance.gameCrap.historyPanelManager.AddDiceState(diceState);

    }

    public void NaturalButton()
    {
        DiceState diceState = GameHelper.Instance.RandomDice();

        while (!diceState.IsNatural())
        {
            diceState = GameHelper.Instance.RandomDice();
        }

        CanvasControl.Instance.gameCrap.diceManager.ThrowTwoDices(diceState);

    }

    public void PointButton()
    {
        DiceState diceState = GameHelper.Instance.RandomDice();

        while (!diceState.IsPoint())
        {
            diceState = GameHelper.Instance.RandomDice();
        }

        CanvasControl.Instance.gameCrap.diceManager.ThrowTwoDices(diceState);

    }

    public void ShowLogSwitch()
    {
       
        //if(Logbg.gameObject.activeInHierarchy)
        //    Logbg.gameObject.SetActive(false);
        //else
        //    Logbg.gameObject.SetActive(true);

        GameHelper.Instance.coinCollectEffect.TestRunEffect(0);

    }

    public void ClearLog()
    {
        logText.text = "";
    }

    public void Log(string s)
    {
        StringBuilder sb = new StringBuilder(logText.text);

        sb.Append(System.Environment.NewLine).Append(s);

        logText.text = sb.ToString();

    }

    public void OkButton()
    {
        int diceNumber;
        if (int.TryParse(diceInputField.text, out diceNumber))
        {
            if (diceNumber >= 2 && diceNumber <= 12)
            {
                DiceState diceState = GameHelper.Instance.RandomDice();

                while (diceState.Sum != diceNumber)
                {
                    diceState = GameHelper.Instance.RandomDice();
                }

                diceInputField.text = "";
                CanvasControl.Instance.gameCrap.diceManager.ThrowTwoDices(diceState);

            
            }
        }
    }

    public void ResetCoinsButton()
    {
        GameHelper.player.ResetData();
    }

    public void ResetDailyGiftButton()
    {
        PlayerPrefs.DeleteKey("LoginCount");
        PlayerPrefs.DeleteKey("LastTime");
    }

    public void ResetHourlyGiftButton()
    {
        PlayerPrefs.DeleteKey("LastGetHourlyGiftTime");
    }

    public void LoadInterstitialButton()
    {
        IronSourceControl.Instance.LoadInterstitial();
    }

    public void PlayIntertitialButton()
    {
        IronSourceControl.Instance.ShowInterstitial();
    }

    public void ResetAllButton()
    {
        PlayerPrefs.DeleteAll();
    }


    public void AchievementButton()
    {
        if (showPanelTransform.gameObject.activeInHierarchy)
        {
            showPanelTransform.gameObject.SetActive(false);
        }
        else
        {
            StringBuilder sb = new StringBuilder();

            showPanelTransform.gameObject.SetActive(true);
            for (int i = 1; i < CanvasControl.Instance.gameAchievement.GameAchievementInfoList.Count; i++)
            {
                sb.Append(CanvasControl.Instance.gameAchievement.GameAchievementInfoList[i].ToString()).Append(System.Environment.NewLine);
            }

            showText.text = sb.ToString();
        }
    }


    public void AchievementsResetButton()
    {
        CanvasControl.Instance.gameAchievement.ResetAchievementsData(); 
    }
}
