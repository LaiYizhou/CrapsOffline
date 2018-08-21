using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromotionPanel : MonoBehaviour {

    [SerializeField] private Text chipText;
    [SerializeField] private Text originalChipText;
    [SerializeField] private Text priceText;

    private MarketProduct marketProduct;


    public void Init(MarketProduct marketProduct)
    {
        this.marketProduct = marketProduct;

        if (chipText != null)
        {
            chipText.text = GameHelper.CoinLongToString(marketProduct.ChipAmount);
        }

        if (originalChipText != null && marketProduct.OriginalChipAmount > 0)
        {
            originalChipText.text = GameHelper.CoinLongToString(marketProduct.OriginalChipAmount);
        }

        if (priceText != null)
        {
            priceText.text = marketProduct.Price.ToString();
        }


    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClaimButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        GameHelper.Instance.purchaseMessage.ShowWaitImage();
        IAPManager.Instance.BuyProductID(this.marketProduct.Id);
    }

    public void OnMultiClaimButtonClicked(string id)
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        GameHelper.Instance.purchaseMessage.ShowWaitImage();
        IAPManager.Instance.BuyProductID(this.marketProduct.Id);
    }

}
