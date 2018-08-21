using UnityEngine;

public class Player
{

    private long coins;
    public long Coins
    {
        get
        {

            if (PlayerPrefs.HasKey("Coins"))
            {
                string s = PlayerPrefs.GetString("Coins");
                return long.Parse(s);
            }
            else
            {
                return -1;
            }

        }
        private set
        {
            if (value >= 0)
            {
                coins = value;
                Save();

                if (GameHelper.Instance != null)
                {
                    GameHelper.Instance.UpdatePlayerCoin();
                }

            }
            
        }
    }

    private bool isPaid;
    public bool IsPaid
    {
        get
        {
            if (PlayerPrefs.HasKey("IsPaid"))
            {
                return PlayerPrefs.GetInt("IsPaid") == 1;
            }
            else
            {
                return false;
            }
        }

        private set
        {
            isPaid = value;
            PlayerPrefs.SetInt("IsPaid", isPaid ? 1 : 0);
            if (CanvasControl.Instance != null)
            {
                CanvasControl.Instance.UpdateRewardedButton();
                CanvasControl.Instance.UpdateInterstitial();
                CanvasControl.Instance.gameStore.UpdateAdItem(!isPaid);
            }
            
        }
    }

    

    public Player()
    {
        if (Coins == -1)
            ChangeCoins(1 + GameHelper.StartCoins);

        IsPaid = IsPaid;
    }

    public void ResetData()
    {
        this.Coins = GameHelper.StartCoins;
        IsPaid = false;

        PlayerPrefs.DeleteKey("Coins");
        PlayerPrefs.DeleteKey("IsPaid");

    }

    public void SetIsPaid(bool flag)
    {
        this.IsPaid = flag;
    }

    public void ChangeCoins(long addNumber)
    {
        this.Coins += addNumber;
    }

    private void Save()
    {
        string s = coins.ToString();
        PlayerPrefs.SetString("Coins", s);
    }

   
}
