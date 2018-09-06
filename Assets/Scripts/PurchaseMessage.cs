using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseMessage : MonoBehaviour
{

    [Header("Purchase")]
    [SerializeField] private Transform purchasingTransform;
    [SerializeField] private Image purchasingImage;

    [SerializeField] private Transform purchasedTransform;
    [SerializeField] private Text purchaseCoinsText;

    [SerializeField] private Transform purchasedFailTransform;

    [Header("Scratcher")]
    [SerializeField] private Transform scratcherPurchasedTransform;

    public void ShowWaitImage()
    {
        purchasingTransform.gameObject.SetActive(true);
    }

    public void ShowScratcherPurchasedTransform()
    {
        CanvasControl.Instance.gameScratcherHall.ResetGameScratcherHall();

        purchasingTransform.gameObject.SetActive(false);
        purchasedFailTransform.gameObject.SetActive(false);
        purchasedTransform.gameObject.SetActive(false);

        scratcherPurchasedTransform.gameObject.SetActive(true);

        ScratcherManager.Instance.NumberAddOne(EScratcherType.Poker);
    }

    public void ShowPurchasedTransform(long number)
    {

        CanvasControl.Instance.gameStore.ResetGameStore();
        CanvasControl.Instance.gamePromotion.ResetGamePromotion();

        purchasingTransform.gameObject.SetActive(false);
        purchasedFailTransform.gameObject.SetActive(false);

        purchaseCoinsText.text = GameHelper.CoinLongToString(number);
        purchasedTransform.gameObject.SetActive(true);

        StartCoroutine("DelayReset");
    }

    public void ShowPurchasedFailTransform()
    {

        purchasingTransform.gameObject.SetActive(false);

        purchasedFailTransform.gameObject.SetActive(true);

        purchasedTransform.gameObject.SetActive(false);

        StartCoroutine("DelayReset");
    }

    public void ResetAllTransforms()
    {
        purchasingTransform.gameObject.SetActive(false);
        purchasedFailTransform.gameObject.SetActive(false);
        purchasedTransform.gameObject.SetActive(false);

        scratcherPurchasedTransform.gameObject.SetActive(false);
    }

    IEnumerator DelayReset()
    {
        yield return new WaitForSeconds(3.0f);

        ResetAllTransforms();
    }

    private float z;
    void Update()
    {

        z += (Time.deltaTime * 250);

        if (z > 360.0f)
            z -= 360.0f;

        purchasingImage.GetComponent<RectTransform>().localEulerAngles = new Vector3(0.0f, 0.0f, z);

    }
}
