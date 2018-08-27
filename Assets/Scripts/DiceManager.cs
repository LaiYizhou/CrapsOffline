using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{

    [SerializeField] private Button readyButton;
    [SerializeField] private Button rebetButton;
    [SerializeField] private Button rollButton;
    [SerializeField] private Button undoButton;
    [SerializeField] private Button clearButton;

    [SerializeField] private HisDice hisDice;

    [SerializeField] private ThrowingDice dice1;
    public ThrowingDice Dice1
    {
        get { return dice1; }
    }

    [SerializeField] private ThrowingDice dice2;
    public ThrowingDice Dice2
    {
        get { return dice2; }
    }

    private Vector2 dice1OriginalVector2;
    private Vector2 dice2OriginalVector2;

    public bool IsInBox = true;
    public bool IsOpenDiceCollider = false;

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

            //temp code
            if(!CanvasControl.Instance.gameHall.gameObject.activeInHierarchy)
                PlaySpokenAudios();


            StartCoroutine(DelayResetTwoDices());   
        }

    }

    private void PlaySpokenAudios()
    {
        if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum == 2)
        {
            if (!CanvasControl.Instance.gameCrap.IsPointOn)
            {
                AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._2_no_point_1);
            }
            else
            {
                AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._2_1);
            }
        }
        else if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum == 3)
        {
            if (!CanvasControl.Instance.gameCrap.IsPointOn)
            {
                AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._3_no_point_1);
            }
            else
            {
                AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._3_1);
            }
        }
        else if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum == 4)
        {
            if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsHard())
            {
                AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._4_hard_1);
            }
            else
            {
                if (!CanvasControl.Instance.gameCrap.IsPointOn)
                {
                    AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._4_no_point_1);
                }
                else
                {
                    if (CanvasControl.Instance.gameCrap.CurrentCrapsPointValue == 4)
                    {
                        AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._4_winner_1);
                    }
                    else
                    {
                        AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._4_1);
                    }
                }
            }
        }
        else if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum == 5)
        {
            if (!CanvasControl.Instance.gameCrap.IsPointOn)
            {
                AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._5_no_point_1);
            }
            else
            {
                if (CanvasControl.Instance.gameCrap.CurrentCrapsPointValue == 5)
                {
                    AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._5_winner_1);
                }
                else
                {
                    AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._5_1);
                }
            }
        }
        else if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum == 6)
        {
            if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsHard())
            {
                AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._6_hard_1);
            }
            else
            {
                if (!CanvasControl.Instance.gameCrap.IsPointOn)
                {
                    AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._6_no_point_1);
                }
                else
                {
                    if (CanvasControl.Instance.gameCrap.CurrentCrapsPointValue == 6)
                    {
                        AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._6_winner_1);
                    }
                    else
                    {
                        AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._6_1);
                    }
                }
            }
        }
        else if(CanvasControl.Instance.gameCrap.CurrentDiceState.Sum == 7)
        {
            if (!CanvasControl.Instance.gameCrap.IsPointOn)
            {
                AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._7_no_point_1);
            }
            else
            {
                AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._7_out_1);
            }
        }
        else if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum == 8)
        {
            if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsHard())
            {
                AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._8_hard_1);
            }
            else
            {
                if (!CanvasControl.Instance.gameCrap.IsPointOn)
                {
                    AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._8_no_point_1);
                }
                else
                {
                    if (CanvasControl.Instance.gameCrap.CurrentCrapsPointValue == 8)
                    {
                        AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._8_winner_1);
                    }
                    else
                    {
                        AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._8_1);
                    }
                }
            }
        }
        else if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum == 9)
        {
            if (!CanvasControl.Instance.gameCrap.IsPointOn)
            {
                AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._9_no_point_1);
            }
            else
            {
                if (CanvasControl.Instance.gameCrap.CurrentCrapsPointValue == 9)
                {
                    AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._9_winner_1);
                }
                else
                {
                    AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._9_1);
                }
            }
        }
        else if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum == 10)
        {
            if (CanvasControl.Instance.gameCrap.CurrentDiceState.IsHard())
            {
                AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._10_hard_1);
            }
            else
            {
                if (!CanvasControl.Instance.gameCrap.IsPointOn)
                {
                    AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._10_no_point_1);
                }
                else
                {
                    if (CanvasControl.Instance.gameCrap.CurrentCrapsPointValue == 10)
                    {
                        AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._10_winner_1);
                    }
                    else
                    {
                        AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._10_1);
                    }
                }
            }
        }
        else if (CanvasControl.Instance.gameCrap.CurrentDiceState.Sum == 11)
        {
            if (!CanvasControl.Instance.gameCrap.IsPointOn)
            {
                AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._11_no_point_1);
            }
            else
            {
                AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._11_1);
            }
        }
        else
        {
            if (!CanvasControl.Instance.gameCrap.IsPointOn)
            {
                AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._12_no_point_1);
            }
            else
            {
                AudioControl.Instance.PlaySpokenSound(AudioControl.ESpokenAudioClip._12_1);
            }
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
            IsOpenDiceCollider = false;

            StartCoroutine(DelayUpdateButton());   
        });
    }

    public void ResetData()
    {
        StopCoroutine("DelayUpdateButton");

        dice1.GetComponent<RectTransform>().anchoredPosition = dice1OriginalVector2;
        dice2.GetComponent<RectTransform>().anchoredPosition = dice2OriginalVector2;

        hisDice.gameObject.SetActive(true);
        hisDice.Reset();

        dice1.gameObject.SetActive(false);
        dice2.gameObject.SetActive(false);

        IsInBox = true;
        IsOpenDiceCollider = false;

        rollButton.interactable = true;
        undoButton.interactable = true;
        clearButton.interactable = true;
        CanvasControl.Instance.gameCrap.chipsManager.IsChipsCanDrag = true;

    }

    IEnumerator DelayUpdateButton()
    {
        yield return new WaitForSeconds(0.6f);

        //readyButton.gameObject.SetActive(true);
        //rebetButton.gameObject.SetActive(true);

        rollButton.interactable = true;
        undoButton.interactable = true;
        clearButton.interactable = true;
        CanvasControl.Instance.gameCrap.chipsManager.IsChipsCanDrag = true;
    }

    public void Roll()
    {
        if (CanvasControl.Instance.gameCrap.chipsManager.GetAllChipsValue() > 0)
        {
         
            DiceState diceState = GameHelper.Instance.RandomDice();

            CanvasControl.Instance.gameCrap.SetGameStateText(CanvasControl.Instance.gameCrap.chipsManager.GetAllChipsValue(), true);
            CanvasControl.Instance.gameCrap.OneRollWinAndLoseResult = 0;

            ThrowTwoDices(diceState);
        }
        else
        {
            GameHelper.Instance.ShowDialogMessage("Please place a bet on the table.");
        }

        
    }

    //public void Ready()
    //{
    //    readyButton.gameObject.SetActive(false);
    //    rebetButton.gameObject.SetActive(false);

    //    rollButton.gameObject.SetActive(true);
    //    rollButton.interactable = true;
    //}

    //public void Rebet()
    //{

    //}

    public void ThrowTwoDices(DiceState diceState)
    {

        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.RollDice);

        IsInBox = false;

        rollButton.interactable = false;
        undoButton.interactable = false;
        clearButton.interactable = false;
        CanvasControl.Instance.gameCrap.chipsManager.IsChipsCanDrag = false;

        hisDice.Init(diceState);
        hisDice.gameObject.SetActive(false);

        CanvasControl.Instance.gameCrap.CurrentDiceState = diceState;

        dice1.gameObject.SetActive(true);
        dice1.Throw(GetRandomDirection(), GetRandomVelocity());

        dice2.gameObject.SetActive(true);
        dice2.Throw(GetRandomDirection(), GetRandomVelocity());

        StartCoroutine(DelayOpenDiceCollider());
    }

    IEnumerator DelayOpenDiceCollider()
    {
        yield return new WaitForSeconds(0.2f);

        IsOpenDiceCollider = true;
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

    
}
