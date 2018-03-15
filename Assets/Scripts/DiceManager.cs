using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DiceManager : MonoBehaviour
{

    [SerializeField] private HisDice hisDice;

    [SerializeField] private ThrowingDice dice1;
    [SerializeField] private ThrowingDice dice2;

    private Vector2 dice1OriginalVector2;
    private Vector2 dice2OriginalVector2;

    public bool IsInBox = true;

    // Use this for initialization
    void Start()
    {
        dice1OriginalVector2 = dice1.GetComponent<RectTransform>().anchoredPosition;
        dice2OriginalVector2 = dice2.GetComponent<RectTransform>().anchoredPosition;
    }

    private float duration = 0.8f;
    public void ResetTwoDices()
    {

        if (!dice1.IsThrow && !dice2.IsThrow)
        {
            StartCoroutine(DelayResetTwoDices());   
        }

    }

    IEnumerator DelayResetTwoDices()
    {
        yield return new WaitForSeconds(0.8f);

        Sequence sequence = DOTween.Sequence();

        sequence.Insert(0.0f, dice1.GetComponent<RectTransform>().DOLocalMove(dice1OriginalVector2, duration));
        sequence.Insert(0.0f, dice2.GetComponent<RectTransform>().DOLocalMove(dice2OriginalVector2, duration));

        sequence.AppendCallback(() =>
        {
            hisDice.gameObject.SetActive(true);

            dice1.gameObject.SetActive(false);
            dice2.gameObject.SetActive(false);

            IsInBox = true;
        });
    }

    public void ThrowTwoDices(DiceState diceState)
    {
        //DiceState diceState = GameHelper.Instance.RandomDice();
        IsInBox = false;
        hisDice.Init(diceState);

        CanvasControl.Instance.gameCrap.CurrentDiceState = diceState;
        CanvasControl.Instance.gameCrap.historyPanelManager.AddDiceState(diceState);

        hisDice.gameObject.SetActive(false);

        dice1.gameObject.SetActive(true);
        dice1.Throw(GetRandomDirection(), GetRandomVelocity());

        dice2.gameObject.SetActive(true);
        dice2.Throw(GetRandomDirection(), GetRandomVelocity());
    }

    private Vector2 GetRandomDirection()
    {
        float f = Random.Range(0.2f, 1.5f);
        return new Vector2(1.0f, f);
    }

    private float GetRandomVelocity()
    {
        float f = Random.Range(600.0f, 1200.0f);
        return f;
    }

    
	// Update is called once per frame
	void Update () {
	
	}
}
