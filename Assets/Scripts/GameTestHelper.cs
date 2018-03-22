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

	// Use this for initialization
	IEnumerator Start ()
	{
        yield return new WaitForEndOfFrame();

	    Instance = this;
	    //GameTestHelper.Instance.Log(GameHelper.player.Coins.ToString());

    }

    // Update is called once per frame
	void Update () {
	
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
       
        if(Logbg.gameObject.activeInHierarchy)
            Logbg.gameObject.SetActive(false);
        else
            Logbg.gameObject.SetActive(true);
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

}
