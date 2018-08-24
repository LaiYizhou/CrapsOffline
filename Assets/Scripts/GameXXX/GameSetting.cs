using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    [SerializeField]
    private bool isShow;

    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;
    [SerializeField] private Button musicButton;

    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;
    [SerializeField] private Button soundButton;

    [SerializeField] private GameObject payOutsGameObject;

    public void Show()
    {
        this.gameObject.SetActive(true);
        isShow = true;

        UpdateButton();

    }

    public void OnPayOutCloseButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        payOutsGameObject.gameObject.SetActive(false);

    }

    public void OnSoundButtonClicked()
    {

        AudioControl.Instance.IsSoundOn = !AudioControl.Instance.IsSoundOn;
        UpdateButton();

        if (AudioControl.Instance.IsSoundOn)
            AudioControl.Instance.PlaySoundOnSetting(AudioControl.EAudioClip.ButtonClick);

        //Hide();
    }

    public void OnMusicButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        AudioControl.Instance.IsMusicOn = !AudioControl.Instance.IsMusicOn;
        UpdateButton();

        //Hide();
    }

    private void UpdateButton()
    {
        if (AudioControl.Instance != null)
        {
            musicButton.GetComponent<Image>().sprite = AudioControl.Instance.IsMusicOn ? musicOnSprite : musicOffSprite;
            soundButton.GetComponent<Image>().sprite = AudioControl.Instance.IsSoundOn ? soundOnSprite : soundOffSprite;
        }
    }

    public void OnPayoutButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        Hide();
        payOutsGameObject.gameObject.SetActive(true);
    }

    public void OnRestorePurchaseButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        //Hide();
        IAPManager.Instance.RestorePurchases();
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
        isShow = false;
    }

    public void Switch()
    {
        if (isShow)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }


    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
