using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStore : MonoBehaviour
{

    [SerializeField] private Button closeButton;


    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    private void OnCloseButtonClicked()
    {
        this.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start ()
    {
        closeButton.onClick.AddListener(OnCloseButtonClicked);

    }

    public void BuyProduct(string id)
    {
        IAPManager.Instance.BuyProductID(id);
    }

    // Update is called once per frame
	void Update () {
	
	}
}
