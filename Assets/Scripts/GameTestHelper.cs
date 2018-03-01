using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTestHelper : MonoBehaviour
{

    public static GameTestHelper Instance;

    [SerializeField] private InputField levelInputField;
    [SerializeField] private InputField diceInputField;
    [SerializeField] private HisDice showeDice;
    [SerializeField] private Text coinsText;

	// Use this for initialization
	IEnumerator Start ()
	{
        yield return new WaitForEndOfFrame();

	    Instance = this;
	    ShowPlayerCoin();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RandomDiceButton()
    {
        DiceState diceState = GameHelper.Instance.RandomDice();

        showeDice.Init(diceState);

        CanvasControl.Instance.gameCrap.CurrentDiceState = diceState;
        CanvasControl.Instance.gameCrap.historyPanelManager.AddDiceState(diceState);
    }

    public void CrapsButton()
    {
        DiceState diceState = GameHelper.Instance.RandomDice();

        while (!diceState.IsCraps())
        {
            diceState = GameHelper.Instance.RandomDice();
        }

        showeDice.Init(diceState);

        CanvasControl.Instance.gameCrap.CurrentDiceState = diceState;
        CanvasControl.Instance.gameCrap.historyPanelManager.AddDiceState(diceState);

    }

    public void NaturalButton()
    {
        DiceState diceState = GameHelper.Instance.RandomDice();

        while (!diceState.IsNatural())
        {
            diceState = GameHelper.Instance.RandomDice();
        }

        showeDice.Init(diceState);

        CanvasControl.Instance.gameCrap.CurrentDiceState = diceState;
        CanvasControl.Instance.gameCrap.historyPanelManager.AddDiceState(diceState);
    }

    public void PointButton()
    {
        DiceState diceState = GameHelper.Instance.RandomDice();

        while (!diceState.IsPoint())
        {
            diceState = GameHelper.Instance.RandomDice();
        }

        showeDice.Init(diceState);

        CanvasControl.Instance.gameCrap.CurrentDiceState = diceState;
        CanvasControl.Instance.gameCrap.historyPanelManager.AddDiceState(diceState);
    }

    public void LoadButton()
    {
        int levelId;
        if (int.TryParse(levelInputField.text, out levelId))
        {
            if(levelId > 0 && levelId <= 6)
                CanvasControl.Instance.gameCrap.chipsManager.BuildCandiChips(GameHelper.Instance.GetCrapSceneInfo(levelId));

            levelInputField.text = "";
            CanvasControl.Instance.gameHall.gameObject.SetActive(false);

        }
            
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

                showeDice.Init(diceState);

                CanvasControl.Instance.gameCrap.CurrentDiceState = diceState;
                CanvasControl.Instance.gameCrap.historyPanelManager.AddDiceState(diceState);
            }
        }
    }

    public void ResetCoinsButton()
    {
        GameHelper.player.ResetData();
    }

    public void ShowPlayerCoin()
    {
        coinsText.text = GameHelper.player.CoinToString();
    }

    public void ShowTutorialButton()
    {
       CanvasControl.Instance.gameTutorial.Show();
    }
}
