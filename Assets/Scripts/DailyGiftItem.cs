using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class DailyGiftItem : MonoBehaviour
{

    public enum EImageType
    {
        Normal,
        FiveDay,
        Today
    }

    [Space(5)]
    [SerializeField] private Image bgImage;
    [SerializeField] private Sprite fiveDaySprite;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite todaySprite;

    [Space(5)]
    [SerializeField] private Text dayText;
    [SerializeField] private Text coinText;

    [Space(5)]
    [SerializeField] private Image markImage;
    [SerializeField] private Image coverImage;

    private EImageType eImageType;
    private bool isMarked;

    private int dayNumber;
    private int coinNumber;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetImageType(EImageType eImageType)
    {
        this.eImageType = eImageType;
        switch (this.eImageType)
        {
                
            case EImageType.FiveDay:
                bgImage.sprite = fiveDaySprite; break;
            case EImageType.Normal:
                bgImage.sprite = normalSprite; break;
            case EImageType.Today:
                bgImage.sprite = todaySprite; break;
            default:
                bgImage.sprite = todaySprite; break;
           
        }
    }

    public void SetText(int dayNumber, int coinNumber)
    {
        this.dayNumber = dayNumber;
        this.coinNumber = coinNumber;

        if(this.dayNumber > 0 && this.dayNumber <= 30)
            dayText.text = "DAY " + this.dayNumber.ToString();

        coinText.text = GameHelper.CoinLongToString(this.coinNumber);

    }

    public void SetMark(bool flag)
    {
        this.isMarked = flag;
        SetImageType(dayNumber % 5 == 0 ? EImageType.FiveDay : EImageType.Normal);

        if (this.isMarked)
        {
            markImage.fillAmount = 1.0f;
            coverImage.gameObject.SetActive(true);
        }
        else
        {
            markImage.fillAmount = 0.0f;
            coverImage.gameObject.SetActive(false);
        }

    }

    public void Mark()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.DailyGiftSign);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(markImage.DOFillAmount(1.0f, 0.5f));

        sequence.AppendCallback(()=>
        {
            SetMark(true);
            //CanvasControl.Instance.gameHall.ShowAddCoins(this.coinNumber, false);
            GameHelper.Instance.ShowAddCoins(this.coinNumber, false);
        });
    }

}
