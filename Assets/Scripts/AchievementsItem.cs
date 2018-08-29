using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsItem : MonoBehaviour
{


    private AchievementInfo achievementInfo;

    [SerializeField] private Text descText;
    [SerializeField] private Image sliderBar;
    [SerializeField] private Text chipsText;
    [SerializeField] private Button collectButton;
    [SerializeField] private Sprite collectSprite;
    [SerializeField] private Sprite goSprite;
    [SerializeField] private Transform coverTransform;


    public void Init(AchievementInfo info)
    {
        this.achievementInfo = info;

        descText.text = info.Description;
        sliderBar.fillAmount = info.CurrentValue / (float) info.TargetValue;
        chipsText.text = GameHelper.CoinLongToString(info.RewardChips);

        if (info.IsCollected)
        {
            collectButton.gameObject.SetActive(false);
            coverTransform.gameObject.SetActive(true);
        }
        else
        {
            coverTransform.gameObject.SetActive(false);
            collectButton.gameObject.SetActive(true);

            collectButton.GetComponent<Image>().sprite = !info.IsComplete ? goSprite : collectSprite;

        }


    }

    public void OnButtonClicked()
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        if (this.achievementInfo == null || ! this.achievementInfo.IsComplete)
        {
            CanvasControl.Instance.gameAchievement.gameObject.SetActive(false);
        }
        else
        {
            if (CanvasControl.Instance.gameAchievement.GameAchievementInfoList.Contains(this.achievementInfo))
            {

                Vector3 sourcePos =
                    GameHelper.Instance.ToCanvasLocalPos(this.transform.TransformPoint(collectButton.transform.localPosition));

                // GameHelper.player.ChangeCoins() is called in RunEffect();
                GameHelper.Instance.coinCollectEffect.RunEffect(this.achievementInfo.RewardChips, sourcePos);


                CanvasControl.Instance.gameAchievement.GameAchievementInfoList[this.achievementInfo.Id].Collect();
                this.achievementInfo.Collect();
                Init(this.achievementInfo);
            }
        }
        
    }
}
