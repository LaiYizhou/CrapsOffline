using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HisDice : MonoBehaviour
{

    [SerializeField]
    private Image image1;
    [SerializeField]
    private Image image2;

    public void Init(DiceState diceState)
    {
        image1.sprite = GameHelper.Instance.GetDiceSprite(diceState.GetNumber1());
        image2.sprite = GameHelper.Instance.GetDiceSprite(diceState.GetNumber2());
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
