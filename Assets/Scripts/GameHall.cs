using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameHall : MonoBehaviour
{

    [SerializeField] private Text coinsText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdatePlayerCoin()
    {
        coinsText.text = GameHelper.CoinToString(GameHelper.player.Coins);
    }

    public void OnDailyGiftsButtonClicked()
    {
        CanvasControl.Instance.gameDailyGift.Show();
    }

    public void OnCrapPlayNowButtonClicked()
    {

    }

    public void LoadCrapScene(int levelId)
    {
        //todo
        // player coins Limit

        if (levelId > 0 && levelId <= 6)
        {
            if (GameHelper.player.Coins >= GameHelper.Instance.GetCrapSceneInfo(levelId).JoinMin)
            {
                CanvasControl.Instance.gameCrap.Init(levelId);
                this.gameObject.SetActive(false);
            }
            else
            {
                GameTestHelper.Instance.Tip("Not enough Coins ! ! !");
            }
   
        }
        
    }
}
