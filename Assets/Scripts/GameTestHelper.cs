using UnityEngine;
using System.Collections;

public class GameTestHelper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RandomDiceButton()
    {
        DiceState diceState = GameHelper.Instance.RandomDice();

        CanvasControl.Instance.historyPanelManager.AddDiceState(diceState);
    }
}
