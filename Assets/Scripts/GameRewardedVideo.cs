using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameRewardedVideo : MonoBehaviour {


    [SerializeField] private Transform panel;
    [SerializeField] private Button closeButton;


    public void Show()
    {

        //if (!GameHelper.player.IsPaid && IronSourceControl.Instance.IsRewardedVideoReady)
        {
            panel.transform.localScale = Vector3.zero;

            this.gameObject.SetActive(true);

            Sequence sequence = DOTween.Sequence();
            sequence.Insert(0.0f, panel.DOScale(Vector3.one * 1.02f, 0.2f));
            sequence.Insert(0.0f, panel.DOLocalMove(new Vector3(0.0f, 0.0f), 0.2f));
            sequence.Insert(0.2f, panel.DOScale(Vector3.one, 0.2f));

        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCloseButtonClicked()
    {

        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        this.gameObject.SetActive(false);
    }

    public void OnClaimButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

       IronSourceControl.Instance.ShowRewardedVideoButtonClicked();
    }
}
