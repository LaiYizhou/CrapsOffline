using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class HisDice : MonoBehaviour
{

    [SerializeField]
    private Image image1;
    [SerializeField]
    private Image image2;

    public void Init(DiceState diceState)
    {
        image1.sprite = GameHelper.Instance.GetDiceSprite(diceState.Number1);
        image2.sprite = GameHelper.Instance.GetDiceSprite(diceState.Number2);

        this.GetComponent<RectTransform>().DOScale(Vector3.one, 0.6f);
    }

    public void Reset()
    {
        image1.sprite = GameHelper.Instance.GetDiceSprite(1);
        image2.sprite = GameHelper.Instance.GetDiceSprite(1);
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
