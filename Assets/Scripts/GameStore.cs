using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStore : MonoBehaviour
{

    [SerializeField] private Button closeButton;
    [SerializeField] private Transform panel;

    [Header("Purchase")]
    [SerializeField] private Image Purchasing;

    [SerializeField] private Transform purchasedTransform;
    [SerializeField] private Text purchaseCoinsText;

    [SerializeField] private Transform purchasedFailTransform;

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    private void OnCloseButtonClicked()
    {

        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        this.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start ()
    {
        closeButton.onClick.AddListener(OnCloseButtonClicked);

    }

    public void ShowWaitImage()
    {
        Purchasing.gameObject.SetActive(true);
    }

    public void ShowPurchasedTransform(long number)
    {
        panel.gameObject.SetActive(false);
        Purchasing.gameObject.SetActive(false);

        purchasedFailTransform.gameObject.SetActive(false);

        purchaseCoinsText.text = GameHelper.CoinLongToString(number);
        purchasedTransform.gameObject.SetActive(true);

        StartCoroutine(DelayReset());
    }

    public void ShowPurchasedFailTransform()
    {
        panel.gameObject.SetActive(false);
        Purchasing.gameObject.SetActive(false);

        purchasedFailTransform.gameObject.SetActive(true);

        purchasedTransform.gameObject.SetActive(false);

        StartCoroutine(DelayReset());
    }

    IEnumerator DelayReset()
    {
        yield return new WaitForSeconds(3.0f);

        this.gameObject.SetActive(false);

        panel.gameObject.SetActive(true);

        Purchasing.gameObject.SetActive(false);
        purchasedFailTransform.gameObject.SetActive(false);
        purchasedTransform.gameObject.SetActive(false);
    }

    public void BuyProduct(string id)
    {

        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        IAPManager.Instance.BuyProductID(id);
    }

    // Update is called once per frame
    private float z;
    void Update () {

        z += (Time.deltaTime * 250);

        if (z > 360.0f)
            z -= 360.0f;

        Purchasing.GetComponent<RectTransform>().localEulerAngles = new Vector3(0.0f, 0.0f, z);

    }
}
