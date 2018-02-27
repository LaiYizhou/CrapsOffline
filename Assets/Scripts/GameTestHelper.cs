using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTestHelper : MonoBehaviour
{

    [SerializeField] private InputField levelInputField;
    [SerializeField] private HisDice showeDice;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RandomDiceButton()
    {
        DiceState diceState = GameHelper.Instance.RandomDice();

        showeDice.Init(diceState);

        CanvasControl.Instance.historyPanelManager.AddDiceState(diceState);
    }

    public void LoadButton()
    {
        int levelId;
        if (int.TryParse(levelInputField.text, out levelId))
        {
            if(levelId > 0 && levelId <= 6)
                CanvasControl.Instance.chipsManager.BuildCandiChips(GameHelper.Instance.GetCrapSceneInfo(levelId));

            levelInputField.text = "";
            CanvasControl.Instance.gameHall.gameObject.SetActive(false);

        }
            
    }

    public void ShowTutorialButton()
    {
       CanvasControl.Instance.gameTutorial.Show();
    }
}
