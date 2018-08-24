using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class GameStore : MonoBehaviour
{

    [SerializeField] private Button closeButton;
    [SerializeField] private Transform panel;

    [Space(10)] [SerializeField] private Transform adItemTransform;

    public void Show()
    {

        panel.transform.localScale = Vector3.zero;

        this.gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();

        sequence.Insert(0.0f, panel.DOScale(Vector3.one * 1.02f, 0.2f));
        sequence.Insert(0.0f, panel.DOLocalMove(new Vector3(0.0f, 0.0f), 0.2f));

        sequence.Insert(0.2f, panel.DOScale(Vector3.one, 0.2f));

    }

    public void UpdateAdItem(bool isShow)
    {
        if (isShow)
        {
            panel.GetComponent<RectTransform>().sizeDelta = new Vector2(837.0f, 553.0f);
            panel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            adItemTransform.gameObject.SetActive(true);
        }
        else
        {
            panel.GetComponent<RectTransform>().sizeDelta = new Vector2(837.0f, 487.0f);
            panel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            adItemTransform.gameObject.SetActive(false);
        }
    }

    private void OnCloseButtonClicked()
    {

        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        ResetGameStore();
        GameHelper.Instance.purchaseMessage.ResetAllTransforms();

        int p = Random.Range(0, 100);
        if (p < GameHelper.CloseStore_RewardedVideo_P && IronSourceControl.Instance.IsRewardedVideoReady)
        {
            CanvasControl.Instance.gameRewardedVideo.Show();
        }
        else
        {
            CanvasControl.Instance.gamePromotion.Show(GamePromotion.EPromotionType.CloseStore);
        }


    }

    public void ResetGameStore()
    {
        this.gameObject.SetActive(false);
        panel.gameObject.SetActive(true);
       
    }

// Use this for initialization
	void Start ()
    {
        closeButton.onClick.AddListener(OnCloseButtonClicked);

    }

    public void BuyProduct(string id)
    {

        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        GameHelper.Instance.purchaseMessage.ShowWaitImage();
        IAPManager.Instance.BuyProductID(id);
    }


}
