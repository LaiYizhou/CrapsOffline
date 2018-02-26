using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class HistoryPanelManager : MonoBehaviour
{
    [Header("historyButton")]
    [SerializeField] private Button historyButton;
    [SerializeField] private Sprite historyUpSprite;
    [SerializeField] private Sprite historyDownSprite;
    private bool isShowPanel;

    [Space(10)]
    [SerializeField] private Image historyPanelImage;
    
    [SerializeField] private Transform historyPanel;

    private const int MaxNumber = 12;
    private Queue<DiceState> diceQueue = new Queue<DiceState>();
    private int currentNumber = 0;

    // Use this for initialization
	void Start ()
	{

	    historyPanelImage.fillAmount = 0.0f;
	    isShowPanel = false;

        historyButton.onClick.AddListener(OnHistoryButtonClicked);

    }

    private float duration = 0.2f;
    private void OnHistoryButtonClicked()
    {
        isShowPanel = !isShowPanel;
        if (isShowPanel)
        {
            historyPanelImage.DOFillAmount(1.0f, duration);
            historyButton.GetComponent<Image>().sprite = historyDownSprite;
        }
        else
        {
            historyPanelImage.DOFillAmount(0.0f, duration);
            historyButton.GetComponent<Image>().sprite = historyUpSprite;
        }
    }

    public void AddDiceState(DiceState diceState)
    {
        if (diceQueue.Count < MaxNumber)
        {
            diceQueue.Enqueue(diceState);
        }
        else
        {
            diceQueue.Dequeue();
            diceQueue.Enqueue(diceState);
        }

        BuildHisDice(diceState);
    }

    private void BuildHisDice(DiceState diceState)
    {
        GameObject goPrefab = Resources.Load("HisDice") as GameObject;
        
        GameObject go = Instantiate(goPrefab) as GameObject;
        go.transform.SetParent(historyPanel);
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        go.transform.SetAsFirstSibling();

        currentNumber++;

        HisDice hisDice = go.GetComponent<HisDice>();
        hisDice.Init(diceState);

        if (currentNumber > MaxNumber)
        {
            Destroy(historyPanel.GetChild(currentNumber-1).gameObject);
            currentNumber--;
        }


    }

    // Update is called once per frame
    void Update () {
	
	}
}
