using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GamePromotion : MonoBehaviour
{

    public enum EPromotionType
    {
        LoginSale,
        RemoveAd,
        BackToHall,
        CloseStore,

        None

    }


    [SerializeField] private Transform panel;
    [SerializeField] private Button closeButton;

    [SerializeField] private EPromotionType promotionType;

    [SerializeField] private List<PromotionPanel> promotionPanelList; 
    
    public void Show(EPromotionType promotionType)
    {
        this.promotionType = promotionType;

        panel.transform.localScale = Vector3.zero;

        this.gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0.0f, panel.DOScale(Vector3.one * 1.02f, 0.2f));
        sequence.Insert(0.0f, panel.DOLocalMove(new Vector3(0.0f, 0.0f), 0.2f));
        sequence.Insert(0.2f, panel.DOScale(Vector3.one, 0.2f));

        ShowPromotionPanel();

    }

    private void ShowPromotionPanel()
    {
        if (this.promotionType == EPromotionType.CloseStore)
        {
            int index = Random.Range(0, 100);
            PromotionPanel promotionPanel = index > 50 ? promotionPanelList[0] : promotionPanelList[1];

            promotionPanel.Init(IAPManager.Instance.GetMarketProduct(EMarketProduct.CloseShopSale));
            promotionPanel.gameObject.SetActive(true);

        }
        else if (this.promotionType == EPromotionType.BackToHall)
        {
            int index = Random.Range(0, 100);
            PromotionPanel promotionPanel = index > 50 ? promotionPanelList[2] : promotionPanelList[3];

            promotionPanel.Init(IAPManager.Instance.GetMarketProduct(EMarketProduct.BackToHallSale));
            promotionPanel.gameObject.SetActive(true);
        }
        else if (this.promotionType == EPromotionType.RemoveAd)
        {
            int index = Random.Range(0, 100);
            if (index > 50)
            {
                PromotionPanel promotionPanel = promotionPanelList[4];
                promotionPanel.Init(IAPManager.Instance.GetMarketProduct("Craps_removeads_sale_4_99"));
                promotionPanel.gameObject.SetActive(true);
            }
            else
            {
                PromotionPanel promotionPanel = promotionPanelList[5];
                promotionPanel.Init(IAPManager.Instance.GetMarketProduct("Craps_removeads_sale_9_99"));
                promotionPanel.gameObject.SetActive(true);
            }
            
        }
        else if (this.promotionType == EPromotionType.LoginSale)
        {
            int index = Random.Range(0, 100);
            PromotionPanel promotionPanel = index > 50 ? promotionPanelList[6] : promotionPanelList[7];
            promotionPanel.gameObject.SetActive(true);
        }
        else
        {
            
        }
    }

    public void OnCloseButtonClicked()
    {

        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        GameHelper.Instance.purchaseMessage.ResetAllTransforms();
        ResetGamePromotion();
    }

    public void ResetGamePromotion()
    {
        if(this.gameObject.activeInHierarchy)
            StartCoroutine(ResetPanelList());
    }


    private IEnumerator ResetPanelList()
    {
        for(int i = 0; i<promotionPanelList.Count; i++)
            promotionPanelList[i].gameObject.SetActive(false);

        yield return new WaitForEndOfFrame();

        this.gameObject.SetActive(false);
    }

}
