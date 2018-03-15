using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{

    [SerializeField] private Button readyButton;
    [SerializeField] private Button rebetButton;
    [SerializeField] private Button rollButton;

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
            Debug.Log("Reset TwoDices");
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

            StartCoroutine(DelayUpdateButton());   
        });
    }

    IEnumerator DelayUpdateButton()
    {
        yield return new WaitForSeconds(0.6f);

        //readyButton.gameObject.SetActive(true);
        //rebetButton.gameObject.SetActive(true);

        //rollButton.gameObject.SetActive(false);
        rollButton.interactable = true;
    }

    public void Roll()
    {
        DiceState diceState = GameHelper.Instance.RandomDice();

        CanvasControl.Instance.gameCrap.SetGameStateText(CanvasControl.Instance.gameCrap.chipsManager.GetAllChipsValue(), true);
        CanvasControl.Instance.gameCrap.OneRollResult = 0;

        ThrowTwoDices(diceState);
    }

    public void Ready()
    {
        readyButton.gameObject.SetActive(false);
        rebetButton.gameObject.SetActive(false);

        rollButton.gameObject.SetActive(true);
        rollButton.interactable = true;
    }

    public void Rebet()
    {

    }

    public void ThrowTwoDices(DiceState diceState)
    {
        //DiceState diceState = GameHelper.Instance.RandomDice();
        IsInBox = false;

        rollButton.interactable = false;

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
