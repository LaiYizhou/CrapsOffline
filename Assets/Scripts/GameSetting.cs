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


    public void Show()
    {
        this.gameObject.SetActive(true);
        isShow = true;

        UpdateButton();

    }

    public void OnSoundButtonClicked()
    {

        AudioControl.Instance.IsSoundOn = !AudioControl.Instance.IsSoundOn;
        UpdateButton();

        //if (AudioControl.Instance.IsSoundOn)
            //AudioControl.Instance.PlaySoundOnSetting(AudioControl.EAudioClip.ButtonClick_SFX);

    }

    public void OnMusicButtonClicked()
    {
        //AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick_SFX);

        AudioControl.Instance.IsMusicOn = !AudioControl.Instance.IsMusicOn;
        UpdateButton();
    }

    private void UpdateButton()
    {
        if (AudioControl.Instance != null)
        {
            musicButton.GetComponent<Image>().sprite = AudioControl.Instance.IsMusicOn ? musicOnSprite : musicOffSprite;
            soundButton.GetComponent<Image>().sprite = AudioControl.Instance.IsSoundOn ? soundOnSprite : soundOffSprite;
        }
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
