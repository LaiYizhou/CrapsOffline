using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameScratcher : MonoBehaviour
{

    private List<long> winChipsList = new List<long>()
    {
        20000,
        40000,
        70000,
        130000,
        200000,
        300000,
        450000,
        680000,
        1100000,
        1700000
    };

    [SerializeField] private PokerItem targetPoker;
    [SerializeField] private PokersManager pokersManager;

    [SerializeField] private Image arrowImage;
    private Vector3 arrowOriginalPos = new Vector3(-126.3f, -106.0f, 0.0f);

    [SerializeField] private List<Text> chipTextList;

    private int remainPokerNumber = 0;
    private int greaterPokerNumber = 0;

    public int GreaterPokerNumber
    {
        get { return greaterPokerNumber; }

        set
        {
            greaterPokerNumber = value;

            arrowImage.GetComponent<RectTransform>()
                .DOLocalMoveY(-106.0f + greaterPokerNumber * 22.0f, 0.5f);

            UpdateTextColor();
        }
    }

    public void Show()
    {
        Poker poker = GameHelper.Instance.GetRandomTargetPoker();

        Debug.Log(poker.ToString());

        targetPoker.Init(poker);
        pokersManager.Init(poker);

        arrowImage.GetComponent<RectTransform>().localPosition = arrowOriginalPos;
        GreaterPokerNumber = 0;
        remainPokerNumber = 9;

    }

    public void OnScratchAllButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        pokersManager.ScratchAll();
    }

    public void ScratchOnePoker(Poker poker)
    {
        if (poker > targetPoker.Poker)
            GreaterPokerNumber++;

        remainPokerNumber--;

        if (remainPokerNumber == 0)
        {
            StartCoroutine(DelayExitScratch());
        }

    }

    IEnumerator DelayExitScratch()
    {
        yield return new WaitForSeconds(0.5f);

        ScratcherManager.Instance.NumberMinusOne(EScratcherType.Poker);

        this.gameObject.SetActive(false);
        pokersManager.Reset();

        CanvasControl.Instance.gameScratcherHall.gameObject.SetActive(true);
        CanvasControl.Instance.gameScratcherHall.Show(winChipsList[greaterPokerNumber]);

    }

    private void UpdateTextColor()
    {
        chipTextList.ForEach((go) => { go.color = Color.white;});
        chipTextList[greaterPokerNumber].color = new Color(1.0f, 214.0f/255.0f, 99.0f/255.0f, 1.0f);
    }


}
