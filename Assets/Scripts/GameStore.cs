using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStore : MonoBehaviour
{

    [SerializeField] private Button closeButton;
    [SerializeField] private Transform panel;

    [Header("Purchase")]
    [SerializeField] private Transform purchasingTransform;
    [SerializeField] private Image purchasingImage;

    [SerializeField] private Transform purchasedTransform;
    [SerializeField] private Text purchaseCoinsText;

    [SerializeField] private Transform purchasedFailTransform;

    [Space(10)]
    [SerializeField] private Transform adItemTransform;

    public void Show()
    {
        //ShowAdItem();
        this.gameObject.SetActive(true);
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

        this.gameObject.SetActive(false);
        ClickScreenToReset();
    }

	// Use this for initialization
	void Start ()
    {
        closeButton.onClick.AddListener(OnCloseButtonClicked);

    }

    public void OnMaskButtonClicked()
    {
        StopCoroutine("DelayReset");
        ClickScreenToReset();
    }

    public void ShowWaitImage()
    {
        purchasingTransform.gameObject.SetActive(true);
    }

    public void ShowPurchasedTransform(long number)
    {
        panel.gameObject.SetActive(false);
        purchasingTransform.gameObject.SetActive(false);

        purchasedFailTransform.gameObject.SetActive(false);

        purchaseCoinsText.text = GameHelper.CoinLongToString(number);
        purchasedTransform.gameObject.SetActive(true);

        StartCoroutine("DelayReset");
    }

    public void ShowPurchasedFailTransform()
    {
        panel.gameObject.SetActive(false);
        purchasingTransform.gameObject.SetActive(false);

        purchasedFailTransform.gameObject.SetActive(true);

        purchasedTransform.gameObject.SetActive(false);

        StartCoroutine("DelayReset");
    }

    private void ClickScreenToReset()
    {
        this.gameObject.SetActive(false);

        panel.gameObject.SetActive(true);

        purchasingTransform.gameObject.SetActive(false);
        purchasedFailTransform.gameObject.SetActive(false);
        purchasedTransform.gameObject.SetActive(false);
    }

    IEnumerator DelayReset()
    {
        yield return new WaitForSeconds(3.0f);

        ClickScreenToReset();
    }

    public void BuyProduct(string id)
    {

        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        ShowWaitImage();
        IAPManager.Instance.BuyProductID(id);
    }

    // Update is called once per frame
    private float z;
    void Update () {

        z += (Time.deltaTime * 250);

        if (z > 360.0f)
            z -= 360.0f;

        purchasingImage.GetComponent<RectTransform>().localEulerAngles = new Vector3(0.0f, 0.0f, z);

    }
}
